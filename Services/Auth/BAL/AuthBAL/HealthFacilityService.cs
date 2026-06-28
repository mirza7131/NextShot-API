using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonDTOs.LocationDTO;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedisCache;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace AuthBAL
{
    public class HealthFacilityService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HealthFacility> _uowHealthFacility;
        private UnitOfWork<HealthFacilityType> _uowHealthFacilityType;
        private readonly HRService _hrService;
        private string[] myInClause;//= new string[] { "011", "012", "068", "016", "043", "036" ,"037"}; // Health Facilities Types Allow Only
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;
        private readonly string _hrTokneUserName;
        private readonly string _hrToknePassword;
        private readonly string _hrBaseUrl;
        private readonly string _dtlLabCode;
        #endregion

        #region Constructor

        public HealthFacilityService(
            TokenService tokenService, 
            UnitOfWork<HealthFacility> uowHealthFacility,
            UnitOfWork<HealthFacilityType> uowHealthFacilityType,
            IMapper mapper, 
            HRService hrService,
            IRedisCacheService cacheService, IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHealthFacility = uowHealthFacility;
            _uowHealthFacilityType = uowHealthFacilityType;
            _hrService = hrService;
            _cacheService = cacheService;
            myInClause = config.GetSection("HealthFacility").GetSection("TypesAllowed").Get<string[]>() ?? new string[0];
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
            _hrTokneUserName = config.GetSection("HrTokenUserName").Value ?? string.Empty;
            _hrToknePassword = config.GetSection("HrTokenPassword").Value ?? string.Empty;
            _hrBaseUrl = config.GetSection("HrBaseUrl").Value ?? string.Empty;
            _dtlLabCode = config.GetSection("DTLLabCode").Value ?? string.Empty;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHealthFacilityDto> CreateOrEdit(CreateOrEditHealthFacilityDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHealthFacilityDto> Create(CreateOrEditHealthFacilityDto input)
        {
            var obj = _mapper.Map<HealthFacility>(input);
            FillEntity(obj);
            HealthFacility responseObj = await _uowHealthFacility.Repository.Insert(obj);
            await _uowHealthFacility.CommitAsync();
            //RefreshHealthFacilityCacheList();
            if (_isStaticDDEnable)
                await RefreshHealthFacilityStaticCacheList();

            return _mapper.Map<CreateOrEditHealthFacilityDto>(responseObj);
        }

        private async Task<CreateOrEditHealthFacilityDto> Update(CreateOrEditHealthFacilityDto input)
        {
            var dbObj = await _uowHealthFacility.Repository.GetById(input.HealthFacilityId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowHealthFacility.Repository.Update(obj!);
            await _uowHealthFacility.CommitAsync();
            //RefreshHealthFacilityCacheList();
            if (_isStaticDDEnable)
                await RefreshHealthFacilityStaticCacheList();

            return _mapper.Map<CreateOrEditHealthFacilityDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowHealthFacility.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowHealthFacility.Repository.Update(dbObj!);
            await _uowHealthFacility.CommitAsync();
            //RefreshHealthFacilityCacheList();
            if (_isStaticDDEnable)
                await RefreshHealthFacilityStaticCacheList();

            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<ViewHealthFacilityDto>> GetAll(Expression<Func<HealthFacility, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {

            //var _uowUser = new UnitOfWork<User>(_uowHealthFacility.GetDbContext());
            //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();

            var dbUser = TokenService.GetUserLoggedInfo();

            List<HealthFacility> responseObj = await _uowHealthFacility.Repository.GetALL(filter)
                .Include(x => x.Tehsil)
                    .ThenInclude(x => x!.District)
                        .ThenInclude(x => x!.Division)
                            .ThenInclude(x => x!.Province)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.ProvinceId), x => x.Tehsil!.District!.Division!.Province!.ProvinceId == dbUser!.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DivisionId), x => x.Tehsil!.District!.Division!.DivisionId == dbUser!.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DistrictId), x => x.Tehsil!.District!.DistrictId == dbUser!.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.TehsilId), x => x.Tehsil!.TehsilId == dbUser!.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.HealthFacilityId), x => x.HealthFacilityId == dbUser!.HealthFacilityId)
                .ToListAsync();
            return _mapper.Map<List<ViewHealthFacilityDto>>(responseObj);
        }

        public async Task<List<ViewHealthFacilityDto>> GetAll(FilterHealthFacilityDto filter)
        {
            var dbHealthFacilities = new List<ViewHealthFacilityDto>();
            if (_isStaticDDEnable)
                dbHealthFacilities = LocationStaticDto.GetHealthFacilityDropdowns();

            if (!AppCommonMethod.IsNullOrEmptyList<ViewHealthFacilityDto>(dbHealthFacilities) && _isStaticDDEnable)
            {
                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.ProvinceId == filter.ProvinceId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.DivisionId == filter.DivisionId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.DistrictId == filter.DistrictId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.TehsilId == filter.TehsilId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.HealthFacilityId == filter.HealthFacilityId).ToList();
                
                return dbHealthFacilities.OrderBy(x => x.Name).ToList();

            }
            else
            {
                List<HealthFacility> responseObj = await _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
                
                dbHealthFacilities = _mapper.Map<List<ViewHealthFacilityDto>>(responseObj);
                
                if(_isStaticDDEnable)
                    LocationStaticDto.SetHealthFacilityDropdowns(dbHealthFacilities);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.ProvinceId == filter.ProvinceId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.DivisionId == filter.DivisionId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.DistrictId == filter.DistrictId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.TehsilId == filter.TehsilId).ToList();
                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    dbHealthFacilities = dbHealthFacilities.Where(x => x.HealthFacilityId == filter.HealthFacilityId).ToList();

                
            }
            return dbHealthFacilities.OrderBy(x => x.Name).ToList();

            //List<HealthFacility> responseObj = await _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.ProvinceId), x => x.ProvinceId == filter!.ProvinceId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.DivisionId), x => x.DivisionId == filter!.DivisionId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.DistrictId), x => x.DistrictId == filter!.DistrictId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.TehsilId), x => x.TehsilId == filter!.TehsilId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.HealthFacilityId), x => x.HealthFacilityId == filter!.HealthFacilityId)
            //.ToListAsync();
            //return _mapper.Map<List<ViewHealthFacilityDto>>(responseObj);
        }

        public async Task<ViewPagerDto<ViewHealthFacilityDto>> GetAllWithPagination(FilterHealthFacilityDto filter)
        {
            var list = _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.ProvinceId), x => x.ProvinceId == filter!.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.DivisionId), x => x.DivisionId == filter!.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.DistrictId), x => x.DistrictId == filter!.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.TehsilId), x => x.TehsilId == filter!.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter!.HealthFacilityId), x => x.HealthFacilityId == filter!.HealthFacilityId)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name!.ToLower().StartsWith(filter.SearchString) || x.Code.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewHealthFacilityDto> IQueryableList = list.Select(x =>
                new ViewHealthFacilityDto
                {

                    HealthFacilityId = x.HealthFacilityId,
                    Name = x.Name,
                    Code = x.Code,
                    HealthFacilityTypeCode = x.HealthFacilityTypeCode,
                    HealthFacilityTypeId = x.HealthFacilityTypeId,
                    ProvinceId = x.ProvinceId,
                    ProvinceCode = x.ProvinceCode,
                    DivisionId = x.DivisionId,
                    DivisionCode = x.DivisionCode,
                    DistrictId = x.DistrictId,
                    DistrictCode = x.DistrictCode,
                    TehsilId = x.TehsilId,
                    TehsilCode = x.TehsilCode,
                    UnionCouncilId = x.UnionCouncilId,
                    UnionCouncilCode = x.UnionCouncilCode,
                    IsActive = x.IsActive
                });

            var pagedList = await PagedListDto<ViewHealthFacilityDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewHealthFacilityDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }

        public async Task<ViewHealthFacilityDto> GetById(int input)
        {
            HealthFacility? responseObj = await _uowHealthFacility.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHealthFacilityDto>(responseObj);
        }


        public async Task<CreateOrEditHealthFacilityDto> CreateOrUpdateFromHr(CreateOrEditHealthFacilityDto input)
        {

            HealthFacility? responseObj = await _uowHealthFacility.Repository.GetALL(x => x.HrId == input.HrId).FirstOrDefaultAsync();


            if(!AppCommonMethod.IsNullObject(responseObj))
            {

                //var dbObj = await _uowHealthFacility.Repository.GetById(input.HealthFacilityId!);

                //if (AppCommonMethod.IsNullObject(dbObj))
                //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                //var obj = _mapper.Map(input, responseObj);
                //responseObj!.HrId = input.HrId;
                //FillEntity(responseObj!);

                //_uowHealthFacility.Repository.Update(responseObj!);
                //await _uowHealthFacility.CommitAsync();

                //await RefreshHealthFacilityCacheList();

            }
            else
            {
                var obj = _mapper.Map<HealthFacility>(input);
                FillEntity(obj);
                var responseObjj = await _uowHealthFacility.Repository.Insert(obj);
                await _uowHealthFacility.CommitAsync();
                //RefreshHealthFacilityCacheList();
                if (_isStaticDDEnable)
                    await RefreshHealthFacilityStaticCacheList();

                return _mapper.Map<CreateOrEditHealthFacilityDto>(responseObjj);
            }

            return _mapper.Map<CreateOrEditHealthFacilityDto>(responseObj);

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //return _mapper.Map<ViewHealthFacilityDto>(responseObj);
        }


        public async Task GetHrHealthFacilitesAsync()
        {
            //var token = await GetHrAuthTokenAsync();

            //HttpClient client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://hrmis.pshealthpunjab.gov.pk/api/HealthFacility/GetDetailHFList");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            //var content = await response.Content.ReadAsStringAsync();

            //ViewHrHealthFacilityDto healtFacilities = JsonConvert.DeserializeObject<ViewHrHealthFacilityDto>(content);

            var healthFacilities = await _hrService.GetHrFacilities(_hrTokneUserName, _hrToknePassword, _hrBaseUrl);

            if (healthFacilities.Data != null)
            {
                foreach (var item in healthFacilities.Data)
                {
                    var tempObj = new CreateOrEditHealthFacilityDto();

                    tempObj.HrId = item.Id;
                    tempObj.Name = item.FullName;
                    tempObj.DistrictCode = item.DistrictCode;
                    tempObj.DivisionCode = item.DivisionCode;
                    tempObj.Code = item.HFMISCode;
                    tempObj.HealthFacilityTypeCode = item.HFTypeCode;
                    tempObj.TehsilCode = item.TehsilCode;
                    //tempObj.HealthFacilityTypeCode = item.

                    await CreateOrEdit(tempObj);
                    //await CreateOrUpdateFromHr(tempObj);
                }
            }
            //return healtFacilities;

        }

        public async Task UpdateHealthFacilitiesFromHr()
        {
            
            var healthFacilities = await _hrService.GetHrFacilities(_hrTokneUserName, _hrToknePassword, _hrBaseUrl);

            if (healthFacilities.Data != null)
            {
                foreach (var item in healthFacilities.Data)
                {
                    var tempObj = new CreateOrEditHealthFacilityDto();

                    tempObj.HrId = item.Id;
                    tempObj.Name = item.FullName;
                    tempObj.DistrictCode = item.DistrictCode;
                    tempObj.DivisionCode = item.DivisionCode;
                    tempObj.Code = item.HFMISCode;
                    tempObj.HealthFacilityTypeCode = item.HFTypeCode;
                    tempObj.TehsilCode = item.TehsilCode;
                    //tempObj.HealthFacilityTypeCode = item.

                    //await CreateOrEdit(tempObj);
                    await CreateOrUpdateFromHr(tempObj);
                }
            }
            //return healtFacilities;
            if (_isStaticDDEnable)
                await RefreshHealthFacilityStaticCacheList();

        }

        public async void RefreshHealthFacilityCacheList()
        {
            //var listHealthFacilities = await _uowHealthFacility.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //CacheData.HealthFacilities = _mapper.Map<List<CacheHealthFacilityDto>>(listHealthFacilities);

            var listHealthFacilities = await _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            var healthFacilities = _mapper.Map<List<CacheHealthFacilityDto>>(listHealthFacilities);

            _cacheService.Set<List<CacheHealthFacilityDto>?>(CacheKeyConstant.HealthFacility, healthFacilities, null, null);
        }

        public async Task RefreshHealthFacilityStaticCacheList()
        {
            var dbHealthFacilities = await _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .ToListAsync();
            var dbData = _mapper.Map<List<ViewHealthFacilityDto>>(dbHealthFacilities);

            if (!AppCommonMethod.IsNullOrEmptyList<ViewHealthFacilityDto>(dbData) && _isStaticDDEnable)
                LocationStaticDto.SetHealthFacilityDropdowns(dbData);
        }

        #endregion

        #region Helper Methods

        public async Task<List<HealthFacilityDropdownDto>> GetHealthFacilityDropdown(bool OnlyHMISHf = false)
        {
            if (_isRedisCacheEnable)
            {
                var healthFacilities = _cacheService.Get<List<CacheHealthFacilityDto>?>(CacheKeyConstant.HealthFacility);
                if (healthFacilities == null)
                {

                    var dbHealthFacilities = await _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .WhereIf(OnlyHMISHf == true, x => x.IsRunningHmis == true)
                        .ToListAsync();

                    var cacheData = _mapper.Map<List<CacheHealthFacilityDto>>(dbHealthFacilities);

                    healthFacilities = _cacheService.Set<List<CacheHealthFacilityDto>?>(CacheKeyConstant.HealthFacility, cacheData, null, null);
                }

                return healthFacilities!.Select(x => new HealthFacilityDropdownDto
                {
                    Id = x.HealthFacilityId,
                    Name = x.Name,
                    ParentId = x.TehsilId,
                    Code = x.Code,
                    HfTypeCode = x.HealthFacilityTypeCode,
                    IsRunningHMIS = x.IsRunningHMIS
                }).ToList();
            }
            else
            {
              //  var tokenUser = TokenService.GetUserLoggedInfo();

                // get all healthfacilities
                var filter = new FilterHealthFacilityDto();
                var dbHealthFacilities = await GetAll(filter);

                if (!AppCommonMethod.IsNullOrEmptyList<ViewHealthFacilityDto>(dbHealthFacilities))
                {
                    if (OnlyHMISHf == true)
                        dbHealthFacilities = dbHealthFacilities.Where(x => x.IsRunningHmis == true).ToList();

                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DivisionId))
                    //    dbHealthFacilities = dbHealthFacilities.Where(x => x.DivisionId == tokenUser.DivisionId).ToList();
                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DistrictId))
                    //    dbHealthFacilities = dbHealthFacilities.Where(x => x.DistrictId == tokenUser.DistrictId).ToList();
                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.TehsilId))
                    //    dbHealthFacilities = dbHealthFacilities.Where(x => x.TehsilId == tokenUser.TehsilId).ToList();
                    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                    //    dbHealthFacilities = dbHealthFacilities.Where(x => x.HealthFacilityId == tokenUser.HealthFacilityId).ToList();
                
                }

                return dbHealthFacilities.Select(x => new HealthFacilityDropdownDto
                {
                    Id = x.HealthFacilityId,
                    Name = x.Name,
                    ParentId = x.TehsilId,
                    Code = x.Code,
                    HfTypeCode = x.HealthFacilityTypeCode,
                    IsRunningHMIS = x.IsRunningHmis
                }).ToList();

                //var healthFacilityList = _mapper.Map<List<HealthFacilityDropdownDto>>(dbHealthFacility);

                //if (!AppCommonMethod.IsNullOrEmptyList<ViewHealthFacilityDto>(dbHealthFacilities) && _isStaticDDEnable)
                //{
                //if (OnlyHMISHf == true)
                //    dbHealthFacilities =  dbHealthFacilities.Where(x => x.IsRunningHmis == true).ToList();

                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DivisionId))
                //    dbHealthFacilities = dbHealthFacilities.Where(x => x.DivisionId == tokenUser.DivisionId).ToList();
                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DistrictId))
                //    dbHealthFacilities = dbHealthFacilities.Where(x => x.DistrictId == tokenUser.DistrictId).ToList();
                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.TehsilId))
                //    dbHealthFacilities = dbHealthFacilities.Where(x => x.TehsilId == tokenUser.TehsilId).ToList();
                //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                //    dbHealthFacilities = dbHealthFacilities.Where(x => x.HealthFacilityId == tokenUser.HealthFacilityId).ToList();

                //return dbHealthFacilities.Select(x => new HealthFacilityDropdownDto
                //{
                //    Id = x.HealthFacilityId,
                //    Name = x.Name,
                //    ParentId = x.TehsilId,
                //    Code = x.Code,
                //    HfTypeCode = x.HealthFacilityTypeCode,
                //    IsRunningHMIS = x.IsRunningHmis
                //}).ToList();
                //}
                //else
                //{
                //    var dbHealthFacilitiesList = await _uowHealthFacility.Repository.GetALL(x => myInClause.Contains(x.HealthFacilityTypeCode) && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                //        .ToListAsync();

                //    var dbData = _mapper.Map<List<CacheHealthFacilityDto>>(dbHealthFacilitiesList);

                //    if (!AppCommonMethod.IsNullOrEmptyList<CacheHealthFacilityDto>(dbData) && _isStaticDDEnable)
                //        LocationStaticDto.SetHealthFacilityDropdowns(dbData);

                //    if (OnlyHMISHf == true)
                //        dbData = dbData.Where(x => x.IsRunningHMIS == true).ToList();

                //    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DivisionId))
                //    //    dbData = dbData.Where(x => x.DivisionId == tokenUser.DivisionId).ToList();
                //    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.DistrictId))
                //    //    dbData = dbData.Where(x => x.DistrictId == tokenUser.DistrictId).ToList();
                //    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.TehsilId))
                //    //    dbData = dbData.Where(x => x.TehsilId == tokenUser.TehsilId).ToList();
                //    //if (!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId))
                //    //    dbData = dbData.Where(x => x.HealthFacilityId == tokenUser.HealthFacilityId).ToList();

                //    return dbData.Select(x => new HealthFacilityDropdownDto
                //    {
                //        Id = x.HealthFacilityId,
                //        Name = x.Name,
                //        ParentId = x.TehsilId,
                //        Code = x.Code,
                //        HfTypeCode = x.HealthFacilityTypeCode,
                //        IsRunningHMIS = x.IsRunningHMIS
                //    }).ToList();
                //}
            }
        }

        public async Task<List<HealthFacilityDropdownDto>> GetAllForConsignment()
        {
            
            var tokenUser = TokenService.GetUserLoggedInfo();
            
            // get all healthfacilities
            var filter = new FilterHealthFacilityDto();
            var dbHealthFacilities = await GetAll(filter);

            if(tokenUser!.FormType == CommonStringConstant.HCPForm)
            {
                dbHealthFacilities = dbHealthFacilities.Where(x => x.Code == _dtlLabCode).ToList();
            }
            
            return dbHealthFacilities.Select(x => new HealthFacilityDropdownDto
            {
                Id = x.HealthFacilityId,
                Name = x.Name,
                ParentId = x.TehsilId,
                Code = x.Code,
                HfTypeCode = x.HealthFacilityTypeCode,
                IsRunningHMIS = x.IsRunningHmis
            }).ToList();
                
        }

        private void FillEntity(HealthFacility obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.HealthFacilityId))
            {
                //obj.HealthFacilityId = Guid.NewGuid();
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
        private void FillEntityDelete(HealthFacility obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

        #region Get HealthFacility For Dashboard Filters
        public async Task<List<HealthFacilityDropdownDashboardFilterDto>> GetHealthFacilityDashboardFilterDropdown(bool OnlyHMISHf = false)
        {
            var dbHealthFacilities = await _uowHealthFacility.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .ToListAsync();

            return dbHealthFacilities!.Select(x => new HealthFacilityDropdownDashboardFilterDto
            {
                Id = x.HealthFacilityId,
                Name = x.Name,
                ParentId = x.TehsilId,
                TehsilId = x.TehsilId,
                DistrictId = x.DistrictId,
                DivisionId = x.DivisionId,
                Code = x.Code,
                HfTypeCode = x.HealthFacilityTypeCode,
                IsRunningHMIS = x.IsRunningHmis
            }).ToList();

        }

        public async Task<List<HealthFacilityTypeDropdownDto>> GetHealthFacilityTypeDropdown()
        {
            var dbHealthFacilitiesType = await _uowHealthFacilityType.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            return dbHealthFacilitiesType!.Select(x => new HealthFacilityTypeDropdownDto
            {
                Id = x.HealthFacilityTypeId,
                Name = x.Name,
                HfTypeCode = x.Code
            }).ToList();
        }
        #endregion
        #region HR

        //public async Task<string> GetHrAuthTokenAsync()
        //{
        //    var data1 = new Dictionary<string, string>
        //    {
        //        {"username", "admin"},
        //        {"password", "e75wh"},
        //        {"grant_type", "password"}
        //    };

        //    var url = "https://hrmis.pshealthpunjab.gov.pk/Token";
        //    using var client = new HttpClient();

        //    var response = await client.PostAsync(url, new FormUrlEncodedContent(data1));

        //    var token = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<Token>(token);

        //    return result.access_token;


        //}

        //public class Token
        //{
        //    public string access_token;
        //}
        #endregion
    }
}
