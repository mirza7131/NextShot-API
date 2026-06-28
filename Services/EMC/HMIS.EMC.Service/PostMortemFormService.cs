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
using System.Data;

namespace HMIS.EMC.Service
{
    public class PostMortemFormService<TEntity> : IPostMortemForm where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly IMapper _mapper;
        private UnitOfWork<Mlcpostmortem> _uowMlcpostmortem;
        private UnitOfWork<MlcbodyIdentifierInfo> _uowMlcbodyIdentifierInfo;
        private UnitOfWork<PostmortemExternalExamination> _uowPostmortemExternalExamination;
        private UnitOfWork<PostMortemInternalExamination> _uowPostmortemInternalExamination;
        private UnitOfWork<PostmortemReport> _uowPostmortemReport;
        private UnitOfWork<Mlc> _unitOfWorkMlc;
        private readonly UploadFiles _fileUploader;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        #endregion

        #region Constructor
        public PostMortemFormService(TokenService tokenService, IMapper mapper, UnitOfWork<Mlcpostmortem> uowMlcpostmortem,
            UnitOfWork<MlcbodyIdentifierInfo> uowMlcbodyIdentifierInfo, UnitOfWork<PostmortemExternalExamination> uowPostmortemExternalExamination,
            UnitOfWork<PostMortemInternalExamination> uowPostmortemInternalExamination, UnitOfWork<PostmortemReport> uowPostmortemReport, UploadFiles uploadFiles,
            PatientDiagnoseService patientDiagnoseService,
            IQrCodeGeneratorHelper qrCodeGenerator, UnitOfWork<Mlc> unitOfWorkMlc)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowMlcpostmortem = uowMlcpostmortem;
            _uowMlcbodyIdentifierInfo = uowMlcbodyIdentifierInfo;
            _uowPostmortemExternalExamination = uowPostmortemExternalExamination;
            _uowPostmortemInternalExamination = uowPostmortemInternalExamination;
            _uowPostmortemReport = uowPostmortemReport;
            _fileUploader = uploadFiles;
            _patientDiagnoseService = patientDiagnoseService;
            _qrCodeGenerator = qrCodeGenerator;
            _unitOfWorkMlc = unitOfWorkMlc;
        }
        #endregion

        #region CUD

        // Create or Edit General Form
        public async Task<CreateOrEditPostMortemGeneralFormDto> CreateOrEdit(CreateOrEditPostMortemGeneralFormDto input)
        {
            try
            { 
                var obj=await _uowMlcpostmortem.GetDbContext().Mlcpostmortems.Where(x=>x.PatientVisitId == input.PatientVisitId).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullObject(obj))
                {
                    //input.PatientDiagnoseId = Guid.NewGuid();
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
        // Create or Edit External Form
        public async Task<CreateOrEditPostMortemExternalFormDto> CreateOrEdit(CreateOrEditPostMortemExternalFormDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.PostmortemExternalExaminationId))
                {
                    input.PatientDiagnoseId = Guid.NewGuid();
                    return await CreateExternal(input);
                }
                else
                {
                    return await UpdateExternal(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CreateOrEditPostMortemInternalFormDto> CreateOrEdit(CreateOrEditPostMortemInternalFormDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.PostMortemInternalExaminationId))
                {
                    input.PatientDiagnoseId = Guid.NewGuid();
                    return await CreateInternal(input);
                }
                else
                {
                    return await UpdateInternal(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateOrEditPostMortemReportDto> CreateOrEdit(CreateOrEditPostMortemReportDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.PostmortemReportId))
                {
                    input.PatientDiagnoseId = Guid.NewGuid();
                    return await CreateReport(input);
                }
                else
                {
                    return await UpdateReport(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<CreateOrEditPostMortemGeneralFormDto> Create(CreateOrEditPostMortemGeneralFormDto input)
        {
            //using (var trans = _uowMlcpostmortem.GetDbContext().Database.BeginTransaction())
            //{
                try
                {

                if (input.mlc.BookNo != null)
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


                if (input.mlc.BookNo == null)
                {
                    input.PatientDiagnoseId = null;
                }

                //// For Closing Visit
                //var _uowPatientOpenVisits = new UnitOfWork<PatientOpenVisit>(_uowMlcpostmortem.GetDbContext());

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
                //// Closing Visit till here


                var obj = _mapper.Map<Mlcpostmortem>(input);

                    //MLC Common
                    var _uowMlc = new UnitOfWork<Mlc>(_uowMlcpostmortem.GetDbContext());

                    var mle = _mapper.Map<Mlc>(input.mlc);
                    mle.Mlcid = Guid.NewGuid();
                    mle.PatientId = input.PatientId;
                    mle.PatientVisitId = input.PatientVisitId;
                    mle.PatientDiagnoseId = input.PatientDiagnoseId;
                    mle.HealthFacilityId = input.HealthFacilityId;
                    mle.IsActive = true;
                    mle.ActionTypeId = (int)ActionTypeEnum.Create;
                    mle.CreatedOn = DateTime.Now;
                    mle.CreatedBy = _tokenService.GetUserId();

                    // Generate MlcNo    here
                    var random = new Random();
                    var chars = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
                    var randomNo = new string(Enumerable.Repeat(chars, 7)
                       .Select(s => s[random.Next(s.Length)]).ToArray());

                    mle.Mlcno = input.HealthFacilityId.ToString() + "-" + randomNo + "-" + DateTime.Now.Year.ToString();
                    // Set Mlc ID For MlcPostmortem
                    obj.Mlcid = mle.Mlcid;

                var random1 = new Random();
                var chars1 = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
                var randomNo1 = new string(Enumerable.Repeat(chars, 7)
                   .Select(s => s[random.Next(s.Length)]).ToArray());

                input.Pmrno = input.HealthFacilityId.ToString() + "-" + randomNo1 + "-" + DateTime.Now.Year.ToString();
                // Set Pmrno For MlcPostmortem
                obj.Pmrno = input.Pmrno;


                // Mlc Body Identifiers
                if (!AppCommonMethod.IsNullOrEmptyList(input.BodyIdentifierInfoDtos))
                    {
                        foreach (var item in input.BodyIdentifierInfoDtos)
                        {
                            var obj1 = _mapper.Map<MlcbodyIdentifierInfo>(item);
                            obj1.Mlcid = mle.Mlcid;
                            FillEntityBodyIdentifierInfo(obj1);
                            MlcbodyIdentifierInfo objs = await _uowMlcbodyIdentifierInfo.Repository.Insert(obj1);
                            await _uowMlcbodyIdentifierInfo.CommitAsync();
                        }
                    }

                    // Mlc Police Info
                    if (!AppCommonMethod.IsNullObject(input.mlcpoliceInfos))
                    {
                        var objMlcPlc = _mapper.Map<MlcpoliceInfo>(input.mlcpoliceInfos);
                        objMlcPlc.Mlcid = mle.Mlcid;
                        objMlcPlc.MlcpoliceInfoId = Guid.NewGuid();
                        objMlcPlc.CreatedOn = DateTime.Now;
                        objMlcPlc.CreatedBy = _tokenService.GetUserId();


                        await _uowMlcbodyIdentifierInfo.GetDbContext().AddAsync(objMlcPlc);
                        await _uowMlcbodyIdentifierInfo.GetDbContext().SaveChangesAsync();
                    }




                    FillEntity(obj);
                    //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


                    //if (profile != null)
                    //{
                    //    obj.FormTypeProfileId = profile.ProfileId;

                    //}
                    //Mlc mlc = await _uowMlc.Repository.Insert(mle);
                    //await _uowMlc.CommitAsync();

                    obj.Mlc = mle;
                    obj.IsActive = true;
                    Mlcpostmortem responseObj = await _uowMlcpostmortem.Repository.Insert(obj);

                    await _uowMlcpostmortem.CommitAsync();
                    //trans.Commit();
                    return _mapper.Map<CreateOrEditPostMortemGeneralFormDto>(responseObj);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            //}
        }

        private async Task<CreateOrEditPostMortemGeneralFormDto> Update(CreateOrEditPostMortemGeneralFormDto input)
        {
            try
            {

                if (!AppCommonMethod.IsNullObject(input.mlc))
                {
                    if (input.mlc.BookNo != null)
                    {

                        if (input.PatientDiagnoseId == null)
                        {
                            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                            {
                                var chkPtDiagnose = await _uowMlcpostmortem.GetDbContext().PatientDiagnoses.Where(x => x.PatientDiagnoseId == input.PatientDiagnoseId)
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
                                    input.mlc.PatientDiagnoseId = digService.PatientDiagnoseId;
                                    input.mlcpoliceInfos.PatientDiagnoseId = digService.PatientDiagnoseId;
                                }
                            }
                        }
                    }

                    if(input.IsReportCount != true)
                    {
                        if (input.mlc.BookNo == null)
                        {
                            input.PatientDiagnoseId = null;
                        }
                    }


                    if (input.mlc == null)
                    {
                        input.PatientDiagnoseId = null;
                    }
                }




                var o = await _uowMlcpostmortem.GetDbContext().Mlcpostmortems
                    .Where(x => x.PatientVisitId == input.PatientVisitId)
                    .Include(x=>x.Mlc).FirstOrDefaultAsync();

                var dbObj = await _uowMlcpostmortem.Repository.GetById(o.MlcpostmortemId);


                if (AppCommonMethod.IsNullObject(dbObj))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                // Mlc Body Identifiers
                //if (!AppCommonMethod.IsNullOrEmptyList(input.BodyIdentifierInfoDtos))
                //{

                //if (!AppCommonMethod.IsNullOrEmptyList(lst))
                //{
                if (!AppCommonMethod.IsNullOrEmptyList(input.BodyIdentifierInfoDtos))
                {
                    InsertOrUpdateBodyIdentifier(input);
                }                   
                    //}
                    //else
                    //{
                    //    foreach (var item in input.BodyIdentifierInfoDtos)
                    //    {
                    //        var obj1 = _mapper.Map<MlcbodyIdentifierInfo>(item);
                    //        obj1.Mlcid = input.Mlcid;
                    //        obj1.PatientDiagnoseId = input.PatientDiagnoseId;
                    //        FillEntityBodyIdentifierInfo(obj1);

                    //        MlcbodyIdentifierInfo objs = await _uowMlcbodyIdentifierInfo.Repository.Insert(obj1);
                    //        await _uowMlcbodyIdentifierInfo.CommitAsync();
                    //    }
                    //}

                //}

                // Mlc Police Info
                //if (AppCommonMethod.IsNullObject(input.mlcpoliceInfos))
                //    input.mlcpoliceInfos = new CreateOrEditMlcPoliceInfoDto();
                if (!AppCommonMethod.IsNullObject(input.mlcpoliceInfos))
                {
                    if (AppCommonMethod.IsNullOrEmptyGuid(input.mlcpoliceInfos.MlcpoliceInfoId))
                    {
                        var objMlcPlc = _mapper.Map<MlcpoliceInfo>(input.mlcpoliceInfos);
                        objMlcPlc.Mlcid = input.Mlcid;
                        objMlcPlc.MlcpoliceInfoId = Guid.NewGuid();
                        objMlcPlc.CreatedOn = DateTime.Now;
                        objMlcPlc.CreatedBy = _tokenService.GetUserId();
                        objMlcPlc.PatientDiagnoseId = input.PatientDiagnoseId;

                        await _uowMlcbodyIdentifierInfo.GetDbContext().AddAsync(objMlcPlc);
                        await _uowMlcbodyIdentifierInfo.GetDbContext().SaveChangesAsync();
                    }
                    else
                    {
                        var policeInfoObj = await _uowMlcpostmortem.GetDbContext().MlcpoliceInfos
                            .Where(x => x.PatientVisitId == input.PatientVisitId).FirstOrDefaultAsync();
                        if (!AppCommonMethod.IsNullObject(policeInfoObj))
                        {
                            policeInfoObj.Mlcid = input.Mlcid;
                            policeInfoObj.PolicePeron2NameDesignation = input.mlcpoliceInfos.PolicePeron2NameDesignation;
                            policeInfoObj.PolicePersonNameDesignation = input.mlcpoliceInfos.PolicePersonNameDesignation;
                            policeInfoObj.CommentsByPolice = input.mlcpoliceInfos.CommentsByPolice;
                            policeInfoObj.PoliceStationName = input.mlcpoliceInfos.PoliceStationName;
                            policeInfoObj.PoliceStationAddress = input.mlcpoliceInfos.PoliceStationAddress;
                            policeInfoObj.PatientDiagnoseId = input.PatientDiagnoseId;
                            policeInfoObj.UpdatedOn = DateTime.Now;
                            policeInfoObj.UpdatedBy = _tokenService.GetUserId();

                            _uowMlcbodyIdentifierInfo.GetDbContext().MlcpoliceInfos.Update(policeInfoObj);
                            await _uowMlcbodyIdentifierInfo.GetDbContext().SaveChangesAsync();
                        }
                    }
                }

                FillEntity(dbObj!);
                //var x = dbObj;

                // get Mlc Single Record
                //var snglMlc = new UnitOfWork<Mlc>(_uowMlcpostmortem.GetDbContext());

                //var recMlc = snglMlc.GetDbContext().Mlcs.Where(x=>x.PatientId == input.PatientId).FirstOrDefault();

                // Edit Mlc
                //var _uowMlc = new UnitOfWork<Mlc>(_uowMlcpostmortem.GetDbContext());
                //if (AppCommonMethod.IsNullObject(input.mlc))
                //{
                //    input.mlc = new CreateOrEditMLCDto();

                //    input.mlc.Mlcid = Guid.NewGuid();
                //    input.mlc.PatientId = input.PatientId;
                //    input.mlc.PatientVisitId = input.PatientVisitId;
                //    input.mlc.PatientVisitId = input.PatientVisitId;
                //    input.mlc.PatientDiagnoseId = input.PatientDiagnoseId;
                //    input.mlc.IsFinalReport = input.IsFinalReport;

                //    var mlc = _mapper.Map<Mlc>(input.mlc);
                //    mlc.Mlcno = input.HealthFacilityId.ToString() + "-" + DateTime.Now.Ticks.ToString() + "-" + DateTime.Now.Year.ToString();
                //    mlc.BookNo = input.mlc.BookNo;

                //    mlc.CreatedOn = DateTime.Now;
                //    mlc.CreatedBy = _tokenService.GetUserId();
                //    await _uowMlc.Repository.Insert(mlc);
                //    await _uowMlc.CommitAsync();
                //}
                //else
                //{
                //    var mlc = await _uowMlc.GetDbContext().Mlcs.Where(x => x.PatientId == input.PatientId).FirstOrDefaultAsync();
                //    mlc.DoctorId = input.mlc.DoctorId;
                //    mlc.BookNo = input.mlc.BookNo;
                //    mlc.PoliceDistrict = input.mlc.PoliceDistrict;
                //    mlc.IsFinalReport = input.IsFinalReport;
                //    _uowMlc.Repository.Update(mlc);
                //    await _uowMlc.CommitAsync();
                //}

                //dbObj!.Mlc!.MlcbodyIdentifierInfos.Clear();
                //dbObj!.Mlc!.MlcpoliceInfos.Clear();

                input.MlcpostmortemId = dbObj.MlcpostmortemId;
                //dbObj.Mlc.IsFinalReport = input.IsFinalReport;

                //checked if PoliceDistrict and Case Against is null

                //var _uowMlc = new UnitOfWork<Mlc>(_uowMlcpostmortem.GetDbContext());


                if (!AppCommonMethod.IsNullObject(input.mlc))
                {
                    if (input.mlc.PoliceDistrict == null || input.mlc.CaseAgainst == null)
                    {
                        if (!AppCommonMethod.IsNullOrEmptyGuid(input.mlc.Mlcid))
                        {
                            var mlc = await _unitOfWorkMlc.GetDbContext().Mlcs
                            .Where(x => x.Mlcid == input.mlc.Mlcid)
                        .OrderByDescending(x => x.CreatedOn)
                        .FirstOrDefaultAsync();

                            input.mlc.CaseAgainst = mlc.CaseAgainst;
                            input.mlc.PoliceDistrict = mlc.PoliceDistrict;
                            
                            //if (mlc.IsBookNoGenerated == false || mlc.IsBookNoGenerated == null)
                            //{
                            //    if (input.mlc.BookNo != null)
                            //    {
                            //        var bkno = _unitOfWorkMlc.GetDbContext().Mlcs.Where(x => x.BookNo == input.mlc.BookNo && x.HealthFacilityId == input.HealthFacilityId).ToList();
                            //        if (!AppCommonMethod.IsNullOrEmptyList(bkno))
                            //            throw new UserFriendlyException(CommonMessageConstant.DuplicateBookNumberFound);
                            //    }
                            //}
                        }
                        
                    }

                    input.Mlcid = dbObj.Mlcid;
                    input.Pmrno = dbObj.Pmrno;
                    input.mlc.Mlcid = dbObj.Mlcid;
                    input.mlc.PatientId = dbObj.Mlc.PatientId;
                    input.mlc.PatientVisitId = dbObj.Mlc.PatientVisitId;
                    input.mlc.HealthFacilityId = dbObj.Mlc.HealthFacilityId;
                    input.mlc.PatientDiagnoseId = dbObj.Mlc.PatientDiagnoseId;
                    input.mlc.Mlcno = dbObj.Mlc.Mlcno;

                    if (AppCommonMethod.IsNullOrEmptyGuid(input.mlc.DoctorId))
                    {
                        input.mlc.DoctorId = dbObj.Mlc.DoctorId;
                    }

                    dbObj.Mlc.ActionTypeId = (int)ActionTypeEnum.Edit;
                    dbObj.Mlc.IsActive = true;
                    dbObj.Mlc.UpdatedBy = _tokenService.GetUserId();
                    dbObj.Mlc.UpdatedOn = DateTime.Now;                    
                }

                if (input.IsReportCount == true)
                {
                    input.mlc.MlctypeProfileId = dbObj.Mlc.MlctypeProfileId;
                }

                var obj = _mapper.Map(input, dbObj);
                obj.Mlc.IsFinalReport = obj.IsFinalReport;
                dbObj.Mlc.PatientDiagnoseId = input.PatientDiagnoseId;

                if (obj.Mlc.BookNo != null)
                {
                    obj.Mlc.IsBookNoGenerated = true;
                }



                if (input.IsReportCount == true)
                {
                    if (input.ReportCounts != null)
                    {
                        obj.Mlc.ReportCounts = input.ReportCounts + 1;

                        // For Qr Code

                        string qrCodeValue = "SrNo = " + "1234543" + "," + "MLCNo = " + input.mlc.Mlcno + ","
                        + "Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;


                        string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                        if (obj.Mlc.QrCodeImagePath == null)
                        {
                            var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                            obj.Mlc.QrCodeImagePath = qrCodeImageUrl;
                        }
                    }
                    else
                    {
                        obj.Mlc.ReportCounts = 1;

                        string qrCodeValue = "MLCNo = " + input.mlc.Mlcno + ","
                        + "Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;


                        string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                        if (obj.Mlc.QrCodeImagePath == null)
                        {
                            var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                            obj.Mlc.QrCodeImagePath = qrCodeImageUrl;
                        }
                    }
                }

                    //obj.MlcpostmortemId = o.MlcpostmortemId;


                _uowMlcpostmortem.Repository.Update(obj!);
                await _uowMlcpostmortem.CommitAsync();

                return _mapper.Map<CreateOrEditPostMortemGeneralFormDto>(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // External Form
        private async Task<CreateOrEditPostMortemExternalFormDto> CreateExternal(CreateOrEditPostMortemExternalFormDto input)
        {
            try
            {
                var obj = _mapper.Map<PostmortemExternalExamination>(input);



                FillEntityExternal(obj);
                //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


                //if (profile != null)
                //{
                //    obj.FormTypeProfileId = profile.ProfileId;

                //}

                PostmortemExternalExamination responseObj = await _uowPostmortemExternalExamination.Repository.Insert(obj);

                await _uowPostmortemExternalExamination.CommitAsync();
                return _mapper.Map<CreateOrEditPostMortemExternalFormDto>(responseObj);

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private async Task<CreateOrEditPostMortemExternalFormDto> UpdateExternal(CreateOrEditPostMortemExternalFormDto input)
        {
            var dbObj = await _uowPostmortemExternalExamination.Repository.GetById(input.PostmortemExternalExaminationId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityExternal(obj!);

            _uowPostmortemExternalExamination.Repository.Update(obj!);
            await _uowPostmortemExternalExamination.CommitAsync();

            return _mapper.Map<CreateOrEditPostMortemExternalFormDto>(obj);
        }


        #endregion


        #region Read Operations
        // Get All Post-Mortem Patients
        public async Task<ViewPagerDto<GetAllPostMortemPatientsListDto>> GetAllPostMortemPatients(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowMlcpostmortem.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mlc].[SpGetPostMortemPatient]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "All");
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllPostMortemPatientsListDto> lst = ds.Tables[0].ToList<GetAllPostMortemPatientsListDto>();


                    IEnumerable<GetAllPostMortemPatientsListDto> lstto;

                    //dynamic objList;
                    List<GetAllPostMortemPatientsListDto> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x => x.FullName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();

                    //var pagedList = await PagedListDto<MLEAllPatientListDto>.ToPagedListAsync(
                    //    lstto,
                    //    filter.PageNumber,
                    //    filter.PageSize
                    //   );

                    var pagedList = await Task.Run(() => PagedListDto<GetAllPostMortemPatientsListDto>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<GetAllPostMortemPatientsListDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<GetAllPostMortemPatientsListDto>>(pagedList)
                    };

                    return responseObject;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        // Get Single Post-Mortem Patient By Patient ID

        public async Task<List<PostMortemRecordDto>> GetSinglePostMortemFormByPatientId(Guid PatientId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowMlcpostmortem.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mlc].[SpGetPostMortemPatient]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PostMortemRecordDto> lst = ds.Tables[0].ToList<PostMortemRecordDto>();



                    return lst;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion

        #region Helper Methods

        private async Task<CreateOrEditPostMortemInternalFormDto> CreateInternal(CreateOrEditPostMortemInternalFormDto input)
        {
            //var _uowPostmortemInternalExamination = new UnitOfWork<PostMortemInternalExamination>(_uowMlcpostmortem.GetDbContext());
            //var _uowPostmortemInternalExamination = new UnitOfWork<PostMortemInternalExamination>(_uowMlcpostmortem.GetDbContext());
            //var _uowProfile = new UnitOfWork<Profile>(_uowMlcpostmortem.GetDbContext());
            var obj = _mapper.Map<PostMortemInternalExamination>(input);

            FillEntityInternal(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            PostMortemInternalExamination responseObj = await _uowPostmortemInternalExamination.Repository.Insert(obj);
            await _uowPostmortemInternalExamination.CommitAsync();
            return _mapper.Map<CreateOrEditPostMortemInternalFormDto>(responseObj);
        }

        private async Task<CreateOrEditPostMortemInternalFormDto> UpdateInternal(CreateOrEditPostMortemInternalFormDto input)
        {
            var dbObj = await _uowPostmortemInternalExamination.Repository.GetById(input.PostMortemInternalExaminationId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityInternal(obj!);

            _uowPostmortemInternalExamination.Repository.Update(obj!);
            await _uowPostmortemInternalExamination.CommitAsync();

            return _mapper.Map<CreateOrEditPostMortemInternalFormDto>(obj);
        }

        private async Task<CreateOrEditPostMortemReportDto> CreateReport(CreateOrEditPostMortemReportDto input)
        {
            var obj = _mapper.Map<PostmortemReport>(input);


            FillEntityReport(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowPostmortemReport.GetDbContext());
            var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPostmortemReport.GetDbContext());

            // Insert In ImageBase64 Table
            //
            if (input.PoliceSignature != null)
            {
                if (input.PoliceSignature.base64 != "")
                {
                    //obj.PoliceSignatureImageId = input.PoliceSignature.ImageTypeProfileId;
                    var ptSigImg = _mapper.Map<PatientImage>(input.PoliceSignature);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.PoliceSignature.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientImageId = Guid.NewGuid();
                    obj.PoliceSignatureImageId = ptSigImg.PatientImageId;
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
            }

            if (input.PoliceFingerPrint != null)
            {
                if (input.PoliceFingerPrint.base64 != "")
                {
                    //obj.PoliceFingerPrintId = input.PoliceFingerPrint.ImageTypeProfileId;
                    var ptSigImg = _mapper.Map<PatientImage>(input.PoliceFingerPrint);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.PoliceFingerPrint.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceFingerPrint.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientImageId = Guid.NewGuid();
                    obj.PoliceFingerPrintId = ptSigImg.PatientImageId;
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
            }

            if (input.MlcManualReport != null)
            {
                if (input.MlcManualReport.base64 != "")
                {
                    //obj.PatientManualReportId = input.MlcManualReport.ImageTypeProfileId;
                    var ptSigImg = _mapper.Map<PatientImage>(input.MlcManualReport);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.MlcManualReport.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcManualReport.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientImageId = Guid.NewGuid();
                    obj.PatientManualReportId = ptSigImg.PatientImageId;

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
            }

            if (input.MlcDrawImage != null)
            {
                if (input.MlcDrawImage.base64 != "")
                {
                    //obj.PatientDrawImgId = input.MlcDrawImage.ImageTypeProfileId;
                    var ptSigImg = _mapper.Map<PatientImage>(input.MlcDrawImage);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.MlcDrawImage.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcDrawImage.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientImageId = Guid.NewGuid();
                    obj.PatientDrawImgId = ptSigImg.PatientImageId;

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
            }

            PostmortemReport responseObj = await _uowPostmortemReport.Repository.Insert(obj);
            await _uowPostmortemReport.CommitAsync();
            return _mapper.Map<CreateOrEditPostMortemReportDto>(responseObj);
        }

        private async Task<CreateOrEditPostMortemReportDto> UpdateReport(CreateOrEditPostMortemReportDto input)
        {
             var dbObj = await _uowPostmortemReport.Repository.GetById(input.PostmortemReportId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowPostmortemReport.GetDbContext());
            var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPostmortemReport.GetDbContext());

            var obj = _mapper.Map(input, dbObj);
            FillEntityReport(obj!);



            if (input.PoliceSignatureImageId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PoliceSignatureImageId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == pi.PatientImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.PoliceSignature))
                {
                    if (input.PoliceSignature.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.PoliceSignature.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.PoliceSignature))
                {
                    if (input.PoliceSignature.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PoliceSignatureImageId = input.PoliceSignature.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.PoliceSignature);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PoliceSignature.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.PoliceSignatureImageId = ptimg.PatientImageId;


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


            if (input.PoliceFingerPrintId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PoliceFingerPrintId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == pi.PatientImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.PoliceFingerPrint))
                {
                    if (input.PoliceFingerPrint.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.PoliceFingerPrint.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceFingerPrint.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.PoliceFingerPrint))
                {
                    if (input.PoliceFingerPrint.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PoliceFingerPrintId = input.PoliceSignature.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.PoliceFingerPrint);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PoliceFingerPrint.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceFingerPrint.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.PoliceFingerPrintId = ptimg.PatientImageId;


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

            if (input.PatientManualReportId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientManualReportId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == pi.PatientImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.MlcManualReport))
                {
                    if (input.MlcManualReport.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.MlcManualReport.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcManualReport.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.MlcManualReport))
                {
                    if (input.MlcManualReport.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientManualReportId = input.MlcManualReport.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.MlcManualReport);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.MlcManualReport.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcManualReport.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.PatientManualReportId = ptimg.PatientImageId;


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

            if (input.PatientDrawImgId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientDrawImgId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == pi.PatientImageId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(input.MlcDrawImage))
                {
                    if (input.MlcDrawImage.base64 != "")
                    {

                        b64.ActionTypeId = 2;
                        b64.Base64 = input.MlcDrawImage.base64;
                        b64.UpdatedOn = DateTime.Now;
                        b64.UpdatedBy = _tokenService.GetUserId();

                        pi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcDrawImage.base64, _tokenService.GetAccessToken());
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
                if (!AppCommonMethod.IsNullObject(input.MlcDrawImage))
                {
                    if (input.MlcDrawImage.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();
                        //obj.PatientDrawImgId = input.MlcDrawImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.MlcDrawImage);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.MlcDrawImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcDrawImage.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.PatientDrawImgId = ptimg.PatientImageId;


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

            _uowPostmortemReport.Repository.Update(obj!);
            await _uowPostmortemReport.CommitAsync();

            return _mapper.Map<CreateOrEditPostMortemReportDto>(obj);
        }

        private void FillEntity(Mlcpostmortem obj)
        {
            if (obj.MlcpostmortemId == Guid.Empty)
            {
                obj.MlcpostmortemId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
                obj.IsActive = true;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                obj.IsActive = true;
            }
        }

        // External

        private void FillEntityExternal(PostmortemExternalExamination obj)
        {
            if (obj.PostmortemExternalExaminationId == Guid.Empty)
            {
                obj.PostmortemExternalExaminationId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
                obj.IsActive = true;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                obj.IsActive = true;
            }
        }

        private void FillEntityInternal(PostMortemInternalExamination obj)
        {
            if (obj.PostMortemInternalExaminationId == Guid.Empty)
            {
                obj.PostMortemInternalExaminationId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
                obj.IsActive = true;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                obj.IsActive = true;
            }
        }

        private void FillEntityBodyIdentifierInfo(MlcbodyIdentifierInfo obj)
        {
            if (obj.MlcbodyIdentifierInfoId == Guid.Empty)
            {
                obj.MlcbodyIdentifierInfoId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
                obj.IsActive = true;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                obj.IsActive = true;
            }
        }

        private void FillEntityReport(PostmortemReport obj)
        {
            if (obj.PostmortemReportId == Guid.Empty)
            {
                obj.PostmortemReportId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
                obj.IsActive = true;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                obj.IsActive = true;
            }
        }
        private void FillEntityDelete(Mlcpostmortem obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        public void InsertOrUpdateBodyIdentifier(CreateOrEditPostMortemGeneralFormDto input)
        {

            var listMLCBodyIdentifier = _uowMlcbodyIdentifierInfo.GetDbContext().MlcbodyIdentifierInfos
  .Where(x => x.Mlcid == input.Mlcid).ToList();

            if (!AppCommonMethod.IsNullOrEmptyList(listMLCBodyIdentifier))
            {
                foreach (var bdyResFront in input.BodyIdentifierInfoDtos)
                {
                    if (AppCommonMethod.IsNullOrEmptyGuid(bdyResFront.MlcbodyIdentifierInfoId))
                    {
                        if (AppCommonMethod.IsNullObject(bdyResFront))
                            throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        var obj1 = _mapper.Map<MlcbodyIdentifierInfo>(bdyResFront);
                        obj1.PatientId = input.PatientId;
                        obj1.PatientVisitId = input.PatientVisitId;
                        obj1.Mlcid = input.Mlcid;
                        obj1.PatientDiagnoseId = input.PatientDiagnoseId;
                        obj1.HealthFacilityId = input.HealthFacilityId;
                        FillEntityBodyIdentifierInfo(obj1);

                        _uowMlcbodyIdentifierInfo.GetDbContext().MlcbodyIdentifierInfos.Add(obj1);
                        _uowMlcbodyIdentifierInfo.GetDbContext().SaveChanges();
                    }
                    else
                    {
                        var existingItem = listMLCBodyIdentifier
                            .FirstOrDefault(item => item.MlcbodyIdentifierInfoId == bdyResFront.MlcbodyIdentifierInfoId);

                        if (existingItem != null)
                        {
                         
                            // Edit the record
                            if (AppCommonMethod.IsNullObject(bdyResFront))
                                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            var obj1 = _mapper.Map(bdyResFront, existingItem);
                            obj1.PatientId = input.PatientId;
                            obj1.PatientVisitId = input.PatientVisitId;
                            obj1.Mlcid = input.Mlcid;
                            obj1.PatientDiagnoseId = input.PatientDiagnoseId;
                            obj1.HealthFacilityId = input.HealthFacilityId;
                            FillEntityBodyIdentifierInfo(obj1);

                            _uowMlcbodyIdentifierInfo.GetDbContext().MlcbodyIdentifierInfos.Update(obj1);
                            _uowMlcbodyIdentifierInfo.GetDbContext().SaveChanges();
                        }
                    }
                }

                // Second loop for deleting records
                foreach (var existingItem in listMLCBodyIdentifier)
                {
                    var matchingDto = input.BodyIdentifierInfoDtos
                        .FirstOrDefault(bdyResFront => bdyResFront.MlcbodyIdentifierInfoId == existingItem.MlcbodyIdentifierInfoId);

                    if (AppCommonMethod.IsNullObject(matchingDto))
                    {
                        // Delete the record

                        var obj1 = _mapper.Map(matchingDto, existingItem);
                        obj1.PatientId = input.PatientId;
                        obj1.PatientVisitId = input.PatientVisitId;
                        obj1.Mlcid = input.Mlcid;
                        obj1.PatientDiagnoseId = input.PatientDiagnoseId;
                        obj1.HealthFacilityId = input.HealthFacilityId;

                        obj1.DeletedBy = _tokenService.GetUserId();
                        obj1.DeletedOn = DateTime.Now;
                        obj1.ActionTypeId = (int)ActionTypeEnum.Deleted;

                        _uowMlcbodyIdentifierInfo.GetDbContext().MlcbodyIdentifierInfos.Update(obj1);
                        _uowMlcbodyIdentifierInfo.GetDbContext().SaveChanges();

                        //var obj1 = _mapper.Map<MlcbodyIdentifierInfo>(matchingDto);
                        //obj1.Mlcid = input.Mlcid;
                        //obj1.PatientDiagnoseId = input.PatientDiagnoseId;
                        //obj1.PatientId = input.PatientId;
                        //obj1.PatientVisitId = input.PatientVisitId;
                        //obj1.HealthFacilityId = input.HealthFacilityId;
                        //obj1.DeletedBy = _tokenService.GetUserId();
                        //obj1.DeletedOn = DateTime.Now;
                        //obj1.ActionTypeId = (int)ActionTypeEnum.Deleted;
                        //FillEntityBodyIdentifierInfo(obj1);

                        //_uowMlcbodyIdentifierInfo.GetDbContext().MlcbodyIdentifierInfos.Update(obj1);
                        //_uowMlcbodyIdentifierInfo.GetDbContext().SaveChanges();
                    }
                }

            }

            //
            else
            {
                    // Insert
                    foreach (var item in input.BodyIdentifierInfoDtos)
                    {
                        var dbObjMLCBody = new MlcbodyIdentifierInfo();


                        //var obj1 = _mapper.Map(item, dbObjMLCBody);
                        item.PatientId = input.PatientId;
                        item.PatientVisitId = input.PatientVisitId;
                        var obj1 = _mapper.Map<MlcbodyIdentifierInfo>(item);
                        obj1.Mlcid = input.Mlcid;
                        obj1.PatientDiagnoseId = input.PatientDiagnoseId;
                        obj1.HealthFacilityId = input.HealthFacilityId;
                        FillEntityBodyIdentifierInfo(obj1);

                        _uowMlcbodyIdentifierInfo.GetDbContext().MlcbodyIdentifierInfos.Add(obj1);
                        _uowMlcbodyIdentifierInfo.GetDbContext().SaveChanges();
                    }
                }

        }
        #endregion
    }
}
