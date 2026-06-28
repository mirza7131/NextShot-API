using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.HfDepartmentDto;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.SectionLookupDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonDTOs.LocationDTO;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedisCache;
using System.Data;
using System.Linq.Expressions;

namespace AuthBAL
{
    //[Authorize]
    public class HfDepartmentService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HfDepartment> _uowHfDepartment;
        private readonly HfDepartmentSectionService<HfDepartmentSection> _HfDepartmentSectionService;
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;
        #endregion

        #region Constructor

        public HfDepartmentService(TokenService tokenService, UnitOfWork<HfDepartment> uowHfDepartment, IMapper mapper, 
            HfDepartmentSectionService<HfDepartmentSection> HfDepartmentSectionService,
            IRedisCacheService cacheService, IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHfDepartment = uowHfDepartment;
            _HfDepartmentSectionService = HfDepartmentSectionService;
            _cacheService = cacheService;
            _isRedisCacheEnable = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                                 config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActiveRedisCache") :
                                 (
                                 config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                  config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActiveRedisCache") :
                                  false
                                 );
            _isStaticDDEnable = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                                    config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActiveStaticDD") :
                                    (
                                    config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                     config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActiveStaticDD") :
                                     false
                                    );
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHfDepartmentDto> CreateOrEdit(CreateOrEditHfDepartmentDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HfDepartmentId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHfDepartmentDto> Create(CreateOrEditHfDepartmentDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
            {
                if (AppCommonMethod.IsNullorZeroInt(TokenService.GetUserHfId()))
                    throw new UserFriendlyException(CommonMessageConstant.HealthFacilityIdNotAvailable);
                else
                    input.HealthFacilityId = TokenService.GetUserHfId();
            }

            int listOld = await _uowHfDepartment.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.DepartmentLookupId == input.DepartmentLookupId && x.HealthFacilityId == input.HealthFacilityId).CountAsync();

            if (listOld > 0)
                throw new UserFriendlyException(CommonMessageConstant.HFDepartmentAlreadyExists);

            var obj = _mapper.Map<HfDepartment>(input);

            FillEntity(obj);

            foreach (var itemSection in input.SectionIds!)
            {
                obj.HfDepartmentSections.Add(new HfDepartmentSection
                {
                    HfDepartmentId = obj.HfDepartmentId,
                    //DepartmentLookupId = input.DepartmentLookupId,
                    SectionLookupId = itemSection,
                    IsActive = true,
                    CreatedBy = _tokenService.GetUserId(),
                    CreatedOn = DateTime.Now,
                    ActionTypeId = (int)ActionTypeEnum.Create,
                });
            }
            
            HfDepartment responseObj = await _uowHfDepartment.Repository.Insert(obj);

            await _uowHfDepartment.CommitAsync();

            //RefreshHfDepartmentCacheList();
            //_HfDepartmentSectionService.RefreshHfDepartmentSectionCacheList();

            if(_isStaticDDEnable) {
                await RefreshHfDepartmentStaticCacheList();
                await _HfDepartmentSectionService.RefreshHfDepartmentSectionStaticCacheList();
            }

            return _mapper.Map<CreateOrEditHfDepartmentDto>(responseObj);
        }

        private async Task<CreateOrEditHfDepartmentDto> Update(CreateOrEditHfDepartmentDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
            {
                if (AppCommonMethod.IsNullorZeroInt(TokenService.GetUserHfId()))
                    throw new UserFriendlyException(CommonMessageConstant.HealthFacilityIdNotAvailable);
                else
                    input.HealthFacilityId = TokenService.GetUserHfId();
            }

            var listOld = await _uowHfDepartment.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.DepartmentLookupId == input.DepartmentLookupId && x.HealthFacilityId == input.HealthFacilityId).FirstOrDefaultAsync();

            var dbObj = await _uowHfDepartment.Repository.GetById(input.HfDepartmentId!);
            
            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            if (!AppCommonMethod.IsNullObject(listOld) && listOld!.HfDepartmentId != dbObj!.HfDepartmentId)
                throw new UserFriendlyException(CommonMessageConstant.HFDepartmentAlreadyExists);

            await _HfDepartmentSectionService.DeleteByHfDepartmentId(input.HfDepartmentId??0); // delete all pre department section against hf department

            var obj = _mapper.Map(input, dbObj);

            FillEntity(obj!);

            foreach (var itemSection in input.SectionIds!)
            {
                obj.HfDepartmentSections.Add(new HfDepartmentSection
                {
                    HfDepartmentId = obj.HfDepartmentId,
                    //DepartmentLookupId = input.DepartmentLookupId,
                    SectionLookupId = itemSection,
                    IsActive = true,
                    CreatedBy = _tokenService.GetUserId(),
                    CreatedOn = DateTime.Now,
                    ActionTypeId = (int)ActionTypeEnum.Create,
                });
            }

            _uowHfDepartment.Repository.Update(obj!);
            await _uowHfDepartment.CommitAsync();
            //_uowHfDepartment.GetDbContext().Database.CloseConnection();
            //RefreshHfDepartmentCacheList();
            //_HfDepartmentSectionService.RefreshHfDepartmentSectionCacheList();

            if (_isStaticDDEnable)
            {
                await RefreshHfDepartmentStaticCacheList();
                await _HfDepartmentSectionService.RefreshHfDepartmentSectionStaticCacheList();
            }

            return _mapper.Map<CreateOrEditHfDepartmentDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowHfDepartment.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);
            _uowHfDepartment.Repository.Update(dbObj!);
            await _uowHfDepartment.CommitAsync();

            //RefreshHfDepartmentCacheList();

            if (_isStaticDDEnable)
            {
                await RefreshHfDepartmentStaticCacheList();
                await _HfDepartmentSectionService.RefreshHfDepartmentSectionStaticCacheList();
            }

            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewHfDepartmentDto>> GetAll(Expression<Func<HfDepartment, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<HfDepartment> responseObj = await _uowHfDepartment.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewHfDepartmentDto>>(responseObj);
        }


        public async Task<ViewHfDepartmentDto> GetById(int input)
        {
            HfDepartment? responseObj = await _uowHfDepartment.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHfDepartmentDto>(responseObj);
        }

        public async Task<List<ViewDepartmentLookupDto>> GetHfDepartmentsByHealthFacility(int? HealthFacilityId)
        {

            if (AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                HealthFacilityId = TokenService.GetUserHfId();

            var responseObj = await _uowHfDepartment.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.DepartmentLookup)
                .WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == HealthFacilityId)
                .Select(y => new ViewDepartmentLookupDto { 
                HfDepartmentId = y.HfDepartmentId,
                DepartmentLookupId = y.DepartmentLookup!.DepartmentLookupId,
                Name = y.DepartmentLookup.Name,
                DisplayName = y.DepartmentLookup.DisplayName,
                Description = y.DepartmentLookup.Description,
                IsActive = y.DepartmentLookup.IsActive
            }).ToListAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<List<ViewDepartmentLookupDto>>(responseObj);
        }

        

        public async Task<ViewPagerDto<ViewHfDepartmentDto>> GetAllWithDepartment(PagerDto filter)
        {

            var _uowUser = new UnitOfWork<User>(_uowHfDepartment.GetDbContext());
            var userList = _uowUser.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToList();

            var list = _uowHfDepartment.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.HealthFacility!.Name!.ToLower().StartsWith(filter.SearchString) || x!.DepartmentLookup!.Name!.ToLower().StartsWith(filter.SearchString))

                ////.WhereIf(!TokenService.IsSuperAdmin(),x =>x.HealthFacilityId == TokenService.GetUserHfId())
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacility!.Tehsil!.District!.Division!.Province!.ProvinceId == filter.ProvinceId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacility!.Tehsil!.District!.Division!.DivisionId == filter.DivisionId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacility!.Tehsil!.District!.DistrictId == filter.DistrictId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacility!.Tehsil!.TehsilId == filter.TehsilId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacility!.HealthFacilityId == filter.HealthFacilityId)
                //.WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                //.WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                //.OrderByDescending(x => x.CreatedOn)
                //.Include(x => x.HealthFacility)
                //    .ThenInclude(x => x!.Tehsil)
                //        .ThenInclude(x => x!.District)
                //            .ThenInclude(x => x!.Division)
                //                .ThenInclude(x => x!.Province)
                //.Include(x => x.DepartmentLookup);

            //.WhereIf(!TokenService.IsSuperAdmin(),x =>x.HealthFacilityId == TokenService.GetUserHfId())
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacility!.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacility!.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacility!.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacility!.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacility!.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn)
                //.Include(x => x.HealthFacility)
                //.Include(x => x.DepartmentLookup)
                ;

            IQueryable<ViewHfDepartmentDto> IQueryableList = list.Select(y => new ViewHfDepartmentDto
            {
                HfDepartmentId = y.HfDepartmentId,
                DepartmentLookupId = y.DepartmentLookupId,
                HealthFacilityId = y.HealthFacilityId,
                HealthFacilityName = y.HealthFacility!.Name,
                DepartmentName = y.DepartmentLookup!.Name,
                CreatedBy = y.CreatedBy,
                CreatedOn = y.CreatedOn,
                UpdatedBy = y.UpdatedBy,
                UpdatedOn = y.UpdatedOn,
                SectionIds = y.HfDepartmentSections.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.SectionLookupId).ToList(),
                HfDepartmentSections = y.HfDepartmentSections.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new ViewHfDepartmentSectionDto
                {
                    HfDepartmentSectionId = x.HfDepartmentSectionId,
                    HfDepartmentId = x.HfDepartmentId,
                    //DepartmentLookupId = x.DepartmentLookupId,
                    SectionLookupId = x.SectionLookupId,
                    SectionName = x.SectionLookup!.Name,
                    VitalsFloorNo = x.VitalsFloorNo,
                    VitalsRoomNo = x.VitalsRoomNo,
                    DoctorFloorNo = x.DoctorFloorNo,
                    DoctorRoomNo = x.DoctorRoomNo,
                    PharmacyFloorNo = x.PharmacyFloorNo,
                    PharmacyRoomNo = x.PharmacyRoomNo,
                    PathalogyFloorNo = x.PathalogyFloorNo,
                    PathalogyRoomNo = x.PathalogyRoomNo,
                    AlmonerFloorNo = x.AlmonerFloorNo,
                    AlmonerRoomNo = x.AlmonerRoomNo,
                    IsActive = x.IsActive
                }).Where(x => x.HfDepartmentId == y.HfDepartmentId).ToList(),
                SectionNames = string.Join(",", y.HfDepartmentSections.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.SectionLookup!.Name)),
                IsActive = y.IsActive,
            });
            //IQueryable<ViewHfDepartmentDto> IQueryableList = list.Select(y => new ViewHfDepartmentDto
            //{
            //    HfDepartmentId = y.HfDepartmentId,
            //    DepartmentLookupId = y.DepartmentLookupId,
            //    HealthFacilityId = y.HealthFacilityId,
            //    HealthFacilityName = y.HealthFacility!.Name,
            //    DepartmentName = y.DepartmentLookup!.Name,
            //    CreatedBy = y.CreatedBy,
            //    CreatedOn = y.CreatedOn,
            //    UpdatedBy = y.UpdatedBy,
            //    UpdatedOn = y.UpdatedOn,
            //    SectionIds = y.HfDepartmentSections.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.SectionLookupId).ToList(),
            //    SectionNames = string.Join(",", y.HfDepartmentSections.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.SectionLookup!.Name)),
            //    IsActive = y.IsActive,
            //});

            //return _mapper.Map<List<ViewHfDepartmentDto>>(department);


            var pagedList = await PagedListDto<ViewHfDepartmentDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                item.CreatedByName = userList.Where(x => x.UserId == item.CreatedBy).Select(x => x.FullName).FirstOrDefault();
                item.UpdatedByName = userList.Where(x => x.UserId == item.UpdatedBy).Select(x => x.FullName).FirstOrDefault();
            }

            var responseObject = new ViewPagerDto<ViewHfDepartmentDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList,
            };

            return responseObject;
        }

        public async Task RefreshHfDepartmentCacheList()
        {
            //var listhfDepartments = await _uowHfDepartment.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.DepartmentLookup).ToListAsync();
            //CacheData.HfDepartments = _mapper.Map<List<CacheHfDepartmentDto>>(listhfDepartments);

            var listhfDepartments = await _uowHfDepartment.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.DepartmentLookup).ToListAsync();

            var hfDepartments = _mapper.Map<List<CacheHfDepartmentDto>>(listhfDepartments);

            _cacheService.Set<List<CacheHfDepartmentDto>?>(CacheKeyConstant.HfDepartment, hfDepartments, null, null);

        }

        public async Task<List<HfDepartmentDropdownDto>> GetAllHfDepartmentFromSP()
        {
            var tokenUser = TokenService.GetUserLoggedInfo();
            var hfDepartment = new List<HfDepartmentDropdownDto>();

            if (_isStaticDDEnable)
                hfDepartment = LocationStaticDto.GetHfDepartmentDropdowns();

            if (!AppCommonMethod.IsNullOrEmptyList<HfDepartmentDropdownDto>(hfDepartment) && _isStaticDDEnable)
                return hfDepartment;
            else
            {

                var conn = _uowHfDepartment.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPGetAllHfDepartmentsByHealthFacilityId]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                    //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser!.HealthFacilityId);

                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                    //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser!.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HfDepartmentDropdownDto> lst = ds.Tables[0].ToList<HfDepartmentDropdownDto>();

                    if (!AppCommonMethod.IsNullOrEmptyList<HfDepartmentDropdownDto>(lst) && _isStaticDDEnable)
                        LocationStaticDto.SetHfDepartmentDropdowns(lst);
                    return lst;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task RefreshHfDepartmentStaticCacheList()
        {
            var tokenUser = TokenService.GetUserLoggedInfo();
            var conn = _uowHfDepartment.GetDbContext().Database.GetDbConnection();
            try
            {

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[SPGetAllHfDepartmentsByHealthFacilityId]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser!.HealthFacilityId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                List<HfDepartmentDropdownDto> lst = ds.Tables[0].ToList<HfDepartmentDropdownDto>();
                if (_isStaticDDEnable)
                    LocationStaticDto.SetHfDepartmentDropdowns(lst);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        #endregion

        #region Helper Methods

        public async Task<List<HfDepartmentDropdownDto>> GetHfDepartmentDropdown()
        {
            if (_isRedisCacheEnable)
            {
                var hfDepartments = _cacheService.Get<List<CacheHfDepartmentDto>?>(CacheKeyConstant.HfDepartment);

                if (hfDepartments == null)
                {
                    var dbHfDepartments = await _uowHfDepartment.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.DepartmentLookup).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheHfDepartmentDto>>(dbHfDepartments);
                    hfDepartments = _cacheService.Set<List<CacheHfDepartmentDto>?>(CacheKeyConstant.HfDepartment, cacheData, null, null);
                }

                return hfDepartments!.Select(x => new HfDepartmentDropdownDto
                {
                    Id = x.HfDepartmentId,
                    Name = x.DepartmentLookup!.Name,
                    LookupId = x.DepartmentLookupId,
                    ParentId = x.HealthFacilityId
                }).ToList();
            }
            else
            {

                var tokenUser = TokenService.GetUserLoggedInfo();
                var hfDepartment = new List<HfDepartmentDropdownDto>();

                // if not health facility user then no need of department & section
                if (AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                    return hfDepartment;
                else
                {
                    // if health facility user then get department & section
                    hfDepartment = await GetAllHfDepartmentFromSP();

                    if (!AppCommonMethod.IsNullorZeroInt(tokenUser.HealthFacilityId))
                        hfDepartment = hfDepartment.Where(x => x.ParentId == tokenUser.HealthFacilityId).ToList();

                    return hfDepartment;
                }

                //var tokenUser = TokenService.GetUserLoggedInfo();
                //var dbHfDepartments = await _uowHfDepartment.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                //    .Include(x => x.HealthFacility)
                //    .Include(x => x.DepartmentLookup)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.DivisionId), x => x.HealthFacility!.DivisionId == tokenUser.DivisionId)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.DistrictId), x => x.HealthFacility!.DistrictId == tokenUser.DistrictId)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.TehsilId), x => x.HealthFacility!.TehsilId == tokenUser.TehsilId)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId), x => x.HealthFacilityId == tokenUser.HealthFacilityId)
                //    .ToListAsync();

                //return dbHfDepartments!.Select(x => new HfDepartmentDropdownDto
                //{
                //    Id = x.HfDepartmentId,
                //    Name = x.DepartmentLookup!.Name,
                //    LookupId = x.DepartmentLookupId,
                //    ParentId = x.HealthFacilityId
                //}).ToList();

            }


            
        }

        public async Task<List<HfDepartmentDropdownDto>> GetHfDepartmentDropdownByHealthFacility(int HealthFacilityId)
        {

            var tokenUser = TokenService.GetUserLoggedInfo();
            var hfDepartment = new List<HfDepartmentDropdownDto>();
          
            // if health facility user then get department & section
            hfDepartment = await GetAllHfDepartmentFromSP();
            

            if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                hfDepartment = hfDepartment.Where(x => x.ParentId == HealthFacilityId).ToList();

            return hfDepartment;
        }


        public async Task<LocationDto> GetDepartmentAndSectionByHealthFacility(int HealthFacilityId)
        {

            LocationDto response = new LocationDto();

            // if health facility user then get department & section
            var hfDepartment = await GetHfDepartmentDropdownByHealthFacility(HealthFacilityId);
            var hfDepartmentSection = await _HfDepartmentSectionService.GetHfDepartmentSectionDropdownByHealthFacility(HealthFacilityId);


            response.HfDepartmentDropdown = hfDepartment;
            response.HfDepartmentSectionDropdown = hfDepartmentSection;

            return response;

        }
        private void FillEntity(HfDepartment obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.HfDepartmentId))
            {
                //obj.SectionId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(HfDepartment obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }
}
