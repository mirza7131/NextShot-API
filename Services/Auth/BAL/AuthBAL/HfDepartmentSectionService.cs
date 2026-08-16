using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.HfDepartmentDto;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.SectionLookupDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonDTOs.LocationDTO;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Aggregator.API.Models.Dto.Hr;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedisCache;
using System.Data;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class HfDepartmentSectionService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HfDepartmentSection> _uowHfDepartmentSection;
        private UnitOfWork<DepartmentLookup> _uowDepartmentLookUp;
        private readonly IRedisCacheService _cacheService;
        private readonly HRService _hrService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;
        private readonly bool _isOnline;
        #endregion

        #region Constructor

        public HfDepartmentSectionService(TokenService tokenService, UnitOfWork<HfDepartmentSection> uowHfDepartmentSection, 
            IMapper mapper , 
            UnitOfWork<DepartmentLookup> _uowDepartmentLookUp,
            IRedisCacheService cacheService,
             IConfiguration config,
             HRService hrService
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHfDepartmentSection = uowHfDepartmentSection;
            _cacheService = cacheService;
            _hrService = hrService;
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

            _isOnline = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                                    config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") :false;
            
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHfDepartmentSectionDto> CreateOrEdit(CreateOrEditHfDepartmentSectionDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HfDepartmentSectionId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHfDepartmentSectionDto> Create(CreateOrEditHfDepartmentSectionDto input)
        {
            var obj = _mapper.Map<HfDepartmentSection>(input);
            FillEntity(obj);
            HfDepartmentSection responseObj = await _uowHfDepartmentSection.Repository.Insert(obj);
            await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();

            if(_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();

            return _mapper.Map<CreateOrEditHfDepartmentSectionDto>(responseObj);
        }

        
        private async Task<CreateOrEditHfDepartmentSectionDto> Update(CreateOrEditHfDepartmentSectionDto input)
        {
            var dbObj = await _uowHfDepartmentSection.Repository.GetById(input.HfDepartmentSectionId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowHfDepartmentSection.Repository.Update(obj!);
            await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();
            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();

            return _mapper.Map<CreateOrEditHfDepartmentSectionDto>(obj);
        }

        public async Task<List<CreateOrEditHfDepartmentSectionDto>> BulkCreateOrEdit(List<CreateOrEditHfDepartmentSectionDto> inputs)
        {
            foreach (var input in inputs)
            {
                if (AppCommonMethod.IsNullorZeroInt(input.HfDepartmentSectionId))
                    await BulkCreate(input);
                else
                    await BulkUpdate(input);

            }
            //await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();

            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();
            return inputs;

        }
        private async Task BulkCreate(CreateOrEditHfDepartmentSectionDto input)
        {
            var obj = _mapper.Map<HfDepartmentSection>(input);
            FillEntity(obj);

            //foreach (var obj in objs) { 
                await _uowHfDepartmentSection.Repository.Insert(obj);
            //}
            await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();
            //return _mapper.Map<CreateOrEditHfDepartmentSectionDto>(responseObj);
            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();

        }

        private async Task BulkUpdate(CreateOrEditHfDepartmentSectionDto input)
        {
            var dbObj = await _uowHfDepartmentSection.Repository.GetById(input.HfDepartmentSectionId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowHfDepartmentSection.Repository.Update(obj!);
            await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();
            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();

            //return _mapper.Map<List<CreateOrEditHfDepartmentSectionDto>>(obj);
        }
        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowHfDepartmentSection.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowHfDepartmentSection.Repository.Update(dbObj!);
            await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();

            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();

            return true;
        }

        public async Task<bool> DeleteByHfDepartmentId(int HfDepartmentId)
        {
            var list =  await _uowHfDepartmentSection.Repository.GetALL(x => x.HfDepartmentId == HfDepartmentId).ToListAsync();

            if (list.Count() == 0)
                return true;
                //throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            foreach (var item in list)
            {
                FillEntityDelete(item);
                _uowHfDepartmentSection.Repository.Delete(item);
            }
           
            await _uowHfDepartmentSection.CommitAsync();
            //RefreshHfDepartmentSectionCacheList();

            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();
            return true;
        }


        public async Task<bool> DumpSectionAgainstHfDepartment(DumpHfDepartmentSection input)
        {
            var hrResponse = new List<HealthFacilityWardBedsDto>();

            if (!AppCommonMethod.IsNullorZeroInt(input.HfHrId))
                hrResponse = await _hrService.GetHealthFacilityWardBeds(input.HfHrId, true);
            else
                hrResponse = await _hrService.GetHealthFacilityWardBeds(0, true);

            var hrHealthFacilityIds = hrResponse.Select(x => x.healthFacility_Id).Distinct().ToList();

            var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowHfDepartmentSection.GetDbContext());
            var _uowHfDepartment = new UnitOfWork<HfDepartment>(_uowHfDepartmentSection.GetDbContext());
            var _uowHealthFacility = new UnitOfWork<HealthFacility>(_uowHfDepartmentSection.GetDbContext());

            var healthFacilities = await _uowHealthFacility.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNull(hrHealthFacilityIds), x => hrHealthFacilityIds.Contains((int)x.HrId))
                .ToListAsync();

            var healthFacilityIds = healthFacilities.Select(x => x.HealthFacilityId).Distinct().ToList();


            var hfDepartments = new List<HfDepartment>();
            if (!AppCommonMethod.IsNullOrEmptyList<HealthFacility>(healthFacilities))
            {
                hfDepartments = await _uowHfDepartment.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNull(healthFacilityIds), x => healthFacilityIds.Contains((int)x.HealthFacilityId))
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(input.HfHrId), x => x.HealthFacility.HrId == input.HfHrId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(input.DepartmentLookupId), x => x.DepartmentLookupId == input.DepartmentLookupId)
                .ToListAsync();

                
            }
    
            // make check if department not exist then add department

            var hfDepartmentIds = hfDepartments.Select(x => x.HfDepartmentId).Distinct().ToList();

            var lookupSections = await _uowSectionLookup.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(input.DepartmentLookupId), x => x.DepartmentLookupId == input.DepartmentLookupId)
                .ToListAsync();

            var hfDepartmentSections = new List<HfDepartmentSection>();
            if (!AppCommonMethod.IsNullOrEmptyList<HfDepartment>(hfDepartments))
            {
                hfDepartmentSections = await _uowHfDepartmentSection.Repository.GetALL()
                    .WhereIf(!AppCommonMethod.IsNull(hfDepartmentIds), x => hfDepartmentIds.Contains((int)x.HfDepartmentId))
                    .ToListAsync();
            }

            foreach (var healthFacility in healthFacilities)
            {
                // get IPD Department againt this Facility
                var dbHfDepartments = hfDepartments.Where(x => x.HealthFacilityId == healthFacility.HealthFacilityId && x.DepartmentLookupId == input.DepartmentLookupId).ToList();

                if (AppCommonMethod.IsNullOrEmptyList<HfDepartment>(dbHfDepartments))
                {
                    var createHfDepartment = new CreateOrEditHfDepartmentDto();
                    createHfDepartment.HealthFacilityId = healthFacility.HealthFacilityId;
                    createHfDepartment.DepartmentLookupId = input.DepartmentLookupId;

                    var responseHfDepartment = await CreateHfDepartment(createHfDepartment);
                    var dbHfDepartment = _mapper.Map<HfDepartment>(responseHfDepartment);
                    dbHfDepartments.Add(dbHfDepartment);
                }

                foreach (var hfDepartment in dbHfDepartments)
                {
                    var dbDepartmentSections = hfDepartmentSections.Where(x => x.HfDepartmentId == hfDepartment.HfDepartmentId).ToList();

                    // Soft Delete
                    foreach (var dbDepartmentSection in dbDepartmentSections)
                    {
                        if (!lookupSections.Any(c => c.SectionLookupId == dbDepartmentSection.SectionLookupId))
                        {
                            FillEntityDelete(dbDepartmentSection);
                            _uowHfDepartmentSection.Repository.Update(dbDepartmentSection);
                        }
                    }

                    // Update and Insert
                    foreach (var section in lookupSections)
                    {
                        var dbSection = dbDepartmentSections
                            .Where(c => c.SectionLookupId == section.SectionLookupId && c.HfDepartmentId == hfDepartment.HfDepartmentId)
                            .FirstOrDefault();

                        var hrSection = hrResponse
                            .Where(c => c.ward_Id == section.HrWardId && c.healthFacility_Id == healthFacility.HrId)
                            .FirstOrDefault();

                        if (!AppCommonMethod.IsNull(hrSection))
                        {
                            if (dbSection != null)
                            {
                                // Update
                                //var objSection = _mapper.Map(section, dbSection);
                                var objSection = dbSection;
                                FillEntity(objSection);
                                objSection.BedQuantityInWard = (int)hrSection.sanctioned;

                                objSection.SectionLookupId = section.SectionLookupId;
                                objSection.IsActive = section.IsActive;

                                _uowHfDepartmentSection.Repository.Update(objSection);
                            }
                            else
                            {
                                // Insert 
                                var objSection = new HfDepartmentSection();
                                //var objSection = _mapper.Map<SectionLookup>(section);

                                FillEntity(objSection);
                                objSection.BedQuantityInWard = (int)hrSection.sanctioned;
                                objSection.HfDepartmentId = hfDepartment.HfDepartmentId;
                                objSection.SectionLookupId = section.SectionLookupId;

                                objSection.IsActive = section.IsActive;

                                //dbSections.Add(objSection);
                                await _uowHfDepartmentSection.Repository.Insert(objSection);
                            }
                        }
                    }
                }
            }

            await _uowHfDepartmentSection.CommitAsync();
            //await RefreshHfDepartmentSectionCacheList();

            if (_isStaticDDEnable)
                await RefreshHfDepartmentSectionStaticCacheList();

            return true;
        }

        private async Task<CreateOrEditHfDepartmentDto> CreateHfDepartment(CreateOrEditHfDepartmentDto input)
        {
            var _uowHfDepartment = new UnitOfWork<HfDepartment>(_uowHfDepartmentSection.GetDbContext());
            var obj = _mapper.Map<HfDepartment>(input);
            obj.IsActive = true;
            obj.CreatedBy = _tokenService.GetUserId();
            obj.CreatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Create;
            await _uowHfDepartment.Repository.Insert(obj);
            await _uowHfDepartment.CommitAsync();

            var responseObj = await _uowHfDepartment.Repository
                .GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.DepartmentLookupId == input.DepartmentLookupId)
                .FirstOrDefaultAsync();

            return _mapper.Map<CreateOrEditHfDepartmentDto>(responseObj);
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewHfDepartmentSectionDto>> GetAll(Expression<Func<HfDepartmentSection, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<HfDepartmentSection> responseObj = await _uowHfDepartmentSection.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewHfDepartmentSectionDto>>(responseObj);
        }


        public async Task<ViewHfDepartmentSectionDto> GetById(int input)
        {
            HfDepartmentSection? responseObj = await _uowHfDepartmentSection.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHfDepartmentSectionDto>(responseObj);
        }

        public async Task<List<ViewHfDepartmentSectionDto>> GetHfDepartmentSectionsByHfDepartmentId(int HfDepartmentId)
        {

            //_uowHfDepartmentSection

            var responseObj = await _uowHfDepartmentSection.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.HfDepartmentId == HfDepartmentId).Include(x => x.SectionLookup)
                .Select(y => new ViewHfDepartmentSectionDto
                {
                    HfDepartmentSectionId = y.HfDepartmentSectionId,
                    HfDepartmentId = y.HfDepartmentId,
                    DepartmentLookupId = y.SectionLookup!.DepartmentLookupId,
                    //DepartmentName = y.DepartmentLookup!.Name,
                    SectionLookupId = y.SectionLookupId,
                    SectionName = y.SectionLookup.Name,
                    IsActive = y.IsActive
                }).ToListAsync();

            return _mapper.Map<List<ViewHfDepartmentSectionDto>>(responseObj);
        }

        public async Task<List<ViewHfDepartmentSectionDto>> GetAllWithDepartmentSection()
        {
            var responseObj = await _uowHfDepartmentSection.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.SectionLookup)
                .Select(y => new ViewHfDepartmentSectionDto
                {
                    HfDepartmentSectionId = y.HfDepartmentSectionId,
                    //DepartmentLookupId = y.DepartmentLookupId,
                    //DepartmentName = y.DepartmentLookup!.Name,
                    SectionLookupId = y.SectionLookupId,
                    SectionName = y.SectionLookup!.Name,
                    IsActive = y.IsActive

                }).ToListAsync();

            return _mapper.Map<List<ViewHfDepartmentSectionDto>>(responseObj);
        }

        public async Task RefreshHfDepartmentSectionCacheList()
        {
            //var listHfDepartmentSections = await _uowHfDepartmentSection.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.SectionLookup).ToListAsync();
            //CacheData.HfDepartmentSections = _mapper.Map<List<CacheHfDepartmentSectionDto>>(listHfDepartmentSections);

            var listHfDepartmentSections = await _uowHfDepartmentSection.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.SectionLookup).ToListAsync();

            var hfDepartmentSections = _mapper.Map<List<CacheHfDepartmentSectionDto>>(listHfDepartmentSections);

            _cacheService.Set<List<CacheHfDepartmentSectionDto>?>(CacheKeyConstant.HfDepartmentSection, hfDepartmentSections, null, null);

        }

        public async Task RefreshHfDepartmentSectionStaticCacheList()
        {
            var tokenUser = TokenService.GetUserLoggedInfo();

            var conn = _uowHfDepartmentSection.GetDbContext().Database.GetDbConnection();
            try
            {

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[SPGetAllHfDepartmentSectionByHealthFacilityId]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser!.HealthFacilityId);

                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DepartmentId))
                //    sqlComm.Parameters.AddWithValue("@DepartmentLookupId", tokenUser!.DepartmentId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                List<DropdownSectionDto> lst = ds.Tables[0].ToList<DropdownSectionDto>();
                
                if (_isStaticDDEnable)
                    LocationStaticDto.SetHfDepartmentSectionDropdowns(lst);

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

        public async Task<List<GetBedsSectionWiseDto>> GetBedsBySectionId(int? HealthFacilityId, int? DepartmentLookupId, int? SectionLookupId)
        {
            using (var db = new NextShotContext())
            {
                var conn = _uowHfDepartmentSection.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[GetAllBedSectionWise]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);
                    
                    sqlComm.Parameters.AddWithValue("@DepartmentLookupId", DepartmentLookupId);

                    sqlComm.Parameters.AddWithValue("@SectionLookupId", SectionLookupId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetBedsSectionWiseDto> lst = ds.Tables[0].ToList<GetBedsSectionWiseDto>();

                    foreach (var item in lst)
                    {
                        item.Name = "Bed No: " + item.Id.ToString();
                        item.Name += (item.Occupied > 0) ? " - Already Occupied By (" + item.Occupied + ") Patients" : "";
                    }

                    return lst;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<List<DropdownSectionDto>> GetAllHfDepartmentSectionFromSP()
        {
            var tokenUser = TokenService.GetUserLoggedInfo();
            var hfDepartmentSection = new List<DropdownSectionDto>();

            if (_isStaticDDEnable)
                hfDepartmentSection = LocationStaticDto.GetHfDepartmentSectionDropdowns();

            if (!AppCommonMethod.IsNullOrEmptyList<DropdownSectionDto>(hfDepartmentSection) && _isStaticDDEnable)
                return hfDepartmentSection;
            else
            {

                var conn = _uowHfDepartmentSection.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPGetAllHfDepartmentSectionByHealthFacilityId]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                    //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser!.HealthFacilityId);

                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DepartmentId))
                    //    sqlComm.Parameters.AddWithValue("@DepartmentLookupId", tokenUser!.DepartmentId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DropdownSectionDto> lst = ds.Tables[0].ToList<DropdownSectionDto>();

                    if (!AppCommonMethod.IsNullOrEmptyList<DropdownSectionDto>(lst) && _isStaticDDEnable)
                        LocationStaticDto.SetHfDepartmentSectionDropdowns(lst);

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

        #endregion

        #region Helper Methods

        public async Task<List<DropdownSectionDto>> GetHfDepartmentSectionDropdown()
        {
            if (_isRedisCacheEnable)
            {
                var hfDepartmentSections = _cacheService.Get<List<CacheHfDepartmentSectionDto>?>(CacheKeyConstant.HfDepartmentSection);

                if (hfDepartmentSections == null)
                {
                    var dbHfDepartmentSections = await _uowHfDepartmentSection.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.SectionLookup).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheHfDepartmentSectionDto>>(dbHfDepartmentSections);

                    hfDepartmentSections = _cacheService.Set<List<CacheHfDepartmentSectionDto>?>(CacheKeyConstant.HfDepartmentSection, cacheData, null, null);
                }

                return hfDepartmentSections!.Select(x => new DropdownSectionDto
                {
                    Id = x.HfDepartmentSectionId,
                    Name = x.SectionLookup!.Name,
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
                    LookupId = x.SectionLookupId,
                    ParentId = x.HfDepartmentId,
                    ParentLookupId = x.SectionLookup.ConsultantSectionLookupId,
                    SpecialityFee = x.SectionLookup.SpecialityFee
                }).ToList();
            }
            else
            {
                var tokenUser = TokenService.GetUserLoggedInfo();
                var hfDepartmentSection = new List<DropdownSectionDto>();

                // if not health facility user then no need of department & section
                if (AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                    return hfDepartmentSection;
                else
                {
                    // if health facility user then get department & section
                    hfDepartmentSection = await GetAllHfDepartmentSectionFromSP();

                    if (!AppCommonMethod.IsNullorZeroInt(tokenUser.HealthFacilityId))
                        hfDepartmentSection = hfDepartmentSection.Where(x => x.HealthFacilityId == tokenUser.HealthFacilityId).ToList();

                    if (_isOnline)
                    {
                        hfDepartmentSection = hfDepartmentSection.Where(x => 
                            x.SpecialityRunningMode == (int)SpecialityRunningMode.Both_OnlineOffline || 
                            x.SpecialityRunningMode == (int)SpecialityRunningMode.Online ||
                            x.SpecialityRunningMode == null) // null consider as both
                            .ToList();
                    } else {
                        hfDepartmentSection = hfDepartmentSection.Where(x =>
                            x.SpecialityRunningMode == (int)SpecialityRunningMode.Both_OnlineOffline ||
                            x.SpecialityRunningMode == (int)SpecialityRunningMode.Offline ||
                            x.SpecialityRunningMode == null) // null consider as both
                            .ToList();
                    }
                    
                        

                    return hfDepartmentSection;
                }


                //var tokenUser = TokenService.GetUserLoggedInfo();
                //var hfDepartmentSection = new List<DropdownSectionDto>();

                //// if not health facility user then no need of department & section
                //if (AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                //    return hfDepartmentSection;
                //else
                //{ // if health facility user then get department & section

                //    if (_isStaticDDEnable)
                //        hfDepartmentSection = LocationStaticDto.GetHfDepartmentSectionDropdowns();

                //    if (!AppCommonMethod.IsNullOrEmptyList<DropdownSectionDto>(hfDepartmentSection) && _isStaticDDEnable)
                //        return hfDepartmentSection;
                //    else
                //    {

                //        var conn = _uowHfDepartmentSection.GetDbContext().Database.GetDbConnection();
                //        try
                //        {

                //            DataSet ds = new DataSet();
                //            SqlCommand sqlComm = new SqlCommand("[SPGetAllHfDepartmentSectionByHealthFacilityId]", (SqlConnection)conn);
                //            sqlComm.CommandType = CommandType.StoredProcedure;

                //            if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser!.HealthFacilityId);

                //            //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DepartmentId))
                //            //    sqlComm.Parameters.AddWithValue("@DepartmentLookupId", tokenUser!.DepartmentId);

                //            SqlDataAdapter da = new SqlDataAdapter();
                //            da.SelectCommand = sqlComm;
                //            await Task.Run(() => da.Fill(ds));
                //            List<DropdownSectionDto> lst = ds.Tables[0].ToList<DropdownSectionDto>();

                //            if (!AppCommonMethod.IsNullOrEmptyList<DropdownSectionDto>(lst) && _isStaticDDEnable)
                //                LocationStaticDto.SetHfDepartmentSectionDropdowns(lst);

                //            return lst;
                //        }
                //        catch (Exception ex)
                //        {
                //            throw;
                //        }
                //        finally
                //        {
                //            conn.Close();
                //        }
                //    }
                //}


                //var tokenUser = TokenService.GetUserLoggedInfo();
                //var dbHfDepartmentSections = await _uowHfDepartmentSection.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                //    .Include(x => x.HfDepartment).ThenInclude(y => y!.HealthFacility)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.DivisionId), x => x.HfDepartment!.HealthFacility!.DivisionId == tokenUser.DivisionId)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.DistrictId), x => x.HfDepartment!.HealthFacility!.DistrictId == tokenUser.DistrictId)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.TehsilId), x => x.HfDepartment!.HealthFacility!.TehsilId == tokenUser.TehsilId)
                //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId), x => x.HfDepartment!.HealthFacility!.HealthFacilityId == tokenUser.HealthFacilityId)
                //    .Include(x => x.SectionLookup).ToListAsync();

                //return dbHfDepartmentSections!.Select(x => new DropdownSectionDto
                //{
                //    Id = x.HfDepartmentSectionId,
                //    Name = x.SectionLookup!.Name,
                //    VitalsFloorNo = x.VitalsFloorNo,
                //    VitalsRoomNo = x.VitalsRoomNo,
                //    DoctorFloorNo = x.DoctorFloorNo,
                //    DoctorRoomNo = x.DoctorRoomNo,
                //    PharmacyFloorNo = x.PharmacyFloorNo,
                //    PharmacyRoomNo = x.PharmacyRoomNo,
                //    PathalogyFloorNo = x.PathalogyFloorNo,
                //    PathalogyRoomNo = x.PathalogyRoomNo,
                //    AlmonerFloorNo = x.AlmonerFloorNo,
                //    AlmonerRoomNo = x.AlmonerRoomNo,
                //    LookupId = x.SectionLookupId,
                //    ParentId = x.HfDepartmentId,
                //    ParentLookupId = x.SectionLookup.ConsultantSectionLookupId
                //}).ToList();
            }

            //if (CacheData.HfDepartmentSections.Count() <= 0)
            //{
            //    CacheData.HfDepartmentSections = _mapper.Map<List<CacheHfDepartmentSectionDto>>(await _uowHfDepartmentSection.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.SectionLookup).ToListAsync());
            //}

            //var hfDepartmentSections = CacheData.HfDepartmentSections.Select(x => new DropdownDto
            //{
            //    Id = x.HfDepartmentSectionId,
            //    Name = x.SectionLookup!.Name,
            //    LookupId = x.SectionLookupId,
            //    ParentId = x.HfDepartmentId
            //}).ToList();

            //return hfDepartmentSections;



        //}



        }

        public async Task<List<DropdownSectionDto>> GetHfDepartmentSectionDropdownByHealthFacility(int HealthFacilityId)
        {

            //var tokenUser = TokenService.GetUserLoggedInfo();
            //var hfDepartment = new List<DropdownSectionDto>();

            var conn = _uowHfDepartmentSection.GetDbContext().Database.GetDbConnection();
            try
            {

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[SPGetAllHfDepartmentSectionByHealthFacilityId]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DepartmentId))
                //    sqlComm.Parameters.AddWithValue("@DepartmentLookupId", tokenUser!.DepartmentId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                List<DropdownSectionDto> lst = ds.Tables[0].ToList<DropdownSectionDto>();

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
        private void FillEntity(HfDepartmentSection obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.HfDepartmentSectionId))
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
        private void FillEntityDelete(HfDepartmentSection obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
