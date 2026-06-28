using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityCategoryDto;
using AuthDAL.Models.Dto.HealthFacilityTypeDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class HealthFacilityTypeService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HealthFacilityType> _uowHealthFacilityType;

        #endregion

        #region Constructor

        public HealthFacilityTypeService(TokenService tokenService, UnitOfWork<HealthFacilityType> uowHealthFacilityType, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHealthFacilityType = uowHealthFacilityType;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHealthFacilityTypeDto> CreateOrEdit(CreateOrEditHealthFacilityTypeDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityTypeId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHealthFacilityTypeDto> Create(CreateOrEditHealthFacilityTypeDto input)
        {
            var obj = _mapper.Map<HealthFacilityType>(input);
            FillEntity(obj);
            HealthFacilityType responseObj = await _uowHealthFacilityType.Repository.Insert(obj);
            await _uowHealthFacilityType.CommitAsync();
            return _mapper.Map<CreateOrEditHealthFacilityTypeDto>(responseObj);
        }

        private async Task<CreateOrEditHealthFacilityTypeDto> Update(CreateOrEditHealthFacilityTypeDto input)
        {
            var dbObj = await _uowHealthFacilityType.Repository.GetById(input.HealthFacilityTypeId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowHealthFacilityType.Repository.Update(obj!);
            await _uowHealthFacilityType.CommitAsync();

            return _mapper.Map<CreateOrEditHealthFacilityTypeDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowHealthFacilityType.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowHealthFacilityType.Repository.Update(dbObj!);
            await _uowHealthFacilityType.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewHealthFacilityTypeDto>> GetAll(Expression<Func<HealthFacilityType, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<HealthFacilityType> responseObj = await _uowHealthFacilityType.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewHealthFacilityTypeDto>>(responseObj);
        }

        public async Task<ViewHealthFacilityTypeDto> GetById(int input)
        {
            HealthFacilityType? responseObj = await _uowHealthFacilityType.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHealthFacilityTypeDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(HealthFacilityType obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.HealthFacilityTypeId))
            {
                //obj.HealthFacilityTypeId = Guid.NewGuid();
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
        private void FillEntityDelete(HealthFacilityType obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
