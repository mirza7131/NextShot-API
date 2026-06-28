using AppCommonMethods;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HMIS.Patient.Service.Common;
using HMIS.Patient.Domain.Models.DTO.PatientVisitFlowDto;

namespace HMIS.Patient.Service
{
    public class PatientVisitFlowService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientVisitFlow> _uowPatientVisitFlow;

        #endregion

        #region Constructor

        public PatientVisitFlowService(TokenService tokenService, UnitOfWork<PatientVisitFlow> uowPatientVisitFlow, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientVisitFlow = uowPatientVisitFlow;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientVisitFlowDto> CreateOrEdit(CreateOrEditPatientVisitFlowDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientVisitFlowId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientVisitFlowDto> Create(CreateOrEditPatientVisitFlowDto input)
        {
            await UpdatePatientVisitFlowFollowUp(input);
            var obj = _mapper.Map<PatientVisitFlow>(input);
            FillEntity(obj);
            obj.IsActive = true;
            obj.IsVisitClose = false;
            PatientVisitFlow responseObj = await _uowPatientVisitFlow.Repository.Insert(obj);
            await _uowPatientVisitFlow.CommitAsync();
            return _mapper.Map<CreateOrEditPatientVisitFlowDto>(responseObj);
        }

        private async Task<CreateOrEditPatientVisitFlowDto> Update(CreateOrEditPatientVisitFlowDto input)
        {
            var dbObj = await _uowPatientVisitFlow.Repository.GetById(input.PatientVisitFlowId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowPatientVisitFlow.Repository.Update(obj!);
            await _uowPatientVisitFlow.CommitAsync();

            return _mapper.Map<CreateOrEditPatientVisitFlowDto>(obj);
        }

        private async Task UpdatePatientVisitFlowFollowUp(CreateOrEditPatientVisitFlowDto input)
        {
            var currentVisit = await _uowPatientVisitFlow.GetDbContext().PatientOpenVisits.Where(x => x.PatientOpenVisitId == input.PatientVisitId).FirstOrDefaultAsync();
            int PatientTreatmentCycleNumber = 0;



            if (AppCommonMethod.IsNullObject(currentVisit))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var lastPatientDiagnose = await _uowPatientVisitFlow.GetDbContext().PatientDiagnoses
                .Where(x => x.DocDepartmentLookupId == input.CurrentDepartmentId && x.DocSectionLookupId == input.CurrentSectionId && x.PatientId ==  currentVisit!.PatientId)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync();

            if (lastPatientDiagnose != null && lastPatientDiagnose.FollowupDate != null)
            {
                var _lastPatientVisitFlow = await _uowPatientVisitFlow.Repository.GetALL().Where(x => 
                x.PatientVisitId == lastPatientDiagnose.PatientVisitId 
                && x.CurrentDepartmentId == input.CurrentDepartmentId 
                && x.CurrentSectionId == input.CurrentSectionId)
                    .FirstOrDefaultAsync();

                // As Per Khursheed Working
                var lastPatientVisitFlow = _lastPatientVisitFlow == null ? null : _lastPatientVisitFlow.IsFollowUp  == true ? _lastPatientVisitFlow : null;
                
                if (currentVisit?.SectionLookup?.FormType == CommonStringConstant.TbForm)
                {

                    if (AppCommonMethod.IsNullObject(_lastPatientVisitFlow))
                    {
                        var PatientTreatmentCycleNo = await _uowPatientVisitFlow.Repository.GetALL().OrderByDescending(x => x.PatientTreatmentCycleNo).Select(x => x.PatientTreatmentCycleNo).FirstOrDefaultAsync();
                        PatientTreatmentCycleNumber = PatientTreatmentCycleNo == null ? 1 : (int)PatientTreatmentCycleNo + 1;
                    }
                    else
                    {
                        if ((int)(_lastPatientVisitFlow?.PatientTreatmentCycleNo ?? 0) > 0)
                            PatientTreatmentCycleNumber = (int)(_lastPatientVisitFlow?.PatientTreatmentCycleNo ?? 0);
                        else
                        {
                            var PatientTreatmentCycleNo = await _uowPatientVisitFlow.Repository.GetALL().OrderByDescending(x => x.PatientTreatmentCycleNo).Select(x => x.PatientTreatmentCycleNo).FirstOrDefaultAsync();
                            PatientTreatmentCycleNumber = PatientTreatmentCycleNo == null ? 1 : (int)PatientTreatmentCycleNo + 1;
                        }
                    }



                    //******* Araiz
                    // this is for new patient first time coming in tb
                    //if (AppCommonMethod.IsNullObject(lastPatientVisitFlow))
                    //{
                    //    if (AppCommonMethod.IsNullObject(_lastPatientVisitFlow))
                    //    {
                    //        var PatientTreatmentCycleNo = await _uowPatientVisitFlow.Repository.GetALL().OrderByDescending(x => x.PatientTreatmentCycleNo).Select(x => x.PatientTreatmentCycleNo).FirstOrDefaultAsync();
                    //        PatientTreatmentCycleNumber = PatientTreatmentCycleNo == null ? 1 : (int)PatientTreatmentCycleNo + 1;
                    //    }
                    //    else
                    //    {
                    //        if (AppCommonMethod.IsNullorZeroInt(_lastPatientVisitFlow?.PatientTreatmentCycleNo))
                    //        {
                    //            var PatientTreatmentCycleNo = await _uowPatientVisitFlow.Repository.GetALL().OrderByDescending(x => x.PatientTreatmentCycleNo).Select(x => x.PatientTreatmentCycleNo).FirstOrDefaultAsync();
                    //            PatientTreatmentCycleNumber = PatientTreatmentCycleNo == null ? 1 : (int)PatientTreatmentCycleNo + 1;
                    //        }
                    //        else
                    //            PatientTreatmentCycleNumber = (int)(_lastPatientVisitFlow?.PatientTreatmentCycleNo ?? 1);
                    //    }
                    //}
                    //else
                    //{
                    //    var PatientTreatmentCycleNo = await _uowPatientVisitFlow.Repository.GetALL().OrderByDescending(x => x.PatientTreatmentCycleNo).Select(x => x.PatientTreatmentCycleNo).FirstOrDefaultAsync();
                    //    PatientTreatmentCycleNumber = PatientTreatmentCycleNo == null ? 1 : (int)PatientTreatmentCycleNo + 1;
                    //}
                }         

                // This is for TB FollowUp no 
                if (currentVisit?.SectionLookup?.FormType == CommonStringConstant.TbForm && AppCommonMethod.IsNullObject(lastPatientVisitFlow))
                {
                    input.FollowUpNo = 0;
                    input.IsFollowUp = true;
                    input.PatientTreatmentCycleNo = PatientTreatmentCycleNumber;
                }
                else
                {
                    input.FollowUpNo = 1;
                    input.IsFollowUp = true;
                    input.PatientTreatmentCycleNo = PatientTreatmentCycleNumber;
                }


                if (lastPatientVisitFlow != null)
                {
                    input.FollowUpNo += lastPatientVisitFlow.FollowUpNo??1;
                    input.LastVisitId = lastPatientVisitFlow!.PatientVisitId;
                    input.PatientTreatmentCycleNo = lastPatientVisitFlow.PatientTreatmentCycleNo;
                }
                else
                {
                    
                    input.LastVisitId = lastPatientDiagnose!.PatientVisitId;
                }
            }
            else
            {
                if (currentVisit?.SectionLookup?.FormType == CommonStringConstant.TbForm)
                {
                    var PatientTreatmentCycleNo = await _uowPatientVisitFlow.Repository.GetALL().OrderByDescending(x => x.PatientTreatmentCycleNo).Select(x => x.PatientTreatmentCycleNo).FirstOrDefaultAsync();
                    if (AppCommonMethod.IsNullorZeroInt(PatientTreatmentCycleNo))
                    {
                        input.PatientTreatmentCycleNo = 1;
                    }
                    else
                    {
                        input.PatientTreatmentCycleNo = PatientTreatmentCycleNo + 1;
                    }
                }
            }
        }


        //public async Task<CreateOrEditPatientVisitFlowDto> CreatePatientWorkLog(CreateOrEditPatientVisitFlowDto input)
        //{

        //    var obj = _mapper.Map<PatientVisitFlow>(input);
        //    FillEntity(obj);
        //    //return obj;
        //    PatientVisitFlow responseObj = await _uowPatientVisitFlow.Repository.Insert(obj);
        //    await _uowPatientVisitFlow.Save();
        //    return _mapper.Map<CreateOrEditPatientVisitFlowDto>(responseObj);
        //}

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientVisitFlow.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientVisitFlow.Repository.Update(dbObj!);
            await _uowPatientVisitFlow.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientVisitFlowDto>> GetAll(Expression<Func<PatientVisitFlow, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<PatientVisitFlow> responseObj = await _uowPatientVisitFlow.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewPatientVisitFlowDto>>(responseObj);
        }

        public async Task<ViewPatientVisitFlowDto> GetById(Guid input)
        {
            PatientVisitFlow? responseObj = await _uowPatientVisitFlow.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientVisitFlowDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(PatientVisitFlow obj)
        {
            if (obj.PatientVisitFlowId == Guid.Empty)
            {
                obj.PatientVisitFlowId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientVisitFlow obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
