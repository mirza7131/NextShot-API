using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.MenuDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AuthDAL.Models.Dto.TehsilDto;
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
    public class TehsilService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Tehsil> _uowTehsil;
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;
        #endregion

        #region Constructor

        public TehsilService(TokenService tokenService, UnitOfWork<Tehsil> uowTehsil, IMapper mapper, IRedisCacheService cacheService, IConfiguration config)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowTehsil = uowTehsil;
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

        public async Task<CreateOrEditTehsilDto> CreateOrEdit(CreateOrEditTehsilDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.TehsilId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditTehsilDto> Create(CreateOrEditTehsilDto input)
        {
            var obj = _mapper.Map<Tehsil>(input);
            FillEntity(obj);
            Tehsil responseObj = await _uowTehsil.Repository.Insert(obj);
            await _uowTehsil.CommitAsync();
            //RefreshTehsilCacheList();
            if (_isStaticDDEnable)
                await RefreshTehsilStaticCacheList();

            return _mapper.Map<CreateOrEditTehsilDto>(responseObj);
        }

        private async Task<CreateOrEditTehsilDto> Update(CreateOrEditTehsilDto input)
        {
            var dbObj = await _uowTehsil.Repository.GetById(input.TehsilId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowTehsil.Repository.Update(obj!);
            await _uowTehsil.CommitAsync();
            //RefreshTehsilCacheList();
            if (_isStaticDDEnable)
                await RefreshTehsilStaticCacheList();

            return _mapper.Map<CreateOrEditTehsilDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowTehsil.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowTehsil.Repository.Update(dbObj!);
            await _uowTehsil.CommitAsync();
            //RefreshTehsilCacheList();
            if (_isStaticDDEnable)
                await RefreshTehsilStaticCacheList();

            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewTehsilDto>> GetAll(Expression<Func<Tehsil, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<Tehsil> responseObj = await _uowTehsil.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewTehsilDto>>(responseObj);
        }

        public async Task<ViewTehsilDto> GetById(int input)
        {
            Tehsil? responseObj = await _uowTehsil.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewTehsilDto>(responseObj);
        }

        public async Task<List<ViewTehsilDto>> GetAllByDistrictId(int DistrictId)
        {
            List<Tehsil> responseObj = await _uowTehsil.Repository.GetALL(x => x.DistrictId == DistrictId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).ToListAsync();
            return _mapper.Map<List<ViewTehsilDto>>(responseObj);
        }

        public async void RefreshTehsilCacheList()
        {

            //var listTehsils =   await _uowTehsil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //CacheData.Tehsils = _mapper.Map<List<CacheTehsilDto>>(listTehsils);

            var listTehsils = await _uowTehsil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            var tehsils = _mapper.Map<List<CacheTehsilDto>>(listTehsils);

            _cacheService.Set<List<CacheTehsilDto>?>(CacheKeyConstant.Tehsil, tehsils, null, null);
        }

        public async Task RefreshTehsilStaticCacheList()
        {

            var tehsils = await _uowTehsil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .Select(x => new DDTDropdownDto
                        {
                            Id = x.TehsilId,
                            Name = x.Name,
                            ParentId = x.DistrictId,
                            Code = x.Code
                        }).ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(tehsils) && _isStaticDDEnable)
                LocationStaticDto.SetTehsilDropdowns(tehsils);
        }

        #endregion

        #region Helper Methods

        public async Task<List<DDTDropdownDto>> GetTehsilDropdown()
        {

            if (_isRedisCacheEnable)
            {
                var tehsils = _cacheService.Get<List<CacheTehsilDto>?>(CacheKeyConstant.Tehsil);

                if (tehsils == null)
                {
                    var dbDistricts = await _uowTehsil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheTehsilDto>>(dbDistricts);

                    tehsils = _cacheService.Set<List<CacheTehsilDto>?>(CacheKeyConstant.Tehsil, cacheData, null, null);
                }

                return tehsils!.Select(x => new DDTDropdownDto
                {
                    Id = x.TehsilId,
                    Name = x.Name,
                    ParentId = x.DistrictId,
                    Code = x.Code
                }).ToList();
            }
            else
            {
                var tehsils = new List<DDTDropdownDto>();

                if (_isStaticDDEnable)
                    tehsils = LocationStaticDto.GetTehsilDropdowns();

                if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(tehsils) && _isStaticDDEnable)
                    return tehsils;
                else
                {
                    tehsils = await _uowTehsil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .Select(x => new DDTDropdownDto
                        {
                            Id = x.TehsilId,
                            Name = x.Name,
                            ParentId = x.DistrictId,
                            Code = x.Code
                        }).ToListAsync();

                    if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(tehsils) && _isStaticDDEnable)
                        LocationStaticDto.SetTehsilDropdowns(tehsils);

                    return tehsils;

                }
            }

            //if (CacheData.Tehsils.Count() <= 0)
            //{
            //    CacheData.Tehsils = _mapper.Map<List<CacheTehsilDto>>(await _uowTehsil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync());
            //}

            //var tehsils = CacheData.Tehsils.Select(x => new DropdownDto
            //{
            //    Id = x.TehsilId,
            //    Name = x.Name,
            //    ParentId = x.DistrictId,
            //    Code = x.Code
            //}).ToList();

            //return tehsils;
        }
        private void FillEntity(Tehsil obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.TehsilId))
            {
                //obj.TehsilId = Guid.NewGuid();
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
        private void FillEntityDelete(Tehsil obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }


}