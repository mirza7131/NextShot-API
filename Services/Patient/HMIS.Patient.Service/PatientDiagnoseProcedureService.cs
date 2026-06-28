using AppCommonMethods;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.CdcDto;
using HMIS.Patient.Domain.Models.DTO.DentalDto;
using HMIS.Patient.Domain.Models.DTO.MedicineAdvisedDto;
using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;
using HMIS.Patient.Domain.Models.DTO.NursingEventDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseProcedureDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using DbModel = HMIS.Patient.Domain.Models.DbModels;

namespace HMIS.Patient.Service
{
    public class PatientDiagnoseProcedureService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientDiagnoseProcedure> _uowPatientDiagnoseProcedure;
        private readonly NursingEventsService<NursingEvent> _nursingEventsService;
        #endregion

        #region Constructor

        public PatientDiagnoseProcedureService(
            TokenService tokenService,
            UnitOfWork<PatientDiagnoseProcedure> uowPatientDiagnoseProcedure,
            IMapper mapper,
            NursingEventsService<NursingEvent> nursingEventsService
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientDiagnoseProcedure = uowPatientDiagnoseProcedure;
            _nursingEventsService = nursingEventsService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientDiagnoseProcedureDto> CreateOrEdit(CreateOrEditPatientDiagnoseProcedureDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseProcedureId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientDiagnoseProcedureDto> Create(CreateOrEditPatientDiagnoseProcedureDto input)
        {
            var obj = _mapper.Map<PatientDiagnoseProcedure>(input);
            FillEntity(obj);
            PatientDiagnoseProcedure responseObj = await _uowPatientDiagnoseProcedure.Repository.Insert(obj);
            await _uowPatientDiagnoseProcedure.Save();
            
            
            //CreateOrEditNursingEventDto objNursingEvents = new CreateOrEditNursingEventDto();

            //objNursingEvents.PatientId = input.PatientId;
            //objNursingEvents.PatientDiagnoseId = input.PatientDiagnoseId;
            //objNursingEvents.PatientVisitId = input.PatientVisitId;
            //objNursingEvents.Events = CommonPrases.MedicineAdvised;

            //await _nursingEventsService.CreateEvent(objNursingEvents);


            return _mapper.Map<CreateOrEditPatientDiagnoseProcedureDto>(responseObj);

            

        }

        private async Task<CreateOrEditPatientDiagnoseProcedureDto> Update(CreateOrEditPatientDiagnoseProcedureDto input)
        {
            var dbObj = await _uowPatientDiagnoseProcedure.Repository.GetById(input.PatientDiagnoseProcedureId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientDiagnoseProcedure.Repository.Update(obj!);
            await _uowPatientDiagnoseProcedure.CommitAsync();
            return _mapper.Map<CreateOrEditPatientDiagnoseProcedureDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientDiagnoseProcedure.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientDiagnoseProcedure.Repository.Update(dbObj!);
            await _uowPatientDiagnoseProcedure.CommitAsync();
            return true;
        }

        //public async Task<CreateOrEditMedicineAdvisedDto> DiscontinueMedicineAdvised(CreateOrEditMedicineAdvisedDto input)
        //{
        //    var dbObj = await _uowPatientDiagnoseProcedure.Repository.GetById(input.MedicineAdvisedId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    dbObj.IsDiscontinue = input.IsDiscontinue;
        //    //var obj = _mapper.Map(input, dbObj);
        //    FillEntity(dbObj!);
        //    _uowMedicineAdvised.Repository.Update(dbObj!);
        //    await _uowMedicineAdvised.CommitAsync();
        //    return _mapper.Map<CreateOrEditMedicineAdvisedDto>(dbObj);
        //}

        //public async Task<CreateOrEditMedicineAdvisedDto> UpdateMedicineAdvisedDays(CreateOrEditMedicineAdvisedDto input)
        //{
        //    var dbObj = await _uowMedicineAdvised.Repository.GetById(input.MedicineAdvisedId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    dbObj.IsDiscontinue = input.IsDiscontinue;
        //    dbObj.Days = input.Days;
        //    //var obj = _mapper.Map(input, dbObj)
        //    FillEntity(dbObj!);
        //    _uowMedicineAdvised.Repository.Update(dbObj!);
        //    await _uowMedicineAdvised.CommitAsync();
        //    return _mapper.Map<CreateOrEditMedicineAdvisedDto>(dbObj);
        //}

        #endregion

        #region Read Operations

        public async Task<List<ViewPatientDiagnoseProcedureDto>> GetByPatientVisitId(Guid patientVisitId)
        {
            List<PatientDiagnoseProcedure> responseObj = await _uowPatientDiagnoseProcedure.Repository.GetALL(x => x.PatientVisitId == patientVisitId).ToListAsync();
           
            return _mapper.Map<List<ViewPatientDiagnoseProcedureDto>>(responseObj);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(PatientDiagnoseProcedure obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseProcedureId))
            {
                obj.PatientDiagnoseProcedureId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityDelete(PatientDiagnoseProcedure obj)
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
