using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityCategoryDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class HealthFacilityCategoryService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HealthFacilityCategory> _uowHealthFacilityCategory;

        #endregion

        #region Constructor

        public HealthFacilityCategoryService(TokenService tokenService, UnitOfWork<HealthFacilityCategory> uowHealthFacilityCategory, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHealthFacilityCategory = uowHealthFacilityCategory;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHealthFacilityCategoryDto> CreateOrEdit(CreateOrEditHealthFacilityCategoryDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityCategoryId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHealthFacilityCategoryDto> Create(CreateOrEditHealthFacilityCategoryDto input)
        {
            var obj = _mapper.Map<HealthFacilityCategory>(input);
            FillEntity(obj);
            HealthFacilityCategory responseObj = await _uowHealthFacilityCategory.Repository.Insert(obj);
            await _uowHealthFacilityCategory.CommitAsync();
            return _mapper.Map<CreateOrEditHealthFacilityCategoryDto>(responseObj);
        }

        private async Task<CreateOrEditHealthFacilityCategoryDto> Update(CreateOrEditHealthFacilityCategoryDto input)
        {
            var dbObj = await _uowHealthFacilityCategory.Repository.GetById(input.HealthFacilityCategoryId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowHealthFacilityCategory.Repository.Update(obj!);
            await _uowHealthFacilityCategory.CommitAsync();

            return _mapper.Map<CreateOrEditHealthFacilityCategoryDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowHealthFacilityCategory.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowHealthFacilityCategory.Repository.Update(dbObj!);
            await _uowHealthFacilityCategory.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewHealthFacilityCategoryDto>> GetAll(Expression<Func<HealthFacilityCategory, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<HealthFacilityCategory> responseObj = await _uowHealthFacilityCategory.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewHealthFacilityCategoryDto>>(responseObj);
        }

        public async Task<ViewHealthFacilityCategoryDto> GetById(int input)
        {
            HealthFacilityCategory? responseObj = await _uowHealthFacilityCategory.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHealthFacilityCategoryDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(HealthFacilityCategory obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.HealthFacilityCategoryId))
            {
                //obj.HealthFacilityCategoryId = Guid.NewGuid();
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
        private void FillEntityDelete(HealthFacilityCategory obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
