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
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service
{
    public class FitnessCertificateService<TEntity> : IFitnessCertificate where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<FitnessCertificate> _uowFitnessCertificate;
        private UnitOfWork<PatientImage> _uowPatientImage;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly UploadFiles _fileUploader;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        private readonly bool _isDevelopment;
        private readonly string _qrBaseUrl;
        #endregion

        #region Constructor
        public FitnessCertificateService(TokenService tokenService, IMapper mapper, UnitOfWork<FitnessCertificate> uowFitnessCertificate, UploadFiles uploadFiles, UnitOfWork<PatientImage> uowPatientImage, PatientDiagnoseService patientDiagnoseService, IQrCodeGeneratorHelper qrCodeGenerator, IConfiguration config)
        {
            _tokenService = tokenService;
            _uowFitnessCertificate = uowFitnessCertificate;
            _mapper = mapper;
            _fileUploader = uploadFiles;
            _uowPatientImage = uowPatientImage;
            _patientDiagnoseService = patientDiagnoseService;
            _qrCodeGenerator = qrCodeGenerator;

            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _qrBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("CrystalUrl").Value ?? string.Empty;
            else
                _qrBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("CrystalUrl").Value ?? string.Empty;
        }
        #endregion

        #region CUD 
        public async Task<CreateOrEditFitnessCertificateDto> CreateOrEdit(CreateOrEditFitnessCertificateDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.FitnessCertificateId))
                {
                    return await Create(input);
                }
                else
                {
                    return await Update(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Create Record
        private async Task<CreateOrEditFitnessCertificateDto> Create(CreateOrEditFitnessCertificateDto input)
        {
            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowFitnessCertificate.GetDbContext());

            PatientDiagnoseDto digDto = new PatientDiagnoseDto();
            digDto.FormType = input.FormType;
            digDto.DocDepartmentLookupId = input.DocDepartmentLookupId;
            digDto.DocSectionLookupId = input.DocSectionLookupId;
            digDto.PatientId = input.PatientId;
            digDto.PatientVisitId = input.PatientVisitId;

            var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);

            input.PatientDiagnoseId = digService.PatientDiagnoseId;

            var obj = _mapper.Map<FitnessCertificate>(input);

            // Generate Tracking Id here
                var random = new Random();
                var chars = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
                var randomNo = new string(Enumerable.Repeat(chars, 7)
                   .Select(s => s[random.Next(s.Length)]).ToArray());

            obj.TrackingId = input.HealthFacilityId.ToString() + "-" + randomNo + "-" + DateTime.Now.Year.ToString();

            var emc = _mapper.Map<Emc>(input.emc);
            emc.EmcId = Guid.NewGuid();
            emc.PatientId = obj.PatientId;
            emc.PatientVisitId = obj.PatientVisitId;
            emc.PatientDiagnoseId = obj.PatientDiagnoseId;
            emc.HealthFacilityId = obj.HealthFacilityId;
            emc.CreatedOn = DateTime.Now;
            emc.CreatedBy = _tokenService.GetUserId();
            emc.ActionTypeId = (int)ActionTypeEnum.Create;

            var _uowEmc = new UnitOfWork<Emc>(_uowFitnessCertificate.GetDbContext());

            await _uowEmc.Repository.Insert(emc);
            await _uowEmc.CommitAsync();

            // Start QrCode here
            if (obj.QrCodeImagePath == null)
            {
                //string qrCodeValue = "Patient Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;
                string qrCodeValue = "";

                if (obj.DesignationAppliedFor == "Arms License")
                {
                    qrCodeValue = _qrBaseUrl + "GetSingleFitnessCertificateForAslah?patientVisitId="+obj.PatientVisitId;
                }
                else
                {
                    qrCodeValue = _qrBaseUrl + "GetSingleFitnessCertificate?patientVisitId=" + obj.PatientVisitId;
                }


                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                if (obj.QrCodeImagePath == null)
                {
                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                    obj.QrCodeImagePath = qrCodeImageUrl;
                }
            }

            // End QrCode here


            if (input.EmployeeLetterNo != null)
            {
                var empLetterNo = await _uowFitnessCertificate.GetDbContext()
                    .FitnessCertificates
                    .Where(x => x.EmployeeLetterNo == input.EmployeeLetterNo && x.HealthFacilityId == input.HealthFacilityId)
                    .FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullObject(empLetterNo))
                {
                    if (empLetterNo.IsEmployeeLetterNoGenerated == true)
                    {
                        throw new UserFriendlyException(CommonMessageConstant.DuplicateEmployeeLetterNumberFound);
                    }
                }

            }


            FillEntity(obj);


            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            // Patient Image

            if (!AppCommonMethod.IsNullObject(input.PatientImage))
            {
                if (input.PatientImage.base64 != "")
                {
                    var ImageBaseSixty4 = new ImageBaseSixtyFour();
                    //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                    var ptimg = _mapper.Map<PatientImage>(input.PatientImage);
                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.PatientImage.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientImage.base64, _tokenService.GetAccessToken());
                    ptimg.PatientImageId = Guid.NewGuid();

                    obj.PatientImageId = ptimg.PatientImageId;


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

            // Right Index

            if (!AppCommonMethod.IsNullObject(input.RightIndexImage))
            {
                if (input.RightIndexImage.base64 != "")
                {
                    var ImageBaseSixty4 = new ImageBaseSixtyFour();
                    //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                    var ptimg = _mapper.Map<PatientImage>(input.RightIndexImage);
                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.RightIndexImage.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightIndexImage.base64, _tokenService.GetAccessToken());
                    ptimg.PatientImageId = Guid.NewGuid();

                    obj.RightIndexImageId = ptimg.PatientImageId;


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

            // Right Middle

            if (!AppCommonMethod.IsNullObject(input.RightMiddle))
            {
                if (input.RightMiddle.base64 != "")
                {
                    var ImageBaseSixty4 = new ImageBaseSixtyFour();
                    //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                    var ptimg = _mapper.Map<PatientImage>(input.RightMiddle);
                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.RightMiddle.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightMiddle.base64, _tokenService.GetAccessToken());
                    ptimg.PatientImageId = Guid.NewGuid();

                    obj.RightMiddleImageId = ptimg.PatientImageId;


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


            // Right Little

            if (!AppCommonMethod.IsNullObject(input.RightLittle))
            {
                if (input.RightLittle.base64 != "")
                {
                    var ImageBaseSixty4 = new ImageBaseSixtyFour();
                    //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                    var ptimg = _mapper.Map<PatientImage>(input.RightLittle);
                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.RightLittle.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightLittle.base64, _tokenService.GetAccessToken());
                    ptimg.PatientImageId = Guid.NewGuid();

                    obj.RightLittleImageId = ptimg.PatientImageId;


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


            // Right Ring

            if (!AppCommonMethod.IsNullObject(input.RightRing))
            {
                if (input.RightRing.base64 != "")
                {
                    var ImageBaseSixty4 = new ImageBaseSixtyFour();
                    //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                    var ptimg = _mapper.Map<PatientImage>(input.RightRing);
                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.RightRing.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightRing.base64, _tokenService.GetAccessToken());
                    ptimg.PatientImageId = Guid.NewGuid();

                    obj.RightRingImageId = ptimg.PatientImageId;


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

            // Right Thumb
            if (!AppCommonMethod.IsNullObject(input.RightThumb))
            {
                if (input.RightThumb.base64 != "")
                {
                    var ImageBaseSixty4 = new ImageBaseSixtyFour();
                    //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                    var ptimg = _mapper.Map<PatientImage>(input.RightThumb);
                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.RightThumb.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightThumb.base64, _tokenService.GetAccessToken());
                    ptimg.PatientImageId = Guid.NewGuid();

                    obj.RightThumbImageId = ptimg.PatientImageId;


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


            // Insert in Fitness Serology
            var _uowSerology = new UnitOfWork<FitnessSerology>(_uowFitnessCertificate.GetDbContext());

            var serologyObj = _mapper.Map<FitnessSerology>(input.Serology);
            serologyObj.FitnessSerologyId = Guid.NewGuid();
            serologyObj.PatientId = input.PatientId;
            serologyObj.PatientVisitId = input.PatientVisitId;
            serologyObj.PatientDiagnoseId = input.PatientDiagnoseId;
            serologyObj.HealthFacilityId = input.HealthFacilityId;
            serologyObj.FitnessCertificateId = obj.FitnessCertificateId;
            serologyObj.CreatedOn = DateTime.Now;
            serologyObj.CreatedBy = _tokenService.GetUserId();
            serologyObj.ActionTypeId = (int)ActionTypeEnum.Create;
            serologyObj.IsActive = true;

            await _uowSerology.Repository.Insert(serologyObj);
            await _uowSerology.CommitAsync();

            // Insert in CBC
            var _uowCbc = new UnitOfWork<FitnessCbc>(_uowFitnessCertificate.GetDbContext());

            var cbcObj = _mapper.Map<FitnessCbc>(input.Cbc);
            cbcObj.FitnessCbcid = Guid.NewGuid();
            cbcObj.PatientId = input.PatientId;
            cbcObj.PatientVisitId = input.PatientVisitId;
            cbcObj.PatientDiagnoseId = input.PatientDiagnoseId;
            cbcObj.HealthFacilityId = input.HealthFacilityId;
            cbcObj.FitnessCertificateId = obj.FitnessCertificateId;
            cbcObj.CreatedOn = DateTime.Now;
            cbcObj.CreatedBy = _tokenService.GetUserId();
            cbcObj.ActionTypeId = (int)ActionTypeEnum.Create;
            cbcObj.IsActive = true;

            await _uowCbc.Repository.Insert(cbcObj);
            await _uowCbc.CommitAsync();

            // Insert in Urine CE
            var _uowUrineCE = new UnitOfWork<FitnessUrineCe>(_uowFitnessCertificate.GetDbContext());

            var urineObj = _mapper.Map<FitnessUrineCe>(input.UrineCe);
            urineObj.FitnessUrineCeid = Guid.NewGuid();
            urineObj.PatientId = input.PatientId;
            urineObj.PatientVisitId = input.PatientVisitId;
            urineObj.PatientDiagnoseId = input.PatientDiagnoseId;
            urineObj.HealthFacilityId = input.HealthFacilityId;
            urineObj.FitnessCertificateId = obj.FitnessCertificateId;
            urineObj.CreatedOn = DateTime.Now;
            urineObj.CreatedBy = _tokenService.GetUserId();
            urineObj.ActionTypeId = (int)ActionTypeEnum.Create;
            urineObj.IsActive = true;

            await _uowUrineCE.Repository.Insert(urineObj);
            await _uowUrineCE.CommitAsync();

            // Insert in General Or Widal Parameter
            var _uowGeneralOrWidalParameter = new UnitOfWork<FitnessGeneralParameter>(_uowFitnessCertificate.GetDbContext());

            var generalOrWidalObj = _mapper.Map<FitnessGeneralParameter>(input.GeneralOrWidalParameter);
            generalOrWidalObj.FitnessGeneralParameterId = Guid.NewGuid();
            generalOrWidalObj.PatientId = input.PatientId;
            generalOrWidalObj.PatientVisitId = input.PatientVisitId;
            generalOrWidalObj.PatientDiagnoseId = input.PatientDiagnoseId;
            generalOrWidalObj.HealthFacilityId = input.HealthFacilityId;
            generalOrWidalObj.FitnessCertificateId = obj.FitnessCertificateId;
            generalOrWidalObj.CreatedOn = DateTime.Now;
            generalOrWidalObj.CreatedBy = _tokenService.GetUserId();
            generalOrWidalObj.ActionTypeId = (int)ActionTypeEnum.Create;
            generalOrWidalObj.IsActive = true;

            await _uowGeneralOrWidalParameter.Repository.Insert(generalOrWidalObj);
            await _uowGeneralOrWidalParameter.CommitAsync();

            // Insert in Stool Examination
            var _uowStoolExamination = new UnitOfWork<FitnessStoolExamination>(_uowFitnessCertificate.GetDbContext());

            var stoolExaminationObj = _mapper.Map<FitnessStoolExamination>(input.StoolExamination);
            stoolExaminationObj.FitnessStoolExaminationId = Guid.NewGuid();
            stoolExaminationObj.PatientId = input.PatientId;
            stoolExaminationObj.PatientVisitId = input.PatientVisitId;
            stoolExaminationObj.PatientDiagnoseId = input.PatientDiagnoseId;
            stoolExaminationObj.HealthFacilityId = input.HealthFacilityId;
            stoolExaminationObj.FitnessCertificateId = obj.FitnessCertificateId;
            stoolExaminationObj.CreatedOn = DateTime.Now;
            stoolExaminationObj.CreatedBy = _tokenService.GetUserId();
            stoolExaminationObj.ActionTypeId = (int)ActionTypeEnum.Create;
            stoolExaminationObj.IsActive = true;

            await _uowStoolExamination.Repository.Insert(stoolExaminationObj);
            await _uowStoolExamination.CommitAsync();

            obj.IsEmployeeLetterNoGenerated = true;

            // Insert in Mental Assessment
            if (!AppCommonMethod.IsNullOrEmptyList(input.PsyChologicalAssessment))
            {
                foreach (var item in input.PsyChologicalAssessment)
                {
                    var _uowMentalAssessment = new UnitOfWork<MentalAssessment>(_uowFitnessCertificate.GetDbContext());
                    var mentalAssessment = _mapper.Map<MentalAssessment>(item);
                    mentalAssessment.MentalAssessmentId = Guid.NewGuid();
                    mentalAssessment.IsActive = true;
                    mentalAssessment.PatientId = input.PatientId;
                    mentalAssessment.PatientVisitId = input.PatientVisitId;
                    mentalAssessment.PatientDiagnoseId = input.PatientDiagnoseId;
                    mentalAssessment.HealthFacilityId = input.HealthFacilityId;
                    mentalAssessment.ActionTypeId = (int)ActionTypeEnum.Create;
                    mentalAssessment.CreatedOn = DateTime.Now;
                    mentalAssessment.CreatedBy = _tokenService.GetUserId();
                    await _uowMentalAssessment.Repository.Insert(mentalAssessment);
                    await _uowMentalAssessment.CommitAsync();
                }
            }

            FitnessCertificate responseObj = await _uowFitnessCertificate.Repository.Insert(obj);

            await _uowFitnessCertificate.CommitAsync();
            return _mapper.Map<CreateOrEditFitnessCertificateDto>(responseObj);
        }

        private async Task<CreateOrEditFitnessCertificateDto> Update(CreateOrEditFitnessCertificateDto input)
        {
            var dbObj = await _uowFitnessCertificate.Repository.GetById(input.FitnessCertificateId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);

            //var emc = _mapper.Map<Emc>(input.emc);
            //emc.PatientId = obj.PatientId;
            //emc.PatientVisitId = obj.PatientVisitId;
            //emc.PatientDiagnoseId = obj.PatientDiagnoseId;
            //emc.HealthFacilityId = obj.HealthFacilityId;
            //emc.CreatedOn = DateTime.Now;
            //emc.CreatedBy = _tokenService.GetUserId();

            //var _uowEmc = new UnitOfWork<Emc>(_uowFitnessCertificate.GetDbContext());

            //_uowEmc.Repository.Update(emc);
            //await _uowEmc.CommitAsync();

            FillEntity(obj!);

            // for qr code
            //string qrCodeValue = "Patient Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;
            string qrCodeValue = "";

            if (obj.DesignationAppliedFor == "Arms License")
            {
                qrCodeValue = _qrBaseUrl + "GetSingleFitnessCertificateForAslah?patientVisitId=" + obj.PatientVisitId;
            }
            else
            {
                qrCodeValue = _qrBaseUrl + "GetSingleFitnessCertificate?patientVisitId=" + obj.PatientVisitId;
            }

            string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                    obj.QrCodeImagePath = qrCodeImageUrl;
            //

            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowFitnessCertificate.GetDbContext());

            // Patient Image

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
                        //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.PatientImage);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientImage.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.PatientImageId = ptimg.PatientImageId;


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

            // Right Index

            if (input.RightIndexImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.RightIndexImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.RightIndexImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.RightIndexImage))
                {
                    if (input.RightIndexImage.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.RightIndexImage.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightIndexImage.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.RightIndexImage))
                {
                    if (input.RightIndexImage.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.RightIndexImage);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.RightIndexImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightIndexImage.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.RightIndexImageId = ptimg.PatientImageId;


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


            // Right Middle

            if (input.RightMiddleImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.RightMiddleImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.RightMiddleImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.RightMiddle))
                {
                    if (input.RightMiddle.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.RightMiddle.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightMiddle.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.RightMiddle))
                {
                    if (input.RightMiddle.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.RightMiddle);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.RightMiddle.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightMiddle.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.RightMiddleImageId = ptimg.PatientImageId;


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

            // Right Little


            if (input.RightLittleImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.RightLittleImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.RightLittleImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.RightLittle))
                {
                    if (input.RightLittle.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.RightLittle.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightLittle.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.RightLittle))
                {
                    if (input.RightLittle.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.RightLittle);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.RightLittle.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightLittle.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.RightLittleImageId = ptimg.PatientImageId;


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

            // Right Ring
            if (input.RightRingImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.RightRingImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.RightRingImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.RightRing))
                {
                    if (input.RightRing.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.RightRing.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightRing.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.RightRing))
                {
                    if (input.RightRing.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.RightRing);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.RightRing.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightRing.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.RightRingImageId = ptimg.PatientImageId;


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

            // Right Thumb

            if (input.RightThumbImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.RightThumbImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.RightThumbImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.RightThumb))
                {
                    if (input.RightThumb.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.RightThumb.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightThumb.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.RightThumb))
                {
                    if (input.RightThumb.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientImageProfileId = input.PatientImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.RightThumb);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.RightThumb.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.RightThumb.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.RightThumbImageId = ptimg.PatientImageId;


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


            if (!AppCommonMethod.IsNullOrEmptyGuid(input.FitnessCertificateId))
            {
                var _uowSerology = new UnitOfWork<FitnessSerology>(_uowFitnessCertificate.GetDbContext());

                //var dbObjSerology = await _uowSerology.Repository.GetById(input.PatientVisitId!);
                var dbObjSerology = await _uowSerology.GetDbContext().FitnessSerologies.Where(x => x.PatientVisitId == input.PatientVisitId).FirstOrDefaultAsync();


                var serologyObj = _mapper.Map(input.Serology, dbObjSerology);

                serologyObj.PatientId = input.PatientId;
                serologyObj.PatientVisitId = input.PatientVisitId;
                serologyObj.PatientDiagnoseId = input.PatientDiagnoseId;
                serologyObj.HealthFacilityId = input.HealthFacilityId;
                serologyObj.UpdatedOn = DateTime.Now;
                serologyObj.UpdatedBy = _tokenService.GetUserId();
                serologyObj.ActionTypeId = (int)ActionTypeEnum.Edit;
                serologyObj.IsActive = true;

                _uowSerology.Repository.Update(serologyObj);
                await _uowSerology.CommitAsync();

                // Insert in CBC
                var _uowCbc = new UnitOfWork<FitnessCbc>(_uowFitnessCertificate.GetDbContext());

                //var dbObjCbc = await _uowCbc.Repository.GetById(input.PatientVisitId!);
                var dbObjCbc = await _uowCbc.GetDbContext().FitnessCbcs.Where(x => x.PatientVisitId == input.PatientVisitId)
                    .FirstOrDefaultAsync();


                var cbcObj = _mapper.Map(input.Cbc, dbObjCbc);

                cbcObj.PatientId = input.PatientId;
                cbcObj.PatientVisitId = input.PatientVisitId;
                cbcObj.PatientDiagnoseId = input.PatientDiagnoseId;
                cbcObj.HealthFacilityId = input.HealthFacilityId;
                cbcObj.UpdatedOn = DateTime.Now;
                cbcObj.UpdatedBy = _tokenService.GetUserId();
                cbcObj.ActionTypeId = (int)ActionTypeEnum.Edit;
                cbcObj.IsActive = true;

                _uowCbc.Repository.Update(cbcObj);
                await _uowCbc.CommitAsync();

                // Insert in Urine CE
                var _uowUrineCE = new UnitOfWork<FitnessUrineCe>(_uowFitnessCertificate.GetDbContext());

                //var dbObjUrineCE = await _uowUrineCE.Repository.GetById(input.PatientVisitId!);
                var dbObjUrineCE = await _uowUrineCE.GetDbContext().FitnessUrineCes.Where(x => x.PatientVisitId == input.PatientVisitId)
                    .FirstOrDefaultAsync();


                var urineObj = _mapper.Map(input.UrineCe, dbObjUrineCE);

                urineObj.PatientId = input.PatientId;
                urineObj.PatientVisitId = input.PatientVisitId;
                urineObj.PatientDiagnoseId = input.PatientDiagnoseId;
                urineObj.HealthFacilityId = input.HealthFacilityId;
                urineObj.UpdatedOn = DateTime.Now;
                urineObj.UpdatedBy = _tokenService.GetUserId();
                urineObj.ActionTypeId = (int)ActionTypeEnum.Edit;
                urineObj.IsActive = true;


                _uowUrineCE.Repository.Update(urineObj);
                await _uowUrineCE.CommitAsync();

                // Insert in General Or Widal Parameter
                var _uowGeneralOrWidalParameter = new UnitOfWork<FitnessGeneralParameter>(_uowFitnessCertificate.GetDbContext());

                //var dbObjGnrlOrWidl = await _uowGeneralOrWidalParameter.Repository.GetById(input.PatientVisitId!);
                var dbObjGnrlOrWidl = await _uowGeneralOrWidalParameter.GetDbContext().FitnessGeneralParameters.Where(x => x.PatientVisitId == input.PatientVisitId)
                    .FirstOrDefaultAsync();


                var generalOrWidalObj = _mapper.Map(input.GeneralOrWidalParameter, dbObjGnrlOrWidl);


                generalOrWidalObj.PatientId = input.PatientId;
                generalOrWidalObj.PatientVisitId = input.PatientVisitId;
                generalOrWidalObj.PatientDiagnoseId = input.PatientDiagnoseId;
                generalOrWidalObj.HealthFacilityId = input.HealthFacilityId;
                generalOrWidalObj.UpdatedOn = DateTime.Now;
                generalOrWidalObj.UpdatedBy = _tokenService.GetUserId();
                generalOrWidalObj.ActionTypeId = (int)ActionTypeEnum.Edit;
                generalOrWidalObj.IsActive = true;

                _uowGeneralOrWidalParameter.Repository.Update(generalOrWidalObj);
                await _uowGeneralOrWidalParameter.CommitAsync();

                // Insert in Stool Examination
                var _uowStoolExamination = new UnitOfWork<FitnessStoolExamination>(_uowFitnessCertificate.GetDbContext());



                //var dbObjStoolExamination = await _uowStoolExamination.Repository.GetById(input.PatientVisitId!);
                var dbObjStoolExamination = await _uowStoolExamination.GetDbContext().FitnessStoolExaminations.Where(x => x.PatientVisitId == input.PatientVisitId)
                    .FirstOrDefaultAsync();


                var stoolExaminationObj = _mapper.Map(input.StoolExamination, dbObjStoolExamination);

                //var stoolExaminationObj = _mapper.Map<FitnessStoolExamination>(input.StoolExamination);
                stoolExaminationObj.PatientId = input.PatientId;
                stoolExaminationObj.PatientVisitId = input.PatientVisitId;
                stoolExaminationObj.PatientDiagnoseId = input.PatientDiagnoseId;
                stoolExaminationObj.HealthFacilityId = input.HealthFacilityId;
                stoolExaminationObj.UpdatedOn = DateTime.Now;
                stoolExaminationObj.UpdatedBy = _tokenService.GetUserId();
                stoolExaminationObj.ActionTypeId = (int)ActionTypeEnum.Edit;
                stoolExaminationObj.IsActive = true;

                _uowStoolExamination.Repository.Update(stoolExaminationObj);
                await _uowStoolExamination.CommitAsync();

                // Insert in Mental Assessment
                if (!AppCommonMethod.IsNullOrEmptyList(input.PsyChologicalAssessment))
                {
                    foreach (var item in input.PsyChologicalAssessment)
                    {
                        if (!AppCommonMethod.IsNullOrEmptyGuid(item!.MentalAssessmentId))
                        {
                            var dbObjMentalAssessment = await _uowStoolExamination.GetDbContext().MentalAssessments.Where(x => x.MentalAssessmentId == item.MentalAssessmentId)
                            .FirstOrDefaultAsync();

                            var _uowMentalAssessment = new UnitOfWork<MentalAssessment>(_uowFitnessCertificate.GetDbContext());
                            var mentalAssessment = _mapper.Map(item, dbObjMentalAssessment);
                            mentalAssessment.IsActive = true;
                            mentalAssessment.PatientId = input.PatientId;
                            mentalAssessment.PatientVisitId = input.PatientVisitId;
                            mentalAssessment.PatientDiagnoseId = input.PatientDiagnoseId;
                            mentalAssessment.HealthFacilityId = input.HealthFacilityId;
                            mentalAssessment.ActionTypeId = (int)ActionTypeEnum.Edit;
                            mentalAssessment.UpdatedOn = DateTime.Now;
                            mentalAssessment.UpdatedBy = _tokenService.GetUserId();
                            _uowMentalAssessment.Repository.Update(mentalAssessment);
                            await _uowMentalAssessment.CommitAsync();
                        }
                    }
                }
            }

            _uowFitnessCertificate.Repository.Update(obj!);
            await _uowFitnessCertificate.CommitAsync();

            return _mapper.Map<CreateOrEditFitnessCertificateDto>(obj);
        }

        #endregion

        #region Read
        public async Task<ViewPagerDto<GetAllFitnessCertificateDto>> GetAllFitnessCertificatePatients(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowFitnessCertificate.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetFitnessCertificateRecords]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "All");
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllFitnessCertificateDto> lst = ds.Tables[0].ToList<GetAllFitnessCertificateDto>();

                    IEnumerable<GetAllFitnessCertificateDto> lstto;

                    //dynamic objList;
                    List<GetAllFitnessCertificateDto> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x => x.FullName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();

                    //var pagedList = await PagedListDto<MLEAllPatientListDto>.ToPagedListAsync(
                    //    lstto,
                    //    filter.PageNumber,
                    //    filter.PageSize
                    //   );

                    var pagedList = await Task.Run(() => PagedListDto<GetAllFitnessCertificateDto>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<GetAllFitnessCertificateDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<GetAllFitnessCertificateDto>>(pagedList)
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

        //public async Task<List<GetSingleFitnessCertificateDto>> GetSingleFitnessCertificatePatientInfo(Guid PatientVisitId)
        //{
        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowFitnessCertificate.GetDbContext().Database.GetDbConnection();
        //        try
        //        {

        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("[emc].[SpGetFitnessCertificateRecords]", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;
        //            sqlComm.Parameters.AddWithValue("@Activity", "ById");
        //            sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<GetSingleFitnessCertificateDto> lst = ds.Tables[0].ToList<GetSingleFitnessCertificateDto>();



        //            return lst;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        public async Task<GetSingleFitnessCertificateDtoWithQuestions> GetSingleFitnessCertificatePatientInfo(Guid PatientVisitId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowFitnessCertificate.GetDbContext().Database.GetDbConnection();
                try
                {
                    GetSingleFitnessCertificateDtoWithQuestions fitness = new GetSingleFitnessCertificateDtoWithQuestions();

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetFitnessCertificateRecords]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    fitness.fitnessSingleRecord = ds.Tables[0].ToList<GetSingleFitnessCertificateDto>();
                    fitness.PsychologicalQuestionsAndAnswer = ds.Tables[1].ToList<PsychologicalQuestionAndAnswers>();


                    return fitness;
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

        public async Task<List<ViewGetAllIpsychologicalAssessmentQuestion>> GetTenPsychologicalAssessmentQuestions()
        {
            var lst = await _uowFitnessCertificate.GetDbContext()
                .ViewGetAllIpsychologicalAssessmentQuestions
                .OrderBy(x => Guid.NewGuid()).Take(10).ToListAsync();
            return lst;
        }
        #endregion

        #region Helper Methods

        private void FillEntity(FitnessCertificate obj)
        {
            if (obj.FitnessCertificateId == Guid.Empty)
            {
                obj.FitnessCertificateId = Guid.NewGuid();
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
        private void FillEntityDelete(FitnessCertificate obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }
        #endregion
    }
}