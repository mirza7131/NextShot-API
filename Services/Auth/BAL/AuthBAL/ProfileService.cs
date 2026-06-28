using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Repositories;
using autoMapper = AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using System.Linq.Expressions;
using AppCommonMethods;
using AuthDAL.Repositories.UOW;
using Microsoft.EntityFrameworkCore;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using Azure.Identity;
using AuthBAL.Common;
using Microsoft.AspNetCore.Mvc;
using CommonDTOs.LocationDTO;
using RedisCache;
using Microsoft.Extensions.Configuration;
using AppCommonMethods.AppConstants;

namespace AuthBAL
{
    public class ProfileService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly autoMapper.IMapper _mapper;
        private UnitOfWork<Profile> _uowProfile;
        private readonly IRedisCacheService _cacheService;
        private readonly bool _isRedisCacheEnable;
        private string[] measlesDiseaseList;
        #endregion

        #region Constructor

        public ProfileService(
            TokenService tokenService, 
            UnitOfWork<Profile> uowProfile, 
            autoMapper.IMapper mapper , 
            IRedisCacheService redisCacheService, 
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowProfile = uowProfile;
            _cacheService = redisCacheService;
            measlesDiseaseList = config.GetSection("HealthFacility").GetSection("MeaslesDiseasList").Get<string[]>() ?? new string[0];
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

        public async Task<CreateOrEditProfileDto> CreateOrEdit(CreateOrEditProfileDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.ProfileId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditProfileDto> Create(CreateOrEditProfileDto input)
        {
            var obj = _mapper.Map<Profile>(input);
            FillEntity(obj);
            Profile responseObj = await _uowProfile.Repository.Insert(obj);
            await _uowProfile.CommitAsync();
            await RefreshProfileCacheList();
            return _mapper.Map<CreateOrEditProfileDto>(responseObj);
        }

        private async Task<CreateOrEditProfileDto> Update(CreateOrEditProfileDto input)
        {
            var dbObj = await _uowProfile.Repository.GetById(input.ProfileId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowProfile.Repository.Update(obj!);
            await _uowProfile.CommitAsync();
            await RefreshProfileCacheList();
            return _mapper.Map<CreateOrEditProfileDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowProfile.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowProfile.Repository.Update(dbObj!);
            await _uowProfile.CommitAsync();
            await RefreshProfileCacheList();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewProfileDto>> GetAll(Expression<Func<Profile, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<Profile> responseObj = await _uowProfile.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewProfileDto>>(responseObj);
        }

        public async Task<ViewPagerDto<ViewProfileDto>> GetAllWithPagination(FilterProfileDto filter)
        {
            var list = _uowProfile.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString) || x.ShortName.ToLower().StartsWith(filter.SearchString) || x.ProfileType.Name.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewProfileDto> IQueryableList = list.Select(x =>
               new ViewProfileDto
               {
                   ProfileId = x.ProfileId,
                   Name = x.Name,
                   ShortName = x.ShortName,
                   ProfileTypeId = x.ProfileTypeId,
                   ProfileTypeName = x.ProfileType.Name,
                   IsActive = x.IsActive,
                   IsDssDisease = x.IsDssDisease
               });

            var pagedList = await PagedListDto<ViewProfileDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewProfileDto>
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

        public async Task<ViewProfileDto> GetById(Guid input)
        {
            Profile? responseObj = await _uowProfile.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewProfileDto>(responseObj);
        }

        public async Task<ViewPagerDto<ViewProfileDto>> GetAllWithProfileType(FilterProfileDto filter)
        {

            var list = _uowProfile.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString) || x.ShortName.ToLower().StartsWith(filter.SearchString) || x.ProfileType.Name.ToLower().StartsWith(filter.SearchString))
                .Include(x => x.ProfileType);

            IQueryable<ViewProfileDto> IQueryableList = list.Select(x =>
                new ViewProfileDto
                {
                    ProfileId = x.ProfileId,
                    Name = x.Name,
                    ShortName = x.ShortName,
                    ProfileTypeId = x.ProfileTypeId,
                    ProfileTypeName = x.ProfileType.Name,
                    IsActive = x.IsActive,
                    IsDssDisease = x.IsDssDisease
                });

            var pagedList = await PagedListDto<ViewProfileDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewProfileDto>
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

        public async Task RefreshProfileCacheList()
        {
                var dbProfiles = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.ProfileType).ToListAsync();

                var cacheData = _mapper.Map<List<CacheProfileDto>>(dbProfiles);

                _cacheService.Set<List<CacheProfileDto>?>(CacheKeyConstant.Profile, cacheData, null, null);
        }

        public async Task<List<CacheProfileDto>> GetProfileByProfileType(string shortName)
        {
            var dbUser = TokenService.GetUserLoggedInfo();
            var isMeasles = false;
            if (!AppCommonMethod.IsNullObject(dbUser))
            {
                if (dbUser!.UserRoleList.Where(x => x.ShortName?.ToLower() == RoleConst.Measles.ToLower()).Count() > 0)
                    isMeasles = true;
            }

            
                
            if (_isRedisCacheEnable)
            {
                var profiles = _cacheService.Get<List<CacheProfileDto>?>(CacheKeyConstant.Profile);

                if (profiles == null)
                {
                    var dbProfiles = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.ProfileType).OrderBy(x => x.SequenceNo).ToListAsync();

                    var cacheData = _mapper.Map<List<CacheProfileDto>>(dbProfiles);

                    profiles = _cacheService.Set<List<CacheProfileDto>?>(CacheKeyConstant.Profile, cacheData, null, null);
                }

                return profiles!.Where(x => x.ProfileType.ShortName == shortName).OrderBy(x => x.SequenceNo).ToList();
            }
            else
            {
               
                var dbProfiles = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.ProfileType).OrderBy(x => x.SequenceNo).ToListAsync();
                
                var cacheData = _mapper.Map<List<CacheProfileDto>>(dbProfiles);

                if(shortName == CommonStringConstant.DiseaseListShortName && isMeasles)
                    cacheData = cacheData!.Where(x => x.ProfileType.ShortName == shortName && measlesDiseaseList.Contains(x.ShortName)).OrderBy(x => x.SequenceNo).ToList();

                return cacheData!.Where(x => x.ProfileType.ShortName == shortName).OrderBy(x => x.SequenceNo).ToList();

            }
            //if (CacheData.Profiles.Count() <= 0)
            //{
            //    var dbProfiles = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.ProfileType).ToListAsync();

            //    if (dbProfiles != null)
            //        CacheData.Profiles = _mapper.Map<List<CacheProfileDto>>(dbProfiles);
            //    else
            //        CacheData.Profiles = new List<CacheProfileDto>();
            //}

            ////var listProfiles = await _uowProfile.Repository.GetALL(x=>x.ProfileType.ShortName == shortName && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
            //var listProfiles = CacheData.Profiles.Where(x => x.ProfileType.ShortName == shortName).ToList();
            //return listProfiles;
        }



        public async Task<List<CacheProfileDto>> GetProfileByShortName(string shortName)
        {
            var profiles = _cacheService.Get<List<CacheProfileDto>?>(CacheKeyConstant.Profile);

            if (profiles == null)
            {

                var dbProfiles = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.ProfileType).ToListAsync();

                var cacheData = _mapper.Map<List<CacheProfileDto>>(dbProfiles);

                profiles = _cacheService.Set<List<CacheProfileDto>?>(CacheKeyConstant.Profile, cacheData, null, null);
            }

            return profiles!.Where(x => x.ShortName == shortName).ToList();
        }



        public async Task<List<LabTest>> GetDataByProfile(string shortName)
        {

            var _uowLabTests = new UnitOfWork<LabTest>(_uowProfile.GetDbContext());
            
            var profiles = _uowProfile.Repository.GetALL(x => x.ShortName == shortName).FirstOrDefault();

            if (profiles != null)
            {
                var labCategories = _uowLabTests.Repository
                    .GetALL(x => x.LabTestCategoryProfileId == profiles.ProfileId)
                    .ToList();
                if (labCategories != null)
                {
                    return labCategories;
                }
            }
            return null;
        }

        #endregion

        #region Helper Methods

        private void FillEntity(Profile obj)
        {
            if (obj.ProfileId == Guid.Empty)
            {
                obj.ProfileId = Guid.NewGuid();
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
        private void FillEntityDelete(Profile obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }

        }

        #endregion
    }
}
