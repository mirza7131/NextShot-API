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
    public class IcvCertificateService<TEntity>:IicvCertificate where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly IMapper _mapper;
        private UnitOfWork<IcvCertificate> _uowIcvCertificate;
        private readonly UploadFiles _fileUploader;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;
        private readonly bool _isDevelopment;
        private readonly string _qrBaseUrl;
        #endregion

        #region Constructor
        public IcvCertificateService(TokenService tokenService, IMapper mapper, UnitOfWork<IcvCertificate> uowIcvCertificate, PatientDiagnoseService patientDiagnoseService, IQrCodeGeneratorHelper qrCodeGenerator, UploadFiles fileUploader, IConfiguration config)
        {
            _tokenService = tokenService;            
            _uowIcvCertificate = uowIcvCertificate;
            _mapper = mapper;
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
        public async Task<CreateOrEditIcvCertificateDto> CreateOrEdit(CreateOrEditIcvCertificateDto input)
        {
            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.IcvCertificateId))
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

        private async Task<CreateOrEditIcvCertificateDto> Create(CreateOrEditIcvCertificateDto input)
        {
            PatientDiagnoseDto digDto = new PatientDiagnoseDto();
            digDto.FormType = input.FormType;
            digDto.DocDepartmentLookupId = input.DocDepartmentLookupId;
            digDto.DocSectionLookupId = input.DocSectionLookupId;
            digDto.PatientId = input.PatientId;
            digDto.PatientVisitId = input.PatientVisitId;

            var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);

            input.PatientDiagnoseId = digService.PatientDiagnoseId;

            var obj = _mapper.Map<IcvCertificate>(input);

            var emc = _mapper.Map<Emc>(input.emc);
            emc.EmcId = Guid.NewGuid();
            emc.PatientId = obj.PatientId;
            emc.PatientVisitId = obj.PatientVisitId;
            emc.PatientDiagnoseId = obj.PatientDiagnoseId;
            emc.HealthFacilityId = obj.HealthFacilityId;
            emc.CreatedOn = DateTime.Now;
            emc.CreatedBy = _tokenService.GetUserId();
            emc.ActionTypeId = (int)ActionTypeEnum.Create;

            var _uowEmc = new UnitOfWork<Emc>(_uowIcvCertificate.GetDbContext());

            await _uowEmc.Repository.Insert(emc);
            await _uowEmc.CommitAsync();

            if (obj.QrCodeImagePath == null)
            {
                //string qrCodeValue = "Patient Name = " + input.Name + "," + "Hospital = " + input.HealthFacilityName;
                string qrCodeValue = _qrBaseUrl + "GetSingleIcvCertificate?patientVisitId=" + obj.PatientVisitId;


                string compressedBase64QrCode = "data:image/png;base64," + Convert.ToBase64String(_qrCodeGenerator.GenerateQrCode(qrCodeValue));

                if (obj.QrCodeImagePath == null)
                {
                    var qrCodeImageUrl = await _fileUploader.UploadDocumentsForCrystalReport(CommonStringConstant.EMCMLE, compressedBase64QrCode, _tokenService.GetAccessToken());
                    obj.QrCodeImagePath = qrCodeImageUrl;
                }
            }


            if (input.OpdNo != null)
            {
                var empLetterNo = await _uowIcvCertificate.GetDbContext()
                    .IcvCertificates
                    .Where(x => x.OpdNo == input.OpdNo && x.HealthFacilityId == input.HealthFacilityId)
                    .FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullObject(empLetterNo))
                {
                    if (empLetterNo.IsOpdNoGenerated == true)
                    {
                        throw new UserFriendlyException(CommonMessageConstant.DuplicateOpviNumberFound);
                    }
                }

            }

            FillEntity(obj);

 
            //var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            //if (profile != null)
            //{
            //    obj.FormTypeProfileId = profile.ProfileId;

            //}

            obj.IsOpdNoGenerated = true;

            IcvCertificate responseObj = await _uowIcvCertificate.Repository.Insert(obj);

            await _uowIcvCertificate.CommitAsync();
            return _mapper.Map<CreateOrEditIcvCertificateDto>(responseObj);
        }

        private async Task<CreateOrEditIcvCertificateDto> Update(CreateOrEditIcvCertificateDto input)
        {
            var dbObj = await _uowIcvCertificate.Repository.GetById(input.IcvCertificateId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);

            // for qr code
            //string qrCodeValue =  "Patient Name = " + input.Name + "," + "Hospital = " + input.HealthFacilityName;
            string qrCodeValue = _qrBaseUrl + "GetSingleIcvCertificate?patientVisitId=" + obj.PatientVisitId;


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

            //var _uowEmc = new UnitOfWork<Emc>(_uowIcvCertificate.GetDbContext());

            //_uowEmc.Repository.Update(emc);
            //await _uowEmc.CommitAsync();

            FillEntity(obj!);

            _uowIcvCertificate.Repository.Update(obj!);
            await _uowIcvCertificate.CommitAsync();

            return _mapper.Map<CreateOrEditIcvCertificateDto>(obj);
        }
        #endregion


        #region Get
        public async Task<ViewPagerDto<IcvPatientDto>> GetIcvPatientsList(SearchFilterDto? filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowIcvCertificate.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetICVPatients]", (SqlConnection)conn);
                    sqlComm.Parameters.AddWithValue("@DoctorId", filter.DoctorId);
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<IcvPatientDto> lst = ds.Tables[0].ToList<IcvPatientDto>();

                    IEnumerable<IcvPatientDto> lstto;

                    List<IcvPatientDto> objList = lst.ToList();

                    if (!String.IsNullOrEmpty(filter.SearchString))
                        lstto = objList.Where(x => x.FullName.ToLower().StartsWith(filter.SearchString.ToLower())).AsQueryable();
                    else
                        lstto = objList.AsQueryable();
                    //return lst;

                    var pagedList = await Task.Run(() => PagedListDto<IcvPatientDto>.ToPagedList(
                        lstto.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<IcvPatientDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<IcvPatientDto>>(pagedList)
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

        public async Task<IcvCertificate> GetSingleIcvPatientByPatientId(Guid PatientId)
        {
            try
            {
                var list =await _uowIcvCertificate.GetDbContext().IcvCertificates.Where(x => x.PatientId == PatientId)
                    .FirstOrDefaultAsync();
                if (list != null)
                    return list;
                else
                    throw new UserFriendlyException("No Record Found with that Patient Id..!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Helper Methods

        private void FillEntity(IcvCertificate obj)
        {
            if (obj.IcvCertificateId == Guid.Empty)
            {
                obj.IcvCertificateId = Guid.NewGuid();
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
        private void FillEntityDelete(IcvCertificate obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }
        #endregion
    }
}
