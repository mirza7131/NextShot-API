using System.Linq.Expressions;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineLookupDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.SectionProcedureDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using DbModel = HMIS.Patient.Domain.Models.DbModels;

namespace HMIS.Patient.Service
{
    public class SectionProcedureService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<SectionProcedure> _uowSectionProcedure;

        #endregion

        #region Constructor

        public SectionProcedureService(TokenService tokenService, UnitOfWork<SectionProcedure> uowSectionProcedure, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowSectionProcedure = uowSectionProcedure;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditSectionProcedureDto> CreateOrEdit(CreateOrEditSectionProcedureDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.SectionProcedureId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditSectionProcedureDto> Create(CreateOrEditSectionProcedureDto input)
        {
            var obj = _mapper.Map<SectionProcedure>(input);
            FillEntity(obj);
            SectionProcedure responseObj = await _uowSectionProcedure.Repository.Insert(obj);
            await _uowSectionProcedure.Save();
            return _mapper.Map<CreateOrEditSectionProcedureDto>(responseObj);

        }

        private async Task<CreateOrEditSectionProcedureDto> Update(CreateOrEditSectionProcedureDto input)
        {
            var dbObj = await _uowSectionProcedure.Repository.GetById(input.SectionProcedureId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowSectionProcedure.Repository.Update(obj!);
            await _uowSectionProcedure.CommitAsync();
            return _mapper.Map<CreateOrEditSectionProcedureDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowSectionProcedure.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowSectionProcedure.Repository.Update(dbObj!);
            await _uowSectionProcedure.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewSectionProcedureDto>> GetAll(Expression<Func<SectionProcedure, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<SectionProcedure> responseObj = await _uowSectionProcedure.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewSectionProcedureDto>>(responseObj);
        }


        public async Task<ViewSectionProcedureDto> GetById(int input)
        {
            SectionProcedure? responseObj = await _uowSectionProcedure.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewSectionProcedureDto>(responseObj);
        }

        public async Task<List<ViewSectionProcedureDto>> GetBySectionLookupId(int input)
        {
            List<SectionProcedure> responseObj = await _uowSectionProcedure.Repository.GetALL(x=>x.SectionLookupId == input).ToListAsync();

            return _mapper.Map<List<ViewSectionProcedureDto>>(responseObj);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(SectionProcedure obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.SectionProcedureId))
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

        private void FillEntityDelete(SectionProcedure obj)
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
