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
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using HMIS.EMC.Service.Interfaces.QrCodeHelper;
using System.IO.Compression;

namespace HMIS.EMC.Service
{
    public class MLEFormService<TEntity> : IMLE where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly IMapper _mapper;
        private UnitOfWork<MlebasicInfo> _uowGeneral;
        private UnitOfWork<PatientImage> _uowPatientImage;
        private UnitOfWork<PatientDiagnose> _uowPatientDiagnose;
        private readonly UploadFiles _fileUploader;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        #endregion

        #region Constructor
        public MLEFormService(IQrCodeGeneratorHelper qrCodeGenerator,TokenService tokenService, IMapper mapper, UnitOfWork<MlebasicInfo> uowGeneral, UploadFiles uploadFiles, UnitOfWork<PatientImage> uowPatientImage, PatientDiagnoseService patientDiagnoseService)
        {
                _tokenService = tokenService;
                _mapper = mapper;   
                _uowGeneral = uowGeneral;
                _fileUploader = uploadFiles;
                _uowPatientImage = uowPatientImage;
            _patientDiagnoseService = patientDiagnoseService;
            _qrCodeGenerator = qrCodeGenerator;
        }

        #endregion

        #region CUD
        public async Task<CreateOrEditMLEBasicInfoDto> CreateOrEdit(CreateOrEditMLEBasicInfoDto input)
            {
            try
            {
                var obj=await _uowGeneral.GetDbContext().MlebasicInfos.Where(x=>x.PatientVisitId == input.PatientVisitId).FirstOrDefaultAsync();
                    if (AppCommonMethod.IsNullObject(obj))
                    {
                        return await CreateMleBasicInfo(input);
                    }
                    else
                    {
                        input.MlebasicInfoId = obj.MlebasicInfoId;
                        return await UpdateMleBasicInfo(input);
                    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateOrEditMLEExaminationDto> CreateOrEdit(CreateOrEditMLEExaminationDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.MleexaminationId))
                {
                    return await CreateMleExamination(input);
                }
                else
                {
                    return await UpdateMleExamination(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateOrEditMleReportDto> CreateOrEdit(CreateOrEditMleReportDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.MlereportId))
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

        public async Task<UpdateAssignDoctorDto> UpdateAssignDoctor(UpdateAssignDoctorDto input)
        {
            try
            {
                var obj = await _uowGeneral.GetDbContext().Mlcs.Where(x => x.Mlcid == input.Mlcid)
                    .FirstOrDefaultAsync();

                obj.ReasonForChangeDoctor = input.ReasonForChangeDoctor;
                obj.DoctorId = input.DoctorId;

                _uowGeneral.GetDbContext().Mlcs.Update(obj);
                await _uowGeneral.GetDbContext().SaveChangesAsync();
                var objReturn = _mapper.Map<UpdateAssignDoctorDto>(obj);
                return objReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.Message);
            }
        }
        #endregion

        #region Read
        public async Task<ViewPagerDto<MLEAllPatientListDto>> GetAllMlePatients(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowGeneral.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mlc].[SpGetMLEPatients]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "All");
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<MLEAllPatientListDto> lst = ds.Tables[0].ToList<MLEAllPatientListDto>();

                    IEnumerable< MLEAllPatientListDto > lstto;

                    //dynamic objList;
                    List<MLEAllPatientListDto> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x=> x.FullName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();

                    //var pagedList = await PagedListDto<MLEAllPatientListDto>.ToPagedListAsync(
                    //    lstto,
                    //    filter.PageNumber,
                    //    filter.PageSize
                    //   );

                    var pagedList = await Task.Run(() => PagedListDto<MLEAllPatientListDto>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber, 
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<MLEAllPatientListDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<MLEAllPatientListDto>>(pagedList)
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
        public async Task<List<ViewMlcdoctorList>> GetAllMlcDoctors(int HealthFacilityId)
        {
            try
            {
                var doclist = await _uowGeneral.GetDbContext().ViewMlcdoctorLists
                    .Where(x => x.HealthFacilityId == HealthFacilityId).ToListAsync();

                if (AppCommonMethod.IsNullOrEmptyList(doclist))
                    throw new UserFriendlyException("No Doctor Found..!");
                else
                    return doclist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<MlePatientsDto>> GetSinglePatientMleInfo(Guid PatientId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowGeneral.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mlc].[SpGetMLEPatients]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<MlePatientsDto> lst = ds.Tables[0].ToList<MlePatientsDto>();



                    return lst;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.InnerException!.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<ViewPagerDto<ViewGetAllPatientThatAreNotCheckedYet>> GetAllMLCDoctorsByHealthFacilityId(SearchFilterDto filter)
        {
            try
            {
                var lst = _uowGeneral.GetDbContext().ViewGetAllPatientThatAreNotCheckedYets
                .Where(x => x.HealthFacilityId == filter.HealthFacilityId)
                .OrderByDescending(x=>x.CreatedOn).AsQueryable();

                if (!String.IsNullOrEmpty(filter.SearchString))
                    lst = lst.Where(x => x.PatientName!.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                else
                    lst = lst.AsQueryable();


                var pagedList = await PagedListDto<ViewGetAllPatientThatAreNotCheckedYet>.ToPagedListAsync(
                       lst,
                       filter.PageNumber,
                       filter.PageSize
                       );

                var responseObject = new ViewPagerDto<ViewGetAllPatientThatAreNotCheckedYet>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientThatAreNotCheckedYet>>(pagedList)
                };

                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.Message);
            }
        }

        public async Task<ViewPagerDto<ViewGetAllPatientForMlc>> GetAllMLCsByHealthFacilityId(SearchFilterDto filter)
        {
            try
            {
                var lst = _uowGeneral.GetDbContext().ViewGetAllPatientForMlcs
                .Where(x => x.HealthFacilityId == filter.HealthFacilityId)
                .OrderByDescending(x => x.CreatedOn).AsQueryable();

                if (!String.IsNullOrEmpty(filter.SearchString))
                    lst = lst.Where(x => x.PatientName!.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                else
                    lst = lst.AsQueryable();


                var pagedList = await PagedListDto<ViewGetAllPatientForMlc>.ToPagedListAsync(
                       lst,
                       filter.PageNumber,
                       filter.PageSize
                       );

                var responseObject = new ViewPagerDto<ViewGetAllPatientForMlc>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientForMlc>>(pagedList)
                };

                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.Message);
            }
        }

        public async Task<UpdateAssignDoctorDto> GetSingleMLCPatientsThatAreNotCheckedYet(Guid MlcId)
        {
            try
            {
                var obj = await _uowGeneral.GetDbContext().ViewGetAllPatientThatAreNotCheckedYets
                .Where(x => x.Mlcid == MlcId)
                .Select(x=>new UpdateAssignDoctorDto
                {
                    Mlcid = x.Mlcid,
                    DoctorId = x.DoctorId
                })
                .FirstOrDefaultAsync();

               return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.Message);
            }
        }


        public async Task<MlebasicInfo> GetSinglePatientBasicInfo(Guid PatientId)
        {
            try
            {
                var obj = await _uowGeneral.GetDbContext().MlebasicInfos
                    .Where(x => x.PatientId == PatientId).FirstOrDefaultAsync();


                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public async Task<MlcrecordDto> GetSinglePatientMleInfo(Guid PatientId)
        //{
        //    try
        //    {
        //        // Get

        //        var obj = await _uowGeneral.GetDbContext().Mlcs
        //            .Where(x => x.PatientId == PatientId)
        //            .Include(x => x.MlebasicInfos)
        //            .ThenInclude(x => x.Mleexaminations)
        //            .ThenInclude(x => x.Mlereports)
        //            .FirstOrDefaultAsync();

        //        var mleObj = await _uowGeneral.GetDbContext().Mlcs.Where(x => x.PatientId == PatientId).FirstOrDefaultAsync();
        //            var mlebasicinfoObj = await _uowGeneral.GetDbContext().MlebasicInfos.Where(x => x.PatientId == PatientId).FirstOrDefaultAsync();
        //            var mleExaminationObj = await _uowGeneral.GetDbContext().Mleexaminations.Where(x => x.PatientId == PatientId).FirstOrDefaultAsync();
        //            var mleReportObj = await _uowGeneral.GetDbContext().Mlereports.Where(x => x.PatientId == PatientId).FirstOrDefaultAsync();

        //        var mlcRec = new MlcrecordDto();
        //        var mle=_mapper.Map<MlcrecordDto>(mleObj);
        //        //mlcRec.MlebasicInfos = mlebasicinfoObj;

        //        mlcRec = mleObj;
        //        //mlcRec

        //        //.AsEnumerable()
        //        //.Select(x=> new MlcrecordDto
        //        //{

        //        //});

        //        return ;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        #endregion

        #region Helper Methods
        /// <summary>
        ///  This Part is For Create Or Edit Mle Basic Info
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

       
        private async Task<CreateOrEditMLEBasicInfoDto> CreateMleBasicInfo(CreateOrEditMLEBasicInfoDto input)
        {
            try
            {
                if (input.BookNo != null)
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


                if (input.BookNo == null)
                {
                    input.PatientDiagnoseId = null;
                }

                var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowGeneral.GetDbContext());

                var obj = _mapper.Map<MlebasicInfo>(input);

                if (!AppCommonMethod.IsNullObject(input.PatientImage))
                {
                    if(input.PatientImage.base64 != "")
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

                        obj.PatientImageProfileId = ptimg.PatientImageId;


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
                // Insert Police Signature Image
                if (!AppCommonMethod.IsNullObject(input.PoliceSignature))
                {
                    if (input.PoliceSignature.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();

                        //obj.PoliceSignatureProfileId = input.PoliceSignature.ImageTypeProfileId;

                        var plcImg = _mapper.Map<PatientImage>(input.PoliceSignature);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        plcImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());
                        plcImg.PatientImageId = Guid.NewGuid();

                        obj.PoliceSignatureProfileId = plcImg.PatientImageId;


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


                // Insert Patient Signature Image
                if (!AppCommonMethod.IsNullObject(input.PatientSignature))
                {
                    if (input.PatientSignature.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();

                        //obj.PatientSignatureProfileId = input.PatientSignature.ImageTypeProfileId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.PatientSignature);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientSignature.base64, _tokenService.GetAccessToken());
                        ptSigImg.PatientImageId = Guid.NewGuid();

                        obj.PatientSignatureProfileId = ptSigImg.PatientImageId;


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

                // Insert Adult finger print
                if (!AppCommonMethod.IsNullObject(input.AdultOrUnderAgeFingerPrint))
                {
                    if (input.AdultOrUnderAgeFingerPrint.base64 != "")
                    {
                        var ImageBaseSixty4 = new ImageBaseSixtyFour();

                        //obj.AdultOrUnderAgeFingerPrintProfileId = input.AdultOrUnderAgeFingerPrint.ImageTypeProfileId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.AdultOrUnderAgeFingerPrint);

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.AdultOrUnderAgeFingerPrint.base64, _tokenService.GetAccessToken());
                        ptSigImg.PatientImageId = Guid.NewGuid();

                        obj.AdultOrUnderAgeFingerPrintProfileId = ptSigImg.PatientImageId;


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
                if (!AppCommonMethod.IsNullObject(input.PoliceFingerPrint))
                {
                    if (input.PoliceFingerPrint.base64 != "")
                    {

                        //obj.PoliceFingerPrintProfileId = input.PoliceFingerPrint.ImageTypeProfileId;

                        var ptSigImg = _mapper.Map<PatientImage>(input.PoliceFingerPrint);

                        var ImageBaseSixty4 = new ImageBaseSixtyFour();

                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.PatientImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceFingerPrint.base64, _tokenService.GetAccessToken());
                        ptSigImg.PatientImageId = Guid.NewGuid();

                        obj.PoliceFingerPrintProfileId = ptSigImg.PatientImageId;


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


                FillEntity(obj);
                //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


                //if (profile != null)
                //{
                //    obj.FormTypeProfileId = profile.ProfileId;

                //}


                // Create MLC Common
                var _uowMlc = new UnitOfWork<Mlc>(_uowGeneral.GetDbContext());

                if(AppCommonMethod.IsNullObject(input.createOrEditMLCDtos))
                    input.createOrEditMLCDtos = new CreateOrEditMLCDto();

                var mlc = _mapper.Map<Mlc>(input.createOrEditMLCDtos);
                //if(input.createOrEditMLCDtos.MlctypeProfileId!=null) { 
                //    obj.Mlcno = input.HealthFacilityId.ToString() +"-" + DateTime.Now.Ticks.ToString() +"-" + DateTime.Now.Year.ToString();                
                //}

                var random = new Random();
                var chars = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
                 var randomNo = new string(Enumerable.Repeat(chars, 7)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                //if(input.IsReportCount == true)
                //{
                //    if (input.ReportCounts != null)
                //    {
                //        mlc.ReportCounts = input.ReportCounts + 1;

                //        // For Qr Code

                //    string qrCodeValue = "SrNo = " + "1234543" + "," + "MLCNo = " + input.Mlcno + ","
                //    + "Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;

                //        //byte[] qrCodeAsBytes = _qrCodeGenerator.GenerateQrCode(qrCodeValue);

                //        //string QrCodebase64String = $"data:image/png;base64,{Convert.ToBase64String(qrCodeAsBytes)}";
                //    } 
                //    else
                //    {
                //        mlc.ReportCounts = 0;
                //    }
                //}

                mlc.Mlcid = Guid.NewGuid();
                mlc.PatientId = input.PatientId;
                mlc.PatientVisitId = input.PatientVisitId;
                mlc.PatientDiagnoseId = input.PatientDiagnoseId;
                mlc.HealthFacilityId = input.HealthFacilityId;
                mlc.Mlcno = input.HealthFacilityId.ToString() + "-" + randomNo + "-" + DateTime.Now.Year.ToString();
                mlc.BookNo = input.BookNo;  
                mlc.DoctorId = input.DoctorId;
                mlc.PoliceDistrict = input.PoliceDistrict;
                mlc.ActionTypeId = (int)ActionTypeEnum.Create;
                mlc.IsActive = true;
                mlc.CreatedOn = DateTime.Now;
                mlc.CreatedBy = _tokenService.GetUserId();
                

                await _uowMlc.Repository.Insert(mlc);
                await _uowMlc.CommitAsync();

                obj.Mlcid = mlc.Mlcid;


                //// For Closing Visit
                //var _uowPatientOpenVisits = new UnitOfWork<PatientOpenVisit>(_uowGeneral.GetDbContext());

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
                obj.HealthFacilityId = input.HealthFacilityId;
                MlebasicInfo responseObj = await _uowGeneral.Repository.Insert(obj);
                await _uowGeneral.CommitAsync();

                return _mapper.Map<CreateOrEditMLEBasicInfoDto>(responseObj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<CreateOrEditMLEBasicInfoDto> UpdateMleBasicInfo(CreateOrEditMLEBasicInfoDto input)
        {
            try
            {
                if (input.BookNo != null)
                {

                    if (input.PatientDiagnoseId == null)
                    {
                        if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                        {
                            var chkPtDiagnose = await _uowGeneral.GetDbContext().PatientDiagnoses.Where(x=>x.PatientDiagnoseId == input.PatientDiagnoseId)
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


                if (input.BookNo == null)
                {
                    input.PatientDiagnoseId = null;
                }

                var dbObj = await _uowGeneral.Repository.GetById(input.MlebasicInfoId!);
                var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowGeneral.GetDbContext());

                if (AppCommonMethod.IsNullObject(dbObj))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                var obj = _mapper.Map(input, dbObj);
                FillEntity(obj!);

                _uowGeneral.Repository.Update(obj!);
                // Edit Mlc
                var _uowMlc = new UnitOfWork<Mlc>(_uowGeneral.GetDbContext());
                if (AppCommonMethod.IsNullObject(input.createOrEditMLCDtos))
                {
                    //if (input.IsReportCount != true)
                    //{
                    //    if (input.BookNo != null)
                    //    {
                    //        var bkno = _uowMlc.GetDbContext().Mlcs.Where(x => x.BookNo == input.BookNo && x.HealthFacilityId == input.HealthFacilityId).ToList();
                    //        if (!AppCommonMethod.IsNullOrEmptyList(bkno))
                    //            throw new UserFriendlyException(CommonMessageConstant.DuplicateBookNumberFound);
                    //    }
                    //}

                    input.createOrEditMLCDtos = new CreateOrEditMLCDto();

                    input.createOrEditMLCDtos.Mlcid=Guid.NewGuid();
                    input.createOrEditMLCDtos.PatientId = input.PatientId;
                    input.createOrEditMLCDtos.PatientVisitId = input.PatientVisitId;
                    input.createOrEditMLCDtos.PatientVisitId = input.PatientVisitId;
                    input.createOrEditMLCDtos.PatientDiagnoseId = input.PatientDiagnoseId;
                    input.createOrEditMLCDtos.IsFinalReport = input.IsFinalReport;

                    var mlc = _mapper.Map<Mlc>(input.createOrEditMLCDtos);
                    mlc.Mlcno = input.HealthFacilityId.ToString() + "-" + DateTime.Now.Ticks.ToString() + "-" + DateTime.Now.Year.ToString();
                    mlc.BookNo = input.BookNo;

                    if (mlc.BookNo != null)
                    {
                        mlc.IsBookNoGenerated = true;
                    }

                    mlc.DoctorId = input.DoctorId;
                    mlc.PoliceDistrict = input.PoliceDistrict;
                    mlc.CreatedOn = DateTime.Now;
                    mlc.CreatedBy = _tokenService.GetUserId();
                    await _uowMlc.Repository.Insert(mlc);
                    await _uowMlc.CommitAsync();
                }
                else
                {
                    var mlc = await _uowMlc.GetDbContext().Mlcs.Where(x => x.Mlcid == input.createOrEditMLCDtos.Mlcid)
                        .OrderByDescending(x=>x.CreatedOn)
                        .FirstOrDefaultAsync();

                    if (mlc != null)
                    {
                        //if (mlc.IsBookNoGenerated == false || mlc.IsBookNoGenerated == null)
                        //{
                        //    if (input.BookNo != null)
                        //    {
                        //        var bkno = _uowMlc.GetDbContext().Mlcs.Where(x => x.BookNo == input.BookNo && x.HealthFacilityId == input.HealthFacilityId).ToList();
                        //        if (!AppCommonMethod.IsNullOrEmptyList(bkno))
                        //            throw new UserFriendlyException(CommonMessageConstant.DuplicateBookNumberFound);
                        //    }
                        //}



                        mlc.DoctorId = input.DoctorId;
                        mlc.BookNo = input.BookNo;
                        mlc.PatientDiagnoseId = input.PatientDiagnoseId;
                        mlc.IsActive = true;
                        mlc.ActionTypeId = (int)ActionTypeEnum.Edit;

                        if (input.PoliceDistrict != null)
                        {
                            mlc.PoliceDistrict = input.PoliceDistrict;
                        }

                        if (mlc.BookNo != null)
                        {
                            mlc.IsBookNoGenerated = true;
                        }

                        if (input.createOrEditMLCDtos!.CaseAgainst != null)
                        {
                            mlc.CaseAgainst = input.createOrEditMLCDtos!.CaseAgainst;
                        }

                        mlc.DoctorId = input.DoctorId;

                        if (input.IsReportCount == true)
                        {
                            if (input.ReportCounts != null)
                            {
                                mlc.ReportCounts = input.ReportCounts + 1;

                                // For Qr Code

                                string qrCodeValue = "SrNo = " + "1234543" + "," + "MLCNo = " + input.Mlcno + ","
                                + "Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;


                                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                                if(mlc.QrCodeImagePath == null)
                                {
                                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                                    mlc.QrCodeImagePath = qrCodeImageUrl;
                                }
                            }
                            else
                            {
                                mlc.ReportCounts = 1;

                                string qrCodeValue = "MLCNo = " + input.Mlcno + ","
                                + "Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;


                                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                                if (mlc.QrCodeImagePath == null)
                                {
                                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                                    mlc.QrCodeImagePath = qrCodeImageUrl;
                                }
                            }
                        }

                        mlc.IsFinalReport = input.IsFinalReport;
                        _uowMlc.Repository.Update(mlc);
                        await _uowMlc.CommitAsync();
                    }
                }


                await _uowGeneral.CommitAsync();


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

                            obj.PatientImageProfileId = ptimg.PatientImageId;


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

                if (input.PatientSignatureImageId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientSignatureImageId).FirstOrDefaultAsync();

                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PatientSignatureImageId)
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

                            //obj.PatientSignatureProfileId = input.PatientSignature.ImageTypeProfileId;

                            var ptSigImg = _mapper.Map<PatientImage>(input.PatientSignature);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PatientSignature.base64, _tokenService.GetAccessToken());
                            ptSigImg.PatientImageId = Guid.NewGuid();

                            obj.PatientSignatureProfileId = ptSigImg.PatientImageId;


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

                if (input.PoliceSignatureImageId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                        .Where(x => x.PatientImageId == input.PoliceSignatureImageId).FirstOrDefaultAsync();


                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PoliceSignatureImageId)
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

                            //obj.PoliceSignatureProfileId = input.PoliceSignature.ImageTypeProfileId;

                            var plcImg = _mapper.Map<PatientImage>(input.PoliceSignature);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            plcImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceSignature.base64, _tokenService.GetAccessToken());
                            plcImg.PatientImageId = Guid.NewGuid();

                            obj.PoliceSignatureProfileId = plcImg.PatientImageId;


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


                if (input.AdultOrUnderAgeFingerPrintProfileId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.AdultOrUnderAgeFingerPrintProfileId).FirstOrDefaultAsync();


                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.AdultOrUnderAgeFingerPrintProfileId)
                       .FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(input.AdultOrUnderAgeFingerPrint))
                    {
                        if (input.AdultOrUnderAgeFingerPrint.base64 != "")
                        {

                            b64.Base64 = input.PatientImage.base64;
                            b64.ActionTypeId = 2;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.AdultOrUnderAgeFingerPrint.base64, _tokenService.GetAccessToken());
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
                    if (!AppCommonMethod.IsNullObject(input.AdultOrUnderAgeFingerPrint))
                    {
                        if (input.AdultOrUnderAgeFingerPrint.base64 != "")
                        {
                            var ImageBaseSixty4 = new ImageBaseSixtyFour();

                            //obj.AdultOrUnderAgeFingerPrintProfileId = input.AdultOrUnderAgeFingerPrint.ImageTypeProfileId;

                            var ptSigImg = _mapper.Map<PatientImage>(input.AdultOrUnderAgeFingerPrint);

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.AdultOrUnderAgeFingerPrint.base64, _tokenService.GetAccessToken());
                            ptSigImg.PatientImageId = Guid.NewGuid();

                            obj.AdultOrUnderAgeFingerPrintProfileId = ptSigImg.PatientImageId;


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
                //var _uowPatientOpenVisits = new UnitOfWork<PatientOpenVisit>(_uowGeneral.GetDbContext());

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

                if (input.PoliceFingerPrintId != null)
                {
                    var psi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PoliceFingerPrintId).FirstOrDefaultAsync();

                    var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PoliceFingerPrintId)
                     .FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(input.PoliceFingerPrint))
                    {
                        if (input.PoliceFingerPrint.base64 != "")
                        {

                            b64.Base64 = input.PatientImage.base64;
                            b64.ActionTypeId = 2;
                            b64.UpdatedOn = DateTime.Now;
                            b64.UpdatedBy = _tokenService.GetUserId();

                            psi.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceFingerPrint.base64, _tokenService.GetAccessToken());
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
                    if (!AppCommonMethod.IsNullObject(input.PoliceFingerPrint))
                    {
                        if (input.PoliceFingerPrint.base64 != "")
                        {
                            //obj.PoliceFingerPrintProfileId = input.PoliceFingerPrint.ImageTypeProfileId;

                            var ptSigImg = _mapper.Map<PatientImage>(input.PoliceFingerPrint);

                            var ImageBaseSixty4 = new ImageBaseSixtyFour();

                            // Insert in Patient Images Table
                            ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                            ImageBaseSixty4.Base64 = input.PatientImage.base64;
                            ImageBaseSixty4.CreatedOn = DateTime.Now;
                            ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                            ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.PoliceFingerPrint.base64, _tokenService.GetAccessToken());
                            ptSigImg.PatientImageId = Guid.NewGuid();

                            obj.PoliceFingerPrintProfileId = ptSigImg.PatientImageId;


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
                return _mapper.Map<CreateOrEditMLEBasicInfoDto>(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private byte[] CompressQrCode(string text)
        {
            byte[] qrCodeBytes = _qrCodeGenerator.GenerateQrCode(text);

            
            using (MemoryStream compressedStream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress))
                {
                    deflateStream.Write(qrCodeBytes, 0, qrCodeBytes.Length);
                }

                return compressedStream.ToArray();
            }
        }

        /// <summary>
        ///  This Part is For Create Or Edit Mle Examination
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<CreateOrEditMLEExaminationDto> CreateMleExamination(CreateOrEditMLEExaminationDto input)
        {
            var _uowMleExamination = new UnitOfWork<Mleexamination>(_uowGeneral.GetDbContext());
            var obj = _mapper.Map<Mleexamination>(input);

            obj.MlebasicInfoId = input.MlebasicInfoId;

            FillEntityExaminatin(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            Mleexamination responseObj = await _uowMleExamination.Repository.Insert(obj);



            await _uowMleExamination.CommitAsync();
            return _mapper.Map<CreateOrEditMLEExaminationDto>(responseObj);
        }

        private async Task<CreateOrEditMLEExaminationDto> UpdateMleExamination(CreateOrEditMLEExaminationDto input)
        {
            var _uowMleExamination = new UnitOfWork<Mleexamination>(_uowGeneral.GetDbContext());
            var dbObj = await _uowMleExamination.Repository.GetById(input.MleexaminationId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityExaminatin(obj!);

            _uowMleExamination.Repository.Update(obj!);
            await _uowMleExamination.CommitAsync();

            return _mapper.Map<CreateOrEditMLEExaminationDto>(obj);
        }

        /// <summary>
        ///  This Part is For Create Or Edit Mle Report
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<CreateOrEditMleReportDto> CreateMleReport(CreateOrEditMleReportDto input)
        {
            var _uowMlereport = new UnitOfWork<Mlereport>(_uowGeneral.GetDbContext());
            var obj = _mapper.Map<Mlereport>(input);
            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowGeneral.GetDbContext());

            obj.MlebasicInfoId = input.MlebasicInfoId;

            // Insert In ImageBase64 Table
            //
            if (input.MlcManualReport != null)
            {
                if (input.MlcManualReport.base64 != "")
                {
                    //obj.MlcManualReportTypeProfileId = input.MlcManualReport.ImageTypeProfileId;
                    var ptSigImg = _mapper.Map<PatientImage>(input.MlcManualReport);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.MlcManualReport.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcManualReport.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientImageId = Guid.NewGuid();

                    obj.MlcManualReportTypeProfileId = ptSigImg.PatientImageId;


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
                    //obj.MlcDrawImageTypeProfileId = input.MlcDrawImage.ImageTypeProfileId;

                    var ptSigImg = _mapper.Map<PatientImage>(input.MlcDrawImage);

                    var ImageBaseSixty4 = new ImageBaseSixtyFour();

                    // Insert in Patient Images Table
                    ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                    ImageBaseSixty4.Base64 = input.MlcManualReport.base64;
                    ImageBaseSixty4.CreatedOn = DateTime.Now;
                    ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                    ptSigImg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcDrawImage.base64, _tokenService.GetAccessToken());
                    ptSigImg.PatientImageId = Guid.NewGuid();

                    obj.MlcDrawImageTypeProfileId = ptSigImg.PatientImageId;


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
            }


            FillEntityReport(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            Mlereport responseObj = await _uowMlereport.Repository.Insert(obj);


            await _uowMlereport.CommitAsync();
            return _mapper.Map<CreateOrEditMleReportDto>(responseObj);
        }

        private async Task<CreateOrEditMleReportDto> UpdateMleReport(CreateOrEditMleReportDto input)
        {
            var _uowMlereport = new UnitOfWork<Mlereport>(_uowGeneral.GetDbContext());
            var dbObj = await _uowMlereport.Repository.GetById(input.MlereportId!);
            var _uowBase64 = new UnitOfWork<ImageBaseSixtyFour>(_uowGeneral.GetDbContext());

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

            if (input.PatientDrawImgId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientDrawImgId)
                    .FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PatientDrawImgId)
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
                        //obj.MlcDrawImageTypeProfileId = input.MlcDrawImage.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.MlcDrawImage);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.MlcDrawImage.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcDrawImage.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.MlcDrawImageTypeProfileId = ptimg.PatientImageId;


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


            if (input.PatientManualReportId != null)
            {
                var pi = await _uowPatientImage.GetDbContext().PatientImages
                    .Where(x => x.PatientImageId == input.PatientManualReportId).FirstOrDefaultAsync();

                var b64 = await _uowBase64.GetDbContext().ImageBaseSixtyFours.Where(x => x.PatientImageId == input.PatientManualReportId)
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
                        //obj.MlcManualReportTypeProfileId = input.MlcManualReport.ImageTypeProfileId;

                        var ptimg = _mapper.Map<PatientImage>(input.MlcManualReport);
                        // Insert in Patient Images Table
                        ImageBaseSixty4.ImageBaseSixtyFourId = Guid.NewGuid();
                        ImageBaseSixty4.Base64 = input.MlcManualReport.base64;
                        ImageBaseSixty4.CreatedOn = DateTime.Now;
                        ImageBaseSixty4.CreatedBy = _tokenService.GetUserId();

                        ptimg.ImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, input.MlcManualReport.base64, _tokenService.GetAccessToken());
                        ptimg.PatientImageId = Guid.NewGuid();

                        obj.MlcManualReportTypeProfileId = ptimg.PatientImageId;


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

            return _mapper.Map<CreateOrEditMleReportDto>(obj);
        }

        private void FillEntity(MlebasicInfo obj)
        {
            if (obj.MlebasicInfoId == Guid.Empty)
            {
                obj.MlebasicInfoId = Guid.NewGuid();
                obj.IsActive = true;
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

        private void FillEntityExaminatin(Mleexamination obj)
        {
            if (obj.MleexaminationId == Guid.Empty)
            {
                obj.MleexaminationId = Guid.NewGuid();
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

        private void FillEntityReport(Mlereport obj)
        {
            if (obj.MlereportId == Guid.Empty)
            {
                obj.MlereportId = Guid.NewGuid();
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
