using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.LocationDto;
using AuthDAL.Models.Dto.UnionCouncilDto;
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
    public class UnionCouncilService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<UnionCouncil> _uowUnionCouncil;
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        #endregion

        #region Constructor

        public UnionCouncilService(
            TokenService tokenService, 
            UnitOfWork<UnionCouncil> uowUnionCouncil, 
            IMapper mapper, 
            IRedisCacheService cacheService, 
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowUnionCouncil = uowUnionCouncil;
            _cacheService = cacheService;
            _isRedisCacheEnable = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                                config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActiveRedisCache") :
                                (
                                config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                 config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActiveRedisCache") :
                                 false
                                );
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditUnionCouncilDto> CreateOrEdit(CreateOrEditUnionCouncilDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.UnionCouncilId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditUnionCouncilDto> Create(CreateOrEditUnionCouncilDto input)
        {
            var obj = _mapper.Map<UnionCouncil>(input);
            FillEntity(obj);
            UnionCouncil responseObj = await _uowUnionCouncil.Repository.Insert(obj);
            await _uowUnionCouncil.CommitAsync();
            RefreshUnionCouncilCacheList();
            return _mapper.Map<CreateOrEditUnionCouncilDto>(responseObj);
        }

        private async Task<CreateOrEditUnionCouncilDto> Update(CreateOrEditUnionCouncilDto input)
        {
            var dbObj = await _uowUnionCouncil.Repository.GetById(input.UnionCouncilId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowUnionCouncil.Repository.Update(obj!);
            await _uowUnionCouncil.CommitAsync();
            RefreshUnionCouncilCacheList();
            return _mapper.Map<CreateOrEditUnionCouncilDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowUnionCouncil.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowUnionCouncil.Repository.Update(dbObj!);
            await _uowUnionCouncil.CommitAsync();
            RefreshUnionCouncilCacheList();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewUnionCouncilDto>> GetAll(Expression<Func<UnionCouncil, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<UnionCouncil> responseObj = await _uowUnionCouncil.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewUnionCouncilDto>>(responseObj);
        }

        public async Task<List<ViewUcDto>> GetAllUcs()
        {
            var _uc = new UnitOfWork<Uc>(_uowUnionCouncil.GetDbContext());
            List<Uc> responseObj = await _uc.Repository.GetALL().ToListAsync();
            return _mapper.Map<List<ViewUcDto>>(responseObj);
        }

        public async Task<ViewUnionCouncilDto> GetById(int input)
        {
            UnionCouncil? responseObj = await _uowUnionCouncil.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewUnionCouncilDto>(responseObj);
        }

        public async Task<List<ViewUnionCouncilDto>> GetAllByTehsilId(int TehsilId)
        {
            List<UnionCouncil> responseObj = await _uowUnionCouncil.Repository.GetALL(x => x.TehsilId == TehsilId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).ToListAsync();
            return _mapper.Map<List<ViewUnionCouncilDto>>(responseObj);
        }

        public async void RefreshUnionCouncilCacheList()
        {
            //var listUnioCouncils = await _uowUnionCouncil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //CacheData.UnionCouncils = _mapper.Map<List<CacheUnionCouncilDto>>(listUnioCouncils);

            var listUnionCouncils = await _uowUnionCouncil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            var unionCouncils = _mapper.Map<List<CacheUnionCouncilDto>>(listUnionCouncils);

            _cacheService.Set<List<CacheUnionCouncilDto>?>(CacheKeyConstant.UnionCouncil, unionCouncils, null, null);
        }

        #endregion

        #region Helper Methods

        public async Task<List<DropdownDto>> GetUnionCouncilDropdown()
        {
            if (_isRedisCacheEnable)
            {
                var unionCouncils = _cacheService.Get<List<CacheUnionCouncilDto>?>(CacheKeyConstant.UnionCouncil);

                if (unionCouncils == null)
                {
                    var dbUnionCouncils = await _uowUnionCouncil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheUnionCouncilDto>>(dbUnionCouncils);

                    unionCouncils = _cacheService.Set<List<CacheUnionCouncilDto>?>(CacheKeyConstant.UnionCouncil, cacheData, null, null);
                }

                return unionCouncils!.Select(x => new DropdownDto
                {
                    Id = x.UnionCouncilId,
                    Name = x.Name,
                    ParentId = x.TehsilId,
                    Code = x.Code
                }).ToList();
            }
            else
            {
                var dbUnionCouncils = await _uowUnionCouncil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                return dbUnionCouncils!.Select(x => new DropdownDto
                {
                    Id = x.UnionCouncilId,
                    Name = x.Name,
                    ParentId = x.TehsilId,
                    Code = x.Code
                }).ToList();

            }

            //if (CacheData.UnionCouncils.Count() <= 0)
            //{
            //    CacheData.UnionCouncils = _mapper.Map<List<CacheUnionCouncilDto>>(await _uowUnionCouncil.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync());
            //}

            //var unionCouncils = CacheData.UnionCouncils.Select(x => new DropdownDto
            //{
            //    Id = x.UnionCouncilId,
            //    Name = x.Name,
            //    ParentId = x.TehsilId,
            //    Code = x.Code
            //}).ToList();

            //return unionCouncils;
        }

        private void FillEntity(UnionCouncil obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.UnionCouncilId))
            {
                //obj.UnionCouncilId = Guid.NewGuid();
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
        private void FillEntityDelete(UnionCouncil obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
