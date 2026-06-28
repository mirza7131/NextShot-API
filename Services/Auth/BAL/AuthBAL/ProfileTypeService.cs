using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class ProfileTypeService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<ProfileType> _uowProfileType;

        #endregion

        #region Constructor

        public ProfileTypeService(TokenService tokenService,UnitOfWork<ProfileType> uowProfileType, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowProfileType = uowProfileType;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditProfileTypeDto> CreateOrEdit(CreateOrEditProfileTypeDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.ProfileTypeId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditProfileTypeDto> Create(CreateOrEditProfileTypeDto input)
        {
            var obj = _mapper.Map<ProfileType>(input);
            FillEntity(obj);
            ProfileType responseObj = await _uowProfileType.Repository.Insert(obj);
            await _uowProfileType.CommitAsync();
            return _mapper.Map<CreateOrEditProfileTypeDto>(responseObj);
        }

        private async Task<CreateOrEditProfileTypeDto> Update(CreateOrEditProfileTypeDto input)
        {
            var dbObj = await _uowProfileType.Repository.GetById(input.ProfileTypeId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowProfileType.Repository.Update(obj!);
            await _uowProfileType.CommitAsync();
            
            return _mapper.Map<CreateOrEditProfileTypeDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowProfileType.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowProfileType.Repository.Update(dbObj!);
            await _uowProfileType.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewProfileTypeDto>> GetAll(Expression<Func<ProfileType, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<ProfileType> responseObj = await _uowProfileType.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewProfileTypeDto>>(responseObj);
        }

        
        public async Task<ViewPagerDto<ViewProfileTypeDto>> GetAllWithPagination(FilterProfileTypeDto filter)
        {
            var list = _uowProfileType.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString) || x.ShortName.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewProfileTypeDto> IQueryableList = list.Select(x =>
                new ViewProfileTypeDto
                {
                    ProfileTypeId = x.ProfileTypeId,
                    Name = x.Name,
                    ShortName = x.ShortName,
                    IsActive =  x.IsActive
                });

            var pagedList = await PagedListDto<ViewProfileTypeDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewProfileTypeDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewProfileTypeDto>>(pagedList)
            };

            return responseObject;
        }

        public async Task<ViewProfileTypeDto> GetById(Guid input)
        {
            ProfileType? responseObj = await _uowProfileType.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewProfileTypeDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(ProfileType obj)
        {
            if (obj.ProfileTypeId == Guid.Empty)
            {
                obj.ProfileTypeId = Guid.NewGuid();
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
        private void FillEntityDelete(ProfileType obj)
        {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
