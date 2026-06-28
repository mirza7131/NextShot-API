using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.MedicineAdvisedRequisitionDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using System.Collections;
using System.Net.NetworkInformation;

namespace HMIS.Patient.Service
{
    public class MedicineAdvisedRequisitionService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<MedicineAdvisedRequisition> _uowMedicineAdvisedRequisition;
        private PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;

        #endregion

        #region Constructor

        public MedicineAdvisedRequisitionService(TokenService tokenService, PatientDiagnoseService<PatientDiagnose> patientDiagnoseService, UnitOfWork<MedicineAdvisedRequisition> uowMedicineAdvisedRequisition, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowMedicineAdvisedRequisition = uowMedicineAdvisedRequisition;
            _patientDiagnoseService = patientDiagnoseService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditMedicineAdvisedRequisitionDto> CreateOrEdit(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.MedicineAdvisedRequisitionId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditMedicineAdvisedRequisitionDto> Create(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            var obj = _mapper.Map<MedicineAdvisedRequisition>(input);
            FillEntity(obj);
            MedicineAdvisedRequisition responseObj = await _uowMedicineAdvisedRequisition.Repository.Insert(obj);
            await _uowMedicineAdvisedRequisition.CommitAsync();
            return _mapper.Map<CreateOrEditMedicineAdvisedRequisitionDto>(responseObj);
        }

        private async Task<CreateOrEditMedicineAdvisedRequisitionDto> Update(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            var dbObj = await _uowMedicineAdvisedRequisition.Repository.GetById(input.MedicineAdvisedRequisitionId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowMedicineAdvisedRequisition.Repository.Update(obj!);
            await _uowMedicineAdvisedRequisition.CommitAsync();

            return _mapper.Map<CreateOrEditMedicineAdvisedRequisitionDto>(obj);
        }

        public async Task<CreateOrEditMedicineAdvisedRequisitionDto> CreateOrEditWithPrescription(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.MedicineAdvisedRequisitionId))
                return await CreateWithPrescription(input);
            else
                return await UpdateWithPrescription(input);
        }

        private async Task<CreateOrEditMedicineAdvisedRequisitionDto> CreateWithPrescription(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            using (var trans = _uowMedicineAdvisedRequisition.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineAdvisedRequisition.GetDbContext());
                    var _uowMedicineAdvised = new UnitOfWork<MedicineAdvised>(_uowMedicineAdvisedRequisition.GetDbContext());
                    var _uowPatientDiagnosisRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineAdvisedRequisition.GetDbContext());

                    var obj = _mapper.Map<MedicineAdvisedRequisition>(input);
                    FillEntity(obj);
                    MedicineAdvisedRequisition responseObj = await _uowMedicineAdvisedRequisition.Repository.Insert(obj);
                    await _uowMedicineAdvisedRequisition.CommitAsync();

                    //List<PatientPrescription> objPrescriptions = new List<PatientPrescription>();

                    // Add Medicine Prescription
                    foreach (var Prescription in input.PatientPrescriptions)
                    {
                        //PatientPrescription objPrescription = new PatientPrescription();

                        var objPrescription = _mapper.Map<PatientPrescription>(Prescription);
                        FillEntityPrescription(objPrescription);
                        //objPrescriptions.Add(objPrescription);
                        objPrescription.MedicineAdvisedRequisitionId = responseObj.MedicineAdvisedRequisitionId;

                        PatientPrescription? responseObjPrescription = await _uowPatientPrescription.Repository.Insert(objPrescription);
                        await _uowPatientPrescription.CommitAsync();
                    }

                    // Update Dispatch Quantity
                    foreach (var Prescription in input.PatientPrescriptions)
                    {
                        //MedicineAdvised objMedAdvised = new MedicineAdvised();
                        //var objPrescription = _mapper.Map<MedicineAdvised>(Prescription);
                        var objMedAdvised = await _uowMedicineAdvised.Repository.GetById(Prescription.MedicineAdvisedId!);

                        if (AppCommonMethod.IsNullObject(objMedAdvised))
                            throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        if (AppCommonMethod.IsNullorZeroInt(objMedAdvised!.QuantityDispatch))
                            objMedAdvised.QuantityRequisition = Prescription.Quantity;
                        else
                            objMedAdvised.QuantityRequisition += Prescription.Quantity;

                        FillEntityMedicineAdvised(objMedAdvised);

                        if (objMedAdvised.QuantityDispatch > objMedAdvised.QuantityPrescribed)
                            throw new UserFriendlyException(CommonMessageConstant.DispenseQuantityWarning);

                        _uowMedicineAdvised.Repository.Update(objMedAdvised);
                        await _uowMedicineAdvised.CommitAsync();
                    }


                    await _patientDiagnoseService.UpdateJsonObj(input.PatientOpenVisitId!, CommonStringConstant.GeneralForm, input.PatientDiagnoseId!, input.PatientId);

                    // below code is move to above function
                    //PatientDiagnosisRecord objPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    //PatientDiagnosisRecord? dbPatientDiagnoseRecord = await _uowPatientDiagnosisRecord.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId).FirstOrDefaultAsync();
                    //var jsonObj = await _patientDiagnoseService.GetJsonObj(input.PatientOpenVisitId!, CommonStringConstant.GeneralForm, input.PatientDiagnoseId);

                    //if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
                    //    objPatientDiagnoseRecord = dbPatientDiagnoseRecord!;

                    //FillEntityDiagnoseRecord(objPatientDiagnoseRecord!);

                    //objPatientDiagnoseRecord.PatientId = input.PatientId;
                    //objPatientDiagnoseRecord.PatientVisitId = input!.PatientOpenVisitId;
                    //objPatientDiagnoseRecord.PatientDiagnoseId = input.PatientDiagnoseId ?? Guid.Empty;
                    //objPatientDiagnoseRecord.FormType = CommonStringConstant.GeneralForm;
                    //objPatientDiagnoseRecord.Json = jsonObj;
                    //objPatientDiagnoseRecord.IsActive = true;

                    //if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
                    //    _uowPatientDiagnosisRecord.Repository.Update(objPatientDiagnoseRecord!);
                    //else
                    //    await _uowPatientDiagnosisRecord.Repository.Insert(objPatientDiagnoseRecord!);
                    //await _uowPatientDiagnosisRecord.CommitAsync();

                    trans.Commit();

                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditMedicineAdvisedRequisitionDto>(input);
        }

        private async Task<CreateOrEditMedicineAdvisedRequisitionDto> UpdateWithPrescription(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            var dbObj = await _uowMedicineAdvisedRequisition.Repository.GetById(input.MedicineAdvisedRequisitionId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowMedicineAdvisedRequisition.Repository.Update(obj!);
            await _uowMedicineAdvisedRequisition.CommitAsync();

            return _mapper.Map<CreateOrEditMedicineAdvisedRequisitionDto>(obj);
        }

        public async Task<CreateOrEditMedicineAdvisedRequisitionDto> AutoRequisition(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            using (var trans = _uowMedicineAdvisedRequisition.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineAdvisedRequisition.GetDbContext());
                    var _uowMedicineAdvised = new UnitOfWork<MedicineAdvised>(_uowMedicineAdvisedRequisition.GetDbContext());
                    var _uowPatientDiagnosisRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineAdvisedRequisition.GetDbContext());

                    var obj = await _uowMedicineAdvisedRequisition.Repository.GetALL(x => x.PatientOpenVisitId == input.PatientOpenVisitId && x.IsAutoGenerated == true && x.Status < (byte)CommonConstant.dispensed).FirstOrDefaultAsync();

                    if(AppCommonMethod.IsNullObject(obj))
                    {
                        obj = _mapper.Map<MedicineAdvisedRequisition>(input);
                        FillEntity(obj);
                        MedicineAdvisedRequisition responseObj = await _uowMedicineAdvisedRequisition.Repository.Insert(obj);
                        await _uowMedicineAdvisedRequisition.CommitAsync();
                    } 

                    //List<PatientPrescription> objPrescriptions = new List<PatientPrescription>();

                    // Add Medicine Prescription
                    foreach (var Prescription in input.PatientPrescriptions)
                    {
                        //PatientPrescription objPrescription = new PatientPrescription();

                        var objPrescription = _mapper.Map<PatientPrescription>(Prescription);
                        FillEntityPrescription(objPrescription);
                        //objPrescriptions.Add(objPrescription);
                        objPrescription.MedicineAdvisedRequisitionId = obj!.MedicineAdvisedRequisitionId;

                        PatientPrescription? responseObjPrescription = await _uowPatientPrescription.Repository.Insert(objPrescription);
                        await _uowPatientPrescription.CommitAsync();
                    }

                    // Update Dispatch Quantity
                    foreach (var Prescription in input.PatientPrescriptions)
                    {
                        //MedicineAdvised objMedAdvised = new MedicineAdvised();
                        //var objPrescription = _mapper.Map<MedicineAdvised>(Prescription);
                        var objMedAdvised = await _uowMedicineAdvised.Repository.GetById(Prescription.MedicineAdvisedId!);

                        if (AppCommonMethod.IsNullObject(objMedAdvised))
                            throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        if (AppCommonMethod.IsNullorZeroInt(objMedAdvised!.QuantityDispatch))
                            objMedAdvised.QuantityRequisition = Prescription.Quantity;
                        else
                            objMedAdvised.QuantityRequisition += Prescription.Quantity;

                        FillEntityMedicineAdvised(objMedAdvised);

                        if (objMedAdvised.QuantityDispatch > objMedAdvised.QuantityPrescribed)
                            throw new UserFriendlyException(CommonMessageConstant.DispenseQuantityWarning);

                        _uowMedicineAdvised.Repository.Update(objMedAdvised);
                        await _uowMedicineAdvised.CommitAsync();
                    }

                    PatientDiagnosisRecord objPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    PatientDiagnosisRecord? dbPatientDiagnoseRecord = await _uowPatientDiagnosisRecord.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId).FirstOrDefaultAsync();
                    var jsonObj = await _patientDiagnoseService.GetJsonObj(input.PatientOpenVisitId!, CommonStringConstant.GeneralForm, input.PatientDiagnoseId);

                    if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
                        objPatientDiagnoseRecord = dbPatientDiagnoseRecord!;

                    FillEntityDiagnoseRecord(objPatientDiagnoseRecord!);

                    objPatientDiagnoseRecord.PatientId = input.PatientId;
                    objPatientDiagnoseRecord.PatientVisitId = input!.PatientOpenVisitId;
                    objPatientDiagnoseRecord.PatientDiagnoseId = input.PatientDiagnoseId ?? Guid.Empty;
                    objPatientDiagnoseRecord.FormType = CommonStringConstant.GeneralForm;
                    objPatientDiagnoseRecord.Json = jsonObj;
                    objPatientDiagnoseRecord.IsActive = true;

                    if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
                        _uowPatientDiagnosisRecord.Repository.Update(objPatientDiagnoseRecord!);
                    else
                        await _uowPatientDiagnosisRecord.Repository.Insert(objPatientDiagnoseRecord!);
                    await _uowPatientDiagnosisRecord.CommitAsync();

                    trans.Commit();

                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditMedicineAdvisedRequisitionDto>(input);
        }

        public async Task UpdatePatientMedicineStatus(Guid PatientPrescriptionId, string Status)
        {
            if (!AppCommonMethod.IsNullOrEmptyGuid(PatientPrescriptionId))
            {
                var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineAdvisedRequisition.GetDbContext());
                var patientPrescription = await _uowPatientPrescription.Repository.GetById(PatientPrescriptionId);
                if (!AppCommonMethod.IsNullObject(patientPrescription))
                {

                    patientPrescription.Status = Status == CommonStringConstant._Received ? (byte)CommonConstant.received :
                                                 Status == CommonStringConstant.Consumed ? (byte)CommonConstant.consumed :
                                                 Status == CommonStringConstant.Returned ? (byte)CommonConstant.returned :
                                                 Status == CommonStringConstant.Cancelled ? (byte)CommonConstant.cancelled : (byte)0;

                    patientPrescription.UpdatedOn = DateTime.Now;
                    patientPrescription.UpdatedBy = _tokenService.GetUserId();
                    _uowPatientPrescription.Repository.Update(patientPrescription);
                    await _uowPatientPrescription.Save();
                }
                else
                {
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);
                }
            }
            else
            {
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);
            }
        }

        public async Task UpdatePatientRequisitionStatus(Guid MedicineAdvisedRequisitionId, int Status)
        {

            var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineAdvisedRequisition.GetDbContext());
            var medicineAdvisedRequisition = await _uowMedicineAdvisedRequisition.Repository.GetById(MedicineAdvisedRequisitionId);
            
            if (AppCommonMethod.IsNullObject(medicineAdvisedRequisition))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            medicineAdvisedRequisition!.Status = (byte)Status;
            medicineAdvisedRequisition!.UpdatedOn = DateTime.Now;
            medicineAdvisedRequisition!.UpdatedBy = _tokenService.GetUserId();
            _uowMedicineAdvisedRequisition.Repository.Update(medicineAdvisedRequisition);
            await _uowMedicineAdvisedRequisition.Save();

            if ((byte)Status == (byte)CommonConstant.cancelled)
            {
                var patientPrescriptions = await _uowPatientPrescription.Repository.GetALL(x => x.MedicineAdvisedRequisitionId == MedicineAdvisedRequisitionId).ToListAsync();
                if (!AppCommonMethod.IsNullOrEmptyList(patientPrescriptions))
                {
                    foreach (var patientPrescription in patientPrescriptions)
                    {
                        PatientPrescription? dbObj = await _uowPatientPrescription.Repository.GetById(patientPrescription.PatientPrescriptionId);

                        if (!AppCommonMethod.IsNullObject(dbObj))
                        {
                            patientPrescription.Status = (byte)CommonConstant.cancelled;
                            medicineAdvisedRequisition!.UpdatedOn = DateTime.Now;
                            medicineAdvisedRequisition!.UpdatedBy = _tokenService.GetUserId();
                            FillEntityPrescriptionDelete(patientPrescription);

                            _uowPatientPrescription.Repository.Update(patientPrescription);
                            await _uowPatientPrescription.Save();
                        }

                        var _uowMedicineAdvised = new UnitOfWork<MedicineAdvised>(_uowMedicineAdvisedRequisition.GetDbContext());
                        MedicineAdvised? objMedAdvised = await _uowMedicineAdvised.Repository.GetById(patientPrescription.MedicineAdvisedId!);

                        if (!AppCommonMethod.IsNullObject(objMedAdvised))
                        {
                            if (AppCommonMethod.IsNullorZeroInt(objMedAdvised!.QuantityDispatch))
                                objMedAdvised.QuantityDispatch = 0; // set to Zero when Dispatch quantity is null
                            else
                                objMedAdvised!.QuantityDispatch -= patientPrescription.Quantity;

                            FillEntityMedicineAdvised(objMedAdvised);

                            if (objMedAdvised.QuantityDispatch < 0)
                                objMedAdvised.QuantityDispatch = 0; // set to Zero when Dispatch quantity is less then zero

                            _uowMedicineAdvised.Repository.Update(objMedAdvised);
                            await _uowMedicineAdvised.CommitAsync();
                        }
                        
                    }

                    
                }
            }
        }


        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowMedicineAdvisedRequisition.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowMedicineAdvisedRequisition.Repository.Update(dbObj!);
            await _uowMedicineAdvisedRequisition.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewMedicineAdvisedRequisitionDto>> GetAll(Expression<Func<MedicineAdvisedRequisition, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<MedicineAdvisedRequisition> responseObj = await _uowMedicineAdvisedRequisition.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewMedicineAdvisedRequisitionDto>>(responseObj);
        }



        public async Task<List<PatientPrescriptionDto>> GetDespencedMedicine(Guid? MedicineAdvisedRequisitionId)
        {
            var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineAdvisedRequisition.GetDbContext());

            var patientPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.MedicineAdvisedRequisitionId == MedicineAdvisedRequisitionId)
                .Select(x => new PatientPrescriptionDto
                {
                    PatientPrescriptionId = x.PatientPrescriptionId,
                    MedicineName = x.MedicineName,
                    MedicineDose = x.MedicineDose,
                    MedicineRoute = x.MedicineRoute,
                    MedicineFrequency = x.MedicineFrequency,
                    MedicineInstruction = x.MedicineInstruction,
                    MedicineDuration = x.MedicineDuration,
                    Quantity = x.Quantity,
                    StatusUpdatedOn = x.UpdatedOn,
                    Status = x.Status == CommonConstant.cancelled ? CommonStringConstant.Cancelled :
                             x.Status == CommonConstant.pending ? CommonStringConstant.Pending :
                             x.Status == CommonConstant.dispensed ? CommonStringConstant.Dispensed :
                             x.Status == CommonConstant.received ? CommonStringConstant._Received :
                             x.Status == CommonConstant.consumed ? CommonStringConstant.Consumed :
                             x.Status == CommonConstant.returned ? CommonStringConstant.Returned : ""
                })
                .ToListAsync();

            //List<PatientPrescriptionDto> IQueryableList = patientPrescription
            //    .Select(x => new PatientPrescriptionDto
            //    {
            //        PatientPrescriptionId = x.PatientPrescriptionId,
            //        MedicineName = x.MedicineName,
            //        MedicineDose = x.MedicineDose,
            //        MedicineRoute = x.MedicineRoute,
            //        MedicineFrequency = x.MedicineFrequency,
            //        MedicineInstruction = x.MedicineInstruction,
            //        MedicineDuration = x.MedicineDuration,
            //        Quantity = x.Quantity,
            //        Status = x.Status == CommonConstant.pending ? CommonStringConstant.Pending :
            //                 x.Status == CommonConstant.dispensed ? CommonStringConstant.Dispensed :
            //                 x.Status == CommonConstant.received ? CommonStringConstant._Received :
            //                 x.Status == CommonConstant.consumed ? CommonStringConstant.Consumed :
            //                 x.Status == CommonConstant.returned ? CommonStringConstant.Returned : ""
            //    }).ToListAsync();


            //var pagedList = await PagedListDto<PatientPrescriptionDto>.ToPagedListAsync(
            //       IQueryableList,
            //       filter.PageNumber,
            //       filter.PageSize
            //       );

            //var responseObject = new ViewPagerDto<PatientPrescriptionDto>
            //{
            //    TotalCount = pagedList.TotalCount,
            //    PageSize = pagedList.PageSize,
            //    CurrentPage = pagedList.CurrentPage,
            //    TotalPages = pagedList.TotalPages,
            //    HasNext = pagedList.HasNext,
            //    HasPrevious = pagedList.HasPrevious,
            //    List = _mapper.Map<List<PatientPrescriptionDto>>(pagedList)
            //};

            return patientPrescription;
        }
        public async Task<ViewPagerDto<ViewGetAllMedicineAdvisedRequisitionDto>> GetAllWithPagination(FilterMedicineAdvisedRequisitionDto filter)
        {
            var list = _uowMedicineAdvisedRequisition.GetDbContext().ViewGetAllMedicineAdvisedRequisitions
                .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(filter.PatientId), x => x.PatientId== filter.PatientId)
                .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(filter.PatientVisitId), x => x.PatientVisitId== filter.PatientVisitId)
                .OrderByDescending(x => x.RequsitionOn);

            IQueryable<ViewGetAllMedicineAdvisedRequisitionDto> IQueryableList = list.Select(x =>
                new ViewGetAllMedicineAdvisedRequisitionDto
                {
                    MedicineAdvisedRequisitionId = x.MedicineAdvisedRequisitionId,
                    PatientId = x.PatientId,
                    PatientDiagnoseId = x.PatientDiagnoseId,
                    RequsitionBy = x.RequsitionBy,
                    RequsitionFor = x.RequsitionFor,
                    RequsitionOn = x.RequsitionOn,
                    Status = x.Status,
                    StatusUpdatedOn = x.UpdatedOn,
                    IsActive = x.IsActive,
                    ActionTypeId = x.ActionTypeId,
                    IsAutoGenerated = x.IsAutoGenerated,
                    IsSelf = (x.CreatedBy == _tokenService.GetUserId()) ? true : false
                }); ;

            var pagedList = await PagedListDto<ViewGetAllMedicineAdvisedRequisitionDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewGetAllMedicineAdvisedRequisitionDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewGetAllMedicineAdvisedRequisitionDto>>(pagedList)
            };

            return responseObject;
        }

        public async Task<ViewMedicineAdvisedRequisitionDto> GetById(Guid input)
        {
            MedicineAdvisedRequisition? responseObj = await _uowMedicineAdvisedRequisition.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewMedicineAdvisedRequisitionDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(MedicineAdvisedRequisition obj)
        {
            if (obj.MedicineAdvisedRequisitionId == Guid.Empty)
            {
                obj.MedicineAdvisedRequisitionId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.Status = (byte)CommonConstant.pending;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(MedicineAdvisedRequisition obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        private void FillEntityPrescription(PatientPrescription obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientPrescriptionId))
            {
                obj.PatientPrescriptionId = Guid.NewGuid();
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

        private void FillEntityPrescriptionDelete(PatientPrescription obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        private void FillEntityDiagnoseRecord(PatientDiagnosisRecord obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnosisRecordId))
            {
                obj.PatientDiagnosisRecordId = Guid.NewGuid();
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

        private void FillEntityMedicineAdvised(MedicineAdvised obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.MedicineAdvisedId))
            {
                obj.MedicineAdvisedId = Guid.NewGuid();
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

        #endregion
    }
}
