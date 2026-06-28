using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DivisionDto;
using AuthDAL.Models.Dto.LocationDto;
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
using RedisCache;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class DivisionService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Division> _uowDivision;
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private readonly bool _isStaticDDEnable;
        
        #endregion

        #region Constructor

        public DivisionService(
            TokenService tokenService, 
            UnitOfWork<Division> uowDivision,
            IMapper mapper, IRedisCacheService cacheService,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowDivision = uowDivision;
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

        public async Task<CreateOrEditDivisionDto> CreateOrEdit(CreateOrEditDivisionDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.DivisionId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditDivisionDto> Create(CreateOrEditDivisionDto input)
        {
            var obj = _mapper.Map<Division>(input);
            FillEntity(obj);
            Division responseObj = await _uowDivision.Repository.Insert(obj);
            await _uowDivision.CommitAsync();
            //await RefreshDivisionCacheList();
            if (_isStaticDDEnable)
                await RefreshDivisionStaticCacheList();
            return _mapper.Map<CreateOrEditDivisionDto>(responseObj);
        }

        private async Task<CreateOrEditDivisionDto> Update(CreateOrEditDivisionDto input)
        {
            var dbObj = await _uowDivision.Repository.GetById(input.DivisionId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowDivision.Repository.Update(obj!);
            await _uowDivision.CommitAsync();
            //await RefreshDivisionCacheList();
            if (_isStaticDDEnable)
                await RefreshDivisionStaticCacheList();
            return _mapper.Map<CreateOrEditDivisionDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowDivision.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowDivision.Repository.Update(dbObj!);
            await _uowDivision.CommitAsync();
            //await RefreshDivisionCacheList();
            if (_isStaticDDEnable)
                await RefreshDivisionStaticCacheList();

            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewDivisionDto>> GetAll(Expression<Func<Division, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<Division> responseObj = await _uowDivision.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewDivisionDto>>(responseObj);
        }


        public async Task<ViewDivisionDto> GetById(int input)
        {
            Division? responseObj = await _uowDivision.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewDivisionDto>(responseObj);
        }

        public async Task<List<ViewDivisionDto>> GetAllByProvinceId(int ProvinceId)
        {
            List<Division> responseObj = await _uowDivision.Repository.GetALL(x => x.ProvinceId == ProvinceId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).ToListAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<List<ViewDivisionDto>>(responseObj);
        }

        public async Task RefreshDivisionCacheList()
        {

            //var listDivisions = await _uowDivision.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //CacheData.Provinces = _mapper.Map<List<CacheProvinceDto>>(listDivisions);

            var listDivisions = await _uowDivision.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            var divisions = _mapper.Map<List<CacheDivisionDto>>(listDivisions);

            _cacheService.Set<List<CacheDivisionDto>?>(CacheKeyConstant.Province, divisions, null, null);
        }

        public async Task RefreshDivisionStaticCacheList()
        {

            var dbDivisions = await _uowDivision.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                         .Select(x => new DDTDropdownDto
                         {
                             Id = x.DivisionId,
                             Name = x.Name,
                             ParentId = x.ProvinceId,
                             Code = x.Code
                         }).ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(dbDivisions) && _isStaticDDEnable)
                LocationStaticDto.SetDivisionDropdowns(dbDivisions);
        }

        #endregion

        #region Helper Methods

        public async Task<List<DDTDropdownDto>> GetDivisionDropdown()
        {
            if (_isRedisCacheEnable)
            {
                var divisions = _cacheService.Get<List<CacheDivisionDto>?>(CacheKeyConstant.Division);

                if (divisions == null)
                {
                    var dbDivisions = await _uowDivision.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheDivisionDto>>(dbDivisions);

                    divisions = _cacheService.Set<List<CacheDivisionDto>?>(CacheKeyConstant.Division, cacheData, null, null);
                }

                return divisions!.Select(x => new DDTDropdownDto
                {
                    Id = x.DivisionId,
                    Name = x.Name,
                    ParentId = x.ProvinceId,
                    Code = x.Code
                }).ToList();
            }
            else
            {
                var dbDivisions = new List<DDTDropdownDto>();

                if (_isStaticDDEnable)
                    dbDivisions = LocationStaticDto.GetDivisionDropdowns();

                if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(dbDivisions) && _isStaticDDEnable)
                    return dbDivisions;
                else
                {
                    dbDivisions = await _uowDivision.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                        .Select(x => new DDTDropdownDto
                        {
                            Id = x.DivisionId,
                            Name = x.Name,
                            ParentId = x.ProvinceId,
                            Code = x.Code
                        }).ToListAsync();

                    if (!AppCommonMethod.IsNullOrEmptyList<DDTDropdownDto>(dbDivisions) && _isStaticDDEnable)
                        LocationStaticDto.SetDivisionDropdowns(dbDivisions);

                    return dbDivisions;
                }
            }

            //if (CacheData.Divisions.Count() <= 0)
            //{
            //    CacheData.Divisions = _mapper.Map<List<CacheDivisionDto>>(await _uowDivision.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync());
            //}

            //var divisionis = CacheData.Divisions.Select(x => new DropdownDto
            //{
            //    Id = x.DivisionId,
            //    Name = x.Name,
            //    ParentId = x.ProvinceId,
            //    Code = x.Code
            //}).ToList();

            //return divisionis;

        }

        private void FillEntity(Division obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.DivisionId))
            {
                //obj.DivisionId = Guid.NewGuid();
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
        private void FillEntityDelete(Division obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
