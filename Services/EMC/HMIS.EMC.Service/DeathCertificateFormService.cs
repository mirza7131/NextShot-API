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
    public class DeathCertificateFormService<TEntity> : IDeathCertificate where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private UnitOfWork<DeathCertificate> _uowDeathCertificate;
        private readonly UploadFiles _fileUploader;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        private readonly bool _isDevelopment;
        private readonly string _qrBaseUrl;
        #endregion

        #region Constructor
        public DeathCertificateFormService(TokenService tokenService, IMapper mapper, 
            UnitOfWork<DeathCertificate> uowIcvCertificate
            , PatientDiagnoseService patientDiagnoseService
            ,IQrCodeGeneratorHelper qrCodeGenerator
            , UploadFiles fileUploader
            , IConfiguration config
            )
        {
            _tokenService = tokenService;   
            _mapper = mapper;   
            _uowDeathCertificate = uowIcvCertificate;
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
        public async Task<CreateOrEditDeathCertificateDto> CreateOrEdit(CreateOrEditDeathCertificateDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.DeathCertificateId))
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


        private async Task<CreateOrEditDeathCertificateDto> Create(CreateOrEditDeathCertificateDto input)
        {
            PatientDiagnoseDto digDto = new PatientDiagnoseDto();
            digDto.FormType = input.FormType;
            digDto.DocDepartmentLookupId = input.DocDepartmentLookupId;
            digDto.DocSectionLookupId = input.DocSectionLookupId;
            digDto.PatientId = input.PatientId;
            digDto.PatientVisitId = input.PatientVisitId;

            var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);

            input.PatientDiagnoseId = digService.PatientDiagnoseId;

            var obj = _mapper.Map<DeathCertificate>(input);

            // Start QrCode here
            if (obj.QrCodeImagePath == null)
            {
                //string qrCodeValue = "Patient Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;
                string qrCodeValue = _qrBaseUrl + "GetSingleDeathCertificate?patientVisitId=" + obj.PatientVisitId;


                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                if (obj.QrCodeImagePath == null)
                {
                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                    obj.QrCodeImagePath = qrCodeImageUrl;
                }
            }
            // End Qr Code

            var random = new Random();
            var chars = DateTime.Now.Ticks + "5465422554654564651234567892132654654987897" + DateTime.Now.Ticks;
            var randomNo = new string(Enumerable.Repeat(chars, 7)
               .Select(s => s[random.Next(s.Length)]).ToArray());

            var emc = _mapper.Map<Emc>(input.emc);
            emc.EmcId = Guid.NewGuid();
            emc.PatientId = obj.PatientId;
            emc.PatientVisitId = obj.PatientVisitId;
            emc.PatientDiagnoseId = obj.PatientDiagnoseId;
            emc.HealthFacilityId = obj.HealthFacilityId;
            emc.CreatedOn = DateTime.Now;
            emc.CreatedBy = _tokenService.GetUserId();
            emc.ActionTypeId = (int)ActionTypeEnum.Create;

            var _uowEmc = new UnitOfWork<Emc>(_uowDeathCertificate.GetDbContext());

            await _uowEmc.Repository.Insert(emc);
            await _uowEmc.CommitAsync();

            FillEntity(obj);

            
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}
            obj.TrackingId = input.HealthFacilityId.ToString() + "-" + randomNo + "-" + DateTime.Now.Year.ToString();

            DeathCertificate responseObj = await _uowDeathCertificate.Repository.Insert(obj);
            await _uowDeathCertificate.CommitAsync();
            return _mapper.Map<CreateOrEditDeathCertificateDto>(responseObj);
        }

        private async Task<CreateOrEditDeathCertificateDto> Update(CreateOrEditDeathCertificateDto input)
        {
            var dbObj = await _uowDeathCertificate.Repository.GetById(input.DeathCertificateId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);

            // for qr code
            //string qrCodeValue = "Patient Name = " + input.FullName + "," + "Hospital = " + input.HealthFacilityName;
            string qrCodeValue = _qrBaseUrl + "GetSingleDeathCertificate?patientVisitId=" + obj.PatientVisitId;


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

            //var _uowEmc = new UnitOfWork<Emc>(_uowDeathCertificate.GetDbContext());

            //_uowEmc.Repository.Update(emc);
            //await _uowEmc.CommitAsync();

            FillEntity(obj!);

            _uowDeathCertificate.Repository.Update(obj!);
            await _uowDeathCertificate.CommitAsync();

            return _mapper.Map<CreateOrEditDeathCertificateDto>(obj);
        }
        #endregion

        #region Read
        public async Task<ViewPagerDto<GetDeathCertificateRecordDto>> GetDeathCertificatePatientsList(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowDeathCertificate.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetDeathCertificate]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "All");
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetDeathCertificateRecordDto> lst = ds.Tables[0].ToList<GetDeathCertificateRecordDto>();


                    IEnumerable<GetDeathCertificateRecordDto> lstto;

                    List<GetDeathCertificateRecordDto> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x => x.FullName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();

                    var pagedList = await Task.Run(() => PagedListDto<GetDeathCertificateRecordDto>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<GetDeathCertificateRecordDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<GetDeathCertificateRecordDto>>(pagedList)
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

        public async Task<List<GetDeathCertificateRecordDto>> GetSingleDeathCertificatePatient(Guid PatientVisitId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowDeathCertificate.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetDeathCertificate]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetDeathCertificateRecordDto> lst = ds.Tables[0].ToList<GetDeathCertificateRecordDto>();



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
        #endregion

        #region Helper Methods
        private void FillEntity(DeathCertificate obj)
        {
            if (obj.DeathCertificateId == Guid.Empty)
            {
                obj.DeathCertificateId = Guid.NewGuid();
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
