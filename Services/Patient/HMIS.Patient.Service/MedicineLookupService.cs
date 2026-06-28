using System.Linq.Expressions;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineLookupDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using DbModel = HMIS.Patient.Domain.Models.DbModels;

namespace HMIS.Patient.Service
{
    public class MedicineLookupService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<MedicineLookup> _uowMedicineLookup;

        #endregion

        #region Constructor

        public MedicineLookupService(TokenService tokenService, UnitOfWork<MedicineLookup> uowMedicineLookup, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowMedicineLookup = uowMedicineLookup;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditMedicineLookupDto> CreateOrEdit(CreateOrEditMedicineLookupDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.MedicineLookupId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditMedicineLookupDto> Create(CreateOrEditMedicineLookupDto input)
        {
            var obj = _mapper.Map<MedicineLookup>(input);
            FillEntity(obj);
            MedicineLookup responseObj = await _uowMedicineLookup.Repository.Insert(obj);
            await _uowMedicineLookup.Save();
            return _mapper.Map<CreateOrEditMedicineLookupDto>(responseObj);

        }

        private async Task<CreateOrEditMedicineLookupDto> Update(CreateOrEditMedicineLookupDto input)
        {
            var dbObj = await _uowMedicineLookup.Repository.GetById(input.MedicineLookupId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowMedicineLookup.Repository.Update(obj!);
            await _uowMedicineLookup.CommitAsync();
            return _mapper.Map<CreateOrEditMedicineLookupDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowMedicineLookup.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowMedicineLookup.Repository.Update(dbObj!);
            await _uowMedicineLookup.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewMedicineLookupDto>> GetAll(Expression<Func<MedicineLookup, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<MedicineLookup> responseObj = await _uowMedicineLookup.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewMedicineLookupDto>>(responseObj);
        }


        public async Task<ViewMedicineLookupDto> GetById(int input)
        {
            MedicineLookup? responseObj = await _uowMedicineLookup.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewMedicineLookupDto>(responseObj);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(MedicineLookup obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.MedicineLookupId))
            {
                //obj.MedicineLookupId = Guid.NewGuid();
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

        private void FillEntityDelete(MedicineLookup obj)
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
