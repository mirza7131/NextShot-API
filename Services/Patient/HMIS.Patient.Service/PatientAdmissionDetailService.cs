using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientAdmissionDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientAssessmentDto;
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
    public class PatientAdmissionDetailService<TEntity> where TEntity : class
    {

        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientAdmissionDetail> _uowPatientAdmissionDetail;

        #endregion

        #region Constructor

        public PatientAdmissionDetailService(TokenService tokenService, UnitOfWork<PatientAdmissionDetail> uowPatientAdmissionDetail, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowPatientAdmissionDetail = uowPatientAdmissionDetail;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientAdmissionDetailDto> CreateOrEdit(CreateOrEditPatientAdmissionDetailDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientAdmissionDetailId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientAdmissionDetailDto> Create(CreateOrEditPatientAdmissionDetailDto input)
        {
            if (input.ShiftBack == true)
            {
                var _uowPatientAdmissionDetailLog = new UnitOfWork<PatientAdmissionDetailLog>(_uowPatientAdmissionDetail.GetDbContext());

                 var logDbObj = _uowPatientAdmissionDetailLog.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).OrderBy(x => x.CreatedOn).FirstOrDefault();

                input.DepartmentLookupId = logDbObj?.ShiftedFromDepartmentLookupId;
                input.SectionLookupId = logDbObj?.ShiftedFromSectionLookupId;

            }
            
            var obj = _mapper.Map<PatientAdmissionDetail>(input);
            FillEntity(obj);
            

            PatientAdmissionDetail responseObj = await _uowPatientAdmissionDetail.Repository.Insert(obj);


            await _uowPatientAdmissionDetail.Save();
            UpdatePatientVisit(input);

            return _mapper.Map<CreateOrEditPatientAdmissionDetailDto>(responseObj);
        }

        private async Task<CreateOrEditPatientAdmissionDetailDto> Update(CreateOrEditPatientAdmissionDetailDto input)
        {
            var dbObj = await _uowPatientAdmissionDetail.Repository.GetById(input.PatientAdmissionDetailId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientAdmissionDetail.Repository.Update(obj!);
            UpdatePatientVisit(input);
            await _uowPatientAdmissionDetail.CommitAsync();
            
            return _mapper.Map<CreateOrEditPatientAdmissionDetailDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientAdmissionDetail.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientAdmissionDetail.Repository.Update(dbObj!);
            await _uowPatientAdmissionDetail.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientAdmissionDetailDto>> GetAll(Expression<Func<PatientAdmissionDetail, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<PatientAdmissionDetail> responseObj = await _uowPatientAdmissionDetail.Repository.GetALL().ToListAsync();
            return _mapper.Map<List<ViewPatientAdmissionDetailDto>>(responseObj);
        }

        public async Task<ViewPatientAdmissionDetailDto> GetById(Guid input)
        {
            PatientAdmissionDetail? responseObj = await _uowPatientAdmissionDetail.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientAdmissionDetailDto>(responseObj);
        }

        public async Task<PatientAdmissionSlipDto> GetAdmissionDetailByVisitId(Guid? VisitId)
        {
            var responseObj = await _uowPatientAdmissionDetail.Repository
                .GetALL(x => x.PatientVisitId == VisitId)
                .Include(x => x.DepartmentLookup)
                .Include(x => x.SectionLookup)
                .Include(x => x.Patient)
                .Include(x => x.PatientVisit)
                .Include(x => x.PatientVisit).ThenInclude(x => x.HealthFacility)
                .Include(x => x.PatientVisit).ThenInclude(x => x.IpdReferredByDepartmentLookup)
                .Include(x => x.PatientVisit).ThenInclude(x => x.IpdReferredBySectionLookup)
                .Include(x => x.PatientVisit).ThenInclude(x => x.CreatedByNavigation)
                .Select(x => new PatientAdmissionSlipDto
                {
                    PatientId = x.PatientId,
                    PatientVisitId = x.PatientVisitId,
                    Mrno = x.Patient.Mrno,
                    MobileNo = x.Patient.MobileNo,
                    CNIC = x.Patient.Cnic,
                    TokenNo = x.PatientVisit.TokenNo,
                    CreatedByName = x.PatientVisit.CreatedByNavigation!.FullName,
                    CreatedOn = x.PatientVisit.CreatedOn,
                    FirstName = x.Patient.FirstName,
                    LastName = x.Patient.LastName,
                    FullName = x.Patient.FullName,
                    Age = x.Patient!.Age,
                    Dob = x.Patient!.Dob,
                    Gender = x.Patient.GenderProfile!.Name,
                    PermanentAddress = x.Patient.ParmanentAddress,
                    BedNo = x.PatientVisit.BedNo,
                    HealthFacilityName = x.PatientVisit.HealthFacility!.Name,
                    VisitDate = x.PatientVisit!.VisitDate,
                    AdmissionDate = x.CreatedOn,
                    IsReferredIpd = x.PatientVisit.IsReferredIpd,
                    IpdDepartmentLookupName = x.DepartmentLookup!.Name,
                    IpdSectionLookupName = x.SectionLookup!.Name,
                    IpdReferredByDepartmentLookupName = x.PatientVisit!.IpdReferredByDepartmentLookup!.Name,
                    IpdReferredBySectionLookupName = x.PatientVisit!.IpdReferredBySectionLookup!.Name,
                    IpdReferredByName = x.PatientVisit!.IpdReferredByNavigation!.FullName,

                }).FirstOrDefaultAsync();

            return _mapper.Map<PatientAdmissionSlipDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private async void UpdatePatientVisit(CreateOrEditPatientAdmissionDetailDto input) {
            
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>();

            var dbObjVisit = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input.PatientVisitId).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(dbObjVisit))
                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

            dbObjVisit.IsReferred = true;
            dbObjVisit.ReferredHealthFacilityId = dbObjVisit.HealthFacilityId;
            dbObjVisit.ReferredDepartmentLookupId = dbObjVisit.DepartementLookupId;
            dbObjVisit.ReferredSectionLookupId = dbObjVisit.SectionLookupId;
            dbObjVisit.DepartementLookupId = input.DepartmentLookupId;
            dbObjVisit.SectionLookupId = input.SectionLookupId;
            dbObjVisit.UpdatedBy = _tokenService.GetUserId();
            dbObjVisit.UpdatedOn = DateTime.Now;
            dbObjVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientOpenVisit.Repository.Update(dbObjVisit!);
            await _uowPatientOpenVisit.CommitAsync();

        }
        
        private void FillEntity(PatientAdmissionDetail obj)
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
        private void FillEntityDelete(PatientAdmissionDetail obj)
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
