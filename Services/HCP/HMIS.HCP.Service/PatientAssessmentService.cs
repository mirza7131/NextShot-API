using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.HCP.Domain.Models.DTO.PatientDiagnoseDtos;
using HMIS.HCP.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Service
{
    public class PatientAssessmentService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientAssessment> _uowPatientAssessment;
        private UnitOfWork<PatientDiagnose> _uowPatientDiagnose;

        #endregion

        #region Constructor

        public PatientAssessmentService(TokenService tokenService, UnitOfWork<PatientAssessment> uowPatientAssessment, UnitOfWork<PatientDiagnose> uowPatientDiagnose, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientAssessment = uowPatientAssessment;
            _uowPatientDiagnose = uowPatientDiagnose;
        }

        #endregion

        //#region CUD Operations

        //public async Task<CreateOrEditPatientAssessmentDto> CreateOrEdit(CreateOrEditPatientAssessmentDto input)
        //{
        //    if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientAssessmentId))
        //        return await Create(input);
        //    else
        //        return await Update(input);
        //}

        //private async Task<CreateOrEditPatientAssessmentDto> Create(CreateOrEditPatientAssessmentDto input)
        //{
        //    var obj = _mapper.Map<PatientAssessment>(input);
        //    FillEntity(obj);
        //    PatientAssessment responseObj = await _uowPatientAssessment.Repository.Insert(obj);
        //    await _uowPatientAssessment.CommitAsync();
        //    return _mapper.Map<CreateOrEditPatientAssessmentDto>(responseObj);
        //}

        //private async Task<CreateOrEditPatientAssessmentDto> Update(CreateOrEditPatientAssessmentDto input)
        //{
        //    var dbObj = await _uowPatientAssessment.Repository.GetById(input.PatientAssessmentId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    var obj = _mapper.Map(input, dbObj);
        //    FillEntity(obj!);

        //    _uowPatientAssessment.Repository.Update(obj!);
        //    await _uowPatientAssessment.CommitAsync();

        //    return _mapper.Map<CreateOrEditPatientAssessmentDto>(obj);
        //}

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowPatientAssessment.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowPatientAssessment.Repository.Update(dbObj!);
        //    await _uowPatientAssessment.CommitAsync();
        //    return true;
        //}


        //#endregion

        //#region Helper Methods

        //private void FillEntity(PatientAssessment obj)
        //{
        //    if (obj.PatientAssessmentId == Guid.Empty)
        //    {
        //        obj.PatientAssessmentId = Guid.NewGuid();
        //        obj.CreatedBy = _tokenService.GetUserId();
        //        obj.CreatedOn = DateTime.Now;
        //        obj.ActionTypeId = (int)ActionTypeEnum.Create;
        //    }
        //    else
        //    {
        //        obj.UpdatedBy = _tokenService.GetUserId();
        //        obj.UpdatedOn = DateTime.Now;
        //        obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        //    }
        //}
        //private void FillEntityDelete(PatientAssessment obj)
        //{
        //    obj.DeletedBy = _tokenService.GetUserId();
        //    obj.DeletedOn = DateTime.Now;
        //    obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        //}

        //#endregion

        public async Task<PatientAssessment> GetPatientPreviousAssessment(Guid input)
        {
            //PatientScreening? responseObj = await _uowPatientScreening.Repository.GetById(input);
            var responseObj = await _uowPatientAssessment.Repository.GetALL(x => x.PatientId == input && (
            x.ActionTypeId == 1 || x.ActionTypeId == 2)).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<PatientAssessment>(responseObj);
        }
        // Check this API for PRevious Followup
        public async Task<PatientDiagnose> GetPatientLastFollowupDate(Guid input)
        {
            var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientAssessment.GetDbContext());
            //PatientScreening? responseObj = await _uowPatientScreening.Repository.GetById(input);
            var responseObj = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == input && x.FormType == CommonStringConstant.HCPForm && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<PatientDiagnose>(responseObj);
        }
        public async Task<List<PatientDiagnose>> GetPatientPreviousDiagnose(Guid input)
        {
            //var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientDiagnose.GetDbContext());
            //PatientScreening? responseObj = await _uowPatientScreening.Repository.GetById(input);
            var responseObj = _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == input && x.FormType == CommonStringConstant.HCPForm && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).ToList();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //return _mapper.Map<PatientDiagnose>(responseObj);
            return responseObj;
        }
        public async Task<List<PatientDiagnose>> GetPatientPreviousDiagnoseByScreeningDate(PatientPreviousDiagnoseDto input)
        {
            var responseObj = _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == input.PatientId && x.FormType == CommonStringConstant.HCPForm && (x.ActionTypeId == 1 || x.ActionTypeId == 2) && x.CreatedOn >= input.PreviousScreeningDate).ToList();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //return _mapper.Map<PatientDiagnose>(responseObj);
            return responseObj;
        }
    }
}
