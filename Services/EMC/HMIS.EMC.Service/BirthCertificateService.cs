using AutoMapper;
using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Service.Interfaces;
using HMIS.EMC.Domain.Repositories.UOW;
using JWTAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.EMC.Domain.Models.Dto;
using AppCommonMethods;
using CommonExceptionHandler;
using CommonMessages;
using CommonDTOs.Enums;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using HMIS.EMC.Service.Interfaces.QrCodeHelper;
using FileHandler;
using Microsoft.Extensions.Configuration;

namespace HMIS.EMC.Service
{
    public class BirthCertificateService<TEntity> : IBirthCertificate where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly IMapper _mapper;
        private UnitOfWork<BirthCertificate> _uowBirthCertificate;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        private readonly UploadFiles _fileUploader;
        private readonly bool _isDevelopment;
        private readonly string _qrBaseUrl;
        #endregion

        #region Constructor
        public BirthCertificateService(TokenService tokenService, IMapper mapper, UnitOfWork<BirthCertificate> uowBirthCertificate, PatientDiagnoseService patientDiagnoseService, IQrCodeGeneratorHelper qrCodeGenerator, UploadFiles fileUploader, IConfiguration config)
        {
            this._tokenService = tokenService;
            this._mapper = mapper;
            this._uowBirthCertificate = uowBirthCertificate;
            _patientDiagnoseService = patientDiagnoseService;
            _qrCodeGenerator = qrCodeGenerator;
            _fileUploader = fileUploader;

            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _qrBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("CrystalUrl").Value ?? string.Empty;
            else
                _qrBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("CrystalUrl").Value ?? string.Empty;
        }
        #endregion

        #region CUD
        public async Task<CreateOrEditBirthCertificateDto> CreateOrEdit(CreateOrEditBirthCertificateDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.BirthCertificateId))
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

        private async Task<CreateOrEditBirthCertificateDto> Create(CreateOrEditBirthCertificateDto input)
        {
            PatientDiagnoseDto digDto = new PatientDiagnoseDto();
            digDto.FormType = input.FormType;
            digDto.DocDepartmentLookupId = input.DocDepartmentLookupId;
            digDto.DocSectionLookupId = input.DocSectionLookupId;
            digDto.PatientId = input.PatientId;
            digDto.PatientVisitId = input.PatientVisitId;

            var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);

            input.PatientDiagnoseId = digService.PatientDiagnoseId;

            var obj = _mapper.Map<BirthCertificate>(input);

            //... Generate Qrcode
            if (obj.QrCodeImagePath == null)
            {
                //string qrCodeValue = "Patient Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;
                string qrCodeValue = _qrBaseUrl + "GetSingleBirthCertificate?patientVisitId=" + obj.PatientVisitId;

                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                if (obj.QrCodeImagePath == null)
                {
                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                    obj.QrCodeImagePath = qrCodeImageUrl;
                }
            }

            //End QrCode Code here

            var emc = _mapper.Map<Emc>(input.emc);
            emc.EmcId = Guid.NewGuid();
            emc.PatientId = obj.PatientId;
            emc.PatientVisitId = obj.PatientVisitId;
            emc.PatientDiagnoseId = obj.PatientDiagnoseId;
            emc.HealthFacilityId = obj.HealthFacilityId;
            emc.CreatedOn = DateTime.Now;
            emc.CreatedBy = _tokenService.GetUserId();
            emc.ActionTypeId = (int)ActionTypeEnum.Create;

            var _uowEmc = new UnitOfWork<Emc>(_uowBirthCertificate.GetDbContext());
            await _uowEmc.Repository.Insert(emc);
            await _uowEmc.CommitAsync();

            var random = new Random();
            var chars = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
            var randomNo = new string(Enumerable.Repeat(chars, 7)
               .Select(s => s[random.Next(s.Length)]).ToArray());


            FillEntity(obj);
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            obj.TrackingId = input.HealthFacilityId.ToString() + "-" + randomNo + "-" + DateTime.Now.Year.ToString();

            BirthCertificate responseObj = await _uowBirthCertificate.Repository.Insert(obj);

            await _uowBirthCertificate.CommitAsync();
            return _mapper.Map<CreateOrEditBirthCertificateDto>(responseObj);
        }

        private async Task<CreateOrEditBirthCertificateDto> Update(CreateOrEditBirthCertificateDto input)
        {
            var dbObj = await _uowBirthCertificate.Repository.GetById(input.BirthCertificateId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            // for qr code
            //string qrCodeValue =  "Patient Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;
            string qrCodeValue = _qrBaseUrl + "GetSingleBirthCertificate?patientVisitId=" + obj.PatientVisitId;


            string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                    obj.QrCodeImagePath = qrCodeImageUrl;

            //

            //var emc = _mapper.Map<Emc>(input.emc);
            //emc.PatientId = obj.PatientId;
            //emc.PatientVisitId = obj.PatientVisitId;
            //emc.PatientDiagnoseId = obj.PatientDiagnoseId;
            //emc.HealthFacilityId = obj.HealthFacilityId;
            //emc.CreatedOn = DateTime.Now;
            //emc.CreatedBy = _tokenService.GetUserId();

            //var _uowEmc = new UnitOfWork<Emc>(_uowBirthCertificate.GetDbContext());

            //_uowEmc.Repository.Update(emc);
            //await _uowEmc.CommitAsync();


            FillEntity(obj!);

            _uowBirthCertificate.Repository.Update(obj!);
            await _uowBirthCertificate.CommitAsync();

            return _mapper.Map<CreateOrEditBirthCertificateDto>(obj);
        }
        #endregion

        #region READ
        public async Task<ViewPagerDto<GetAllBirthCertificateDto>> GetAllBirthCertificateDto(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowBirthCertificate.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetBirthCertificateRecords]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "All");
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetAllBirthCertificateDto> lst = ds.Tables[0].ToList<GetAllBirthCertificateDto>();

                    IEnumerable<GetAllBirthCertificateDto> lstto;

                    List<GetAllBirthCertificateDto> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x => x.ChildName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();

                    var pagedList = await Task.Run(() => PagedListDto<GetAllBirthCertificateDto>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<GetAllBirthCertificateDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<GetAllBirthCertificateDto>>(pagedList)
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

        public async Task<List<SingleBirthCertificateRecordDto>> GetSingleBirthCertificateRecord(Guid PatientVisitId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowBirthCertificate.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetBirthCertificateRecords]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SingleBirthCertificateRecordDto> lst = ds.Tables[0].ToList<SingleBirthCertificateRecordDto>();

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

        private void FillEntity(BirthCertificate obj)
        {
            if (obj.BirthCertificateId == Guid.Empty)
            {
                obj.BirthCertificateId = Guid.NewGuid();
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
        private void FillEntityDelete(BirthCertificate obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
