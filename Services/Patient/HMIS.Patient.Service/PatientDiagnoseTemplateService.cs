using System.Linq.Expressions;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseTemplateDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;

namespace HMIS.Patient.Service
{
    public class PatientDiagnoseTemplateService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientDiagnoseTemplate> _uowPatientDiagnoseTemplate;

        #endregion

        #region Constructor

        public PatientDiagnoseTemplateService(TokenService tokenService, UnitOfWork<PatientDiagnoseTemplate> uowPatientDiagnoseTemplate, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowPatientDiagnoseTemplate = uowPatientDiagnoseTemplate;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientDiagnoseTemplateDto> CreateOrEdit(CreateOrEditPatientDiagnoseTemplateDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseTemplateId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientDiagnoseTemplateDto> Create(CreateOrEditPatientDiagnoseTemplateDto input)
        {
            var obj = _mapper.Map<PatientDiagnoseTemplate>(input);
            FillEntity(obj);
            
            PatientDiagnoseTemplate responseObj = await _uowPatientDiagnoseTemplate.Repository.Insert(obj);
            await _uowPatientDiagnoseTemplate.Save();
            return _mapper.Map<CreateOrEditPatientDiagnoseTemplateDto>(responseObj);
        }

        private async Task<CreateOrEditPatientDiagnoseTemplateDto> Update(CreateOrEditPatientDiagnoseTemplateDto input)
        {
            var dbObj = await _uowPatientDiagnoseTemplate.Repository.GetById(input.PatientDiagnoseTemplateId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientDiagnoseTemplate.Repository.Update(obj!);
            await _uowPatientDiagnoseTemplate.CommitAsync();
            return _mapper.Map<CreateOrEditPatientDiagnoseTemplateDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientDiagnoseTemplate.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientDiagnoseTemplate.Repository.Update(dbObj!);
            await _uowPatientDiagnoseTemplate.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientDiagnoseTemplateDto>> GetAll(Expression<Func<PatientDiagnoseTemplate, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<PatientDiagnoseTemplate> responseObj = await _uowPatientDiagnoseTemplate.Repository.GetALL(filter!).ToListAsync();
            return _mapper.Map<List<ViewPatientDiagnoseTemplateDto>>(responseObj);
        }

        public async Task<ViewPatientDiagnoseTemplateDto> GetById(Guid input)
        {
            PatientDiagnoseTemplate? responseObj = await _uowPatientDiagnoseTemplate.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDiagnoseTemplateDto>(responseObj);
        }

        public async Task<List<ViewPatientDiagnoseTemplateDto>> GetByUserId(Guid input)
        {
            
            List<PatientDiagnoseTemplate> responseObj = await _uowPatientDiagnoseTemplate.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.UserId == input)
                .OrderBy(x => x.Name)
                .ToListAsync();
            return _mapper.Map<List<ViewPatientDiagnoseTemplateDto>>(responseObj);
        }

        

        #endregion

        #region Helper Methods

        private void FillEntity(PatientDiagnoseTemplate obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseTemplateId))
            {
                obj.PatientDiagnoseTemplateId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientDiagnoseTemplate obj)
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
