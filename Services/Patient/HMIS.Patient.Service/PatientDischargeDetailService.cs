using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientAdmissionDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseRecordDto;
using HMIS.Patient.Domain.Models.DTO.PatientDischargeDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Service
{
    public class PatientDischargeDetailService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientDischargeDetail> _uowPatientDischargeDetail;
        private readonly PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;


        #endregion

        #region Constructor

        public PatientDischargeDetailService(TokenService tokenService, UnitOfWork<PatientDischargeDetail> uowPatientDischargeDetail, IMapper mapper, PatientDiagnoseService<PatientDiagnose> patientDiagnoseService)
        {
            _tokenService = tokenService;
            _uowPatientDischargeDetail = uowPatientDischargeDetail;
            _patientDiagnoseService = patientDiagnoseService;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientDischargeDetailDto> CreateOrEdit(CreateOrEditPatientDischargeDetailDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDischargeDetailId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientDischargeDetailDto> Create(CreateOrEditPatientDischargeDetailDto input)
        {
            var obj = _mapper.Map<PatientDischargeDetail>(input);
            FillEntity(obj);
            PatientDischargeDetail responseObj = await _uowPatientDischargeDetail.Repository.Insert(obj);
            await _uowPatientDischargeDetail.Save();
            return _mapper.Map<CreateOrEditPatientDischargeDetailDto>(responseObj);
        }

        private async Task<CreateOrEditPatientDischargeDetailDto> Update(CreateOrEditPatientDischargeDetailDto input)
        {
            var dbObj = await _uowPatientDischargeDetail.Repository.GetById(input.PatientDischargeDetailId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientDischargeDetail.Repository.Update(obj!);
            await _uowPatientDischargeDetail.CommitAsync();
            return _mapper.Map<CreateOrEditPatientDischargeDetailDto>(obj);
        }

        //public async Task<CreateOrEditPatientDischargeDetailDto> CreatePatientDischargeUpdateVisit(PatientDischargeDto input)
        //{
        //    // mark if diagnose Type when Patient Discharge
        //    input.PatientDiagnose.IsDischargeDiagnose = true;
        //    var response = await _patientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescriptionForIpd(input.PatientDiagnose);

        //    var obj = _mapper.Map<PatientDischargeDetail>(input);

        //    FillEntity(obj);
        //    PatientDischargeDetail responseObj = await _uowPatientDischargeDetail.Repository.Insert(obj);
        //    await _uowPatientDischargeDetail.Save();
        //    return _mapper.Map<CreateOrEditPatientDischargeDetailDto>(responseObj);
        //}

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientDischargeDetail.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientDischargeDetail.Repository.Update(dbObj!);
            await _uowPatientDischargeDetail.CommitAsync();
            return true;
        }

        public async Task<PatientDischargeDto> DischargePatientVisit(PatientDischargeDto input)
        {
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientDischargeDetail.GetDbContext());
            var dbObj = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //if (!AppCommonMethod.IsNullObject(dbObj) && dbObj!.IsDischarge == true)
            //    throw new UserFriendlyException(CommonMessageConstant.VisitClosed);

            FillEntityForDischargePatients(dbObj!);

            if (!AppCommonMethod.IsNullObject(input.PatientDiagnose))
            {
                input.PatientDiagnose.IsDischargeDiagnose = true;
                var response = await _patientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescriptionForIpd(input.PatientDiagnose);

                if (!AppCommonMethod.IsNullObject(response))
                {
                    input.PatientDischargeDetails.DischargePatientDiagnoseId = response.PatientDiagnoseId;
                    await CreateOrEdit(input.PatientDischargeDetails);
                }

                //if (!AppCommonMethod.IsNullObject(response))
                //{
                //    var _uowPatientAdmissionDetail = new UnitOfWork<PatientAdmissionDetail>(_uowPatientDischargeDetail.GetDbContext());
                //    var dbObjAdmission = await _uowPatientAdmissionDetail.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.IsDischarge != true)
                //        .FirstOrDefaultAsync();

                //    var obj = _mapper.Map<CreateOrEditPatientAdmissionDetailDto>(dbObjAdmission);
                //    obj.IsDischarge = true;

                //    var dbAdmissionObj = _mapper.Map<PatientAdmissionDetail>(obj);
                //    FillEntityForAdmission(dbAdmissionObj);

                //    if (dbAdmissionObj != null)
                //    {
                //        dbAdmissionObj.IsDischarge = true;
                //        _uowPatientAdmissionDetail.Repository.Update(dbAdmissionObj!);
                //        await _uowPatientAdmissionDetail.CommitAsync();
                //    }
                //}
            }
            dbObj.IsDischarge = true;
            _uowPatientOpenVisit.Repository.Update(dbObj!);
            await _uowPatientOpenVisit.CommitAsync();

            return input;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientDischargeDetailDto>> GetAll(Expression<Func<PatientDischargeDetail, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<PatientDischargeDetail> responseObj = await _uowPatientDischargeDetail.Repository.GetALL().ToListAsync();
            return _mapper.Map<List<ViewPatientDischargeDetailDto>>(responseObj);
        }

        public async Task<ViewPatientDischargeDetailDto> GetById(Guid input)
        {
            PatientDischargeDetail? responseObj = await _uowPatientDischargeDetail.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDischargeDetailDto>(responseObj);
        }

        public async Task<PatientDischargeDetailDto> GetDischargeDetailByVisitId(Guid? VisitId)
        {
            var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDischargeDetail.GetDbContext());
            var patientDischargeDetailDto = new PatientDischargeDetail();

            var responseObj = await _uowPatientDischargeDetail.Repository
                .GetALL(x => x.PatientVisitId == VisitId)
                .Include(x => x.DischargeStatusProfile)
                .Include(x => x.PatientVisit)
                .Include(x => x.PatientVisit).ThenInclude(x => x.DepartementLookup)
                .Include(x => x.PatientVisit).ThenInclude(x => x.SectionLookup)
                .Select(x => new PatientDischargeDetailDto
                {
                    PatientId = x.PatientId,
                    PatientVisitId = x.PatientVisitId,
                    DischargeStatusProfileName = x.DischargeStatusProfile!.Name,
                    DepartmentName = x.PatientVisit.DepartementLookup!.Name,
                    SectionName = x.PatientVisit.SectionLookup!.Name,
                    PatientDischargeDetails = _mapper.Map<ViewPatientDischargeDetailDto>(x)
                })
                .FirstOrDefaultAsync();

            if(!AppCommonMethod.IsNullObject(responseObj))
            {
                var diagnoseRecord = _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == responseObj.PatientDischargeDetails.DischargePatientDiagnoseId).FirstOrDefault();

                responseObj!.PatientDiagnose = _mapper.Map<ViewPatientDiagnoseRecordDto>(diagnoseRecord);
            }
            
            return _mapper.Map<PatientDischargeDetailDto>(responseObj);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(PatientDischargeDetail obj)
        {
            if (obj.PatientDischargeDetailId == Guid.Empty)
            {
                obj.PatientDischargeDetailId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientDischargeDetail obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }

        private void FillEntityForDischargePatients(PatientOpenVisit obj)
        {
            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        }

        private void FillEntityForAdmission(PatientAdmissionDetail obj)
        {
            if (obj.PatientAdmissionDetailId == Guid.Empty)
            {
                obj.PatientAdmissionDetailId = Guid.NewGuid();
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

        #endregion
    }
}
