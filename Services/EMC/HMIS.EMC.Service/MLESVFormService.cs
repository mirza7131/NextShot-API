using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler;
using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using HMIS.EMC.Domain.Repositories.UOW;
using HMIS.EMC.Service.Interfaces;
using HMIS.EMC.Service.Interfaces.QrCodeHelper;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service
{
    public class MLESVFormService<TEntity> : IMLESvForm where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly IMapper _mapper;
        private UnitOfWork<MlcsvinitialInfo> _uowMlcSvInitialInfo;
        private UnitOfWork<PatientImage> _uowPatientImage;
        private readonly UploadFiles _fileUploader;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        #endregion

        #region Constructor
        public MLESVFormService(TokenService tokenService, IMapper mapper, UnitOfWork<MlcsvinitialInfo> uowGeneral, UploadFiles uploadFiles, UnitOfWork<PatientImage> uowPatientImage, PatientDiagnoseService patientDiagnoseService, IQrCodeGeneratorHelper qrCodeGenerator)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowMlcSvInitialInfo = uowGeneral;
            _fileUploader = uploadFiles;
            _uowPatientImage = uowPatientImage;
            _patientDiagnoseService = patientDiagnoseService;
            _patientDiagnoseService = patientDiagnoseService;
            _qrCodeGenerator = qrCodeGenerator;
        }

        #endregion

        #region CUD
        // Initial Info
        public async Task<CreateOrEditMlcSvInitialInfoDto> CreateOrEdit(CreateOrEditMlcSvInitialInfoDto input)
        {
            try
            {
                var obj = await _uowMlcSvInitialInfo.GetDbContext().MlcsvinitialInfos.Where(x => x.PatientVisitId == input.PatientVisitId).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullObject(obj))
                {
                    return await CreateMlcSvBasicInfo(input);
                }
                else
                {
                    input.MlcsvinitialInfoId = obj.MlcsvinitialInfoId;
                    return await UpdateMlcSvBasicInfo(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //  Create Initial Info
        private async Task<CreateOrEditMlcSvInitialInfoDto> CreateMlcSvBasicInfo(CreateOrEditMlcSvInitialInfoDto input)
        {
            try
            {
                if (input.EmergencyNo != null)
                {
                    //PatientDiagnoseService diagService = new PatientDiagnoseService(_tokenService, _uowPatientDiagnose, _mapper);
                    PatientDiagnoseDto digDto = new PatientDiagnoseDto();

                    var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);
                    digService.FormType = input.FormType;
                    digService.DocDepartmentLookupId = input.DocDepartmentLookupId;
                    digService.DocSectionLookupId = input.DocSectionLookupId;
                    digService.PatientId = input.PatientId;
                    digService.PatientVisitId = input.PatientVisitId;
                    input.PatientDiagnoseId = digService.PatientDiagnoseId;
                }

                if (input.EmergencyNo == null)
                {
                    input.PatientDiagnoseId = null;
                }

                var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowMlcSvInitialInfo.GetDbContext());

                var obj = _mapper.Map<MlcsvinitialInfo>(input);

                // Insert PatientImage
                if (!AppCommonMethod.IsNullObject(input.PatientImage))
                {
                    if (input.PatientImage.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        input.PatientImage.PatientImageId = Guid.NewGuid();

                        obj.PatientImageId = input.PatientImage.PatientImageId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.PatientImage);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientImage.base64, _tokenService.GetAccessToken());

                        ptSigImg.PatientId = input.PatientId;
                        ptSigImg.ActionTypeId = 1;

                        ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                        ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;

                        ptSigImg.CreatedOn = DateTime.Now;
                        ptSigImg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptSigImg);
                        await _uowPatientImage.CommitAsync();
                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }

                // Police Fingerprint 
                if (!AppCommonMethod.IsNullObject(input.PatientFingerPrint))
                {
                    if (input.PatientFingerPrint.base64 != "")
                    {
                        input.PatientFingerPrint.PatientImageId = Guid.NewGuid();
                        obj.PatientFingerPrintId = input.PatientFingerPrint.PatientImageId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.PatientFingerPrint);

                        var ImageBaseSixty4 = new ImageBaseSixtyFour();

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientFingerPrint.base64, _tokenService.GetAccessToken());
                        ptSigImg.PatientId = input.PatientId;
                        ptSigImg.ActionTypeId = 1;

                        ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                        ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;

                        ptSigImg.CreatedOn = DateTime.Now;
                        ptSigImg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptSigImg);
                        await _uowPatientImage.CommitAsync();

                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }

                // Insert Patient Signature Image
                if (!AppCommonMethod.IsNullObject(input.PatientSignature))
                {
                    if (input.PatientSignature.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        input.PatientSignature.PatientImageId = Guid.NewGuid();
                        obj.PatientSignatureId = input.PatientSignature.PatientImageId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.PatientSignature);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientSignature.base64, _tokenService.GetAccessToken());

                        ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                        ptSigImg.PatientId = input.PatientId;
                        ptSigImg.ActionTypeId = 1;
                        ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                        ptSigImg.CreatedOn = DateTime.Now;
                        ptSigImg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptSigImg);
                        await _uowPatientImage.CommitAsync();
                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }

                // Insert Guardian Signature Image
                if (!AppCommonMethod.IsNullObject(input.GuardianSignature))
                {
                    if (input.GuardianSignature.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        input.GuardianSignature.PatientImageId = Guid.NewGuid();
                        obj.GuardianSignatureId = input.GuardianSignature.PatientImageId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.GuardianSignature);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.GuardianSignature.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.GuardianSignature.base64, _tokenService.GetAccessToken());

                        ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                        ptSigImg.PatientId = input.PatientId;
                        ptSigImg.ActionTypeId = 1;
                        ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                        ptSigImg.CreatedOn = DateTime.Now;
                        ptSigImg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptSigImg);
                        await _uowPatientImage.CommitAsync();
                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }
               
                // Insert Police Signature Image
                if (!AppCommonMethod.IsNullObject(input.PoliceSignature))
                {
                    if (input.PoliceSignature.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        input.PoliceSignature.PatientImageId = Guid.NewGuid();
                        obj.PoliceSignatureId = input.PoliceSignature.PatientImageId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.PoliceSignature);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PoliceSignature.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());

                        ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                        ptSigImg.PatientId = input.PatientId;
                        ptSigImg.ActionTypeId = 1;
                        ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                        ptSigImg.CreatedOn = DateTime.Now;
                        ptSigImg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptSigImg);
                        await _uowPatientImage.CommitAsync();
                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }



                FillEntity(obj);
                //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


                //if (profile != null)
                //{
                //    obj.FormTypeProfileId = profile.ProfileId;

                //}


                // Create MLC Common
                var _uowMlc = new UnitOfWork<Mlc>(_uowMlcSvInitialInfo.GetDbContext());

                if (AppCommonMethod.IsNullObject(input.createOrEditMLCDtos))
                    input.createOrEditMLCDtos = new CreateOrEditMLCDto();

                var mlc = _mapper.Map<Mlc>(input.createOrEditMLCDtos);
                //if(input.createOrEditMLCDtos.MlctypeProfileId!=null) { 
                //    obj.Mlcno = input.HealthFacilityId.ToString() +"-" + DateTime.Now.Ticks.ToString() +"-" + DateTime.Now.Year.ToString();                
                //}

                // For Closing Visit
                //var _uowPatientOpenVisits = new UnitOfWork<PatientOpenVisit>(_uowMlcSvInitialInfo.GetDbContext());

                //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisits.Repository.GetById(input.PatientVisitId!);

                //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                //dbObjPatientVisit.IsOccupied = false;
                //dbObjPatientVisit.OccupiedBy = null;
                //dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                //dbObjPatientVisit.UpdatedOn = DateTime.Now;
                //dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;
                //dbObjPatientVisit.IsDischarge = true;

                //_uowPatientOpenVisits.Repository.Update(dbObjPatientVisit);
                //await _uowPatientOpenVisits.CommitAsync();
                // Closing Visit till here

                var random = new Random();
                var chars = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
                var randomNo = new string(Enumerable.Repeat(chars, 7)
                   .Select(s => s[random.Next(s.Length)]).ToArray());


                mlc.Mlcid = Guid.NewGuid();
                // Set Mlcid For Mlc
                obj.Mlcid = mlc.Mlcid;

                mlc.PatientId = input.PatientId;
                mlc.PatientVisitId = input.PatientVisitId;
                mlc.PatientDiagnoseId = input.PatientDiagnoseId;
                mlc.HealthFacilityId = input.HealthFacilityId;
                mlc.IsActive = true;
                mlc.ActionTypeId = (int)ActionTypeEnum.Create;
                mlc.Mlcno = input.HealthFacilityId.ToString() + "-" + randomNo + "-" + DateTime.Now.Year.ToString();
                mlc.BookNo = input.createOrEditMLCDtos.BookNo;
                mlc.DoctorId = input.createOrEditMLCDtos.DoctorId;

                mlc.CreatedOn = DateTime.Now;
                mlc.CreatedBy = _tokenService.GetUserId();


                await _uowMlc.Repository.Insert(mlc);
                await _uowMlc.CommitAsync();


                MlcsvinitialInfo responseObj = await _uowMlcSvInitialInfo.Repository.Insert(obj);
                await _uowMlcSvInitialInfo.CommitAsync();

                return _mapper.Map<CreateOrEditMlcSvInitialInfoDto>(responseObj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Edit Initial Info
        private async Task<CreateOrEditMlcSvInitialInfoDto> UpdateMlcSvBasicInfo(CreateOrEditMlcSvInitialInfoDto input)
        {
            try
            {

                if (input.EmergencyNo != null)
                {

                    if (input.PatientDiagnoseId == null || input.PatientDiagnoseId.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                        {
                            var chkPtDiagnose = await _uowMlcSvInitialInfo.GetDbContext().PatientDiagnoses.Where(x => x.PatientDiagnoseId == input.PatientDiagnoseId)
                            .ToListAsync();
                            if (AppCommonMethod.IsNullOrEmptyList(chkPtDiagnose))
                            {
                                PatientDiagnoseDto digDto = new PatientDiagnoseDto();
                                digDto.FormType = input.FormType;
                                digDto.DocDepartmentLookupId = input.DocDepartmentLookupId;
                                digDto.DocSectionLookupId = input.DocSectionLookupId;
                                digDto.PatientId = input.PatientId;
                                digDto.PatientVisitId = input.PatientVisitId;

                                var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);

                                input.PatientDiagnoseId = digService.PatientDiagnoseId;
                            }
                        }
                    }
                }

                if (input.EmergencyNo == null)
                {
                    input.PatientDiagnoseId = null;
                }

                var dbObj = await _uowMlcSvInitialInfo.Repository.GetById(input.MlcsvinitialInfoId!);
                var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowMlcSvInitialInfo.GetDbContext());

                if (AppCommonMethod.IsNullObject(dbObj))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                var obj = _mapper.Map(input, dbObj);
                FillEntity(obj!);

                _uowMlcSvInitialInfo.Repository.Update(obj!);
                // Edit Mlc
                var _uowMlc = new UnitOfWork<Mlc>(_uowMlcSvInitialInfo.GetDbContext());
                if (AppCommonMethod.IsNullObject(input.createOrEditMLCDtos))
                {
                    input.createOrEditMLCDtos = new CreateOrEditMLCDto();

                    input.createOrEditMLCDtos.Mlcid = Guid.NewGuid();
                    input.createOrEditMLCDtos.PatientId = input.PatientId;
                    input.createOrEditMLCDtos.PatientVisitId = input.PatientVisitId;
                    input.createOrEditMLCDtos.PatientVisitId = input.PatientVisitId;
                    input.createOrEditMLCDtos.PatientDiagnoseId = input.PatientDiagnoseId;
                    input.createOrEditMLCDtos.IsFinalReport = input.IsFinalReport;

                    //var bkno = _uowMlc.GetDbContext().Mlcs.Where(x => x.BookNo == input.createOrEditMLCDtos.BookNo).ToList();
                    //if (!AppCommonMethod.IsNullOrEmptyList(bkno))
                    //    throw new UserFriendlyException("Book no already exists..!");

                    var mlc = _mapper.Map<Mlc>(input.createOrEditMLCDtos);
                    mlc.ActionTypeId = (int)ActionTypeEnum.Create;
                    mlc.IsActive = true;
                    mlc.Mlcno = input.HealthFacilityId.ToString() + "-" + DateTime.Now.Ticks.ToString() + "-" + DateTime.Now.Year.ToString();
                    mlc.BookNo = input.createOrEditMLCDtos.BookNo;

                        //if (input.createOrEditMLCDtos.BookNo != null)
                        //{
                        //    var bkno = _uowMlc.GetDbContext().Mlcs.Where(x => x.BookNo == input.createOrEditMLCDtos.BookNo && x.HealthFacilityId == input.HealthFacilityId).ToList();
                        //    if (!AppCommonMethod.IsNullOrEmptyList(bkno)) 
                        //        throw new UserFriendlyException(CommonMessageConstant.DuplicateBookNumberFound);
                        //}


                    if (mlc.BookNo != null)
                    {
                        mlc.IsBookNoGenerated = true;
                    }


                    input.Mlcid = mlc.Mlcid;

                    mlc.CreatedOn = DateTime.Now;
                    mlc.CreatedBy = _tokenService.GetUserId();
                    await _uowMlc.Repository.Insert(mlc);
                    await _uowMlc.CommitAsync();
                }
                else
                {
                    var mlc = await _uowMlc.GetDbContext().Mlcs.Where(x => x.Mlcid == input.Mlcid)
                        .OrderByDescending(x=>x.CreatedOn)
                        .FirstOrDefaultAsync();

                    if(mlc != null)
                    {
                        if (mlc.IsBookNoGenerated == false || mlc.IsBookNoGenerated == null)
                        {
                            if (input.createOrEditMLCDtos.BookNo != null)
                            {
                                var bkno = _uowMlc.GetDbContext().Mlcs.Where(x => x.BookNo == input.createOrEditMLCDtos.BookNo && x.HealthFacilityId == input.HealthFacilityId).ToList();
                                if (!AppCommonMethod.IsNullOrEmptyList(bkno))
                                    throw new UserFriendlyException(CommonMessageConstant.DuplicateBookNumberFound);
                            }
                        }

                        if (mlc.BookNo != null)
                        {
                            mlc.IsBookNoGenerated = true;
                        }

                        if(input.IsReportCount == true)
                        {
                            if (input.ReportCounts != null)
                            {
                                mlc.ReportCounts = input.ReportCounts + 1;

                                // For Qr Code

                                string qrCodeValue = "MLCNo = " + input.createOrEditMLCDtos.Mlcno + ","
                                + "Name = " + input.PatientName + "," + "Hospital = " + input.HealthFacilityName;


                                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                                if (mlc.QrCodeImagePath == null)
                                {
                                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                                    mlc.QrCodeImagePath = qrCodeImageUrl;
                                }
                            }
                            else
                            {
                                mlc.ReportCounts = 1;

                                string qrCodeValue = "SrNo = " + "1234543" + "," + "MLCNo = " + input.createOrEditMLCDtos.Mlcno + ","
                                + "Name = " + input.PatientName + "," + "Hospital = " + input.HealthFacilityName;


                                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                                if (mlc.QrCodeImagePath == null)
                                {
                                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                                    mlc.QrCodeImagePath = qrCodeImageUrl;
                                }
                            }
                        }
                        obj.Mlcid = mlc.Mlcid;
                        obj.HealthFacilityId = mlc.HealthFacilityId;
                        mlc.DoctorId = input.createOrEditMLCDtos.DoctorId;
                        mlc.BookNo = input.createOrEditMLCDtos.BookNo;
                        mlc.IsActive = true;
                        mlc.ActionTypeId = (int)ActionTypeEnum.Edit;
                        mlc.PatientDiagnoseId = input.PatientDiagnoseId;
                        mlc.PoliceDistrict = input.createOrEditMLCDtos.PoliceDistrict;
                        mlc.IsFinalReport = input.IsFinalReport;
                        _uowMlc.Repository.Update(mlc);
                        await _uowMlc.CommitAsync();
                    }
                    else
                    {
                        var mlcRec = await _uowMlc.GetDbContext().Mlcs.Where(x => x.PatientVisitId == input.PatientVisitId)
                        .OrderByDescending(x => x.CreatedOn)
                        .FirstOrDefaultAsync();

                        if (mlcRec != null)
                        {
                            obj.Mlcid = mlcRec.Mlcid;
                            obj.HealthFacilityId = mlcRec.HealthFacilityId;
                        }
                    }
                }


                await _uowMlcSvInitialInfo.CommitAsync();


                if (input.PatientImageId != null)
                {
                    var pi = await _uowPatientImage.GetDbContext().PatientImages
                        .Where(x => x.PatientImageId == input.PatientImageId).FirstOrDefaultAsync();

                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PatientImageId)
                        .FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(input.PatientImage))
                    {
                        if (input.PatientImage.base64 != "")
                        {

                            b64.ActionTypeId = 2;
                            b64.Base64 = input.PatientImage.base64;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientImage.base64, _tokenService.GetAccessToken());
                            pi.ActionTypeId = 2;
                            pi.UpdatedOn = DateTime.Now;
                            pi.UpdatedBy = _tokenService.GetUserId();

                            _uowPatientImage.Repository.Update(pi);
                            await _uowPatientImage.CommitAsync();

                            _uowBase64.Repository.Update(b64);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }
                else
                {
                    if (!AppCommonMethod.IsNullObject(input.PatientImage))
                    {
                        if (input.PatientImage.base64 != "")
                        {
                            var ImageBaseSixty4 = new ImageBaseSixtyFour();
                            input.PatientImage.PatientImageId = Guid.NewGuid();
                            obj.PatientImageId = input.PatientImage.PatientImageId;

                            var ptimg = _mapper.Map<PatientImage>(input.PatientImage);
                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientImage.base64, _tokenService.GetAccessToken());

                            ImageBaseSixty4.PatientImageId = ptimg.PatientImageId;

                            ptimg.PatientId = input.PatientId;
                            ptimg.ActionTypeId = 1;
                            ptimg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                            ptimg.CreatedOn = DateTime.Now;
                            ptimg.CreatedBy = _tokenService.GetUserId();

                            await _uowPatientImage.Repository.Insert(ptimg);
                            await _uowPatientImage.CommitAsync();
                            // Insert In ImageBase64 Table
                            await _uowBase64.Repository.Insert(ImageBaseSixty4);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }

                if (input.PatientSignatureId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientSignatureId).FirstOrDefaultAsync();

                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PatientSignatureId)
                       .FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(input.PatientSignature))
                    {
                        if (input.PatientSignature.base64 != "")
                        {

                            b64.ActionTypeId = 2;
                            b64.Base64 = input.PatientImage.base64;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientSignature.base64, _tokenService.GetAccessToken());
                            psi.ActionTypeId = 2;
                            psi.UpdatedOn = DateTime.Now;
                            psi.UpdatedBy = _tokenService.GetUserId();

                            _uowPatientImage.Repository.Update(psi);
                            await _uowPatientImage.CommitAsync();

                            _uowBase64.Repository.Update(b64);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }
                else
                {
                    if (!AppCommonMethod.IsNullObject(input.PatientSignature))
                    {
                        if (input.PatientSignature.base64 != "")
                        {
                            var ImageBaseSixty4 = new ImageBaseSixtyFour();
                            input.PatientSignature.PatientImageId = Guid.NewGuid();
                            obj.PatientSignatureId = input.PatientSignature.PatientImageId;

                            var ptSigImg = _mapper.Map<PatientImage>(input.PatientSignature);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientSignature.base64, _tokenService.GetAccessToken());

                            ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                            ptSigImg.PatientId = input.PatientId;
                            ptSigImg.ActionTypeId = 1;
                            ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                            ptSigImg.CreatedOn = DateTime.Now;
                            ptSigImg.CreatedBy = _tokenService.GetUserId();

                            await _uowPatientImage.Repository.Insert(ptSigImg);
                            await _uowPatientImage.CommitAsync();
                            // Insert In ImageBase64 Table
                            await _uowBase64.Repository.Insert(ImageBaseSixty4);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }

                if (input.PoliceSignatureId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                        .Where(x => x.PatientImageId == input.PoliceSignatureId).FirstOrDefaultAsync();


                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PoliceSignatureId)
                       .FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(input.PoliceSignature))
                    {
                        if (input.PoliceSignature.base64 != "")
                        {
                            b64.Base64 = input.PatientImage.base64;
                            b64.ActionTypeId = 2;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());
                            psi.ActionTypeId = 2;
                            psi.UpdatedOn = DateTime.Now;
                            psi.UpdatedBy = _tokenService.GetUserId();

                            _uowPatientImage.Repository.Update(psi);
                            await _uowPatientImage.CommitAsync();

                            _uowBase64.Repository.Update(b64);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }
                else
                {
                    if (!AppCommonMethod.IsNullObject(input.PoliceSignature))
                    {
                        if (input.PoliceSignature.base64 != "")
                        {
                            var ImageBaseSixty4 = new ImageBaseSixtyFour();
                            input.PoliceSignature.PatientImageId = Guid.NewGuid();
                            obj.PoliceSignatureId = input.PoliceSignature.PatientImageId;

                            var plcImg = _mapper.Map<PatientImage>(input.PoliceSignature);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            plcImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());

                            ImageBaseSixty4.PatientImageId = plcImg.PatientImageId;

                            plcImg.PatientId = input.PatientId;
                            plcImg.ActionTypeId = 1;
                            plcImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                            plcImg.CreatedOn = DateTime.Now;
                            plcImg.CreatedBy = _tokenService.GetUserId();

                            await _uowPatientImage.Repository.Insert(plcImg);
                            await _uowPatientImage.CommitAsync();
                            // Insert In ImageBase64 Table
                            await _uowBase64.Repository.Insert(ImageBaseSixty4);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }

                if (input.GuardianSignatureId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                        .Where(x => x.PatientImageId == input.GuardianSignatureId).FirstOrDefaultAsync();


                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.GuardianSignatureId)
                       .FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(input.GuardianSignature))
                    {
                        if (input.GuardianSignature.base64 != "")
                        {
                            b64.Base64 = input.GuardianSignature.base64;
                            b64.ActionTypeId = 2;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.GuardianSignature.base64, _tokenService.GetAccessToken());
                            psi.ActionTypeId = 2;
                            psi.UpdatedOn = DateTime.Now;
                            psi.UpdatedBy = _tokenService.GetUserId();

                            _uowPatientImage.Repository.Update(psi);
                            await _uowPatientImage.CommitAsync();

                            _uowBase64.Repository.Update(b64);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }
                else
                {
                    if (!AppCommonMethod.IsNullObject(input.GuardianSignature))
                    {
                        if (input.GuardianSignature.base64 != "")
                        {
                            var ImageBaseSixty4 = new ImageBaseSixtyFour();
                            input.GuardianSignature.PatientImageId = Guid.NewGuid();
                            obj.GuardianSignatureId = input.GuardianSignature.PatientImageId;

                            var plcImg = _mapper.Map<PatientImage>(input.GuardianSignature);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.GuardianSignature.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            plcImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.GuardianSignature.base64, _tokenService.GetAccessToken());

                            ImageBaseSixty4.PatientImageId = plcImg.PatientImageId;

                            plcImg.PatientId = input.PatientId;
                            plcImg.ActionTypeId = 1;
                            plcImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                            plcImg.CreatedOn = DateTime.Now;
                            plcImg.CreatedBy = _tokenService.GetUserId();

                            await _uowPatientImage.Repository.Insert(plcImg);
                            await _uowPatientImage.CommitAsync();
                            // Insert In ImageBase64 Table
                            await _uowBase64.Repository.Insert(ImageBaseSixty4);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }

                if (input.PatientFingerPrintId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientFingerPrintId).FirstOrDefaultAsync();


                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PatientFingerPrintId)
                       .FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(input.PatientFingerPrint))
                    {
                        if (input.PatientFingerPrint.base64 != "")
                        {

                            b64.Base64 = input.PatientFingerPrint.base64;
                            b64.ActionTypeId = 2;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientFingerPrint.base64, _tokenService.GetAccessToken());
                            psi.ActionTypeId = 2;
                            psi.UpdatedOn = DateTime.Now;
                            psi.UpdatedBy = _tokenService.GetUserId();

                            _uowPatientImage.Repository.Update(psi);
                            await _uowPatientImage.CommitAsync();

                            _uowBase64.Repository.Update(b64);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }
                else
                {
                    if (!AppCommonMethod.IsNullObject(input.PatientFingerPrint))
                    {
                        if (input.PatientFingerPrint.base64 != "")
                        {
                            var ImageBaseSixty4 = new ImageBaseSixtyFour();
                            input.PatientFingerPrint.PatientImageId = Guid.NewGuid();
                            obj.PatientFingerPrintId = input.PatientFingerPrint.PatientImageId;

                            var ptSigImg = _mapper.Map<PatientImage>(input.PatientFingerPrint);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientFingerPrint.base64, _tokenService.GetAccessToken());
                            ptSigImg.PatientId = input.PatientId;
                            ptSigImg.ActionTypeId = 1;

                            ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                            ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;

                            ptSigImg.CreatedOn = DateTime.Now;
                            ptSigImg.CreatedBy = _tokenService.GetUserId();

                            await _uowPatientImage.Repository.Insert(ptSigImg);
                            await _uowPatientImage.CommitAsync();
                            // Insert In ImageBase64 Table
                            await _uowBase64.Repository.Insert(ImageBaseSixty4);
                            await _uowBase64.CommitAsync();
                        }
                    }
                }

                //if (input.PatientUnderAgeFingerPrintId != null)
                //{
                //    var psi = await _uowPatientImage.GetDbContext().PatientImages
                //        .Where(x => x.PatientImageId == input.PatientUnderAgeFingerPrintId).FirstOrDefaultAsync();

                //    psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.UnderAgeFingerPrint.base64, _tokenService.GetAccessToken());
                //    psi.ActionTypeId = 2;
                //    psi.UpdatedOn = DateTime.Now;
                //    psi.UpdatedBy = _tokenService.GetUserId();

                //    _uowPatientImage.Repository.Update(psi);
                //    await _uowPatientImage.CommitAsync();
                //}

                // For Closing Visit
                //var _uowPatientOpenVisits = new UnitOfWork<PatientOpenVisit>(_uowMlcSvInitialInfo.GetDbContext());

                //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisits.Repository.GetById(input.PatientVisitId!);

                //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                //dbObjPatientVisit.IsOccupied = false;
                //dbObjPatientVisit.OccupiedBy = null;
                //dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                //dbObjPatientVisit.UpdatedOn = DateTime.Now;
                //dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;
                //dbObjPatientVisit.IsDischarge = true;

                //_uowPatientOpenVisits.Repository.Update(dbObjPatientVisit);
                //await _uowPatientOpenVisits.CommitAsync();
                // Closing Visit till here

                return _mapper.Map<CreateOrEditMlcSvInitialInfoDto>(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Examination
        public async Task<CreateOrEditMlcSvExaminationDto> CreateOrEdit(CreateOrEditMlcSvExaminationDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.MlcsvexaminationId))
                {
                    return await CreateMlcSvExamination(input);
                }
                else
                {
                    return await UpdateMlcSvExamination(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Create Examination
        private async Task<CreateOrEditMlcSvExaminationDto> CreateMlcSvExamination(CreateOrEditMlcSvExaminationDto input)
        {
            var _uowMleExamination = new UnitOfWork<Mlcsvexamination>(_uowMlcSvInitialInfo.GetDbContext());
            var obj = _mapper.Map<Mlcsvexamination>(input);

            obj.MlcsvinitialInfoId = input.MlcsvinitialInfoId;

            FillEntityExaminatin(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            Mlcsvexamination responseObj = await _uowMleExamination.Repository.Insert(obj);



            await _uowMleExamination.CommitAsync();
            return _mapper.Map<CreateOrEditMlcSvExaminationDto>(responseObj);
        }
        // Edit Examination
        private async Task<CreateOrEditMlcSvExaminationDto> UpdateMlcSvExamination(CreateOrEditMlcSvExaminationDto input)
        {
            var _uowMleExamination = new UnitOfWork<Mlcsvexamination>(_uowMlcSvInitialInfo.GetDbContext());
            var dbObj = await _uowMleExamination.Repository.GetById(input.MlcsvexaminationId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityExaminatin(obj!);

            _uowMleExamination.Repository.Update(obj!);
            await _uowMleExamination.CommitAsync();

            return _mapper.Map<CreateOrEditMlcSvExaminationDto>(obj);
        }

        // Evidence Collected
        public async Task<CreateOrEditMlcSvEvidenceCollectedDto> CreateOrEdit(CreateOrEditMlcSvEvidenceCollectedDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.MlcsvevidenceCollectedId))
                {
                    return await CreateMlcSvEvidenceCollected(input);
                }
                else
                {
                    return await UpdateMlcSvEvidenceCollected(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Create Evidence Collected
        private async Task<CreateOrEditMlcSvEvidenceCollectedDto> CreateMlcSvEvidenceCollected(CreateOrEditMlcSvEvidenceCollectedDto input)
        {
            var _uowMleEvidenceCollected = new UnitOfWork<MlcsvevidenceCollected>(_uowMlcSvInitialInfo.GetDbContext());
            var obj = _mapper.Map<MlcsvevidenceCollected>(input);

            obj.MlcsvinitialInfoId = input.MlcsvinitialInfoId;

            FillEntityEvidenceCollected(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            MlcsvevidenceCollected responseObj = await _uowMleEvidenceCollected.Repository.Insert(obj);



            await _uowMleEvidenceCollected.CommitAsync();
            return _mapper.Map<CreateOrEditMlcSvEvidenceCollectedDto>(responseObj);
        }
        // Edit Evidence Collected
        private async Task<CreateOrEditMlcSvEvidenceCollectedDto> UpdateMlcSvEvidenceCollected(CreateOrEditMlcSvEvidenceCollectedDto input)
        {
            var _uowMleExamination = new UnitOfWork<MlcsvevidenceCollected>(_uowMlcSvInitialInfo.GetDbContext());
            var dbObj = await _uowMleExamination.Repository.GetById(input.MlcsvevidenceCollectedId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityEvidenceCollected(obj!);

            _uowMleExamination.Repository.Update(obj!);
            await _uowMleExamination.CommitAsync();

            return _mapper.Map<CreateOrEditMlcSvEvidenceCollectedDto>(obj);
        }

        // Report
        public async Task<CreateOrEditMlcSvReportDto> CreateOrEdit(CreateOrEditMlcSvReportDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.MlcsvreportId))
                {
                    return await CreateMleReport(input);
                }
                else
                {
                    return await UpdateMleReport(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Create Report
        private async Task<CreateOrEditMlcSvReportDto> CreateMleReport(CreateOrEditMlcSvReportDto input)
        {
            var _uowMlereport = new UnitOfWork<Mlcsvreport>(_uowMlcSvInitialInfo.GetDbContext());
            var obj = _mapper.Map<Mlcsvreport>(input);
            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowMlcSvInitialInfo.GetDbContext());

            obj.MlcsvinitialInfoId = input.MlcsvinitialInfoId;

            // Insert In ImageBase64 Table
            //
          
                if (input.ManualReport.base64 != "")
                {
                    input.ManualReport.PatientImageId = Guid.NewGuid();
                    obj.ManualReportImageId = input.ManualReport.PatientImageId;
                    var ptSigImg = _mapper.Map<PatientImage>(input.ManualReport);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.ManualReport.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.ManualReport.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientId = input.PatientId;
                    ptSigImg.ActionTypeId = 1;
                    ptSigImg.CreatedOn = DateTime.Now;
                    ptSigImg.CreatedBy = _tokenService.GetUserId();


                    ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                    ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;

                    ptSigImg.CreatedOn = DateTime.Now;
                    ptSigImg.CreatedBy = _tokenService.GetUserId();

                    await _uowPatientImage.Repository.Insert(ptSigImg);
                    await _uowPatientImage.CommitAsync();

                    await _uowBase64.Repository.Insert(ImageBaseSixty4);
                    await _uowBase64.CommitAsync();
                }
            

                if (input.FinalReport.base64 != "")
                {
                    input.FinalReport.PatientImageId = Guid.NewGuid();
                    obj.FinalReportImageId = input.FinalReport.PatientImageId;

                    var ptSigImg = _mapper.Map<PatientImage>(input.FinalReport);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.FinalReport.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.FinalReport.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientId = input.PatientId;
                    ptSigImg.ActionTypeId = 1;
                    ptSigImg.CreatedOn = DateTime.Now;
                    ptSigImg.CreatedBy = _tokenService.GetUserId();

                    ImageBaseSixty4.PatientImageId = ptSigImg.PatientImageId;

                    ptSigImg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;

                    await _uowPatientImage.Repository.Insert(ptSigImg);
                    await _uowPatientImage.CommitAsync();


                    await _uowBase64.Repository.Insert(ImageBaseSixty4);
                    await _uowBase64.CommitAsync();
                }


            FillEntityReport(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            Mlcsvreport responseObj = await _uowMlereport.Repository.Insert(obj);


            await _uowMlereport.CommitAsync();
            return _mapper.Map<CreateOrEditMlcSvReportDto>(responseObj);
        }

        // Update Report
        private async Task<CreateOrEditMlcSvReportDto> UpdateMleReport(CreateOrEditMlcSvReportDto input)
        {
            var _uowMlereport = new UnitOfWork<Mlcsvreport>(_uowMlcSvInitialInfo.GetDbContext());
            var dbObj = await _uowMlereport.Repository.GetById(input.MlcsvreportId!);
            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowMlcSvInitialInfo.GetDbContext());

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityReport(obj!);

            //if (input.PatientDrawImgId != null)
            //{
            //    var psi = await _uowPatientImage.GetDbContext().PatientImages
            //        .Where(x => x.PatientImageId == input.PatientDrawImgId).FirstOrDefaultAsync();

            //    psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcDrawImage.base64, _tokenService.GetAccessToken());
            //    psi.ActionTypeId = 2;
            //    psi.UpdatedOn = DateTime.Now;
            //    psi.UpdatedBy = _tokenService.GetUserId();

            //    _uowPatientImage.Repository.Update(psi);
            //    await _uowPatientImage.CommitAsync();

            //    //.........
            //}

            //................................

            if (input.ManualReportImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.ManualReportImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.ManualReportImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.ManualReport))
                {
                    if (input.ManualReport.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.ManualReport.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.ManualReport.base64, _tokenService.GetAccessToken());
                        pi.ActionTypeId = 2;
                        pi.UpdatedOn = DateTime.Now;
                        pi.UpdatedBy = _tokenService.GetUserId();

                        _uowPatientImage.Repository.Update(pi);
                        await _uowPatientImage.CommitAsync();

                        _uowBase64.Repository.Update(b64);
                        await _uowBase64.CommitAsync();
                    }
                }
            }
            else
            {
                if (!AppCommonMethod.IsNullObject(input.ManualReport))
                {
                    if (input.ManualReport.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        input.ManualReport.PatientImageId = Guid.NewGuid();
                        obj.ManualReportImageId = input.ManualReport.PatientImageId;

                        var ptimg = _mapper.Map<PatientImage>(input.ManualReport);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.ManualReport.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.ManualReport.base64, _tokenService.GetAccessToken());

                        ImageBaseSixty4.PatientImageId = ptimg.PatientImageId;

                        ptimg.PatientId = input.PatientId;
                        ptimg.ActionTypeId = 1;
                        ptimg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                        ptimg.CreatedOn = DateTime.Now;
                        ptimg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptimg);
                        await _uowPatientImage.CommitAsync();
                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }
            }

            //................................

            //if (input.PatientManualReportId != null)
            //{
            //    var psi = await _uowPatientImage.GetDbContext().PatientImages
            //        .Where(x => x.PatientImageId == input.PatientManualReportId).FirstOrDefaultAsync();

            //    psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcManualReport.base64, _tokenService.GetAccessToken());
            //    psi.ActionTypeId = 2;
            //    psi.UpdatedOn = DateTime.Now;
            //    psi.UpdatedBy = _tokenService.GetUserId();

            //    _uowPatientImage.Repository.Update(psi);
            //    await _uowPatientImage.CommitAsync();
            //}


            if (input.FinalReportImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.FinalReportImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.FinalReportImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.FinalReport))
                {
                    if (input.FinalReport.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.FinalReport.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.FinalReport.base64, _tokenService.GetAccessToken());
                        pi.ActionTypeId = 2;
                        pi.UpdatedOn = DateTime.Now;
                        pi.UpdatedBy = _tokenService.GetUserId();

                        _uowPatientImage.Repository.Update(pi);
                        await _uowPatientImage.CommitAsync();

                        _uowBase64.Repository.Update(b64);
                        await _uowBase64.CommitAsync();
                    }
                }
            }
            else
            {
                if (!AppCommonMethod.IsNullObject(input.FinalReport))
                {
                    if (input.FinalReport.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        input.FinalReport.PatientImageId = Guid.NewGuid();
                        obj.FinalReportImageId = input.FinalReport.PatientImageId;

                        var ptimg = _mapper.Map<PatientImage>(input.FinalReport);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.FinalReport.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.FinalReport.base64, _tokenService.GetAccessToken());

                        ImageBaseSixty4.PatientImageId = ptimg.PatientImageId;

                        ptimg.PatientId = input.PatientId;
                        ptimg.ActionTypeId = 1;
                        ptimg.ImageBaseSixtyFourId = ImageBaseSixty4.ImageBaseSixtyFourId;
                        ptimg.CreatedOn = DateTime.Now;
                        ptimg.CreatedBy = _tokenService.GetUserId();

                        await _uowPatientImage.Repository.Insert(ptimg);
                        await _uowPatientImage.CommitAsync();
                        // Insert In ImageBase64 Table
                        await _uowBase64.Repository.Insert(ImageBaseSixty4);
                        await _uowBase64.CommitAsync();
                    }
                }
            }

            _uowMlereport.Repository.Update(obj!);
            await _uowMlereport.CommitAsync();

            return _mapper.Map<CreateOrEditMlcSvReportDto>(obj);
        }

        #endregion

        #region Read
        public async Task<List<GetAllMlcSvSingleRecordDto>> GetSingleMlcSvRecordByPatientId(Guid PatientId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowMlcSvInitialInfo.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mlc].[SpGetMlcSvPatient]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllMlcSvSingleRecordDto> lst = ds.Tables[0].ToList<GetAllMlcSvSingleRecordDto>();



                    return lst;
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

        public async Task<ViewPagerDto<GetAllMLCSVRecord>> GetAllMlcSvRecord(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowMlcSvInitialInfo.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mlc].[SpGetMlcSvPatient]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "All");
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllMLCSVRecord> lst = ds.Tables[0].ToList<GetAllMLCSVRecord>();

                    IEnumerable<GetAllMLCSVRecord> lstto;

                    //dynamic objList;
                    List<GetAllMLCSVRecord> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x => x.PatientName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();


                    var pagedList = await Task.Run(() => PagedListDto<GetAllMLCSVRecord>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<GetAllMLCSVRecord>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<GetAllMLCSVRecord>>(pagedList)
                    };

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
        }
        #endregion

        #region Helper Methods
        private void FillEntity(MlcsvinitialInfo obj)
        {
            if (obj.MlcsvinitialInfoId == Guid.Empty)
            {
                obj.MlcsvinitialInfoId = Guid.NewGuid();
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

        private void FillEntityExaminatin(Mlcsvexamination obj)
        {
            if (obj.MlcsvexaminationId == Guid.Empty)
            {
                obj.MlcsvexaminationId = Guid.NewGuid();
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

        private void FillEntityEvidenceCollected(MlcsvevidenceCollected obj)
        {
            if (obj.MlcsvevidenceCollectedId == Guid.Empty)
            {
                obj.MlcsvevidenceCollectedId = Guid.NewGuid();
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

        private void FillEntityReport(Mlcsvreport obj)
        {
            if (obj.MlcsvreportId == Guid.Empty)
            {
                obj.MlcsvreportId = Guid.NewGuid();
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
