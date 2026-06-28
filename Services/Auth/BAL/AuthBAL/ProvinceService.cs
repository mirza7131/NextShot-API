using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DivisionDto;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.ProvinceDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using CommonDTOs.LocationDTO;
using HealthFacilityBAL;
using RedisCache;
using Microsoft.Extensions.Configuration;

namespace AuthBAL
{
    public class ProvinceService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly DivisionService<Division> _divisionService;
        private readonly DistrictService<District> _districtService;
        private readonly TehsilService<Tehsil> _tehsilService;
        private readonly UnionCouncilService<UnionCouncil> _unionCouncilService;
        private readonly HealthFacilityService<HealthFacility> _healthFacilityService;
        private readonly HfDepartmentService<HfDepartment> _hfDepartmentService;
        private readonly HfDepartmentSectionService<HfDepartmentSection> _hfDepartmentSectionService;
        private readonly IMapper _mapper;
        private UnitOfWork<Province> _uowProvince;
        private UnitOfWork<Division> _uowDivision;
        private UnitOfWork<District> _uowDistrict;
        private UnitOfWork<Tehsil> _uowTehsil;
        private string[] myInClause; // Health Facilities Types Allow Only
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;
        #endregion

        #region Constructor

        public ProvinceService(
                TokenService tokenService, 
                UnitOfWork<Province> uowProvince, 
                IMapper mapper,
                DivisionService<Division> divisionService,
                DistrictService<District> districtService,
                TehsilService<Tehsil> tehsilService,
                UnionCouncilService<UnionCouncil> unionCouncilService,
                HealthFacilityService<HealthFacility> healthFacilityService,
                HfDepartmentService<HfDepartment> hfDepartmentService,
                HfDepartmentSectionService<HfDepartmentSection> hfDepartmentSectionService,

                UnitOfWork<Division> uowDivision,
                UnitOfWork<District> uowDistrict,
                UnitOfWork<Tehsil> uowTehsil,
                IRedisCacheService redisCacheService, IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowProvince = uowProvince;
            _uowDivision = uowDivision;
            _uowDistrict = uowDistrict;
            _uowTehsil = uowTehsil;
            _divisionService = divisionService;
            _districtService = districtService;
            _tehsilService = tehsilService;
            _unionCouncilService = unionCouncilService;
            _healthFacilityService = healthFacilityService;
            _hfDepartmentService = hfDepartmentService;
            _hfDepartmentSectionService = hfDepartmentSectionService;
            _cacheService = redisCacheService;
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


        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditProvinceDto> CreateOrEdit(CreateOrEditProvinceDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.ProvinceId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditProvinceDto> Create(CreateOrEditProvinceDto input)
        {
            var obj = _mapper.Map<Province>(input);
            FillEntity(obj);
            Province responseObj = await _uowProvince.Repository.Insert(obj);
            await _uowProvince.CommitAsync();
            //RefreshProvinceCacheList();
            if(_isStaticDDEnable)
                await RefreshProvinceStaticCacheList();

            return _mapper.Map<CreateOrEditProvinceDto>(responseObj);
        }

        private async Task<CreateOrEditProvinceDto> Update(CreateOrEditProvinceDto input)
        {
            var dbObj = await _uowProvince.Repository.GetById(input.ProvinceId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowProvince.Repository.Update(obj!);
            await _uowProvince.CommitAsync();
            //RefreshProvinceCacheList();
            if (_isStaticDDEnable)
                await RefreshProvinceStaticCacheList();
            return _mapper.Map<CreateOrEditProvinceDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowProvince.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowProvince.Repository.Update(dbObj!);
            await _uowProvince.CommitAsync();
            //RefreshProvinceCacheList();
            if (_isStaticDDEnable)
                await RefreshProvinceStaticCacheList();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewProvinceDto>> GetAll(Expression<Func<Province, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<Province> responseObj = await _uowProvince.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewProvinceDto>>(responseObj);
        }

        public async Task<LocationDto> GetAllLocations(bool IsHmisHf = false)
        {
            //var _uowDistrict = new UnitOfWork<District>(_uowProvince.GetDbContext());
            //var _uowTehsil = new UnitOfWork<Tehsil>(_uowProvince.GetDbContext());
            //var _uowUnionCouncil = new UnitOfWork<UnionCouncil>(_uowProvince.GetDbContext());
            //var _uowHealthFacility = new UnitOfWork<HealthFacility>(_uowProvince.GetDbContext());
            //var _uowHfDepartment = new UnitOfWork<HfDepartment>(_uowProvince.GetDbContext());
            //var _uowHfDepartmentSection = new UnitOfWork<HfDepartmentSection>(_uowProvince.GetDbContext());

            LocationDto obj = new LocationDto();

            obj.ProvinceDropdown = await GetProvinceDropdown();

            obj.DivisionDropdown = await _divisionService.GetDivisionDropdown();

            obj.DistrictDropdown = await _districtService.GetDistrictDropdown(); 

            obj.TehsilDropdown = await _tehsilService.GetTehsilDropdown();

            //obj.UCDropdown = await _unionCouncilService.GetUnionCouncilDropdown();

            obj.HealthFacilityDropdown = await _healthFacilityService.GetHealthFacilityDropdown(IsHmisHf);

            //obj.UCDropdown = await _unionCouncilService.GetAllUcs();

            //obj.HfDepartmentDropdown = await _hfDepartmentService.GetHfDepartmentDropdown();

            //obj.HfDepartmentSectionDropdown = await _hfDepartmentSectionService.GetHfDepartmentSectionDropdown();

            return obj;
        }

        public async Task<ViewProvinceDto> GetById(int input)
        {
            Province? responseObj = await _uowProvince.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewProvinceDto>(responseObj);
        }

        public async void RefreshProvinceCacheList()
        {
            //var listProvinces = await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //CacheData.Provinces = _mapper.Map<List<CacheProvinceDto>>(listProvinces);


            var listProvinces = await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            var Provinces = _mapper.Map<List<CacheProvinceDto>>(listProvinces);

            _cacheService.Set<List<CacheProvinceDto>?>(CacheKeyConstant.Province, Provinces, null, null);
        }

        public async Task RefreshProvinceStaticCacheList()
        {
            var provincesList = await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .Select(x => new ProvinceDropdownDto
                    {
                        Id = x.ProvinceId,
                        Name = x.Name,
                        Code = x.Code
                    })
                    .ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList<ProvinceDropdownDto>(provincesList))
                LocationStaticDto.SetProvinceDropdowns(provincesList);
        }

        #endregion

        #region Get Locations For Dashboard filters
        public async Task<DasboardFiltersDto> GetAllLocationsFilters(bool IsHmisHf = false)
        {

            DasboardFiltersDto obj = new DasboardFiltersDto();

            obj.ProvinceDropdown = await GetProvinceDropdown();

            obj.DivisionDropdown = await _divisionService.GetDivisionDropdown();

            obj.DistrictDropdown = await _districtService.GetDistrictDropdown();

            obj.TehsilDropdown = await _tehsilService.GetTehsilDropdown();

            obj.HealthFacilityTypeDropdown = await _healthFacilityService.GetHealthFacilityTypeDropdown();

            obj.HealthFacilityDropdown = await _healthFacilityService.GetHealthFacilityDashboardFilterDropdown(IsHmisHf);

            obj.HfDepartmentDropdown = await _hfDepartmentService.GetHfDepartmentDropdown();

            obj.HfDepartmentSectionDropdown = await _hfDepartmentSectionService.GetHfDepartmentSectionDropdown();

            return obj;
        }
        #endregion
        #region Helper Methods

        public async Task<List<ProvinceDropdownDto>> GetProvinceDropdown()
        {
            
            if (_isRedisCacheEnable)
            {

                var provinces = _cacheService.Get<List<CacheProvinceDto>?>(CacheKeyConstant.Province);

                if (provinces == null)
                {
                    var dbProvinces = await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheProvinceDto>>(dbProvinces);

                    provinces = _cacheService.Set<List<CacheProvinceDto>?>(CacheKeyConstant.Province, cacheData, null, null);
                }

                return provinces!.Select(x => new ProvinceDropdownDto
                {
                    Id = x.ProvinceId,
                    Name = x.Name,
                    Code = x.Code
                }).ToList();

            }
            else
            {
                var provinces = new List<ProvinceDropdownDto>();

                if(_isStaticDDEnable)
                    provinces = LocationStaticDto.GetProvinceDropdowns();

                if (!AppCommonMethod.IsNullOrEmptyList<ProvinceDropdownDto>(provinces) && _isStaticDDEnable)
                    return provinces;
                else
                {
                    var provincesList = await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .Select(x => new ProvinceDropdownDto
                        {
                            Id = x.ProvinceId,
                            Name = x.Name,
                            Code = x.Code
                        })
                    .ToListAsync();

                    if (!AppCommonMethod.IsNullOrEmptyList<ProvinceDropdownDto>(provincesList) && _isStaticDDEnable)
                        LocationStaticDto.SetProvinceDropdowns(provincesList);

                    return provincesList;
 
                }

                //var provincesList = await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                //    .ToListAsync();
                
                //return provincesList!.Select(x => new ProvinceDropdownDto
                //{
                //    Id = x.ProvinceId,
                //    Name = x.Name,
                //    Code = x.Code
                //}).ToList();
            }

            //if (CacheData.Provinces.Count() <= 0)
            //{
            //    CacheData.Provinces = _mapper.Map<List<CacheProvinceDto>>(await _uowProvince.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync());
            //}

            //var provinces =  CacheData.Provinces.Select(x => new DropdownDto
            // {
            //     Id = x.ProvinceId,
            //     Name = x.Name,
            //     Code = x.Code
            // }).ToList();

            //return provinces;

        }

        private void FillEntity(Province obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.ProvinceId))
            {
                //obj.ProvinceId = Guid.NewGuid();
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
        private void FillEntityDelete(Province obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }



        #endregion
    }
}
