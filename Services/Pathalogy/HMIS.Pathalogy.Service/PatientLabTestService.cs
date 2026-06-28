using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using DbModel = HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDetailDto;
using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto;
using HMIS.Pathalogy.Domain.Models.DTO.ReceiptDto;
using HMIS.Pathalogy.Domain.Models.DTO.ViewPatientLabTestListDto;
using HMIS.Pathalogy.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMIS.Aggregator.API;

using SMSSender.DTO;
using SMSSender;
using Microsoft.Extensions.Configuration;
using HMIS.Pathalogy.Domain.Models.DTO.SmsPatientLabTestDto;
using System.Collections;
using System.Runtime.InteropServices;
using HMIS.Aggregator.API.Models.Dto.EMR;
using CommonDTOs.ResponseDTO;
using Newtonsoft.Json;
using FileHandler;
using HMIS.Pathalogy.Domain.Models.DTO.ViewRiderLabTestListDto;
using HMIS.Pathalogy.Domain.Models.DTO.ProfileDto;
using HMIS.Pathalogy.Domain.Models.DTO.UpdateLabTestResultBulkDto;
using Microsoft.Data.SqlClient;
using System.Data;
using HMIS.Pathalogy.Domain.Models.DTO.HcpPatientDtos;
using AuthDAL.Models.Dto.FeatureDto;
using HMIS.Pathalogy.Domain.Models.DTO.ViewAnmonalListDto;
using AuthDAL.Models.Dto.IntegratedDashboardDTO;
using System.Drawing.Printing;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using CommonDTOs.TBScreeningDTO;

namespace HMIS.Pathalogy.Service
{
    public class PatientLabTestService<TEntity> where TEntity : class
    {

        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        //private readonly BarcodeHandler _barcodeHandler;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientLabTest> _uowPatientLabTest;
        private readonly PatientCommonService _patientCommonService;
        private readonly EMRService _emrService;
        private readonly SMS _smsService;
        private readonly string _labTestBaseURL;
        private readonly UploadFiles _fileUploader;
        private readonly Boolean _smsSend;
        private readonly Boolean _smsSendInDevelopment;

        #endregion

        #region Constructor

        public PatientLabTestService(TokenService tokenService, UnitOfWork<PatientLabTest> uowPatientLabTest, IMapper mapper, PatientCommonService patientCommonService, EMRService emrService, SMS smsService, IConfiguration config, UploadFiles fileUploader)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientLabTest = uowPatientLabTest;
            _patientCommonService = patientCommonService;
            _emrService = emrService;
            _smsService = smsService;
            _fileUploader = fileUploader;
            _labTestBaseURL = config.GetSection("BackendEndpoint").GetSection("LabTestView").Value ?? string.Empty;
            _smsSend = bool.Parse(config.GetSection("SMSConfiguration").GetSection("SendSms").Value) || false;
            _smsSendInDevelopment = bool.Parse(config.GetSection("SMSConfiguration").GetSection("OnDevelopment").Value);
        }

        #endregion

        #region CUD Operations

        //public async Task<CreateOrEditPatientLabTestDto> CreateOrEdit(CreateOrEditPatientLabTestDto input)
        //{
        //    if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientLabTestId))
        //        return await Create(input);
        //    else
        //        return await Update(input);
        //}

        //private async Task<CreateOrEditPatientLabTestDto> Create(CreateOrEditPatientLabTestDto input)
        //{
        //    var obj = _mapper.Map<PatientLabTest>(input);
        //    FillEntity(obj);
        //    PatientLabTest responseObj = await _uowPatientLabTest.Repository.Insert(obj);
        //    await _uowPatientLabTest.CommitAsync();
        //    return _mapper.Map<CreateOrEditPatientLabTestDto>(responseObj);
        //}

        //private async Task<CreateOrEditPatientLabTestDto> Update(CreateOrEditPatientLabTestDto input)
        //{
        //    var dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    var obj = _mapper.Map(input, dbObj);
        //    FillEntity(obj!);

        //    _uowPatientLabTest.Repository.Update(obj!);
        //    await _uowPatientLabTest.CommitAsync();

        //    return _mapper.Map<CreateOrEditPatientLabTestDto>(obj);
        //}

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowPatientLabTest.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowPatientLabTest.Repository.Update(dbObj!);
        //    await _uowPatientLabTest.CommitAsync();
        //    return true;
        //}





        //private async Task<CreateOrEditPatientLabTestDto> Update(CreateOrEditPatientLabTestDto input)
        //{
        //    var dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    var obj = _mapper.Map(input, dbObj);
        //    FillEntity(obj!);

        //    _uowPatientLabTest.Repository.Update(obj!);
        //    await _uowPatientLabTest.CommitAsync();

        //    return _mapper.Map<CreateOrEditPatientLabTestDto>(obj);
        //}

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowPatientLabTest.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowPatientLabTest.Repository.Update(dbObj!);
        //    await _uowPatientLabTest.CommitAsync();
        //    return true;
        //}

        public async Task<UploadResultImageDto> UploadResultImage(UploadResultImageDto input)
        {
            using (var trans = _uowPatientLabTest.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    if (!string.IsNullOrEmpty(input.ResultImageLink))
                        input.ResultImageLink = await _fileUploader.UploadFileToCDN(CommonStringConstant.Pathalogy, input.ResultImageLink, _tokenService.GetAccessToken());

                    PatientLabTest? dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

                    if (AppCommonMethod.IsNullObject(dbObj))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    var obj = _mapper.Map(input, dbObj);
                    obj!.PatientLabTestDetails.Clear();
                    FillEntityUploadImage(obj!);

                    _uowPatientLabTest.Repository.Update(obj!);
                    await _uowPatientLabTest.Save();

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return input;
        }



        public async Task<UpdateLabTestResultDto> UpdateTestResult(UpdateLabTestResultDto input)
        {
            using (var trans = _uowPatientLabTest.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientLabTest.GetDbContext());

                    PatientLabTest? dbObj = new PatientLabTest();
                    if (input.IsExternal)
                        dbObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == input.PatientLabTestId)
                            .Include(x => x.Patient).FirstOrDefaultAsync();
                    else
                        dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

                    if (AppCommonMethod.IsNullObject(dbObj))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    var obj = _mapper.Map(input, dbObj);
                    obj!.PatientLabTestDetails.Clear();
                    FillEntityUpdateResult(obj!);

                    foreach (var patientLabTestDetail in input!.PatientLabTestDetails)
                    {
                        PatientLabTestDetail? dbObjDetail = await _uowPatientLabTestDetail.Repository.GetById(patientLabTestDetail.PatientLabTestDetailId!);

                        if (AppCommonMethod.IsNullObject(dbObjDetail))
                            throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        var objDetail = _mapper.Map(patientLabTestDetail, dbObjDetail);
                        FillEntityDetail(objDetail!);

                        _uowPatientLabTestDetail.Repository.Update(objDetail!);
                        await _uowPatientLabTest.Save();
                    }

                    if (_smsSend && _smsSendInDevelopment)
                    {
                        var infoForSmsBody = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == dbObj!.PatientLabTestId && x.SampleConsignmentDetailId != null)
                            .Include(x => x.Patient)
                            .Include(x => x.LabTest)
                            .Select(x => new SmsBodyPatientLabTestDto
                            {
                                PatientName = x.Patient!.FirstName,
                                PatientPhoneNo = x.Patient!.MobileNo!.Replace("-", ""),
                                TestName = x.LabTest!.Name,
                                Link = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", x.PatientLabTestId.ToString())
                            })
                            .FirstOrDefaultAsync();

                        if (!AppCommonMethod.IsNullObject(infoForSmsBody))
                        {
                            // SMS on Result Generation
                            SendSMSDto smsObj = new SendSMSDto()
                            {
                                Receiver = infoForSmsBody.PatientPhoneNo,

                                Body = $"معزز {infoForSmsBody.PatientName}\r\n" +
                                $" آپ کے لیب ٹیسٹ" +
                                $"{infoForSmsBody.TestName}" +
                                $"کی رپورٹ تیار ہو گئی ہے۔ " +
                                $"\r\n" +
                                $"براے مہربانی اپنی رپورٹ لیب سے یا نیچے دیے گئے لنک سے حاصل کریں۔" +

                                $"\r\n" +
                                $"شکریہ" +

                                $"\r\n" +
                                $"{infoForSmsBody.Link}"
                            };

                            _smsService.SendSMS(smsObj);
                        }

                    }

                    if (input.IsExternal)
                        await UpdateResultStatusInEMR(obj);

                    _uowPatientLabTest.Repository.Update(obj!);
                    await _uowPatientLabTest.Save();

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return input;
        }
        //public async Task<List<TestListDto>> UpdateTestResultBulk(List<TestListDto> input)
        //{
        //    using (var trans = _uowPatientLabTest.GetDbContext().Database.BeginTransaction())
        //    {
        //        try
        //        {

        //            var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientLabTest.GetDbContext());


        //            PatientLabTest? dbObj = new PatientLabTest();

        //            foreach (var item in input)
        //            {
        //            var patientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.BarcodeNo == item.BarcodeNo && x.BatchNumber == item.BatchNumber && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).Include(x => x.Patient).FirstOrDefaultAsync();

        //                if (item.Result.ToLower().Contains("resample"))
        //                {

        //                    //Profile InappropriateSampleObj = _uowPatientLabTest.GetDbContext().Profiles.Where(x => x.ShortName == CommonStringConstant.InappropriateSample);

        //                    var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());

        //                    var InappropriateSampleObj = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.InappropriateSample).FirstOrDefault();

        //                    UpdateLabSampleRejectedReasonDto SampleToReject = new UpdateLabSampleRejectedReasonDto();

        //                    SampleToReject.PatientLabTestId = item.PatientLabTestId;

        //                    if (item.PreGenratedBarcodeNo != null)
        //                    {

        //                        SampleToReject.IsPreGeneratedBarcode = true;
        //                        SampleToReject.PreGeneratedBarcode = item.PreGenratedBarcodeNo;
        //                    }
        //                    //else
        //                    //{
        //                    //    SampleToReject.PreGeneratedBarcode = null;

        //                    //}
        //                    SampleToReject.SampleRejectedReason = InappropriateSampleObj.ProfileId.ToString();
        //                    SampleToReject.SampleRejectedReasonName = CommonStringConstant.InappropriateSampleName;
        //                    //UpdateSampleRejectedStatus(SampleToReject);

        //                    // Code from Reject Sample (UpdateSampleRejectedStatus())
        //                    //PatientLabTest? dbObj = new PatientLabTest();
        //                    if (SampleToReject.IsExternal)
        //                        dbObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == SampleToReject.PatientLabTestId)
        //                            .Include(x => x.Patient).FirstOrDefaultAsync();
        //                    else
        //                        dbObj = await _uowPatientLabTest.Repository.GetById(SampleToReject.PatientLabTestId);

        //                    if (AppCommonMethod.IsNullObject(dbObj))
        //                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //                    FillEntitySampleRejection(dbObj);
        //                    dbObj.BatchResultUploadedOn = DateTime.Today;

        //                    if (SampleToReject.IsPreGeneratedBarcode)
        //                        dbObj.PreGeneratedBarcodeNo = SampleToReject.PreGeneratedBarcode;
        //                    dbObj.SampleRejectedReason = SampleToReject.SampleRejectedReason;

        //                    if (SampleToReject.IsExternal)
        //                        await UpdateSampleRejectionStatusinEMR(dbObj, SampleToReject);

        //                    _uowPatientLabTest.Repository.Update(dbObj);
        //                    await _uowPatientLabTest.Save();
        //                    // Till here (UpdateSampleRejectedStatus())
        //                }
        //                else
        //                {
        //                    // Get LabtestId from HCP Recommended Test Table
        //                    var _uowHCPRecommendedTests = new UnitOfWork<DbModel.HcpRecommendedTest>(_uowPatientLabTest.GetDbContext());
        //                    // Annual PCR HBV
        //                    var HbvAnnualPCR = _uowHCPRecommendedTests.Repository.GetALL(x => x.ShortName == CommonStringConstant.HBVAPCR && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).FirstOrDefault();
        //                    // HCV SVR
        //                    var HcvSVR = _uowHCPRecommendedTests.Repository.GetALL(x => x.ShortName == CommonStringConstant.HCVASVR && (x.ActionTypeId == 1 || x.ActionTypeId == 2)).FirstOrDefault();

        //                    // Condition if labtest ids are equal then
        //                    if (item.LabTestId == HbvAnnualPCR.LabTestId || item.LabTestId == HcvSVR.LabTestId)
        //                    {
        //                        // Get Profiles of SVR/PCR Status to Result Upload
        //                        var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
        //                        var EligibleForAnnualPCRHBVPendingCollection = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HBVEligibleForAnnualPCRHBVPendingCollection).FirstOrDefault();

        //                        var EligibleForSVRHCVPendingCollection = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HCVEligibleForSVRHCVPendingCollection).FirstOrDefault();

        //                        var HBVResultUploaded = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HBVResultUploaded).FirstOrDefault();
        //                        var HCVResultUploaded = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HCVResultUploaded).FirstOrDefault();

        //                        // Get Record from PatientDiagnose Table where Svr/Pcr is recommended.
        //                        var _uowPatientDiagnose = new UnitOfWork<DbModel.PatientDiagnose>(_uowPatientLabTest.GetDbContext());

        //                        PatientDiagnose RecommendedPatientDiagnose = new PatientDiagnose();

        //                        // Assigning Patient Diagnose Object according to test
        //                        if (item.LabTestId == HbvAnnualPCR.LabTestId)
        //                        {
        //                            RecommendedPatientDiagnose = _uowPatientDiagnose.Repository.GetALL(x => x.PcrStatusProfileId == EligibleForAnnualPCRHBVPendingCollection.ProfileId).OrderBy(x => x.CreatedOn).LastOrDefault();
        //                            //if (AppCommonMethod.IsNullObject(RecommendedPatientDiagnose))
        //                            //    throw new UserFriendlyException(CommonMessageConstant.ErrorGettingDiagnoseResult);

        //                            RecommendedPatientDiagnose.PcrStatusProfileId = HBVResultUploaded.ProfileId;
        //                            _uowPatientDiagnose.Repository.Update(RecommendedPatientDiagnose!);
        //                            await _uowPatientDiagnose.Save();
        //                        }
        //                        else if (item.LabTestId == HcvSVR.LabTestId)
        //                        {
        //                            RecommendedPatientDiagnose = _uowPatientDiagnose.Repository.GetALL(x => x.SvrStatusProfileId == EligibleForSVRHCVPendingCollection.ProfileId).OrderBy(x=>x.CreatedOn).LastOrDefault();

        //                            //if (AppCommonMethod.IsNullObject(RecommendedPatientDiagnose))
        //                            //    throw new UserFriendlyException(CommonMessageConstant.ErrorGettingDiagnoseResult);

        //                            RecommendedPatientDiagnose.SvrStatusProfileId = HCVResultUploaded.ProfileId;
        //                            _uowPatientDiagnose.Repository.Update(RecommendedPatientDiagnose!);
        //                            await _uowPatientDiagnose.Save();
        //                        }
        //                    }



        //                    item.PatientId = patientLabTest.PatientId;
        //                    item.PatientLabTestId = patientLabTest.PatientLabTestId;
        //                    item.LabTestId = patientLabTest.LabTestId;
        //                    item.PatientVisitId = patientLabTest.PatientVisitId;

        //                    //if (item.IsExternal)
        //                    //    dbObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == input.PatientLabTestId)
        //                    //        .Include(x => x.Patient).FirstOrDefaultAsync();
        //                    //else
        //                    dbObj = await _uowPatientLabTest.Repository.GetById(patientLabTest.PatientLabTestId);

        //                    if (AppCommonMethod.IsNullObject(dbObj))
        //                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //                    //var obj = _mapper.Map(item, dbObj);
        //                    //obj!.PatientLabTestDetails.Clear();
        //                    //FillEntityUpdateResult(obj!);
        //                    dbObj.IsReportGenerated = true;
        //                    dbObj.ReportGeneratedBy = _tokenService.GetUserId();
        //                    dbObj.ReportGeneratedOn = DateTime.Now;
        //                    dbObj.BatchResultUploadedOn = DateTime.Today;

        //                    dbObj.ReportLink = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", dbObj.PatientLabTestId.ToString());

        //                    dbObj.IsArchived = false;
        //                    dbObj.ArchivedBy = null;
        //                    dbObj.ArchivedOn = null;

        //                    dbObj.UpdatedBy = _tokenService.GetUserId();
        //                    dbObj.UpdatedOn = DateTime.Now;
        //                    dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;


        //                    //foreach (var patientLabTestDetail in patientLabTest.PatientLabTestDetails)
        //                    //{
        //                    //PatientLabTestDetail? dbObjDetail = await _uowPatientLabTestDetail.Repository.GetById(patientLabTest.PatientLabTestId!);
        //                    var dbObjDetail = _uowPatientLabTestDetail.Repository.GetALL(x => x.PatientLabTestId == patientLabTest.PatientLabTestId).ToList();

        //                    if (AppCommonMethod.IsNullObject(dbObjDetail))
        //                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //                    foreach (var objItem in dbObjDetail)
        //                    {
        //                        if (objItem.TestName == CommonMessageConstant.ViralLoad)
        //                        {
        //                            if (objItem.PatientLabTestDetailId == Guid.Empty)
        //                            {
        //                                objItem.Result = item.ViralLoad.ToString();
        //                                objItem.PatientLabTestDetailId = Guid.NewGuid();
        //                                objItem.CreatedBy = _tokenService.GetUserId();
        //                                objItem.CreatedOn = DateTime.Now;
        //                                objItem.ActionTypeId = (int)ActionTypeEnum.Create;
        //                            }
        //                            else
        //                            {
        //                                objItem.Result = item.ViralLoad.ToString();
        //                                objItem.UpdatedBy = _tokenService.GetUserId();
        //                                objItem.UpdatedOn = DateTime.Now;
        //                                objItem.ActionTypeId = (int)ActionTypeEnum.Edit;
        //                            }

        //                            _uowPatientLabTestDetail.Repository.Update(objItem!);
        //                            await _uowPatientLabTest.Save();
        //                            //}
        //                        }
        //                        else if (objItem.TestName == CommonMessageConstant.Result)
        //                        {
        //                            if (objItem.PatientLabTestDetailId == Guid.Empty)
        //                            {
        //                                objItem.Result = item.Result;
        //                                objItem.PatientLabTestDetailId = Guid.NewGuid();
        //                                objItem.CreatedBy = _tokenService.GetUserId();
        //                                objItem.CreatedOn = DateTime.Now;
        //                                objItem.ActionTypeId = (int)ActionTypeEnum.Create;
        //                            }
        //                            else
        //                            {
        //                                objItem.Result = item.Result;
        //                                objItem.UpdatedBy = _tokenService.GetUserId();
        //                                objItem.UpdatedOn = DateTime.Now;
        //                                objItem.ActionTypeId = (int)ActionTypeEnum.Edit;
        //                            }

        //                            _uowPatientLabTestDetail.Repository.Update(objItem!);
        //                            //await _uowPatientLabTest.Save();
        //                            //}
        //                        }
        //                    }

        //                    //////var objDetail = _mapper.Map(patientLabTest.PatientLabTestId, dbObjDetail);
        //                    ////if (dbObjDetail.TestName == "Viral Load")
        //                    ////{

        //                    ////}else if (dbObjDetail.TestName == "Result")
        //                    ////{

        //                    ////}
        //                    //    //FillEntityDetail(objDetail!);
        //                    //    if (dbObjDetail.PatientLabTestDetailId == Guid.Empty)
        //                    //    {
        //                    //        dbObjDetail.PatientLabTestDetailId = Guid.NewGuid();
        //                    //        dbObjDetail.CreatedBy = _tokenService.GetUserId();
        //                    //        dbObjDetail.CreatedOn = DateTime.Now;
        //                    //        dbObjDetail.ActionTypeId = (int)ActionTypeEnum.Create;
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        dbObjDetail.UpdatedBy = _tokenService.GetUserId();
        //                    //        dbObjDetail.UpdatedOn = DateTime.Now;
        //                    //        dbObjDetail.ActionTypeId = (int)ActionTypeEnum.Edit;
        //                    //    }

        //                    //_uowPatientLabTestDetail.Repository.Update(dbObjDetail!);
        //                    //    await _uowPatientLabTest.Save();
        //                    ////}

        //                    //SMS API
        //                    if (_smsSend && _smsSendInDevelopment)
        //                    {
        //                        var infoForSmsBody = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == dbObj!.PatientLabTestId && x.SampleConsignmentDetailId != null)
        //                            .Include(x => x.Patient)
        //                            .Include(x => x.LabTest)
        //                            .Select(x => new SmsBodyPatientLabTestDto
        //                            {
        //                                PatientName = x.Patient!.FirstName,
        //                                PatientPhoneNo = x.Patient!.MobileNo!.Replace("-", ""),
        //                                TestName = x.LabTest!.Name,
        //                                Link = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", x.PatientLabTestId.ToString())
        //                            })
        //                            .FirstOrDefaultAsync();

        //                        if (!AppCommonMethod.IsNullObject(infoForSmsBody))
        //                        {
        //                            // SMS on Result Generation
        //                            SendSMSDto smsObj = new SendSMSDto()
        //                            {
        //                                Receiver = infoForSmsBody.PatientPhoneNo,

        //                                Body = $"معزز {infoForSmsBody.PatientName}\r\n" +
        //                                $" آپ کے لیب ٹیسٹ" +
        //                                $"{infoForSmsBody.TestName}" +
        //                                $"کی رپورٹ تیار ہو گئی ہے۔ " +
        //                                $"\r\n" +
        //                                $"براے مہربانی اپنی رپورٹ لیب سے یا نیچے دیے گئے لنک سے حاصل کریں۔" +

        //                                $"\r\n" +
        //                                $"شکریہ" +

        //                                $"\r\n" +
        //                                $"{infoForSmsBody.Link}"
        //                            };

        //                            _smsService.SendSMS(smsObj);
        //                        }

        //                    }



        //                    //if (input.IsExternal)
        //                    //    await UpdateResultStatusInEMR(obj);

        //                    _uowPatientLabTest.Repository.Update(dbObj!);
        //                    await _uowPatientLabTest.Save();
        //                }
        //            }
        //            trans.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            trans.Rollback();
        //            throw;
        //        }
        //    }

        //    return input;
        //}

        public async Task<List<TestListDto>> UpdateTestResultBulk(List<TestListDto> input)
        {
            using (var trans = _uowPatientLabTest.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientLabTest.GetDbContext());
                    var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
                    var InappropriateSampleObj = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.InappropriateSample).FirstOrDefault();


                    // Get Profiles of SVR/PCR Status to Result Upload
                    //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
                    var EligibleForAnnualPCRHBVPendingCollection = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HBVEligibleForAnnualPCRHBVPendingCollection).FirstOrDefault();
                    var EligibleForSVRHCVPendingCollection = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HCVEligibleForSVRHCVPendingCollection).FirstOrDefault();
                    var HBVResultUploaded = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HBVResultUploaded).FirstOrDefault();
                    var HCVResultUploaded = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.HCVResultUploaded).FirstOrDefault();


                    // Get LabtestId from HCP Recommended Test Table
                    var _uowHCPRecommendedTests = new UnitOfWork<DbModel.HcpRecommendedTest>(_uowPatientLabTest.GetDbContext());
                    // Annual PCR HBV
                    var HbvAnnualPCR = _uowHCPRecommendedTests.Repository.GetALL(x => x.ShortName == CommonStringConstant.HBVAPCR && (x.ActionTypeId != 3)).FirstOrDefault();
                    // HCV SVR
                    var HcvSVR = _uowHCPRecommendedTests.Repository.GetALL(x => x.ShortName == CommonStringConstant.HCVASVR && (x.ActionTypeId != 3)).FirstOrDefault();

                    // Get Record from PatientDiagnose Table where Svr/Pcr is recommended.
                    var _uowPatientDiagnose = new UnitOfWork<DbModel.PatientDiagnose>(_uowPatientLabTest.GetDbContext());


                    PatientLabTest? dbObj = new PatientLabTest();

                    foreach (var item in input)
                    {
                        var patientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.BarcodeNo == item.BarcodeNo && x.BatchNumber == item.BatchNumber && x.ActionTypeId != 3).Include(x => x.Patient).FirstOrDefaultAsync();

                        if (item.Result.ToLower().Contains(CommonStringConstant.resample))
                        {

                            UpdateLabSampleRejectedReasonDto SampleToReject = new UpdateLabSampleRejectedReasonDto();

                            SampleToReject.PatientLabTestId = item.PatientLabTestId;

                            if (item.PreGenratedBarcodeNo != null)
                            {

                                SampleToReject.IsPreGeneratedBarcode = true;
                                SampleToReject.PreGeneratedBarcode = item.PreGenratedBarcodeNo;
                            }
                            SampleToReject.SampleRejectedReason = InappropriateSampleObj.ProfileId.ToString();
                            SampleToReject.SampleRejectedReasonName = CommonStringConstant.InappropriateSampleName;
                            //UpdateSampleRejectedStatus(SampleToReject);

                            // Code from Reject Sample (UpdateSampleRejectedStatus())
                            //PatientLabTest? dbObj = new PatientLabTest();
                            if (SampleToReject.IsExternal)
                                dbObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == SampleToReject.PatientLabTestId)
                                    .Include(x => x.Patient).FirstOrDefaultAsync();
                            else
                                dbObj = await _uowPatientLabTest.Repository.GetById(SampleToReject.PatientLabTestId);

                            if (AppCommonMethod.IsNullObject(dbObj))
                                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            FillEntitySampleRejection(dbObj);
                            dbObj.BatchResultUploadedOn = DateTime.Today;

                            if (SampleToReject.IsPreGeneratedBarcode)
                                dbObj.PreGeneratedBarcodeNo = SampleToReject.PreGeneratedBarcode;
                            dbObj.SampleRejectedReason = SampleToReject.SampleRejectedReason;

                            if (SampleToReject.IsExternal)
                                await UpdateSampleRejectionStatusinEMR(dbObj, SampleToReject);

                            _uowPatientLabTest.Repository.Update(dbObj);
                            await _uowPatientLabTest.Save();
                            // Till here (UpdateSampleRejectedStatus())
                        }
                        else
                        {


                            // Condition if labtest ids are equal then
                            if (item.LabTestId == HbvAnnualPCR.LabTestId || item.LabTestId == HcvSVR.LabTestId)
                            {


                                PatientDiagnose RecommendedPatientDiagnose = new PatientDiagnose();

                                // Assigning Patient Diagnose Object according to test
                                if (item.LabTestId == HbvAnnualPCR.LabTestId)
                                {
                                    RecommendedPatientDiagnose = _uowPatientDiagnose.Repository.GetALL(x => x.PcrStatusProfileId == EligibleForAnnualPCRHBVPendingCollection.ProfileId && x.PatientId == item.PatientId).OrderBy(x => x.CreatedOn).LastOrDefault();
                                    //if (AppCommonMethod.IsNullObject(RecommendedPatientDiagnose))
                                    //    throw new UserFriendlyException(CommonMessageConstant.ErrorGettingDiagnoseResult);

                                    if (!AppCommonMethod.IsNullObject(RecommendedPatientDiagnose))
                                    {
                                        RecommendedPatientDiagnose.PcrStatusProfileId = HBVResultUploaded.ProfileId;
                                        _uowPatientDiagnose.Repository.Update(RecommendedPatientDiagnose!);
                                        await _uowPatientDiagnose.Save();
                                    }
                                }
                                else if (item.LabTestId == HcvSVR.LabTestId)
                                {

                                    RecommendedPatientDiagnose = _uowPatientDiagnose.Repository.GetALL(x => x.SvrStatusProfileId == EligibleForSVRHCVPendingCollection.ProfileId && x.PatientId == item.PatientId).OrderBy(x => x.CreatedOn).LastOrDefault();

                                    //if (AppCommonMethod.IsNullObject(RecommendedPatientDiagnose))
                                    //    throw new UserFriendlyException(CommonMessageConstant.ErrorGettingDiagnoseResult);

                                    if (!AppCommonMethod.IsNullObject(RecommendedPatientDiagnose))
                                    {
                                        RecommendedPatientDiagnose.SvrStatusProfileId = HCVResultUploaded.ProfileId;
                                        _uowPatientDiagnose.Repository.Update(RecommendedPatientDiagnose!);
                                        await _uowPatientDiagnose.Save();
                                    }
                                }
                            }



                            item.PatientId = patientLabTest.PatientId;
                            item.PatientLabTestId = patientLabTest.PatientLabTestId;
                            item.LabTestId = patientLabTest.LabTestId;
                            item.PatientVisitId = patientLabTest.PatientVisitId;

                            dbObj = await _uowPatientLabTest.Repository.GetById(patientLabTest.PatientLabTestId);

                            if (AppCommonMethod.IsNullObject(dbObj))
                                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            dbObj.IsReportGenerated = true;
                            dbObj.ReportGeneratedBy = _tokenService.GetUserId();
                            dbObj.ReportGeneratedOn = DateTime.Now;
                            dbObj.BatchResultUploadedOn = DateTime.Today;

                            dbObj.ReportLink = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", dbObj.PatientLabTestId.ToString());

                            dbObj.IsArchived = false;
                            dbObj.ArchivedBy = null;
                            dbObj.ArchivedOn = null;

                            dbObj.UpdatedBy = _tokenService.GetUserId();
                            dbObj.UpdatedOn = DateTime.Now;
                            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

                            var dbObjDetail = _uowPatientLabTestDetail.Repository.GetALL(x => x.PatientLabTestId == patientLabTest.PatientLabTestId).ToList();

                            if (AppCommonMethod.IsNullObject(dbObjDetail))
                                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            foreach (var objItem in dbObjDetail)
                            {
                                if (objItem.TestName == CommonMessageConstant.ViralLoad)
                                {
                                    if (objItem.PatientLabTestDetailId == Guid.Empty)
                                    {
                                        objItem.Result = item.ViralLoad.ToString();
                                        objItem.PatientLabTestDetailId = Guid.NewGuid();
                                        objItem.CreatedBy = _tokenService.GetUserId();
                                        objItem.CreatedOn = DateTime.Now;
                                        objItem.ActionTypeId = (int)ActionTypeEnum.Create;
                                    }
                                    else
                                    {
                                        objItem.Result = item.ViralLoad.ToString();
                                        objItem.UpdatedBy = _tokenService.GetUserId();
                                        objItem.UpdatedOn = DateTime.Now;
                                        objItem.ActionTypeId = (int)ActionTypeEnum.Edit;
                                    }

                                    _uowPatientLabTestDetail.Repository.Update(objItem!);
                                    await _uowPatientLabTest.Save();
                                }
                                else if (objItem.TestName == CommonMessageConstant.Result)
                                {
                                    if (objItem.PatientLabTestDetailId == Guid.Empty)
                                    {
                                        objItem.Result = item.Result;
                                        objItem.PatientLabTestDetailId = Guid.NewGuid();
                                        objItem.CreatedBy = _tokenService.GetUserId();
                                        objItem.CreatedOn = DateTime.Now;
                                        objItem.ActionTypeId = (int)ActionTypeEnum.Create;
                                    }
                                    else
                                    {
                                        objItem.Result = item.Result;
                                        objItem.UpdatedBy = _tokenService.GetUserId();
                                        objItem.UpdatedOn = DateTime.Now;
                                        objItem.ActionTypeId = (int)ActionTypeEnum.Edit;
                                    }

                                    _uowPatientLabTestDetail.Repository.Update(objItem!);
                                    //await _uowPatientLabTest.Save();
                                }
                            }

                            //SMS API
                            if (_smsSend && _smsSendInDevelopment)
                            {
                                var infoForSmsBody = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == dbObj!.PatientLabTestId && x.SampleConsignmentDetailId != null)
                                    .Include(x => x.Patient)
                                    .Include(x => x.LabTest)
                                    .Select(x => new SmsBodyPatientLabTestDto
                                    {
                                        PatientName = x.Patient!.FirstName,
                                        PatientPhoneNo = x.Patient!.MobileNo!.Replace("-", ""),
                                        TestName = x.LabTest!.Name,
                                        Link = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", x.PatientLabTestId.ToString())
                                    })
                                    .FirstOrDefaultAsync();

                                if (!AppCommonMethod.IsNullObject(infoForSmsBody))
                                {
                                    // SMS on Result Generation
                                    SendSMSDto smsObj = new SendSMSDto()
                                    {
                                        Receiver = infoForSmsBody.PatientPhoneNo,

                                        Body = $"معزز {infoForSmsBody.PatientName}\r\n" +
                                        $" آپ کے لیب ٹیسٹ" +
                                        $"{infoForSmsBody.TestName}" +
                                        $"کی رپورٹ تیار ہو گئی ہے۔ " +
                                        $"\r\n" +
                                        $"براے مہربانی اپنی رپورٹ لیب سے یا نیچے دیے گئے لنک سے حاصل کریں۔" +

                                        $"\r\n" +
                                        $"شکریہ" +

                                        $"\r\n" +
                                        $"{infoForSmsBody.Link}"
                                    };

                                    _smsService.SendSMS(smsObj);
                                }

                            }



                            //if (input.IsExternal)
                            //    await UpdateResultStatusInEMR(obj);

                            _uowPatientLabTest.Repository.Update(dbObj!);
                            await _uowPatientLabTest.Save();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return input;
        }

        public async Task<CreateOrEditPatientLabTestDto> UpdateRider(AssignRiderDto input)
        {
            var dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntity(dbObj);
            dbObj.RiderUserId = input.RiderUserId;
            dbObj.IsArchived = false;
            dbObj.ArchivedBy = null;
            dbObj.ArchivedOn = null;

            _uowPatientLabTest.Repository.Update(dbObj);
            await _uowPatientLabTest.CommitAsync();
            return _mapper.Map<CreateOrEditPatientLabTestDto>(dbObj);
        }

        public async Task<CreateOrEditPatientLabTestDto> UpdateSampleCollectionStatus(UpdateSampleCollectionStatusDto input)
        {


            if (input.SampleTransportMode == CommonStringConstant.LHW)
            {
                IsValidCnic(input!.sampleTransportByLHW!.CNICOfLHS);
                IsValidCnic(input!.sampleTransportByLHW!.CNICOfLHW);
            }


            PatientLabTest? dbObj = new PatientLabTest();
            if (input.IsExternal)
                dbObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == input.PatientLabTestId)
                    .Include(x => x.Patient).FirstOrDefaultAsync();
            else
                dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            FillEntityPendingCollection(dbObj);
            dbObj.SampleTransportMode = input.SampleTransportMode;
            if (input.IsPreGeneratedBarcode)
                dbObj.PreGeneratedBarcodeNo = input.PreGeneratedBarcode;

            if (input.IsExternal)
                await UpdateSampleStatusInEMR(dbObj);

            _uowPatientLabTest.Repository.Update(dbObj);
            await _uowPatientLabTest.CommitAsync();

            var lhwDetails = new SampleTransportByLhw();
            if (input.SampleTransportMode == CommonStringConstant.LHW)
            {
                var _uowSampleTransportByLhw = new UnitOfWork<SampleTransportByLhw>(_uowPatientLabTest.GetDbContext());
                lhwDetails = _mapper.Map<SampleTransportByLhw>(input.sampleTransportByLHW);
                lhwDetails.PatientId = dbObj.PatientId;
                lhwDetails.PatientVisitId = dbObj.PatientVisitId;
                lhwDetails.PatientDiagnoseId = dbObj.PatientDiagnoseId;
                lhwDetails.PatientLabTestId = dbObj.PatientLabTestId;
                FillEntityDetailSampleTransportByLhw(lhwDetails);
                await _uowSampleTransportByLhw.Repository.Insert(lhwDetails);
                await _uowSampleTransportByLhw.Save();
            }

            var data = _mapper.Map<CreateOrEditPatientLabTestDto>(dbObj);

            if (input.SampleTransportMode == CommonStringConstant.LHW)
            {
                data.LHWName = lhwDetails.NameOfLhw;
                data.LHWCnic = lhwDetails.CnicofLhs;
            }

            return data;

        }

        public async Task<CreateOrEditPatientLabTestDto> UpdateSampleRejectedStatus(UpdateLabSampleRejectedReasonDto input)
        {
            PatientLabTest? dbObj = new PatientLabTest();
            if (input.IsExternal)
                dbObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == input.PatientLabTestId)
                    .Include(x => x.Patient).FirstOrDefaultAsync();
            else
                dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntitySampleRejection(dbObj);

            if (input.IsPreGeneratedBarcode)
                dbObj.PreGeneratedBarcodeNo = input.PreGeneratedBarcode;
            dbObj.SampleRejectedReason = input.SampleRejectedReason;

            if (input.IsExternal)
                await UpdateSampleRejectionStatusinEMR(dbObj, input);

            _uowPatientLabTest.Repository.Update(dbObj);
            await _uowPatientLabTest.CommitAsync();
            return _mapper.Map<CreateOrEditPatientLabTestDto>(dbObj);

        }

        public async Task<CreateOrEditPatientLabTestDto> MarkAsArchived(Guid PatientLabTestId)
        {
            PatientLabTest? dbObj = await _uowPatientLabTest.Repository.GetById(PatientLabTestId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityForArchive(dbObj);
            _uowPatientLabTest.Repository.Update(dbObj);
            await _uowPatientLabTest.CommitAsync();
            return _mapper.Map<CreateOrEditPatientLabTestDto>(dbObj);

        }


        public async Task<CreateOrEditPatientLabTestDto> UpdateRiderLabTestStatus(UpdateRiderLabTestStatusDto input)
        {
            //PatientLabTest? dbObj = new PatientLabTest();
            var dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //FillEntityRiderStatus(dbObj);
            dbObj.Status = input.Status;
            if (!string.IsNullOrEmpty(input.PreGeneratedBarcodeNo))
                dbObj.PreGeneratedBarcodeNo = input.PreGeneratedBarcodeNo;

            _uowPatientLabTest.Repository.Update(dbObj);
            await _uowPatientLabTest.CommitAsync();
            return _mapper.Map<CreateOrEditPatientLabTestDto>(dbObj);

        }

        public async Task<CreateOrEditPatientLabTestDto> UpdateStatusOfReportOfConsignmentLabTest(UpdateConsignmentTestReportStatus input)
        {
            PatientLabTest? dbObj = new PatientLabTest();

            dbObj = await _uowPatientLabTest.Repository.GetById(input.PatientLabTestId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            dbObj!.IsConsignementLabTestReportApproved = input.IsConsignementLabTestReportApproved;
            dbObj!.ConsignmentLabTestStatusUpdatedBy = _tokenService.GetUserId();
            dbObj.ConsignmentLabTestStatusUpdatedOn = DateTime.Now;
            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientLabTest.Repository.Update(dbObj);
            await _uowPatientLabTest.CommitAsync();


            if (input.IsConsignementLabTestReportApproved)
                await sendLabTestReportViaSms(input.PatientLabTestId);

            return _mapper.Map<CreateOrEditPatientLabTestDto>(dbObj);

        }



        public async Task<List<ViewAnmonalTestDetail>> UpdateAnmonalLabTestPayementStatus(List<ViewAnmonalTestDetail> anmonalLabTestList)
        {
            int index = 0;
            foreach (var item in anmonalLabTestList)
            {
                //var anmonalLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == item.PatientLabTestId).FirstOrDefaultAsync();
                var anmonalLabTest = await _uowPatientLabTest.Repository.GetById(item.PatientLabTestId);

                if (AppCommonMethod.IsNullObject(anmonalLabTest))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                var labNo = "";
                if (AppCommonMethod.IsNullBool(item.IsRefunded))
                {
                    labNo = await _uowPatientLabTest.GetDbContext().ViewPatientLabTestLabNos.Where(x => !string.IsNullOrEmpty(x.LabNo) && TokenService.GetUserHfId() == x.HealthFacilityId).OrderByDescending(x => x.UpdatedOn).Select(x => x.LabNo).FirstOrDefaultAsync();
                    int seqNo = 1;
                    int currentMonth = DateTime.UtcNow.Month;
                    int monthOfLastTestEntry = 0;


                    if (string.IsNullOrEmpty(labNo))
                        labNo = AppCommonMethod.GenerateSequenceNumber(currentMonth, seqNo);
                    else if (!string.IsNullOrEmpty(labNo))
                        monthOfLastTestEntry = int.Parse(labNo.ToString().Split("-")[0]);
                    //else
                    //{
                    if (currentMonth != monthOfLastTestEntry)
                    {
                        seqNo = 1;
                        labNo = AppCommonMethod.GenerateSequenceNumber(currentMonth, seqNo);
                    }
                    else
                    {
                        labNo = labNo.ToString().Split("-")[1];
                        seqNo = int.Parse(labNo);
                        seqNo++;
                        labNo = AppCommonMethod.GenerateSequenceNumber(currentMonth, seqNo);
                    }
                    //}

                }
                else
                {
                    labNo = anmonalLabTest.LabNo;
                }

                anmonalLabTest!.LabNo = labNo;
                anmonalLabTestList[index].LabNo = labNo;
                anmonalLabTest.IsPaid = true;
                if (item.IsRefunded != true)
                {
                    anmonalLabTest.PaymentReceivedBy = _tokenService.GetUserId();
                    anmonalLabTest.PaymentReceivedOn = DateTime.Now;
                    anmonalLabTest.DiscountInPercentage = item.DiscountInPercentage;
                    anmonalLabTest.DiscountedPrice = item.DiscountedPrice;
                    anmonalLabTest.DiscountedByProfileId = item.DiscountedByProfileId;
                    FillEntity(anmonalLabTest);

                }
                else
                {
                    anmonalLabTest.IsRefunded = item.IsRefunded;
                    anmonalLabTest.RefundReason = item.RefundReason;
                    anmonalLabTest.PaymentRefundBy = item.IsRefunded == true ? _tokenService.GetUserId() : null;
                    anmonalLabTest.PaymentRefundOn = item.IsRefunded == true ? DateTime.Now : null;
                }


                _uowPatientLabTest.Repository.Update(anmonalLabTest);

                await _uowPatientLabTest.Save();
                index++;

            }

            return anmonalLabTestList;
        }


        public async Task<List<ViewAnmonalProcedurePaymentList>> UpdateAnmonalDentalProcedurePayementStatus(List<ViewAnmonalProcedurePaymentList> anmonalProcedureList)
        {
            if (!AppCommonMethod.IsNullOrEmptyList(anmonalProcedureList))
            {
                var visitId = anmonalProcedureList.Select(x => x.PatientVisitId).FirstOrDefault();
                var _uowPatientDiagnoseProcedure = new UnitOfWork<PatientDiagnoseProcedure>(_uowPatientLabTest.GetDbContext());
                var patientProcedureData = await _uowPatientDiagnoseProcedure.Repository.GetALL(x => x.PatientVisitId == visitId).ToListAsync();
                int index = 0;
                foreach (var procedure in anmonalProcedureList)
                {
                    if (!AppCommonMethod.IsNullBool(procedure!.IsPaidProcedureFee))
                    {
                        if ((bool)procedure!.IsPaidProcedureFee)
                        {
                            var data = patientProcedureData.Where(x => x.PatientDiagnoseProcedureId == procedure.PatientDiagnoseProcedureId).FirstOrDefault();
                            if (!AppCommonMethod.IsNullObject(data))
                            {
                                data.IsPaidProcedureFee = true;
                                data.ProcedureFee = procedure.ProcedureFee;
                                data.UpdatedBy = _tokenService.GetUserId();
                                data.UpdatedOn = DateTime.Now;
                                data.PaymentReceivedBy = _tokenService.GetUserId();
                                data.PaymentReceivedOn = DateTime.Now;
                                _uowPatientDiagnoseProcedure.Repository.Update(data);
                                await _uowPatientDiagnoseProcedure.Save();
                            }
                        }
                    }
                    ++index;
                }
            }
            return anmonalProcedureList;
        }
        #endregion

        #region Read Operations

        #region Anmonal Read Operation

        public async Task<AlmonalLabTestCountsDTO> GetAlmonerStat(FilterAnmonarDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var IsPrivateAlmoner = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.PrivateAlmoner) ?? null;

            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatientLabTest.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPAnmonalLabTestCounts]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure; ;
                    var userId = _tokenService.GetUserId();


                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

                    //sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                    //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                    sqlComm.Parameters.AddWithValue("@UserId", filter.IsOverAllDashboard ? null : userId);


                    if (!string.IsNullOrEmpty(filter.LabTestType))
                        sqlComm.Parameters.AddWithValue("@LabTestType", filter.LabTestType);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", 1);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!AppCommonMethod.IsNullObject(IsPrivateAlmoner))
                        sqlComm.Parameters.AddWithValue("@IsPrivate", true);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<AlmonalLabTestCountsDTO> lst = ds.Tables[0].ToList<AlmonalLabTestCountsDTO>();
                    if (lst.Count > 0)
                        return lst[0];
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }


                return null;
            }
        }


        public async Task<ViewPagerDto<ViewAnmonalList>> GetFilteredAnmonalListWithPagination(FilterAnmonarDto filter)
        {

            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            List<ViewAnmonalList> lst = new List<ViewAnmonalList>();
            var responseObject = new ViewPagerDto<ViewAnmonalList>();

            var conn = _uowPatientLabTest.GetDbContext().Database.GetDbConnection();
            try
            {
                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                //var _uowUser = new UnitOfWork<User>(_uowPatientLabTest.GetDbContext());
                //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPAnmonarList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

                //sqlComm.Parameters.AddWithValue("@startdate", startDate);
                //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ListType))
                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                if (!string.IsNullOrEmpty(filter.SearchString))
                    sqlComm.Parameters.AddWithValue("@SearchString", filter.SearchString);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                //    //if (!string.IsNullOrEmpty(filter.User))
                //    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<ViewAnmonalListTotalCount>();
                lst = ds.Tables[1].ToList<ViewAnmonalList>();


                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                responseObject.PageSize = filter.PageSize;
                responseObject.CurrentPage = filter.PageNumber;
                responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                responseObject.HasPrevious = filter.PageNumber > 1;
                responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
                responseObject.List = lst;

                return responseObject;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }


        public async Task<ViewPagerDto<ViewAnmonalProcedureList>> GetFilteredAnmonalProcedureListWithPagination(FilterProcedureDto filter)
        {

            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            List<ViewAnmonalProcedureList> lst = new List<ViewAnmonalProcedureList>();
            var responseObject = new ViewPagerDto<ViewAnmonalProcedureList>();

            var conn = _uowPatientLabTest.GetDbContext().Database.GetDbConnection();
            try
            {
                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPAlmonerProcedureList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

                //sqlComm.Parameters.AddWithValue("@startdate", startDate);
                //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ListType))
                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                if (!string.IsNullOrEmpty(filter.Cnic))
                    sqlComm.Parameters.AddWithValue("@Cnic", filter.Cnic);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                //    //if (!string.IsNullOrEmpty(filter.User))
                //    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<ViewAnmonalListTotalCount>();
                lst = ds.Tables[1].ToList<ViewAnmonalProcedureList>();


                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                responseObject.PageSize = filter.PageSize;
                responseObject.CurrentPage = filter.PageNumber;
                responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                responseObject.HasPrevious = filter.PageNumber > 1;
                responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
                responseObject.List = lst;

                return responseObject;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        //
        public async Task<ViewPagerDto<ViewAnmonalProcedurePaymentList>> GetFilteredViewAnmonalProcedurePaymentList(FilterProcedureDto filter)
        {
            List<ViewAnmonalProcedurePaymentList> lst = new List<ViewAnmonalProcedurePaymentList>();
            var responseObject = new ViewPagerDto<ViewAnmonalProcedurePaymentList>();

            var conn = _uowPatientLabTest.GetDbContext().Database.GetDbConnection();
            try
            {
                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPAlmonerProcedureListForPyament]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ListType))
                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                if (!AppCommonMethod.IsNullOrEmptyGuid(filter.PatientVisitId))
                    sqlComm.Parameters.AddWithValue("@PatientVisitId", filter.PatientVisitId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                lst = ds.Tables[0].ToList<ViewAnmonalProcedurePaymentList>();
                responseObject.List = lst;

                return responseObject;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        //
        public async Task<ViewPagerDto<ViewAnmonalDashboardList>> GeAnmonalDashboardListWithPagination(FilterAnmonarDto filter)
        {

            var loginUser = TokenService.GetUserLoggedInfo();
            var IsPrivateAlmoner = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.PrivateAlmoner) ?? null;

            List<ViewAnmonalDashboardList> lst = new List<ViewAnmonalDashboardList>();
            var responseObject = new ViewPagerDto<ViewAnmonalDashboardList>();

            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            var conn = _uowPatientLabTest.GetDbContext().Database.GetDbConnection();
            try
            {
                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPAlmonarDasboardList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

                //sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                //sqlComm.Parameters.AddWithValue("@EndDate", endDate);

                sqlComm.Parameters.AddWithValue("@UserId", filter.IsOverAllDashboard ? null : _tokenService.GetUserId());
                if (!string.IsNullOrEmpty(filter.LabTestType))
                    sqlComm.Parameters.AddWithValue("@LabTestType", filter.LabTestType);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    sqlComm.Parameters.AddWithValue("@DeplistHeaderNameartmentId", filter.DepartmentId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ListType))
                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                if (!AppCommonMethod.IsNullObject(IsPrivateAlmoner))
                    sqlComm.Parameters.AddWithValue("@IsPrivate", true);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<ViewAnmonalListTotalCount>();
                lst = ds.Tables[1].ToList<ViewAnmonalDashboardList>();


                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                responseObject.PageSize = filter.PageSize;
                responseObject.CurrentPage = filter.PageNumber;
                responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                responseObject.HasPrevious = filter.PageNumber > 1;
                responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
                responseObject.List = lst;

                return responseObject;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public async Task<List<ViewAnmonalTestDetail>> GetFilteredAnmonalLabTestDetailByVisitId(Guid PatientVisitId, int ListType)
        {

            var data = await _uowPatientLabTest.GetDbContext().ViewAnmonalTestDetails
                .Where(x => x.PatientVisitId == PatientVisitId && x.PaidStatus != true && x.LabDepartmentShortName == CommonStringConstant.InternalLabTest)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.UnPaid_Pathalogy), x => (x.TestTypeShortName != CommonStringConstant.LabTypeXray && x.TestTypeShortName != CommonStringConstant.LabTypeUltrasound))
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.UnPaid_Radiology), x => (x.TestTypeShortName == CommonStringConstant.LabTypeXray || x.TestTypeShortName == CommonStringConstant.LabTypeUltrasound))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.UnPaid_Private), x => x.IsPerformedPrivately == true)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.UnPaid_Pathalogy), x => x.IsPerformedPrivately != true)
                .OrderBy(x => x.TestTypeShortName)
                .ThenBy(x => x.SectionName)
                .ThenBy(x => x.TestName)
                .ToListAsync();
            return data;
        }

        public async Task<List<ViewAnmonalTestDetail>> GetFilteredAnmonalPaidLabTestDetailByVisitId(Guid PatientVisitId, int ListType)
        {

            var data = await _uowPatientLabTest.GetDbContext().ViewAnmonalTestDetails
                .Where(x => x.PatientVisitId == PatientVisitId && x.PaidStatus == true && x.LabDepartmentShortName == CommonStringConstant.InternalLabTest)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.Paid_Pathalogy), x => (x.TestTypeShortName != CommonStringConstant.LabTypeXray && x.TestTypeShortName != CommonStringConstant.LabTypeUltrasound))
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.Paid_Radiology), x => (x.TestTypeShortName == CommonStringConstant.LabTypeXray || x.TestTypeShortName == CommonStringConstant.LabTypeUltrasound))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.Paid_Private), x => x.IsPerformedPrivately == true)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(ListType) && (ListType == (int)AlmonerListTypeEnums.Paid_Pathalogy), x => x.IsPerformedPrivately != true)
                .OrderBy(x => x.TestTypeShortName)
                .ThenBy(x => x.SectionName)
                .ThenBy(x => x.TestName)
                .ToListAsync();
            return data;
        }


        #endregion Anmonal Read Operation

        public async Task<List<ViewPatientVisitLabTestListDto>> GetAllTestListByVisitId(Guid PatientVisitId)
        {
            var patientLabTestList = _uowPatientLabTest.Repository
                .GetALL(x => x.ActionTypeId != 3 && x.PatientVisitId == PatientVisitId)
                .Select(x => new ViewPatientVisitLabTestListDto
                {
                    PatientLabTestId = x.PatientLabTestId,
                    PatientId = x.PatientId,
                    PatientVisitId = x.PatientVisitId,
                    LabDepartmentProfileId = x.LabDepartmentProfileId,
                    LabDepartmentName = x.LabDepartmentProfile!.Name,
                    DepartmentShortName = x.LabDepartmentProfile!.ShortName,

                    BarcodeNo = x.BarcodeNo,

                    LabTestId = x.LabTestId,
                    LabTestName = x.LabTest!.Name,

                    LabTestTypeName = x.LabTest.LabTestTypeProfile!.Name,
                    LabTestTypeShortName = x.LabTest!.LabTestTypeProfile.ShortName,
                    LabTestImage = x.ResultImageLink,

                    AdvisedBy = x.TestAdvisedByNavigation!.FullName,
                    AdvisedOn = x.CreatedOn,

                    IsSampleCollected = x.IsSampleCollected,
                    SampleCollectedBy = x.SampleCollectedByNavigation!.FullName,
                    SampleCollectedOn = x.SampleCollectedOn,

                    IsSampleRejected = x.IsSampleRejected,
                    SampleRejectedBy = x.SampleRejectedByNavigation!.FullName,
                    SampleRejectedOn = x.SampleRejectedOn,

                    IsReportGenerated = x.IsReportGenerated,
                    ReportGeneratedBy = x.ReportGeneratedByNavigation!.FullName,
                    ReportGeneratedOn = x.ReportGeneratedOn,

                    TestPrice = x.LabTest.TestPrice!,

                    IsActive = x.IsActive,

                })
                .ToList();

            return patientLabTestList;
        }


        public async Task<ViewPatientLabTestListDto> GetTbPatientLabTestByPatientId(Guid PatientId)
        {
            var dbObj = await _uowPatientLabTest.GetDbContext().ViewPatientLabTestLists
                .Where(x =>
                        x.PatientId == PatientId
                    && x.IsSampleCollected == true
                    && x.IsReportGenerated == null
                    && x.IsSampleRejected == null
                    && x.LabTestName == CommonStringConstant.CXR

                    )
                .OrderByDescending(x => x.AdvisedOn)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.SampleNotCollected);


            var IQueryableList = new ViewPatientLabTestListDto
            {
                PatientLabTestId = dbObj.PatientLabTestId,
                PatientId = dbObj.PatientId,
                PatientVisitId = dbObj.PatientVisitId,

                LabTestId = dbObj.LabTestId,
                BarcodeNo = dbObj.BarcodeNo,
                LabDepartmentName = dbObj.LabDepartmentName,
                Cnic = dbObj.Cnic,
                MrNo = dbObj.MrNo,
                PatientMobileNo = dbObj.PatientMobileNo,

                PatientName = dbObj.PatientName,
                LabTestName = dbObj.LabTestName,
                AdvisedBy = dbObj.AdvisedBy,
                AdvisedOn = dbObj.AdvisedOn,
                TestPrice = dbObj.TestPrice,

                SampleType = dbObj.SampleType,

                IsSampleCollected = dbObj.IsSampleCollected,
                SampleCollectedBy = dbObj.SampleCollectedBy,
                SampleCollectedOn = dbObj.SampleCollectedOn,

                IsReportGenerated = dbObj.IsReportGenerated,
                ReportGeneratedBy = dbObj.ReportGeneratedBy,
                ReportGeneratedOn = dbObj.ReportGeneratedOn,

                IsSampleRejected = dbObj.IsSampleRejected,
                SampleRejectedBy = dbObj.SampleRejectedBy,
                SampleRejectedOn = dbObj.SampleRejectedOn,

                IsAdvisedExternally = dbObj.IsAdvisedExternally,

                SourceDoctorName = dbObj.SourceDoctorName,

                SampleRejectedReason = dbObj.SampleRejectedReason!,

            };

            return IQueryableList;
        }
        public async Task<ViewPagerDto<ViewPatientLabTestListDto>> GetAllPatientLabTestByFilters(FilterViewPatientLabTestListDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var IsRadiologist = false;
            if (!AppCommonMethod.IsNullBool(filter.IsRadiologyReportTab) && filter.IsRadiologyReportTab == true)
                IsRadiologist = true;
            //var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

            var IsPrivateTechnologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.PrivatePathology) ?? null;

            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
            var ReasonsList = new List<DbModel.Profile>();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            if (filter.Stage == CommonStringConstant.SampleRejected)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var startDate = "";
            var endDate = "";

            // Parse the input string using the specified format
            if (filter.StartDate != null)
                startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            if (filter.EndDate != null)
                endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            List<SPPatientLabTestListDto> lst = new List<SPPatientLabTestListDto>();
            var responseObject = new ViewPagerDto<ViewPatientLabTestListDto>();

            var conn = _uowPatientLabTest.GetDbContext().Database.GetDbConnection();
            try
            {
                //if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                //    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                //var _uowUser = new UnitOfWork<User>(_uowPatientLabTest.GetDbContext());
                //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPPatientLabTestList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

                if (!string.IsNullOrEmpty(startDate))
                    sqlComm.Parameters.AddWithValue("@startdate", startDate);
                if (!string.IsNullOrEmpty(endDate))
                    sqlComm.Parameters.AddWithValue("@enddate", endDate);

                //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                if (!string.IsNullOrEmpty(filter.SearchString))
                    sqlComm.Parameters.AddWithValue("@SearchString", filter.SearchString);


                if (!AppCommonMethod.IsNullBool(filter.IsConsignmentCreated))
                    sqlComm.Parameters.AddWithValue("@IsConsignmentCreated", filter.IsConsignmentCreated);

                if (!AppCommonMethod.IsNullorZeroInt(filter.OutSourceConsignmentHealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@OutSourceConsignmentHealthFacilityId", filter.OutSourceConsignmentHealthFacilityId);

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.RadiologyReport, x => x.LabTypeShortName == CommonStringConstant.LabTypeXray || x.LabTypeShortName == CommonStringConstant.LabTypeUltrasound)
                //.WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray || x.LabTypeShortName == CommonStringConstant.LabTypeUltrasound && x.IsPaid == true)

                if (!AppCommonMethod.IsNullBool(filter.Rider))
                    sqlComm.Parameters.AddWithValue("@Rider", filter.Rider);

                if (!AppCommonMethod.IsNullorZeroInt(filter.Stage))
                    sqlComm.Parameters.AddWithValue("@Stage", filter.Stage);

                if (!AppCommonMethod.IsNullBool(IsRadiologist) && IsRadiologist == true)
                    sqlComm.Parameters.AddWithValue("@IsRadiologist", true);

                if (!AppCommonMethod.IsNullObject(IsPrivateTechnologist))
                    sqlComm.Parameters.AddWithValue("@IsPrivate", true);

                //.WhereIf(AppCommonMethod.IsNullObject(IsRadiologist) && filter.Stage != CommonStringConstant.RadiologyReport, x => x.LabTypeShortName != CommonStringConstant.LabTypeXray && x.LabTypeShortName != CommonStringConstant.LabTypeUltrasound)

                if (!AppCommonMethod.IsNullBool(filter.IsExternalSource))
                    sqlComm.Parameters.AddWithValue("@IsExternalSource", filter.IsExternalSource);

                if (!AppCommonMethod.IsNullBool(filter.IsArchive))
                    sqlComm.Parameters.AddWithValue("@IsArchive", filter.IsArchive);



                //.WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
                //.WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))

                //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                //    //if (!string.IsNullOrEmpty(filter.User))
                //    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<SPPatientLabTestListTotalCount>();
                lst = ds.Tables[1].ToList<SPPatientLabTestListDto>();

                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();

                //return responseObject;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }





            //responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
            responseObject.PageSize = filter.PageSize;
            responseObject.CurrentPage = filter.PageNumber;
            responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
            responseObject.HasPrevious = filter.PageNumber > 1;
            responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
            responseObject.List = _mapper.Map<List<ViewPatientLabTestListDto>>(lst);
            //responseObject.List = lst;

            foreach (var item in responseObject.List)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReasonName = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }


            return responseObject;
        }
        //public async Task<ViewPagerDto<ViewPatientLabTestListDto>> GetAllPatientLabTestByFilters(FilterViewPatientLabTestListDto filter)
        //{
        //    var loginUser = TokenService.GetUserLoggedInfo();
        //    var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

        //    //var barcode = _barcodeHandler.GenerateBarcode("0324-1234-000000000011", 37, 25, "TestBarcode");

        //    var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
        //    var ReasonsList = new List<DbModel.Profile>();

        //    List<string> user = new List<string>();
        //    if (filter.User != null)
        //        user = filter!.User!.Split(',').ToList();

        //    if (filter.Stage == CommonStringConstant.SampleRejected)
        //        ReasonsList = _uowProfile.Repository.GetALL()
        //            .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
        //            .ToList();

        //    var list = _uowPatientLabTest.GetDbContext().ViewPatientLabTestLists
        //        .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest)

        //        //.WhereIf(!string.IsNullOrEmpty(filter.SearchString),
        //        //x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
        //        //x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
        //        //x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
        //        //x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
        //        //x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
        //        //x.PatientName!.ToLower().StartsWith(filter.SearchString))

        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByCNIC, x => x.Cnic.ToLower() == filter.SearchString)
        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMobileNo, x => x.PatientMobileNo.ToLower() == filter.SearchString)
        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMrNo, x => x.MrNo.ToLower() == filter.SearchString)
        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBarcode, x => x.BarcodeNo.ToLower() == filter.SearchString || x.PreGeneratedBarcodeNo.ToLower() == filter.SearchString)


        //        .WhereIf(!AppCommonMethod.IsNullBool(filter.IsConsignmentCreated) && filter.IsConsignmentCreated == true, x => x.SampleConsignmentDetailId == null)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.OutSourceConsignmentHealthFacilityId), x => x.OutSourceConsignmentHealthFacilityId == filter.OutSourceConsignmentHealthFacilityId)

        //        //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.AllCollection, x => x.IsPending != true && x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

        //        .WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

        //        .WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true && x.IsPaid == true)

        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true)

        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)

        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.RadiologyReport, x => x.LabTypeShortName == CommonStringConstant.LabTypeXray || x.LabTypeShortName == CommonStringConstant.LabTypeUltrasound)
        //        .WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray || x.LabTypeShortName == CommonStringConstant.LabTypeUltrasound)

        //        .WhereIf(AppCommonMethod.IsNullObject(IsRadiologist) && filter.Stage != CommonStringConstant.RadiologyReport, x => x.LabTypeShortName != CommonStringConstant.LabTypeXray && x.LabTypeShortName != CommonStringConstant.LabTypeUltrasound)

        //        .WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

        //        .WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)

        //        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
        //        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
        //        .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
        //        .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
        //        .OrderByDescending(x => x.AdvisedOn);




        //    IQueryable<ViewPatientLabTestListDto> IQueryableList = list.Select(x =>
        //       new ViewPatientLabTestListDto
        //       {
        //           PatientLabTestId = x.PatientLabTestId,
        //           PatientId = x.PatientId,
        //           PatientVisitId = x.PatientVisitId,

        //           StageName = x.StageName,
        //           StatusCode = x.StatusCode,
        //           StatusName = x.StatusName,

        //           LabTestId = x.LabTestId,
        //           IsPaid = x.IsPaid,
        //           IsEdit = x.IsEdit,
        //           BarcodeNo = x.BarcodeNo,
        //           PreGenratedBarcodeNo = x.PreGeneratedBarcodeNo,
        //           LabDepartmentName = x.LabDepartmentName,
        //           Cnic = x.Cnic,
        //           MrNo = x.MrNo,
        //           PatientMobileNo = x.PatientMobileNo,
        //           PatientName = x.PatientName,
        //           LabTestName = x.LabTestName,
        //           AdvisedBy = x.AdvisedBy,
        //           AdvisedOn = x.AdvisedOn,
        //           TestPrice = x.TestPrice,
        //           SampleType = x.SampleType,
        //           DoctorDepartmentName = x.DocDepartmentName,
        //           DoctorSectionName = x.DocSectionName,

        //           IsOnBedSample = x.IsOnBedSample,

        //           IsFromCallCenter = x.IsFromCallCenter,
        //           IsSampleRequired = x.IsSampleRequired,

        //           IsSampleCollected = x.IsSampleCollected,
        //           SampleCollectedBy = x.SampleCollectedBy,
        //           SampleCollectedOn = x.SampleCollectedOn,

        //           IsReportGenerated = x.IsReportGenerated,
        //           ReportGeneratedBy = x.ReportGeneratedBy,
        //           ReportGeneratedOn = x.ReportGeneratedOn,

        //           IsSampleRejected = x.IsSampleRejected,
        //           SampleRejectedBy = x.SampleRejectedBy,
        //           SampleRejectedOn = x.SampleRejectedOn,

        //           IsAdvisedExternally = x.IsAdvisedExternally,

        //           SourceDoctorName = x.SourceDoctorName,
        //           HealthFacilityName = x.HealthFacilityName,

        //           SampleRejectedReason = x.SampleRejectedReason!,

        //           LabType = x.LabType,
        //           LabTypeShortName = x.LabTypeShortName,
        //           ReportLink = x.ReportLink,
        //           ResultImageLink = x.ResultImageLink,
        //           SampleConsignmentDetailId = x.SampleConsignmentDetailId,
        //           ConsignmentLabTestStatusUpdatedBy = x.ConsignmentLabTestStatusUpdatedBy
        //       });

        //    var pagedList = await PagedListDto<ViewPatientLabTestListDto>.ToPagedListAsync(
        //           IQueryableList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    foreach (var item in pagedList)
        //    {
        //        if (!string.IsNullOrEmpty(item.SampleRejectedReason))
        //        {
        //            var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

        //            item.SampleRejectedReasonName = string.Join(',', ReasonsList
        //                .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
        //        }
        //    }


        //    var responseObject = new ViewPagerDto<ViewPatientLabTestListDto>
        //    {
        //        TotalCount = pagedList.TotalCount,
        //        PageSize = pagedList.PageSize,
        //        CurrentPage = pagedList.CurrentPage,
        //        TotalPages = pagedList.TotalPages,
        //        HasNext = pagedList.HasNext,
        //        HasPrevious = pagedList.HasPrevious,
        //        List = pagedList
        //    };

        //    return responseObject;
        //}
        public async Task<ViewPagerDto<ViewPatientLabTestListDto>> getAllPatientTestListForBatch(FilterViewPatientLabTestListDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

            //var barcode = _barcodeHandler.GenerateBarcode("0324-1234-000000000011", 37, 25, "TestBarcode");

            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
            var ReasonsList = new List<DbModel.Profile>();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            if (filter.Stage == CommonStringConstant.SampleRejected)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var list = _uowPatientLabTest.GetDbContext().ViewPatientLabTestLists
                .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest && x.BatchNumber == null)

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
                x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
                x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PatientName!.ToLower().StartsWith(filter.SearchString))


                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.AllCollection, x => x.IsPending != true && x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



                .WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
                .OrderByDescending(x => x.AdvisedOn);

            IQueryable<ViewPatientLabTestListDto> IQueryableList = list.Select(x =>
               new ViewPatientLabTestListDto
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   StageName = x.StageName,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   LabTestId = x.LabTestId,
                   IsEdit = x.IsEdit,
                   BarcodeNo = x.BarcodeNo,
                   PreGenratedBarcodeNo = x.PreGeneratedBarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,
                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,
                   AdvisedBy = x.AdvisedBy,
                   AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,
                   DoctorDepartmentName = x.DocDepartmentName,
                   DoctorSectionName = x.DocSectionName,
                   IsFromCallCenter = x.IsFromCallCenter,
                   IsSampleRequired = x.IsSampleRequired,

                   IsSampleCollected = x.IsSampleCollected,
                   SampleCollectedBy = x.SampleCollectedBy,
                   SampleCollectedOn = x.SampleCollectedOn,

                   IsReportGenerated = x.IsReportGenerated,
                   ReportGeneratedBy = x.ReportGeneratedBy,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedBy,
                   SampleRejectedOn = x.SampleRejectedOn,

                   IsAdvisedExternally = x.IsAdvisedExternally,

                   SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   LabType = x.LabType,
                   LabTypeShortName = x.LabTypeShortName,
                   ReportLink = x.ReportLink,
                   ResultImageLink = x.ResultImageLink,
                   SampleConsignmentDetailId = x.SampleConsignmentDetailId,
                   ConsignmentLabTestStatusUpdatedBy = x.ConsignmentLabTestStatusUpdatedBy,
                   BatchNumber = x.BatchNumber
               });

            var pagedList = await PagedListDto<ViewPatientLabTestListDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReasonName = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }


            var responseObject = new ViewPagerDto<ViewPatientLabTestListDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }
        public async Task<IQueryable<ViewPatientLabTestListDto>> GetCollectedSamplespatientTestList(FilterViewPatientLabTestListDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;


            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
            var ReasonsList = new List<DbModel.Profile>();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            if (filter.Stage == CommonStringConstant.SampleRejected)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var list = _uowPatientLabTest.GetDbContext().ViewPatientLabTestLists
                .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest && x.BatchNumber == filter.BatchNumber)

                //.WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                //x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
                //x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PatientName!.ToLower().StartsWith(filter.SearchString))


                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



                //.WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)

                //.WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
                //.WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
                //.WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
                //.WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
                //.OrderByDescending(x => x.AdvisedOn)
                ;

            IQueryable<ViewPatientLabTestListDto> IQueryableList = list.Select(x =>
               new ViewPatientLabTestListDto
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   StageName = x.StageName,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   LabTestId = x.LabTestId,
                   IsEdit = x.IsEdit,
                   BarcodeNo = x.BarcodeNo,
                   PreGenratedBarcodeNo = x.PreGeneratedBarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,
                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,
                   AdvisedBy = x.AdvisedBy,
                   AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,
                   DoctorDepartmentName = x.DocDepartmentName,
                   DoctorSectionName = x.DocSectionName,
                   IsFromCallCenter = x.IsFromCallCenter,
                   IsSampleRequired = x.IsSampleRequired,

                   IsSampleCollected = x.IsSampleCollected,
                   SampleCollectedBy = x.SampleCollectedBy,
                   SampleCollectedOn = x.SampleCollectedOn,

                   IsReportGenerated = x.IsReportGenerated,
                   ReportGeneratedBy = x.ReportGeneratedBy,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedBy,
                   SampleRejectedOn = x.SampleRejectedOn,

                   IsAdvisedExternally = x.IsAdvisedExternally,

                   SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   LabType = x.LabType,
                   LabTypeShortName = x.LabTypeShortName,
                   ReportLink = x.ReportLink,
                   ResultImageLink = x.ResultImageLink,
                   SampleConsignmentDetailId = x.SampleConsignmentDetailId,
                   ConsignmentLabTestStatusUpdatedBy = x.ConsignmentLabTestStatusUpdatedBy,
                   BatchNumber = x.BatchNumber
               });

            //var pagedList = await PagedListDto<ViewPatientLabTestListDto>.ToPagedListAsync(
            //       IQueryableList,
            //       filter.PageNumber,
            //       filter.PageSize
            //       );

            foreach (var item in IQueryableList)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReasonName = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }


            //var responseObject = new ViewPagerDto<ViewPatientLabTestListDto>
            //{
            //    TotalCount = pagedList.TotalCount,
            //    PageSize = pagedList.PageSize,
            //    CurrentPage = pagedList.CurrentPage,
            //    TotalPages = pagedList.TotalPages,
            //    HasNext = pagedList.HasNext,
            //    HasPrevious = pagedList.HasPrevious,
            //    List = pagedList
            //};

            //return responseObject;
            return IQueryableList;
        }
        public async Task<int> GetRecentBatchName()
        {
            var totalBatchCreated = _uowPatientLabTest.Repository.GetALL(x => x.BatchNumber != null).GroupBy(x => x.BatchNumber).Count();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //return _mapper.Map<CreateOrEditPatientLabTestDto>(responseObj);
            return totalBatchCreated;
            //var loginUser = TokenService.GetUserLoggedInfo();
            //var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

            ////var barcode = _barcodeHandler.GenerateBarcode("0324-1234-000000000011", 37, 25, "TestBarcode");

            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
            //var ReasonsList = new List<DbModel.Profile>();

            //List<string> user = new List<string>();
            //if (filter.User != null)
            //    user = filter!.User!.Split(',').ToList();

            //if (filter.Stage == CommonStringConstant.SampleRejected)
            //    ReasonsList = _uowProfile.Repository.GetALL()
            //        .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
            //        .ToList();

            //var list = _uowPatientLabTest.GetDbContext().ViewPatientLabTestLists
            //    .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest)

            //    .WhereIf(!string.IsNullOrEmpty(filter.SearchString),
            //    x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
            //    x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
            //    x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
            //    x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
            //    x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
            //    x.PatientName!.ToLower().StartsWith(filter.SearchString))


            //    //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.AllCollection, x => x.IsPending != true && x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

            //    .WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

            //    .WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true)

            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



            //    .WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

            //    .WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

            //    .WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)

            //    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
            //    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
            //    .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
            //    .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
            //    .OrderByDescending(x => x.AdvisedOn);

            //IQueryable<ViewPatientLabTestListDto> IQueryableList = list.Select(x =>
            //   new ViewPatientLabTestListDto
            //   {
            //       PatientLabTestId = x.PatientLabTestId,
            //       PatientId = x.PatientId,
            //       PatientVisitId = x.PatientVisitId,

            //       StageName = x.StageName,
            //       StatusCode = x.StatusCode,
            //       StatusName = x.StatusName,

            //       LabTestId = x.LabTestId,
            //       IsEdit = x.IsEdit,
            //       BarcodeNo = x.BarcodeNo,
            //       PreGenratedBarcodeNo = x.PreGeneratedBarcodeNo,
            //       LabDepartmentName = x.LabDepartmentName,
            //       Cnic = x.Cnic,
            //       MrNo = x.MrNo,
            //       PatientMobileNo = x.PatientMobileNo,
            //       PatientName = x.PatientName,
            //       LabTestName = x.LabTestName,
            //       AdvisedBy = x.AdvisedBy,
            //       AdvisedOn = x.AdvisedOn,
            //       TestPrice = x.TestPrice,
            //       SampleType = x.SampleType,
            //       DoctorDepartmentName = x.DocDepartmentName,
            //       DoctorSectionName = x.DocSectionName,
            //       IsFromCallCenter = x.IsFromCallCenter,
            //       IsSampleRequired = x.IsSampleRequired,

            //       IsSampleCollected = x.IsSampleCollected,
            //       SampleCollectedBy = x.SampleCollectedBy,
            //       SampleCollectedOn = x.SampleCollectedOn,

            //       IsReportGenerated = x.IsReportGenerated,
            //       ReportGeneratedBy = x.ReportGeneratedBy,
            //       ReportGeneratedOn = x.ReportGeneratedOn,

            //       IsSampleRejected = x.IsSampleRejected,
            //       SampleRejectedBy = x.SampleRejectedBy,
            //       SampleRejectedOn = x.SampleRejectedOn,

            //       IsAdvisedExternally = x.IsAdvisedExternally,

            //       SourceDoctorName = x.SourceDoctorName,
            //       HealthFacilityName = x.HealthFacilityName,

            //       SampleRejectedReason = x.SampleRejectedReason!,

            //       LabType = x.LabType,
            //       LabTypeShortName = x.LabTypeShortName,
            //       ReportLink = x.ReportLink,
            //       ResultImageLink = x.ResultImageLink,
            //       SampleConsignmentDetailId = x.SampleConsignmentDetailId,
            //       ConsignmentLabTestStatusUpdatedBy = x.ConsignmentLabTestStatusUpdatedBy
            //   });

            //var pagedList = await PagedListDto<ViewPatientLabTestListDto>.ToPagedListAsync(
            //       IQueryableList,
            //       filter.PageNumber,
            //       filter.PageSize
            //       );

            //foreach (var item in pagedList)
            //{
            //    if (!string.IsNullOrEmpty(item.SampleRejectedReason))
            //    {
            //        var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

            //        item.SampleRejectedReasonName = string.Join(',', ReasonsList
            //            .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
            //    }
            //}


            //var responseObject = new ViewPagerDto<ViewPatientLabTestListDto>
            //{
            //    TotalCount = pagedList.TotalCount,
            //    PageSize = pagedList.PageSize,
            //    CurrentPage = pagedList.CurrentPage,
            //    TotalPages = pagedList.TotalPages,
            //    HasNext = pagedList.HasNext,
            //    HasPrevious = pagedList.HasPrevious,
            //    List = pagedList
            //};

            //return responseObject;
        }

        public async Task<ViewPagerDto<ViewPatientLabTestListDto>> GetAllPatientLabTestByVisit(PatientLabTestByVisitDTO patientHistoryLabTestDTO)
        {


            var list = _uowPatientLabTest.GetDbContext().ViewPatientLabTestLists

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(patientHistoryLabTestDTO.Stage) && patientHistoryLabTestDTO.Stage == CommonStringConstant.PendingCollection,
                 x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest
                 && x.PatientVisitId == patientHistoryLabTestDTO.PatientVisitId
                 && x.IsSampleCollected != true
                 && x.IsReportGenerated != true
                 && x.IsSampleRejected != true)


                .WhereIf(!AppCommonMethod.IsNullorZeroInt(patientHistoryLabTestDTO.Stage) && patientHistoryLabTestDTO.Stage == CommonStringConstant.SampleCollected,
                x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest
                && x.PatientVisitId == patientHistoryLabTestDTO.PatientVisitId
                && x.IsSampleCollected == true
                && x.IsSampleCollected == true
                && x.IsReportGenerated != true)
                .OrderByDescending(x => x.AdvisedOn);

            IQueryable<ViewPatientLabTestListDto> IQueryableList = list.Select(x =>
               new ViewPatientLabTestListDto
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   StageName = x.StageName,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   LabTestId = x.LabTestId,
                   IsEdit = x.IsEdit,
                   BarcodeNo = x.BarcodeNo,
                   PreGenratedBarcodeNo = x.PreGeneratedBarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,
                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,
                   AdvisedBy = x.AdvisedBy,
                   AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,
                   DoctorDepartmentName = x.DocDepartmentName,
                   DoctorSectionName = x.DocSectionName,
                   IsFromCallCenter = x.IsFromCallCenter,
                   IsSampleRequired = x.IsSampleRequired,

                   IsSampleCollected = x.IsSampleCollected,
                   SampleCollectedBy = x.SampleCollectedBy,
                   SampleCollectedOn = x.SampleCollectedOn,

                   IsReportGenerated = x.IsReportGenerated,
                   ReportGeneratedBy = x.ReportGeneratedBy,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedBy,
                   SampleRejectedOn = x.SampleRejectedOn,

                   IsAdvisedExternally = x.IsAdvisedExternally,

                   SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   LabType = x.LabType,
                   LabTypeShortName = x.LabTypeShortName,
                   ReportLink = x.ReportLink,
                   ResultImageLink = x.ResultImageLink,
                   SampleConsignmentDetailId = x.SampleConsignmentDetailId,
                   ConsignmentLabTestStatusUpdatedBy = x.ConsignmentLabTestStatusUpdatedBy
               });

            var pagedList = await PagedListDto<ViewPatientLabTestListDto>.ToPagedListAsync(
                   IQueryableList,
                   patientHistoryLabTestDTO.PageNumber,
                   patientHistoryLabTestDTO.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewPatientLabTestListDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }

        public async Task<ViewPagerDto<ViewRiderLabTestListDto>> GetAllRiderTestList(RiderPatientLabTestListDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
            var ReasonsList = new List<DbModel.Profile>();

            if (AppCommonMethod.IsNullObject(loginUser))
                throw new UserFriendlyException(CommonStringConstant.InvalidToken);

            if (filter.Status == (int)RiderLabTestStatusEnum.Cancelled)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var list = _uowPatientLabTest.GetDbContext().ViewRiderLabTestLists
                .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest
                && x.RiderUserId == loginUser.UserId
                && x.IsSampleRequired == true
                && x.IsAdvisedExternally != true
                )

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
                x.Cnic.Replace("-", "").ToLower().StartsWith(filter.SearchString) ||
                x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
                x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString))

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Status), x => x.StatusCode == filter.Status)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Stage) && filter.Stage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



                //.WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)


                .WhereIf(!AppCommonMethod.IsNullorZeroInt(loginUser.ProvinceId), x => x.ProvinceId == loginUser.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(loginUser.DivisionId), x => x.DivisionId == loginUser.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(loginUser.DistrictId), x => x.DistrictId == loginUser.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(loginUser.TehsilId), x => x.TehsilId == loginUser.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(loginUser.HealthFacilityId), x => x.HealthFacilityId == loginUser.HealthFacilityId)

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)

                //.OrderByDescending(x => x.AdvisedOn)
                ;

            IQueryable<ViewRiderLabTestListDto> IQueryableList = list.Select(x =>
               new ViewRiderLabTestListDto
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   //PatientVisitId = x.PatientVisitId,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   //LabTestId = x.LabTestId,
                   BarcodeNo = x.BarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,

                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,

                   //AdvisedBy = x.AdvisedBy,
                   //AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,

                   //IsSampleRequired = x.IsSampleRequired,

                   //IsSampleCollected = x.IsSampleCollected,
                   //SampleCollectedBy = x.SampleCollectedBy,
                   //SampleCollectedOn = x.SampleCollectedOn,

                   //IsReportGenerated = x.IsReportGenerated,
                   //ReportGeneratedBy = x.ReportGeneratedBy,
                   //ReportGeneratedOn = x.ReportGeneratedOn,

                   //IsSampleRejected = x.IsSampleRejected,
                   //SampleRejectedBy = x.SampleRejectedBy,
                   //SampleRejectedOn = x.SampleRejectedOn,

                   //IsAdvisedExternally = x.IsAdvisedExternally,

                   //SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   //LabType = x.LabType,
                   //LabTypeShortName = x.LabTypeShortName,
                   //ReportLink = x.ReportLink,
                   //ResultImageLink = x.ResultImageLink

               });

            var pagedList = await PagedListDto<ViewRiderLabTestListDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReason = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }


            var responseObject = new ViewPagerDto<ViewRiderLabTestListDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }

        public async Task<ViewPathalogyRegistrationSlipDto> GetByPatientLabTestIdForRegistrationSlip(Guid PatientLabTestId)
        {

            var responseObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == PatientLabTestId)
                .Select(x => new ViewPathalogyRegistrationSlipDto
                {

                    HealthFacilityName = x.PatientVisit!.HealthFacility!.Name!,
                    TokenNo = x.PatientVisit!.TokenNo!,

                    PatientVisitid = x.PatientVisitId,
                    VisitNo = x.PatientVisit.VisitNo,
                    VisitDate = x.PatientVisit.VisitDate,
                    SampleTransportMode = x.SampleTransportMode,
                    PatientName = x.Patient!.FullName!,
                    MrNo = x.Patient.Mrno,
                    Cnic = x.Patient.Cnic,
                    MobileNo = x.Patient.MobileNo,
                    Gender = x.Patient.GenderProfile!.Name,
                    Age = x.Patient.Age,
                    Dob = x.Patient.Dob,

                    TestId = x.LabTestId,
                    BarcodeNo = x.BarcodeNo,
                    TestName = x.LabTest!.Name!,
                    AdvisedBy = x.TestAdvisedByNavigation!.FullName!,
                    AdvisedOn = x.CreatedOn,

                    SampleCollectedby = x.SampleCollectedByNavigation!.FullName!,
                    SampleCollectedOn = x.SampleCollectedOn,

                })
                .FirstOrDefaultAsync();
            return responseObj!;

        }

        public async Task<ViewPatientInfoWithLabTestDetailDto> GetAllPatientLabTestDetailByPatientLabTestId(Guid PatientLabTestId)
        {
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientLabTest.GetDbContext());
            var _uowLabTestDetail = new UnitOfWork<DbModel.LabTestDetail>(_uowPatientLabTest.GetDbContext());

            //List<DbModel.Profile> reasonsList = _uowProfile.Repository.GetALL()
            //        .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
            //        .ToList();

            var responseObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == PatientLabTestId)
               .Select(x => new ViewPatientInfoWithLabTestDetailDto
               {

                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   PatientVisitDate = x.PatientVisit!.VisitDate,

                   HealthFacilityName = x.PatientVisit!.HealthFacility!.Name,

                   PatientName = x.Patient!.FullName!,
                   MrNo = x.Patient.Mrno,
                   Cnic = x.Patient.Cnic,
                   MobileNo = x.Patient.MobileNo,
                   Gender = x.Patient.GenderProfile!.Name,
                   Address = x.Patient.ParmanentAddress,
                   Age = x.Patient.Age,
                   Dob = x.Patient.Dob,

                   LabTestId = x.LabTestId,

                   TestName = x.LabTest!.Name!,
                   BarcodeNo = x.BarcodeNo!,
                   Description = x.LabTest!.Description!,

                   PreGenratedBarcodeNo = x.PreGeneratedBarcodeNo!,
                   TestCategory = x.LabTest!.LabTestCategoryProfile!.Name,

                   TestAdvisedBy = (x.IsAdvisedExternally == true) ? x.SourceDoctorName : x.TestAdvisedByNavigation!.FullName!,
                   TestAdvisedOn = x.CreatedOn,

                   SampleCollectedBy = x.SampleCollectedByNavigation!.FullName!,
                   SampleCollectedOn = x.SampleCollectedOn,

                   ReportGeneratedBy = x.ReportGeneratedByNavigation!.FullName!,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedByNavigation!.FullName!,
                   SampleRejectedOn = x.SampleRejectedOn,

                   SampleRejectedReason = x.SampleRejectedReason,

                   //SampleRejectedReasonsList = reasonsList.Where(y => x.SampleRejectedReason!.Split(',', StringSplitOptions.None).Select(Guid.Parse).ToArray().Contains(y.ProfileId)).Select(z => new RejectedSampleDto
                   //{
                   //    Id = z.ProfileId,
                   //    Name = z.Name,
                   //}).ToList(),

                   TestType = x.LabTest.LabTestTypeProfile!.Name,
                   TestTypeShortName = x.LabTest.LabTestTypeProfile!.ShortName,

                   IsSampleRequired = x.IsSampleRequired,
                   ResultImageLink = x.ResultImageLink,

                   IsActive = x.IsActive,

                   PatientLabTestDetails = _mapper.Map<List<ViewPatientLabTestDetailDto>>(x.PatientLabTestDetails.OrderBy(x => x.SequenceNo).ToList()),

               })
               .FirstOrDefaultAsync();



            foreach (var item in responseObj!.PatientLabTestDetails)
            {
                //var tempList = await _uowLabTestDetail.Repository.GetALL(x => x.LabTestId == item!.LabTestId && x.ActionTypeId != 3).ToListAsync();

                //foreach (var item2 in tempList)
                //{
                var tempProfile = await _uowProfile.Repository.GetALL(x => x.ProfileId == item!.TestResultDropDownTypeProfileId).FirstOrDefaultAsync();

                item.TestResultDropDownTypeProfile = _mapper.Map<ViewProfileDto>(tempProfile);
                // }
            }

            if (!string.IsNullOrEmpty(responseObj!.SampleRejectedReason))
            {


                if (responseObj!.IsSampleRejected == true)
                {
                    var ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

                    var sampleSelectedReasonList = responseObj.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    responseObj.SampleRejectedReasonsList = ReasonsList
                            .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => new RejectedSampleDto
                            {
                                Id = x.ProfileId,
                                Name = x.Name
                            }).ToList();
                }
            }
            return responseObj!;
        }

        #region HCP
        // This API is to get HCP Patient Recommended test
        public async Task<List<RecommendedLabTestToPatientDto>> GetRecommendedLabTestToPatient(Guid PatientId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetRecommendedLabTestToPatient", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<RecommendedLabTestToPatientDto> lst = ds.Tables[0].ToList<RecommendedLabTestToPatientDto>();
                    return lst.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public async Task<List<HcpRecommendedTest>> GetRecommendedLabTestsList()
        {
            var RecommendedTestList = _uowPatientLabTest.GetDbContext().HcpRecommendedTests.Where(x => x.ActionTypeId == 1 || x.ActionTypeId == 2).ToList();
            return RecommendedTestList;
        }

        #endregion

        #endregion

        #region Helper Methods

        public async Task sendLabTestReportViaSms(Guid PatientLabTestId)
        {
            if (_smsSend && _smsSendInDevelopment)
            {
                var infoForSmsBody = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == PatientLabTestId && x.SampleConsignmentDetailId != null)
                    .Include(x => x.Patient)
                    .Include(x => x.LabTest)
                    .Select(x => new SmsBodyPatientLabTestDto
                    {
                        PatientName = x.Patient!.FirstName,
                        PatientPhoneNo = x.Patient!.MobileNo!.Replace("-", ""),
                        TestName = x.LabTest!.Name,
                        Link = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", x.PatientLabTestId.ToString())
                    })
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(infoForSmsBody))
                {
                    // SMS on Result Generation
                    SendSMSDto smsObj = new SendSMSDto()
                    {
                        Receiver = infoForSmsBody.PatientPhoneNo,

                        Body = $"معزز {infoForSmsBody.PatientName}\r\n" +
                        $" آپ کے لیب ٹیسٹ" +
                        $"{infoForSmsBody.TestName}" +
                        $"کی رپورٹ تیار ہو گئی ہے۔ " +
                        $"\r\n" +
                        $"براے مہربانی اپنی رپورٹ لیب سے یا نیچے دیے گئے لنک سے حاصل کریں۔" +

                        $"\r\n" +
                        $"شکریہ" +

                        $"\r\n" +
                        $"{infoForSmsBody.Link}"
                    };

                    _smsService.SendSMS(smsObj);
                }

            }
        }

        private void FillEntity(PatientLabTest obj)
        {
            if (obj.PatientLabTestId == Guid.Empty)
            {
                obj.PatientLabTestId = Guid.NewGuid();
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

            if (obj.PatientLabTestDetails.Count > 0)
            {
                foreach (var patientLabTestDetail in obj.PatientLabTestDetails)
                {
                    if (patientLabTestDetail.PatientLabTestDetailId == Guid.Empty)
                    {
                        patientLabTestDetail.PatientLabTestDetailId = Guid.NewGuid();
                        patientLabTestDetail.CreatedBy = _tokenService.GetUserId();
                        patientLabTestDetail.CreatedOn = DateTime.Now;
                        patientLabTestDetail.ActionTypeId = (int)ActionTypeEnum.Create;
                    }
                    else
                    {
                        patientLabTestDetail.UpdatedBy = _tokenService.GetUserId();
                        patientLabTestDetail.UpdatedOn = DateTime.Now;
                        patientLabTestDetail.ActionTypeId = (int)ActionTypeEnum.Edit;
                    }
                }
            }
        }

        private void FillEntityRiderStatus(PatientLabTest obj)
        {
            if (obj.PatientLabTestId == Guid.Empty)
            {
                obj.PatientLabTestId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {

                obj.StatusUpdatedBy = _tokenService.GetUserId();
                obj.StatusUpdatedOn = DateTime.Now;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityDetail(PatientLabTestDetail obj)
        {
            if (obj.PatientLabTestDetailId == Guid.Empty)
            {
                obj.PatientLabTestDetailId = Guid.NewGuid();
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


        private void FillEntityDetailSampleTransportByLhw(SampleTransportByLhw obj)
        {
            if (obj.SampleTransportByLhwId == Guid.Empty)
            {
                obj.SampleTransportByLhwId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
            }
            else
            {
                obj.Updatedby = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
            }
        }




        //private void FillEntityDelete(PatientLabTest obj)
        //{
        //    if (obj != null)
        //    {
        //        obj.DeletedBy = _tokenService.GetUserId();
        //        obj.DeletedOn = DateTime.Now;
        //        obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        //    }

        //}

        private void FillEntityPendingCollection(PatientLabTest obj)
        {

            obj.IsSampleCollected = true;
            obj.SampleCollectedBy = _tokenService.GetUserId();
            obj.SampleCollectedOn = DateTime.Now;

            obj.IsArchived = false;
            obj.ArchivedBy = null;
            obj.ArchivedOn = null;

            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        }

        private void FillEntitySampleRejection(PatientLabTest obj)
        {

            obj.IsSampleRejected = true;
            obj.SampleRejectedBy = _tokenService.GetUserId();
            obj.SampleRejectedOn = DateTime.Now;

            //if(obj.IsFromCallCenter == true)
            //{
            obj.Status = 5;
            obj.StatusUpdatedBy = _tokenService.GetUserId(); ;
            obj.StatusUpdatedOn = DateTime.Now;
            //}

            //if (obj.IsSampleCollected != true) // check if sample collected
            //    obj.IsSampleCollected = false; // true = Rejected at Result Level; false = Rejected at Sample Level

            obj.IsArchived = false;
            obj.ArchivedBy = null;
            obj.ArchivedOn = null;

            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        }

        private void FillEntityUploadImage(PatientLabTest obj)
        {

            obj.IsArchived = false;
            obj.ArchivedBy = null;
            obj.ArchivedOn = null;

            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        }

        private void FillEntityUpdateResult(PatientLabTest obj)
        {

            obj.IsReportGenerated = true;
            obj.ReportGeneratedBy = _tokenService.GetUserId();
            obj.ReportGeneratedOn = DateTime.Now;

            obj.ReportLink = string.Concat(_labTestBaseURL, "LabReport/result?LabTestID=", obj.PatientLabTestId.ToString());

            obj.IsArchived = false;
            obj.ArchivedBy = null;
            obj.ArchivedOn = null;

            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        }

        private async Task UpdateSampleStatusInEMR(PatientLabTest obj)
        {
            var loginUser = TokenService.GetUserLoggedInfo();

            UpdateLabTestSampleCollectedDto emrSampleCollectedDto = new UpdateLabTestSampleCollectedDto();

            emrSampleCollectedDto.PkId = long.Parse(obj.SourceLabTestId ?? "");
            emrSampleCollectedDto.SystemSourceId = obj!.PatientLabTestId.ToString(); // Patient Lab Test Id
            emrSampleCollectedDto.SourcePatientId = obj!.PatientId.ToString() ?? ""; // Patient Id;
            emrSampleCollectedDto.SourcePatientMrNo = obj!.Patient!.Mrno ?? ""; // Patient Mrno;

            emrSampleCollectedDto.IsSampleReceived = obj.IsSampleCollected ?? false;
            emrSampleCollectedDto.SampleReceivedBy = loginUser.FullName ?? "";
            emrSampleCollectedDto.SampleReceivedDatetime = obj.SampleCollectedOn;

            var response = await _emrService.UpdateSampleCollectedStatus(emrSampleCollectedDto);
            if (response.status == true)
                obj.IsExternalSampleUpdated = true;
            else
                obj.IsExternalSampleUpdated = false;

        }
        private async Task UpdateSampleRejectionStatusinEMR(PatientLabTest obj, UpdateLabSampleRejectedReasonDto input)
        {

            var loginUser = TokenService.GetUserLoggedInfo();

            UpdateLabTestSampleCollectedDto emrSampleCollectedDto = new UpdateLabTestSampleCollectedDto();

            emrSampleCollectedDto.PkId = long.Parse(obj.SourcePkId ?? "");
            emrSampleCollectedDto.SystemSourceId = obj!.PatientLabTestId.ToString(); // Patient Lab Test Id
            emrSampleCollectedDto.SourcePatientId = obj!.PatientId.ToString() ?? ""; // Patient Id;
            emrSampleCollectedDto.SourcePatientMrNo = obj!.Patient!.Mrno ?? ""; // Patient Mrno;

            // sample collected
            emrSampleCollectedDto.IsSampleRejected = obj.IsSampleRejected ?? false;
            emrSampleCollectedDto.SampleRejectedReason = input.SampleRejectedReasonName ?? "";
            emrSampleCollectedDto.SampleRejectedBy = loginUser!.FullName ?? "";
            emrSampleCollectedDto.SampleRejectedDatetime = obj.SampleRejectedOn;

            var response = await _emrService.UpdateResultStatus(emrSampleCollectedDto);
            if (response.status == true)
                obj.IsExternalReportUpdated = false;
            else
                obj.IsExternalReportUpdated = false;
        }
        private async Task UpdateResultStatusInEMR(PatientLabTest obj)
        {

            var loginUser = TokenService.GetUserLoggedInfo();

            UpdateLabTestSampleCollectedDto emrSampleCollectedDto = new UpdateLabTestSampleCollectedDto();

            emrSampleCollectedDto.PkId = long.Parse(obj.SourcePkId ?? "");
            emrSampleCollectedDto.SystemSourceId = obj!.PatientLabTestId.ToString(); // Patient Lab Test Id
            emrSampleCollectedDto.SourcePatientId = obj!.PatientId.ToString() ?? ""; // Patient Id;
            emrSampleCollectedDto.SourcePatientMrNo = obj!.Patient!.Mrno ?? ""; // Patient Mrno;

            // result 
            emrSampleCollectedDto.IsResultUploaded = obj.IsReportGenerated ?? false;
            emrSampleCollectedDto.ResultUploadedBy = loginUser.FullName ?? "";
            emrSampleCollectedDto.ResultUploadDatetime = obj.ReportGeneratedOn;

            var responseLabTestReceipt = await GetAllPatientLabTestDetailByPatientLabTestId(obj!.PatientLabTestId);
            if (!AppCommonMethod.IsNullObject(responseLabTestReceipt))
                emrSampleCollectedDto.ResultJson = JsonConvert.SerializeObject(responseLabTestReceipt);
            emrSampleCollectedDto.ReportURL = obj.ReportLink ?? "";

            var response = await _emrService.UpdateResultStatus(emrSampleCollectedDto);

            if (response.status == true)
                obj.IsExternalReportUpdated = true;
            else
                obj.IsExternalReportUpdated = false;
        }
        private void FillEntityForArchive(PatientLabTest obj)
        {

            obj.IsArchived = true;
            obj.ArchivedBy = _tokenService.GetUserId();
            obj.ArchivedOn = DateTime.Now;
            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Edit;
        }


        public void IsValidCnic(string Cnic)
        {

            if (Cnic == "11111-1111111-1" ||
                Cnic == "22222-2222222-2" ||
                Cnic == "33333-3333333-3" ||
                Cnic == "44444-4444444-4" ||
                Cnic == "55555-5555555-5" ||
                Cnic == "66666-6666666-6" ||
                Cnic == "77777-7777777-7" ||
                Cnic == "88888-8888888-8" ||
                Cnic == "99999-9999999-9")
                throw new UserFriendlyException(CommonMessageConstant.CNICINVALID);


        }
        #endregion

    }

}
