using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DistrictDto;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.MenuDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonDTOs.LocationDTO;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RedisCache;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Numerics;

namespace HealthFacilityBAL
{
    public class DistrictService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<District> _uowDistrict;
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;

        #endregion

        #region Constructor

        public DistrictService(
            TokenService tokenService, 
            UnitOfWork<District> uowDistrict, 
            IMapper mapper, 
            IRedisCacheService cacheService,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowDistrict = uowDistrict;
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

        public async Task<CreateOrEditDistrictDto> CreateOrEdit(CreateOrEditDistrictDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.DistrictId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditDistrictDto> Create(CreateOrEditDistrictDto input)
        {
            var obj = _mapper.Map<District>(input);
            FillEntity(obj);
            District responseObj = await _uowDistrict.Repository.Insert(obj);
            await _uowDistrict.CommitAsync();
            //RefreshDistrictCacheList();
            if (_isStaticDDEnable)
                await RefreshDistrictStaticCacheList();
            return _mapper.Map<CreateOrEditDistrictDto>(responseObj);
        }

        private async Task<CreateOrEditDistrictDto> Update(CreateOrEditDistrictDto input)
        {
            var dbObj = await _uowDistrict.Repository.GetById(input.DistrictId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowDistrict.Repository.Update(obj!);
            await _uowDistrict.CommitAsync();
            //RefreshDistrictCacheList();
            if (_isStaticDDEnable)
                await RefreshDistrictStaticCacheList();

            return _mapper.Map<CreateOrEditDistrictDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowDistrict.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);
            _uowDistrict.Repository.Update(dbObj!);
            await _uowDistrict.CommitAsync();
            //RefreshDistrictCacheList();
            if (_isStaticDDEnable)
                await RefreshDistrictStaticCacheList();

            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewDistrictDto>> GetAll(Expression<Func<District, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<District> responseObj = await _uowDistrict.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewDistrictDto>>(responseObj);
        }

        public async Task<ViewDistrictDto> GetById(int input)
        {
            District? responseObj = await _uowDistrict.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewDistrictDto>(responseObj);
        }

        public async Task<List<ViewDistrictDto>> GetAllByDivisionId(int DivisionId)
        {
            List<District> responseObj = await _uowDistrict.Repository.GetALL(x => x.DivisionId == DivisionId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).ToListAsync();
            return _mapper.Map<List<ViewDistrictDto>>(responseObj);
        }

        public async void RefreshDistrictCacheList()
        {

            //var listDistricts = await _uowDistrict.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //CacheData.Districts = _mapper.Map<List<CacheDistrictDto>>(listDistricts);

            var listDistricts = await _uowDistrict.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            var districts = _mapper.Map<List<CacheDistrictDto>>(listDistricts);

            _cacheService.Set<List<CacheDistrictDto>?>(CacheKeyConstant.District, districts, null, null);
        }

        public async Task RefreshDistrictStaticCacheList()
        {
            var dbDistricts = await _uowDistrict.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .Select(x => new DDTDropdownDto
                        {
                            Id = x.DistrictId,
                            Name = x.Name,
                            ParentId = x.DivisionId,
                            Code = x.Code
                        }).ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(dbDistricts) && _isStaticDDEnable)
                LocationStaticDto.SetDistrictDropdowns(dbDistricts);
        }

        #endregion

        #region Helper Methods

        public async Task<List<DDTDropdownDto>> GetDistrictDropdown()
        {

            if (_isRedisCacheEnable)
            {
                var districts = _cacheService.Get<List<CacheDistrictDto>?>(CacheKeyConstant.District);

                if (districts == null)
                {
                    var dbDistricts = await _uowDistrict.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheDistrictDto>>(dbDistricts);

                    districts = _cacheService.Set<List<CacheDistrictDto>?>(CacheKeyConstant.District, cacheData, null, null);
                }

                return districts!.Select(x => new DDTDropdownDto
                {
                    Id = x.DistrictId,
                    Name = x.Name,
                    ParentId = x.DivisionId,
                    Code = x.Code
                }).ToList();
            }
            else
            {

                var dbDistricts = new List<DDTDropdownDto>();

                if (_isStaticDDEnable)
                    dbDistricts = LocationStaticDto.GetDistrictDropdowns();

                if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(dbDistricts) && _isStaticDDEnable)
                    return dbDistricts;
                else
                {
                    dbDistricts = await _uowDistrict.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .Select(x => new DDTDropdownDto
                        {
                            Id = x.DistrictId,
                            Name = x.Name,
                            ParentId = x.DivisionId,
                            Code = x.Code
                        }).ToListAsync();

                    if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(dbDistricts) && _isStaticDDEnable)
                        LocationStaticDto.SetDistrictDropdowns(dbDistricts);

                    return dbDistricts;
                }
            }

            //if (CacheData.Districts.Count() <= 0)
            //{
            //    CacheData.Districts = _mapper.Map<List<CacheDistrictDto>>(await _uowDistrict.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync());
            //}

            //var districcts = CacheData.Districts.Select(x => new DropdownDto
            //{
            //    Id = x.DistrictId,
            //    Name = x.Name,
            //    ParentId = x.DivisionId,
            //    Code = x.Code
            //}).ToList();

            //return districcts;

        }
        private void FillEntity(District obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.DistrictId))
            {
                //obj.DistrictId = Guid.NewGuid();
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
        private void FillEntityDelete(District obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }


}