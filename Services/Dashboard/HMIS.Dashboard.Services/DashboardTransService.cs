using AppCommonMethods;
using AutoMapper;
using CommonDTOs;
using CommonDTOs.AidsDTO;
using CommonDTOs.Enums;
using CommonDTOs.HealthCertificateDTO;
using CommonDTOs.HealthCouncilDTO;
using CommonDTOs.MedicoLegalDTO;
using CommonDTOs.TBScreeningDTO;
using CommonMessages;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using HMIS.Dashboard.Domain.Models.TransDbModels;
//using HMIS.Dashboard.Domain.Models.DbModels_TB;
using HMIS.Dashboard.Domain.Models.DTO.Common;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.AdminReferedDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.DoctorDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PathologyDashboardPathologyDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PatientRegistrationDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PharmacyDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.VitalDashboard;
using HMIS.Dashboard.Domain.Models.DTO.PaginationDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientVisitFlowDto;
using HMIS.Dashboard.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DbModel = HMIS.Dashboard.Domain.Models.TransDbModels;
using HealthFacility = HMIS.Dashboard.Domain.Models.TransDbModels.HealthFacility;
using HMIS.Dashboard.Domain.Models.DTO.TbMedicineDeliveryData;
using DTOs.UserDTO;
using SMSSender.DTO;
using SMSSender;
//using ZXing;
//using ZXing.Common;
//using System.Drawing;
//using System.Drawing.Imaging;

namespace HMIS.Dashboard.Service
{
    public class DashboardTransService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly TransUnitOfWork<DbModel.Patient> _uowPatient;
        private readonly HealthCertificateService _healthCertificateService;
        private readonly MedicoLegalService _medicoLegalService;
        private readonly HealthCouncilService _healthCouncilService;
        private readonly MIMSService _mimsService;
        private readonly AidsService _aidsService;
        //private readonly PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;
        private TransUnitOfWork<DbModel.PatientDiagnose> _uowPatientDiagnose;
        private TransUnitOfWork<DbModel.MimsMedicineDatum> _uowMimsMedicineDatum;
        private readonly TransUnitOfWork<DbModel.PatientOpenVisit> _uowPatientOpenVisit;
        private readonly TBScreeningService _tbScreeningService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly bool _isActiveOffline;
        private readonly bool _ShowTbMedicineOnSlip;
        private readonly bool _SendMsgForTbMedicine;
        private readonly SMS _smsService;

        #endregion

        #region Constructor

        public DashboardTransService(
            TokenService tokenService,
            IMapper mapper,
            TransUnitOfWork<DbModel.Patient> uowPatient,
            HealthCertificateService healthCertificateService,
            MedicoLegalService medicoLegalService,
            HealthCouncilService healthCouncilService,
            MIMSService mimsService,
            AidsService aidsService,
            TBScreeningService tbScreeningService,
            TransUnitOfWork<DbModel.PatientDiagnose> uowPatientDiagnose,
             TransUnitOfWork<DbModel.PatientOpenVisit> uowPatientOpenVisit,
              TransUnitOfWork<DbModel.MimsMedicineDatum> uowMimsMedicineDatum,
            //PatientDiagnoseService<PatientDiagnose> patientDiagnoseService,
            IConfiguration config,
            SMS smsService
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatient = uowPatient;
            _healthCertificateService = healthCertificateService;
            _medicoLegalService = medicoLegalService;
            _healthCouncilService = healthCouncilService;
            _mimsService = mimsService;
            _aidsService = aidsService;
            _uowMimsMedicineDatum = uowMimsMedicineDatum;
            _tbScreeningService = tbScreeningService;
            //_patientDiagnoseService = patientDiagnoseService;
            _uowPatientDiagnose = uowPatientDiagnose;
            _uowPatientOpenVisit = uowPatientOpenVisit;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;

            _ShowTbMedicineOnSlip = config.GetValue<bool>("ShowTbMedicineOnSlip") == true ? true : false;
            _SendMsgForTbMedicine = config.GetValue<bool>("SendMsgForTbMedicine") == true ? true : false;

            _isActiveOffline = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                           config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") : false;
            _smsService = smsService;
        }

        #endregion

        #region CUD Operations

        #endregion

        #region Read Operations

        //public async Task<List<DashboardDataSyncUtilityLogDto>> getDataSyncUtilityLog(SyncUtilityDashboardFilter filter)
        //{
        //    DashboardDataSyncUtilityLogDto list = new DashboardDataSyncUtilityLogDto();
        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
        //        try
        //        {

        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("[SPDataSyncUtilityLog]", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

        //            //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
        //                sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
        //                sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
        //                sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);
                    
        //            if (!AppCommonMethod.IsNullBool(filter.isAllList))
        //                sqlComm.Parameters.AddWithValue("@isAllList", filter.isAllList);
                    


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
        //                sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
        //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<DashboardDataSyncUtilityLogDto> lst = ds.Tables[0].ToList<DashboardDataSyncUtilityLogDto>();
        //            return lst;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        //public async Task<List<ViewPatientDashboardDto>> GetAll(Expression<Func<PatientDashboard, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    string includeProperties = "")
        //{
        //    List<PatientDashboard> responseObj = await _uowPatientDashboard.Repository.GetALL(filter).ToListAsync();
        //    return _mapper.Map<List<ViewPatientDashboardDto>>(responseObj);
        //}


        //public async Task<ViewPatientDashboardDto> GetById(int input)
        //{
        //    PatientDashboard? responseObj = await _uowPatientDashboard.Repository.GetById(input);

        //    if (AppCommonMethod.IsNullObject(responseObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    return _mapper.Map<ViewPatientDashboardDto>(responseObj);
        //}

        // Obsolete
        //public async Task<ViewPatientDashboardCountDto> GetDashboardCounts(Expression<Func<DbModel.Patient, bool>>? filter = null,
        //    Func<IQueryable, IOrderedQueryable>? orderBy = null,
        //    string includeProperties = "")
        //{
        //    var _uowPatientOpenVisit = new TransUnitOfWork<PatientOpenVisit>(_uowPatient.GetDbContext());
        //    var _uowUser = new TransUnitOfWork<User>(_uowPatient.GetDbContext());

        //    User? dbUser = await _uowUser.Repository.GetById(_tokenService.GetUserId());

        //    ViewPatientDashboardCountDto patientDashboardCount = new ViewPatientDashboardCountDto();

        //    //Get Patients
        //    var patients = await _uowPatient.Repository.GetALL()
        //        //.WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.Province, x => x.ProvinceId == dbUser!.ProvinceId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.Division, x => x.DivisionId == dbUser!.DivisionId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.District, x => x.DistrictId == dbUser!.DistrictId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.Tehsil, x => x.TehsilId == dbUser!.TehsilId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.UnionCouncil, x => x.UnionCouncilId == dbUser!.UcId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.HealthFacility, x => x.HealthFacilityId == dbUser!.HealthFacilityId)
        //        .ToListAsync();

        //    patientDashboardCount.TotalPatients = patients.Count();
        //    patientDashboardCount.TodayPatients = patients.Where(x => x.CreatedOn!.Value.Date == DateTime.Today.Date).Count();

        //    //Get Visits
        //    var patientVisits = await _uowPatientOpenVisit.Repository.GetALL()
        //        .Include(x => x.HealthFacility)

        //        //.WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.Province, x => x.HealthFacility!.UnionCouncil!.Tehsil.District!.Division!.Province!.ProvinceId == dbUser!.ProvinceId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.Division, x => x.HealthFacility!.UnionCouncil!.Tehsil.District!.Division!.DivisionId == dbUser!.DivisionId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.District, x => x.HealthFacility!.UnionCouncil!.Tehsil.District!.DistrictId == dbUser!.DistrictId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.Tehsil, x => x.HealthFacility!.UnionCouncil!.Tehsil.TehsilId == dbUser!.TehsilId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.UnionCouncil, x => x.HealthFacility!.UnionCouncil!.UnionCouncilId == dbUser!.UcId)
        //        .WhereIf(GetUserLevel.GetUserRole(dbUser) == (int)UserLevelEnum.HealthFacility, x => x.HealthFacilityId == dbUser!.HealthFacilityId)
        //        .ToListAsync();

        //    patientDashboardCount.TotalVisits = patientVisits.Count();
        //    patientDashboardCount.TodayVisits = patientVisits.Where(x => x.CreatedOn!.Value.Date == DateTime.Today.Date).Count();

        //    return patientDashboardCount;
        //}

        public async Task<List<ViewPatientOpenVisitCountByGenderByMonthDto>> GetPatientVisitCountByGenderByMonth()
        {
            var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitCountByGenderByMonths
                .WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
                .GroupBy(x => new { x.VisitMonth, x.Gender }).
                Select(x => new ViewPatientOpenVisitCountByGenderByMonthDto
                {
                    VisitMonth = x.Key.VisitMonth,
                    VisitCount = x.Sum(x => x.VisitCount),
                    Gender = x.Key.Gender
                }).ToListAsync();

            return list;
        }

        //public async Task<List<ViewTodayPatientVisitCountByDeptBySecDto>> GetTodayPatientVisitCountByDeptBySec()
        //{
        //    var list = await _uowPatient.GetDbContext().ViewTodayPatientOpenVisitCountByDeptBySecs
        //        .WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
        //        .OrderByDescending(x => x.VisitCount)
        //        .ToListAsync();

        //    return _mapper.Map<List<ViewTodayPatientVisitCountByDeptBySecDto>>(list);
        //}

        #endregion



        //public void GenerateBarcode(string barcodeText, int width, int height, string filename)
        //{
        //    BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>();
        //    barcodeWriter.Format = BarcodeFormat.CODE_39;
        //    barcodeWriter.Options.Width = width;
        //    barcodeWriter.Options.Height = height;
        //    Bitmap barcodeImage = barcodeWriter.Write(barcodeText);
        //    barcodeImage.Save(filename, ImageFormat.Png);
        //}

        #region Registration Dashboard New 
        public async Task<RegistrationDashboardLoginUserAllCountsDTO> getRegistrationDashboardLoginUserAllCounts(DashboardFilter filter)
        {
            RegistrationDashboardLoginUserAllCountsDTO Registrationuserdashboard = new RegistrationDashboardLoginUserAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPRegistrationDashboardLoginUserAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<RegistrationDashboardLoginUserAllCountsDTO> lst = ds.Tables[0].ToList<RegistrationDashboardLoginUserAllCountsDTO>();
                    return lst[0];
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
        public async Task<RegistrationDashboardAllCountsDTO> getRegistrationDashboardAllCounts(DashboardFilter filter)
        {
            RegistrationDashboardAllCountsDTO Registrationdashboard = new RegistrationDashboardAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPRegistrationDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    Registrationdashboard.AllCounts = ds.Tables[0].ToList<RegistrationDashboardAllCounts>();
                    Registrationdashboard.AgeWisePatient = ds.Tables[1].ToList<AgeWisePatientCount>();
                    return Registrationdashboard;
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

        public async Task<PatientDetailsWithPaginationdto> getRegistrationDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto RegistrationList = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPRegistrationDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    RegistrationList.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    RegistrationList.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return RegistrationList;

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

        public async Task<List<OPDSectionWiseTokenCountDTO>> getRegDashboardOPDSectionWiseTokenCount(DashboardFilter filter)
        {

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPRegDashboardOPDSectionWiseTokenCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<OPDSectionWiseTokenCountDTO> lst = ds.Tables[0].ToList<OPDSectionWiseTokenCountDTO>();
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
        public async Task<List<PatientVisitCountByDEO>> GetPatientVisitCountByUser(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetPatientVisitCountByUserEntries", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientVisitCountByDEO> lst = ds.Tables[0].ToList<PatientVisitCountByDEO>();
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

        #endregion

        #region HR Health Dashboard Counts And Listing 
        public async Task<HRHealthDashboardCountDTO> getHRHealthDashboardCounts(DashboardFilter filter)
        {
            HRHealthDashboardCountDTO objRes = new HRHealthDashboardCountDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHRHealthDashboardCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    objRes.TotalRegistered = ds.Tables[0].ToList<HRHealthDashboardCountDTO>().FirstOrDefault().TotalRegistered;
                    objRes.HfTypeWiseCount = ds.Tables[1].ToList<HRHealthDashboardHFTypeCountDTO>();
                    
                    return objRes;
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

        public async Task<List<PatientDetailHRDashboarddto>> getHRHealthDashboardListing(DashboardFilter filter)
        {
            PatientDetailHRDashboarddto objResponse = new PatientDetailHRDashboarddto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHRHealthDashboardListing", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetailHRDashboarddto> lst = ds.Tables[0].ToList<PatientDetailHRDashboarddto>();
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

        public async Task<HRHRSectionWiseDTO> getHRSectionWiseTotalCounts(DashboardFilter filter)
        {
            HRHRSectionWiseDTO objResSec = new HRHRSectionWiseDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHRSectionWiseTotalCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HRHRSectionWiseDTO> lst = ds.Tables[0].ToList<HRHRSectionWiseDTO>();


                    return lst[0];
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
        #region Patient Registration Dashboard

        public async Task<List<GetPatientVisitCountByHealthFacilityByPMIS>> GetPatientVisitCountByHealthFacilityByPMIS(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

                 //.WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                 //.WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value.Date <= filter.EndDate!.Value.Date)
                 //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))

                 .GroupBy(x => new { x.VisitHf })
                .Select(x => new GetPatientVisitCountByHealthFacilityByPMIS
                {
                    name = x.Key.VisitHf,
                    hmisValue = x.Sum(x => x.IsFromPmis ? 0 : 1),
                    pmisValue = x.Sum(x => x.IsFromPmis ? 1 : 0)
                }).OrderBy(x => x.name).ToListAsync();

            return list;
        }

        public async Task<List<PatientVisitCountByHealthFacility>> GetPatientVisitCountByHealthFacility(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))

                .GroupBy(x => new { x.HealthFacilityId, x.VisitHf }).
                Select(x => new PatientVisitCountByHealthFacility
                {
                    name = x.Key.VisitHf,
                    extra = x.Key.HealthFacilityId,
                    value = x.Count()
                }).OrderBy(x => x.name).ToListAsync();

            return list;
        }

        //public async Task<List<PatientVisitCountByUser>> GetPatientVisitCountByUser(DashboardFilter filter)
        //{

        //    List<string> user = new List<string>();
        //    if (filter.User != null)
        //        user = filter!.User!.Split(',').ToList();

        //    var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitDetails
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

        //        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
        //        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
        //        //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))

        //        .GroupBy(x => new { x.PatientVisitCreatedByName, x.PatientVisitCreatedBy,x.PatientVisitCreatedByDesignation,x.PatientVisitCreatedByCnic }).
        //        Select(x => new PatientVisitCountByUser
        //        {
        //            name =  x.Key.PatientVisitCreatedByName + " (" + x.Key.PatientVisitCreatedByDesignation + ")",
        //            extra = x.Key.PatientVisitCreatedBy,
        //            value = x.Count()
        //        }).OrderBy(x => x.name).ToListAsync();

        //    return list;
        //}
        
        public async Task<List<PatientVisitCountByProvince>> GetPatientVisitCountByProvince(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitDetails
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
               .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))

               .GroupBy(x => new { x.PatientProvinceName, x.PatientProvinceId }).
               Select(x => new PatientVisitCountByProvince
               {
                   name = x.Key.PatientProvinceName,
                   extra = new PatientDashboardExtra
                   {
                       patientProvinceId = x.Key.PatientProvinceId,
                   },
                   value = x.Count()
               }).OrderBy(x => x.name).ToListAsync();

            var provinceList = await _uowPatient.GetDbContext().Provinces
                .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .Select(x => new PatientVisitCountByProvince
                {
                    name = x.Name,
                    value = 0
                }).ToListAsync();

            foreach (var item in provinceList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                    {
                        item.value = item2.value;
                        item.extra = item2.extra;
                    }
                }
            }

            return provinceList.OrderBy(x => x.name).ToList();
        }

        public async Task<List<PatientRegisteredCountByProvince>> GetPatientRegisteredCountByProvince(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientRegistrationDetails.Where(x => x.VisitNo == 1)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientCreatedBy.ToString()!))

                 .GroupBy(x => new { x.PatientProvinceName, x.PatientProvinceId }).
                 Select(x => new PatientRegisteredCountByProvince
                 {
                     name = x.Key.PatientProvinceName,
                     extra = new PatientDashboardExtra
                     {
                         patientProvinceId = x.Key.PatientProvinceId,
                     },
                     value = x.Count()
                 }).OrderBy(x => x.name).ToListAsync();

            var provinceList = await _uowPatient.GetDbContext().Provinces
               .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
               .Select(x => new PatientRegisteredCountByProvince
               {
                   name = x.Name,
                   value = 0
               }).ToListAsync();

            foreach (var item in provinceList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                    {
                        item.value = item2.value;
                        item.extra = item2.extra;
                    }
                }
            }

            return provinceList.OrderBy(x => x.name).ToList();
        }

        public async Task<List<PatientVisitCountByProvinceByGender>> GetPatientVisitCountByProvinceByGender(DashboardFilter filter)
        {
            var list = await _uowPatient.GetDbContext()
                    .ViewPatientOpenVisitDetails
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                  .GroupBy(x => new { x.PatientProvinceName, x.Gender })
                  .Select(x => new
                  {
                      province = x.Key.PatientProvinceName,
                      gender = x.Key.Gender,
                      value = x.Count()
                  })
                  .OrderByDescending(x => x.province)
                  .ThenByDescending(x => x.gender)
                  .ToListAsync();


            List<PatientVisitCountByProvinceByGender> responseList = new List<PatientVisitCountByProvinceByGender>();

            var tempProvince = "";

            foreach (var item in list)
            {

                if (tempProvince != item.province)
                {
                    tempProvince = item.province;

                    var tempObj = new PatientVisitCountByProvinceByGender();
                    tempObj.name = item.province;
                    tempObj.series = list.Where(x => x.province == item.province).Select(x => new PatientVisitCountByProvinceByGenderSeries
                    {
                        name = x.gender,
                        value = x.value
                    }).ToList();
                    responseList.Add(tempObj);

                }


            }
            return responseList;
        }

        public async Task<List<PatientNewRegisteredVsRevisit>> GetPatientNewRegisteredVsRevisit(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientNewRegisteredVsRevisit", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientNewRegisteredVsRevisit> lst = ds.Tables[0].ToList<PatientNewRegisteredVsRevisit>();
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

        public async Task<List<PatientVisitCountSelfVsOthers>> GetPatientVisitCountSelfVsOthers(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext()
            .ViewPatientOpenVisitDetails
            .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
            .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
           .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
           .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
            .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
           .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
           .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
            .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn >= filter.StartDate)
            .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn <= filter.EndDate)
            .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))
            .GroupBy(x => new { x.Relation })
            .Select(x => new PatientVisitCountSelfVsOthers
            {
                name = x.Key.Relation,
                value = x.Count()
            })
            .OrderByDescending(x => x.name)
            .ToListAsync();

            var responseList = new List<PatientVisitCountSelfVsOthers>();
            if (list.Count() > 0)
            {
                if (list.Where(x => x.name == CommonConstants.Self).FirstOrDefault() != null)
                    responseList.Add(list.Where(x => x.name == CommonConstants.Self).FirstOrDefault());

                responseList.Add(new PatientVisitCountSelfVsOthers()
                {
                    name = "Others",
                    value = list.Where(x => x.name != CommonConstants.Self).Sum(x => x.value)
                });
            }
            return responseList;
        }

        public async Task<List<PatientVisitCountByGenderBySelfVsOthers>> GetPatientVisitCountByGenderBySelfVsOthers(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //   if(filter.StartDate==filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}

            var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn.Value >= filter.StartDate.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn.Value <= filter.EndDate)
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))
                .GroupBy(x => new { x.Relation, x.Gender, x.GenderId, x.ChartPieSequenceNo })
                .Select(x => new
                {
                    name = x.Key.Relation,
                    gender = x.Key.Gender,
                    genderid = x.Key.GenderId,
                    ChartSeq = x.Key.ChartPieSequenceNo,
                    value = x.Count()
                })
                .OrderBy(x => x.ChartSeq)
                .ToListAsync();

            var qryProfileList = (from _profiles in _uowPatient.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                  join _profileType in _uowPatient.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Gender) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                  select new PatientVisitCountByGender
                                  {
                                      name = _profiles.Name,
                                      value = 0,
                                      extra = _profiles.ProfileId.ToString(),
                                      seqNo = _profiles.ChartPieSequenceNo
                                  });

            var selfGenderList = await qryProfileList.OrderBy(x => x.seqNo).ToListAsync().ConfigureAwait(false);
            var OthersGenderList = await qryProfileList.OrderBy(x => x.seqNo).ToListAsync().ConfigureAwait(false);

            var responseList = new List<PatientVisitCountByGenderBySelfVsOthers>();

            responseList.Add(
                new PatientVisitCountByGenderBySelfVsOthers
                {
                    series = list.Where(x => x.name == CommonConstants.Self)
                            .GroupBy(x => new { x.gender, x.genderid })
                            .Select(x => new PatientVisitCountByGender
                            {
                                name = x.Key.gender,
                                extra = x.Key.genderid.ToString(),
                                value = x.Sum(g => g.value)
                            }).ToList(),
                    relation = CommonConstants.Self
                });

            foreach (var item in selfGenderList)
            {
                foreach (var item2 in responseList[0].series)
                {
                    if (item.name == item2.name)
                    {
                        item.value = item2.value;
                        item.extra = item2.extra;
                    }
                }
            }

            responseList[0].series = selfGenderList.OrderBy(x => x.seqNo).ToList();

            responseList.Add(
              new PatientVisitCountByGenderBySelfVsOthers
              {
                  series = list.Where(x => x.name != CommonConstants.Self)
                           .GroupBy(x => new { x.gender, x.genderid, x.ChartSeq })
                          .Select(x => new PatientVisitCountByGender
                          {
                              name = x.Key.gender,
                              extra = x.Key.genderid.ToString(),
                              seqNo = x.Key.ChartSeq,
                              value = x.Sum(g => g.value)
                          }).OrderBy(x => x.seqNo).ToList(),
                  relation = CommonConstants.Others
              });

            foreach (var item in OthersGenderList)
            {
                foreach (var item2 in responseList[1].series)
                {
                    if (item.name == item2.name)
                    {
                        item.value = item2.value;
                        item.extra = item2.extra;
                    }
                }
            }

            responseList[1].series = OthersGenderList.OrderBy(x => x.seqNo).ToList();


            return responseList;
        }

        public async Task<List<PatientVisitCountByAgeRangeByGender>> GetPatientVisitCountByAgeRangeByGender(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetPatientVisitCountByAgeRangeByGender", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientVisitCountByAgeRangeByGender> lst = ds.Tables[0].ToList<PatientVisitCountByAgeRangeByGender>();
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

        public async Task<List<PatientVisitCountByDeptBySecByMonth>> GetPatientVisitCountByDeptBySecByMonth(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetPatientOpenVisitCountByDeptBySecByMonth", (SqlConnection)conn);
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientVisitCountByDeptBySecByMonth> lst = ds.Tables[0].ToList<PatientVisitCountByDeptBySecByMonth>();
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

            //var list = await _uowPatient.GetDbContext().ViewPatientOpenVisitCountByDeptBySecByMonths
            //    .WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
            //    .OrderByDescending(x => x.VisitCount)
            //    .ToListAsync();

            //return _mapper.Map<List<PatientVisitCountByDeptBySecByMonthDto>>(list);
        }

        public async Task<List<VisitCountByDeptBySec>> GetTodayPatientVisitCountByDeptBySec(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientOpenVisitCountByAllDeptByAllSecByDate", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<VisitCountByDeptBySec> _resultModel = new List<VisitCountByDeptBySec>();
                    if (filter.HealthFacilityId > 0)
                    {
                        List<PatientVisitCountByDeptBySecByDate> lst = ds.Tables[0].ToList<PatientVisitCountByDeptBySecByDate>();

                        foreach (var item in lst)
                        {
                            VisitCountByDeptBySec model = new VisitCountByDeptBySec();
                            model.name = item.SectionName;
                            model.value = item.VisitCount;

                            _resultModel.Add(model);
                        }
                    }
                    else
                    {
                        List<PatientVisitCountByDeptBySecWithouthfIdByDate> lst = ds.Tables[0].ToList<PatientVisitCountByDeptBySecWithouthfIdByDate>();

                        foreach (var item in lst)
                        {
                            VisitCountByDeptBySec model = new VisitCountByDeptBySec();
                            model.name = item.SectionName;
                            model.value = item.VisitCount;

                            _resultModel.Add(model);
                        }

                    }

                    return _resultModel;



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

        //Reg Dashboard Tiles
        public async Task<PatientRegistrationDashboardCardCount> GetRegistrationDashboardCardCount(DashboardFilter filter)
        {


            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientRegistrationDashboardCardCount objResponse = new PatientRegistrationDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDashboardPatientRegistrationTokenTilesCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    List<PatientRegistrationDashboardCardCount> lst = ds.Tables[0].ToList<PatientRegistrationDashboardCardCount>();
                    objResponse = lst[0];



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
            return objResponse;

        }

        ////Reg Dashboard List
        //public async Task<ViewPagerDto<ViewPatientOpenVisitDetail>> GetRegistrationDashboardCardCount(DashboardFilter filter)
        //{

        //    List<string> user = new List<string>();
        //    if (filter.User != null)
        //        user = filter!.User!.Split(',').ToList();

        //    ViewPatientOpenVisitDetail objResponse = new ViewPatientOpenVisitDetail();

        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("SPDashboardPatientTokenList", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;
        //            sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.Date.ToString() : filter.StartDate.Value.Date.ToString());// DateTime.Now.Date.ToString());
        //            sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.Date.ToString() : filter.EndDate.Value.Date.ToString());

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
        //                sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
        //                sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
        //                sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
        //                sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
        //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
        //                sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
        //                sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

        //            if (!string.IsNullOrEmpty(filter.User))
        //                //sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
        //                sqlComm.Parameters.AddWithValue("@UserId", filter.User);

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));

        //            List<ViewPatientOpenVisitDetail> lst = ds.Tables[0].ToList<ViewPatientOpenVisitDetail>();
        //            objResponse = lst[0];
        //            var pagedList = await PagedListDto<ViewPatientOpenVisitDetail>.ToPagedListAsync(
        //          lst,
        //          filter.PageNumber,
        //          filter.PageSize
        //          );

        //            var responseObject = new ViewPagerDto<ViewPatientOpenVisitDetail>
        //            {
        //                TotalCount = pagedList.TotalCount,
        //                PageSize = pagedList.PageSize,
        //                CurrentPage = pagedList.CurrentPage,
        //                TotalPages = pagedList.TotalPages,
        //                HasNext = pagedList.HasNext,
        //                HasPrevious = pagedList.HasPrevious,
        //                List = pagedList
        //            };

        //            return responseObject;



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
        //    return objResponse;

        //}


        #endregion

        #region Doctor dashboard Login Stats
        public async Task<DoctorDashboardLoginUserAllCountsDTO> getDoctorDashboardLoginUserAllCounts(DashboardFilter filter)
        {
            DoctorDashboardLoginUserAllCountsDTO doctordashboards = new DoctorDashboardLoginUserAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorDashboardLoginUserAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DoctorDashboardLoginUserAllCountsDTO> lst = ds.Tables[0].ToList<DoctorDashboardLoginUserAllCountsDTO>();
                    return lst[0];
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
        #region Doctor Dashboard
        public async Task<DoctorDashboardPatientAllCountsDTO> getDoctorDashboardPatientAllCounts(DashboardFilter filter)
        {
            DoctorDashboardPatientAllCountsDTO doctordashboards = new DoctorDashboardPatientAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorDashboardPatientAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    doctordashboards.AllCounts = ds.Tables[0].ToList<DoctorDashboardPatientAllCounts>();
                    doctordashboards.ServedByDoctor = ds.Tables[1].ToList<PatientServedCountByDoctor>();

                    return doctordashboards;
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

        
        #endregion Doctor Dashboard Counts
        #region Doctor Dashboard HF
        public async Task<DoctorDashboardPatientAllCountsDTO> getDoctorDashboardHFPatientAllCounts(DashboardFilter filter)
        {
            DoctorDashboardPatientAllCountsDTO doctordashboardhf = new DoctorDashboardPatientAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorDashboardPatientAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    doctordashboardhf.AllCounts = ds.Tables[0].ToList<DoctorDashboardPatientAllCounts>();
                    doctordashboardhf.ServedByDoctor = ds.Tables[1].ToList<PatientServedCountByDoctor>();
                    doctordashboardhf.DiseaseWiseCount = ds.Tables[2].ToList<PatientCountDiseaseWise>();
                    return doctordashboardhf;
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
        public async Task<PatientDetailsWithPaginationdto> getDoctorDashboardPatientAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto DoctorDashboardList = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorDashboardPatientAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    DoctorDashboardList.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    DoctorDashboardList.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return DoctorDashboardList;

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
        public async Task<PatientDetailsWithPaginationdto> getDoctorDashboardHFPatientAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto DoctorHFDashboardList = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorDashboardPatientAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    DoctorHFDashboardList.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    DoctorHFDashboardList.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return DoctorHFDashboardList;

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
        #region Dashboard Doctor old
        public async Task<List<PatientVisitDoctorCountByUser>> GetPatientVisitDoctorCountByUser(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientDiagnoseDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))

                .GroupBy(x => new { x.PatientVisitCreatedByName, x.PatientDiagnoseCreatedBy, x.PatientVisitCreatedByDesignation }).
                Select(x => new PatientVisitDoctorCountByUser
                {
                    name = x.Key.PatientVisitCreatedByName + " (" + x.Key.PatientVisitCreatedByDesignation + ")",
                    extra = x.Key.PatientDiagnoseCreatedBy,
                    value = x.Count()
                }).OrderBy(x => x.name).ToListAsync();

            return list;
        }


        public async Task<List<PatientVisitDoctorCountByProvince>> GetPatientVisitDoctorCountByProvince(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientDiagnoseDetails
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))

                 .GroupBy(x => new { x.PatientProvinceName }).
                 Select(x => new PatientVisitDoctorCountByProvince
                 {
                     name = x.Key.PatientProvinceName,
                     value = x.Count()
                 }).OrderBy(x => x.name).ToListAsync();

            var provinceList = await _uowPatient.GetDbContext().Provinces
               .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
               .Select(x => new PatientVisitDoctorCountByProvince
               {
                   name = x.Name,
                   value = 0
               }).ToListAsync();

            foreach (var item in provinceList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                        item.value = item2.value;
                }
            }

            return provinceList.OrderBy(x => x.name).ToList();
        }

        public async Task<List<PatientVisitDoctorCountByGender>> GetPatientVisitDoctorCountByGender(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext()
                    .ViewPatientDiagnoseDetails
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientDiagnoseCreatedOn!.Value >= filter.StartDate!.Value)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientDiagnoseCreatedOn!.Value <= filter.EndDate!.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))
                  .GroupBy(x => new { x.Gender })
                  .Select(x => new PatientVisitDoctorCountByGender
                  {
                      name = x.Key.Gender,
                      value = x.Count()
                  })
                  .OrderBy(x => x.name)
                  .ToListAsync();

            var qryProfileList = (from _profiles in _uowPatient.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                  join _profileType in _uowPatient.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Gender) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                  select new PatientVisitDoctorCountByGender
                                  {
                                      name = _profiles.Name,
                                      value = 0
                                  });
            var genderList = await qryProfileList.ToListAsync().ConfigureAwait(false);

            foreach (var item in genderList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                        item.value = item2.value;
                }
            }

            return genderList.OrderBy(x => x.name).ToList();
        }

        public async Task<List<PatientVisitDoctorCountByDisease>> GetPatientVisitDoctorCountByDisease(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientVisitDoctorCountByDisease", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientVisitDoctorCountByDisease> lst = ds.Tables[0].ToList<PatientVisitDoctorCountByDisease>();
                    return lst.OrderByDescending(x => x.VisitCount).ToList();
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
        public async Task<List<PatientDetaildto>> getDocHFInQueuelist(DashboardFilter filter)
        {

            var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
            var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDocInQueuePatient", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                    {
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                        //sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    }


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<DoctorPrescribedMedicineAccumulateQuantity>> GetDoctorPrescribedMedicineAccumulateQuantity(DashboardFilter filter)
        {

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorPrescribedMedicineAccumulateQuantity", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DoctorPrescribedMedicineAccumulateQuantity> lst = ds.Tables[0].ToList<DoctorPrescribedMedicineAccumulateQuantity>();
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



        public async Task<InternalAndExternalLabCounts> GetDoctorRecommendedLabCount(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientCountByLabsInternalExternal", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                    {



                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    }



                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<InternalAndExternalLabCounts> lst = ds.Tables[0].ToList<InternalAndExternalLabCounts>();
                    return lst[0];
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

        // labs Counts Internal And External
        public async Task<InternalAndExternalLabCounts> getInternalAndExternalLabCounts(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientCountByLabsInternalExternal", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.User))
                    {
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    }


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<InternalAndExternalLabCounts> lst = ds.Tables[0].ToList<InternalAndExternalLabCounts>();
                    return lst[0];
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

        public async Task<List<DoctorDashboardCardCount>> GetDoctorDashboardCardCount(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorReferStationsCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DoctorDashboardCardCount> lst = ds.Tables[0].ToList<DoctorDashboardCardCount>();
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

        public async Task<DoctorDashboardHfCardsDTO> getDoctorDashboardHfPatientCount(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDoctorDashboardHfPatientCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DoctorDashboardHfCardsDTO> lst = ds.Tables[0].ToList<DoctorDashboardHfCardsDTO>();
                    return lst[0];
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
        //public async Task<List<ViewPatientQueDto>> GetAllpatientInQue(FilterPatientVisitDto filter)
        //{
        //    var _uowUser = new TransUnitOfWork<User>(_uowPatientDiagnose.GetDbContext());
        //    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();

        //    //Doctor Station 
        //    var _uowProfile = new TransUnitOfWork<DbModel.Profile>(_uowPatientDiagnose.GetDbContext());
        //    Guid? doctorStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

        //    var responseObj = _uowPatientOpenVisit.Repository.GetALL()
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.DepartementLookupId == dbUser.DepartmentId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId), x => x.SectionLookupId == dbUser.SectionId)
        //        .Include(x => x.Patient)
        //        .Where(x =>
        //                x.IsDischarge != true &&
        //                x.VisitDate == DateTime.Today &&
        //                x.HealthFacilityId == filter.HealthFacilityId &&
        //                x.CurrentStationProfileId == doctorStation &&
        //                (x.AttendedBy != null ? x.AttendedBy == _tokenService.GetUserId() : true)
        //            )
        //        .OrderBy(x => x.TokenNo)
        //        .Select(y =>
        //            new ViewPatientQueDto
        //            {
        //                PatientVisitId = y.PatientOpenVisitId,
        //                PatientId = y.PatientId,
        //                TokenNo = y.TokenNo,
        //                Mrno = y.Patient!.Mrno,
        //                CNIC = y.Patient.Cnic,
        //                MobileNo = y.Patient.MobileNo,
        //                FirstName = y.Patient.FirstName,
        //                LastName = y.Patient.LastName,
        //                FullName = y.Patient.FullName,
        //                CreatedBy = y.CreatedBy,
        //                CreatedOn = y.CreatedOn,
        //                UpdatedBy = y.UpdatedBy!,
        //                UpdatedOn = y.UpdatedOn,

        //            });
        //    //var pagedList = await PagedListDto<ViewPatientVisitsListWithDetailDto>.ToPagedListAsync(
        //    //      responseObj,
        //    //      filter.PageNumber,
        //    //      filter.PageSize
        //    //      );
        //    // var responseObject = new ViewPagerDto<ViewPatientVisitsListWithDetailDto>
        //    //{
        //    //    TotalCount = pagedList.TotalCount,
        //    //    PageSize = pagedList.PageSize,
        //    //    CurrentPage = pagedList.CurrentPage,
        //    //    TotalPages = pagedList.TotalPages,
        //    //    HasNext = pagedList.HasNext,
        //    //    HasPrevious = pagedList.HasPrevious,
        //    //    List = pagedList
        //    //};

        //    //return responseObject;

        //    return _mapper.Map<List<ViewPatientQueDto>>(responseObj);
        //}







        #endregion

        #region vital Dashboard Login User Counts 
        public async Task<VitalDashboardLoginUserAllCountsDTO> getVitalDashboardLoginUserAllCounts(DashboardFilter filter)
        {
            VitalDashboardLoginUserAllCountsDTO vitaluserDashboard = new VitalDashboardLoginUserAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPVitalDashboardLoginUserAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<VitalDashboardLoginUserAllCountsDTO> lst = ds.Tables[0].ToList<VitalDashboardLoginUserAllCountsDTO>();
                    return lst[0];
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
        #region vital Dashboard New 
        public async Task<VitalDashboardAllCountsDTO> getVitalDashboardAllCounts(DashboardFilter filter)
        {
            VitalDashboardAllCountsDTO doctorDashboard = new VitalDashboardAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPVitalDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    //if (!string.IsNullOrEmpty(filter.User))
                       // sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<VitalDashboardAllCountsDTO> lst = ds.Tables[0].ToList<VitalDashboardAllCountsDTO>();
                    return lst[0];
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

        public async Task<PatientDetailsWithPaginationdto> getVitalDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto VitalList = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPVitalDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    VitalList.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    VitalList.PatientList = ds.Tables[1].ToList<PatientDetailList>();
                    return VitalList;

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

        public async Task<ViewPatientVisitsListWithDetailDto> getPatientDetail(Guid PatientVisitId)
        {

            var responseObject = _uowPatientOpenVisit.GetDbContext().ViewPatientOpenVisiDashbaordLists.Where(c => c.PatientVisitId == PatientVisitId).Select(x =>
                new ViewPatientVisitsListWithDetailDto
                {
                    PatientVisitId = x.PatientVisitId,
                    PatientId = x.PatientId,
                    FullName = x.PatientName,
                    MobileNo = x.PatientMobileNo,
                    PatientProvinceId = x.PatientProvinceId,
                    Mrno = x.MrNo,
                    Cnic = x.Cnic,
                    VisitDate = x.VisitDate,
                    CreatedBy = x.CreatedBy!,
                    CreatedOn = x.CreatedOn,
                    UpdatedBy = x.UpdatedBy!,
                    UpdatedOn = x.UpdatedOn,
                    DepartmentId = x.DepartementLookupId,
                    Department = x.Department,
                    Section = x.Section,
                    SectionId = x.SectionLookupId,
                    //PatientDiagnoseId = x.PatientDiagnoseId,
                    //FormType = x.FormType,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    Age = x.Age,
                    Dob = x.Dob,
                    IsDischarge = x.IsDischarge,
                    IsVitalSkip = x.IsVitalSkip,
                    IsSscClaimed = x.IsSscClaimed,
                    SscNotConfirmReason = x.SscNotConfirmReason,
                    SscNumber = x.SscNumber,
                    IsEligibleForSsc = x.IsEligibleForSsc,
                    SscNotEligibleReason = x.SscNotEligibleReason,
                    HealthFacilityName = x.HealthFacilityName,
                    HealthFacilityId = x.HealthFacilityId,
                    TokenNo = x.TokenNo
                }).FirstOrDefault();
            if (responseObject == null)
                return null;
            return responseObject;

        }
        #region Vital Dashboard

        //public async Task<List<PatientVisitVitalCountByUser>> GetPatientVisitVitalCountByUser(DashboardFilter filter)
        //{

        //    List<string> user = new List<string>();
        //    if (filter.User != null)
        //        user = filter!.User!.Split(',').ToList();

        //    var list = await _uowPatient.GetDbContext().ViewPatientVitalDetails
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
        //        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

        //        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
        //        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
        //        .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))

        //        .GroupBy(x => new { x.PatientDiagnoseCreatedBy,x.PatientVisitCreatedByName, x.PatientVisitCreatedByDesignation }).
        //        Select(x => new PatientVisitVitalCountByUser
        //        {
        //            name = x.Key.PatientVisitCreatedByName +" ("+ x.Key.PatientVisitCreatedByDesignation +")",
        //            extra = x.Key.PatientDiagnoseCreatedBy,
        //            value = x.Count()
        //        }).OrderBy(x => x.name).ToListAsync();

        //    return list;
        //}

        public async Task<List<PatientVisitCountByUser>> GetPatientVisitVitalCountByUser(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetPatientVisitVitalCountByUserEntries", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientVisitCountByUser> lst = ds.Tables[0].ToList<PatientVisitCountByUser>();
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
        public async Task<List<PatientVisitVitalCountByProvince>> GetPatientVisitVitalCountByProvince(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientVitalDetails
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))

                 .GroupBy(x => new { x.PatientProvinceName, x.PatientProvinceId }).
                 Select(x => new PatientVisitVitalCountByProvince
                 {
                     name = x.Key.PatientProvinceName,
                     extra = new PatientDashboardExtra
                     {
                         patientProvinceId = x.Key.PatientProvinceId,
                     },
                     value = x.Count()
                 }).OrderBy(x => x.name).ToListAsync();

            var provinceList = await _uowPatient.GetDbContext().Provinces
              .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
              .Select(x => new PatientVisitVitalCountByProvince
              {
                  name = x.Name,
                  value = 0
              }).ToListAsync();

            foreach (var item in provinceList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                    {
                        item.value = item2.value;
                        item.extra = item2.extra;
                    }
                }
            }

            return provinceList.OrderBy(x => x.name).ToList();
        }

        public async Task<List<PatientVisitVitalCountByGender>> GetPatientVisitVitalCountByGender(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext()
                    .ViewPatientVitalDetails
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))
                  .GroupBy(x => new { x.Gender, x.GenderId })
                  .Select(x => new
                  {
                      name = x.Key.Gender,
                      genderid = x.Key.GenderId,
                      value = x.Count()
                  })
                  .OrderBy(x => x.name)
                  .ToListAsync();

            var qryProfileList = (from _profiles in _uowPatient.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                  join _profileType in _uowPatient.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Gender) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                  select new PatientVisitVitalCountByGender
                                  {
                                      name = _profiles.Name,
                                      extra = _profiles.ProfileId.ToString(),
                                      value = 0
                                  });
            var genderList = await qryProfileList.ToListAsync().ConfigureAwait(false);

            foreach (var item in genderList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                        item.value = item2.value;
                }
            }

            return genderList.OrderBy(x => x.name).ToList();

        }

        public async Task<List<PatientVisitCountByWeightRange>> GetPatientVisitVitalCountByWeightRange(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetPatientVisitCountByWeightRange", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientVisitCountByWeightRange> lst = ds.Tables[0].ToList<PatientVisitCountByWeightRange>();
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

        public async Task<List<PatientVisitVitalCountByRespiratoryRateRange>> GetPatientVisitVitalCountByRespiratoryRateRange(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var obj = await _uowPatient.GetDbContext().ViewPatientVitalDetails
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()!))
                 .GroupBy(x => 1)
                 .Select(x => new
                 {
                     lowvalue = x.Sum(x => Convert.ToInt32(x.ResperatoryRate) >= 12 && Convert.ToInt32(x.ResperatoryRate) <= 20 ? 1 : 0),
                     highvalue = x.Sum(x => Convert.ToInt32(x.ResperatoryRate) >= 21 && Convert.ToInt32(x.ResperatoryRate) <= 60 ? 1 : 0),//&& Convert.ToDecimal(x.ResperatoryRate) <= 20 ? 1: 0),

                 }).FirstOrDefaultAsync();

            List<PatientVisitVitalCountByRespiratoryRateRange> responseList = new List<PatientVisitVitalCountByRespiratoryRateRange>();
            responseList.Add(
                new PatientVisitVitalCountByRespiratoryRateRange
                {
                    name = "Normal (12-20 b/m)",
                    value = obj?.lowvalue ?? 0
                }
             );

            responseList.Add(
             new PatientVisitVitalCountByRespiratoryRateRange
             {
                 name = "High (21-60 b/m)",
                 value = obj?.highvalue ?? 0
             }
          );

            return responseList;
        }

        public async Task<List<PatientVisitVitalCountByTemperatureRange>> GetPatientVisitVitalCountByTemperatureRange(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var obj = await _uowPatient.GetDbContext().ViewPatientVitalDetails
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()))
                 .GroupBy(x => 1)
                 .Select(x => new
                 {
                     lowvalue = x.Sum(x => Convert.ToInt32(x.Temprature) >= 95 && Convert.ToInt32(x.Temprature) <= 99 ? 1 : 0),
                     highvalue = x.Sum(x => Convert.ToInt32(x.Temprature) >= 100 ? 1 : 0)

                 }).FirstOrDefaultAsync();

            List<PatientVisitVitalCountByTemperatureRange> responseList = new List<PatientVisitVitalCountByTemperatureRange>();
            responseList.Add(
                new PatientVisitVitalCountByTemperatureRange
                {
                    name = "Normal (95-99 F°)",
                    value = obj?.lowvalue ?? 0
                }
             );

            responseList.Add(
             new PatientVisitVitalCountByTemperatureRange
             {
                 name = "High (>= 100F°)",
                 value = obj?.highvalue ?? 0
             }
             );

            return responseList;
        }

        public async Task<List<PatientVisitVitalCountByPulseRateRange>> GetPatientVisitVitalCountByPulseRateRange(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var obj = await _uowPatient.GetDbContext().ViewPatientVitalDetails
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()))
                 .GroupBy(x => 1)
                 .Select(x => new
                 {
                     lowvalue = x.Sum(x => Convert.ToInt32(x.Pulse) < 60 ? 1 : 0),
                     normalvalue = x.Sum(x => Convert.ToInt32(x.Pulse) >= 60 && Convert.ToInt32(x.Pulse) <= 100 ? 1 : 0),
                     highvalue = x.Sum(x => Convert.ToInt32(x.Pulse) > 100 ? 1 : 0)

                 }).FirstOrDefaultAsync();

            List<PatientVisitVitalCountByPulseRateRange> responseList = new List<PatientVisitVitalCountByPulseRateRange>();

            responseList.Add(
            new PatientVisitVitalCountByPulseRateRange
            {
                name = "Low (< 60 p/m)",
                value = obj?.lowvalue ?? 0
            }
            );

            responseList.Add(
              new PatientVisitVitalCountByPulseRateRange
              {
                  name = "Normal (60-100 p/m)",
                  value = obj?.normalvalue ?? 0
              }
            );

            responseList.Add(
                new PatientVisitVitalCountByPulseRateRange
                {
                    name = "High (> 100 p/m)",
                    value = obj?.highvalue ?? 0
                }
             );

            return responseList;
        }

        public async Task<List<PatientVisitVitalCountByBloodPressureRange>> GetPatientVisitVitalCountByBloodPressureRange(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var obj = await _uowPatient.GetDbContext().ViewPatientVitalDetails
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientDiagnoseCreatedBy.ToString()))
                 .GroupBy(x => 1)
                 .Select(x => new
                 {
                     normalvalue = x.Sum(x => Convert.ToInt32(x.BpdiaSystolic) >= 60 && Convert.ToInt32(x.BpdiaSystolic) <= 90 && Convert.ToInt32(x.Bpsystolic) >= 80 && Convert.ToInt32(x.Bpsystolic) <= 120 ? 1 : 0),
                     lowvalue = x.Sum(x => Convert.ToInt32(x.Bpsystolic) < 90 ? 1 : 0),
                     highvalue = x.Sum(x => Convert.ToInt32(x.Bpsystolic) >= 140 ? 1 : 0)

                 }).FirstOrDefaultAsync();

            List<PatientVisitVitalCountByBloodPressureRange> responseList = new List<PatientVisitVitalCountByBloodPressureRange>();

            responseList.Add(
                new PatientVisitVitalCountByBloodPressureRange
                {
                    name = "Low (< 90/60 mmhg)",
                    value = obj?.lowvalue ?? 0
                }
            );

            responseList.Add(
                new PatientVisitVitalCountByBloodPressureRange
                {
                    name = "Normal (90/60-120/80 mmhg)",
                    value = obj?.normalvalue ?? 0
                }
            );

            responseList.Add(
                new PatientVisitVitalCountByBloodPressureRange
                {
                    name = "High (>= 140/90 mmhg)",
                    value = obj?.highvalue ?? 0
                }
            );
            return responseList;
        }

        public async Task<VitalDashboardCardCount> GetVitalDashboardCardCount(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            VitalDashboardCardCount objResponse = new VitalDashboardCardCount();
            //VitalDashboardCardCount DocResponse = new VitalDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDashboardPatientVital", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<VitalDashboardCardCount> lst = ds.Tables[0].ToList<VitalDashboardCardCount>();
                    objResponse = lst[0];
                    //var list = await _patientDiagnoseService.GetAllQue(filter.HealthFacilityId);
                    return objResponse;

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

        #region Pharmacy Dashboard Login Stats
        public async Task<PharmacyDashboardLoginUserAllCountsDTO> getPharmacyDashboardLoginUserAllCounts(DashboardFilter filter)
        {
            PharmacyDashboardLoginUserAllCountsDTO doctorDashboard = new PharmacyDashboardLoginUserAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyDashboardLoginUserAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyDashboardLoginUserAllCountsDTO> lst = ds.Tables[0].ToList<PharmacyDashboardLoginUserAllCountsDTO>();
                    return lst[0];
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
        #region Pharmacy Dashboard new
        public async Task<PharmacyDashboardPatientAllCountsDTO> getPharmacyDashboardAllCounts(DashboardFilter filter)
        {
            PharmacyDashboardPatientAllCountsDTO doctorDashboard = new PharmacyDashboardPatientAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyDashboardPatientAllCountsDTO> lst = ds.Tables[0].ToList<PharmacyDashboardPatientAllCountsDTO>();
                    return lst[0];
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

        public async Task<PatientDetailsWithPaginationdto> getPharmacyDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto PharmacyList = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    PharmacyList.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    PharmacyList.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return PharmacyList;

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

        public async Task<List<PharmacyDashboardMedcineIssuedReport>> GetPharmacyDashboardMedcineIssuedReport(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyDashboardMedcineIssuedReport", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //if (filter.isPaginate)
                    //{
                    //    sqlComm.Parameters.AddWithValue("@TakeRecords", 10);
                    //}
                    //else
                    //{
                    //    sqlComm.Parameters.AddWithValue("@TakeRecords", 0);
                    //}


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyDashboardMedcineIssuedReport> lst = ds.Tables[0].ToList<PharmacyDashboardMedcineIssuedReport>();

                    return lst;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<List<PharmacyDashboardPatientDetailDTO>> getPharmacyDashboardMedcineIssuedPatientDetailList(MedicineIssuedFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyDashboardMedcineIssuedPatientDetailReport", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //if (filter.isPaginate)
                    //{
                    //    sqlComm.Parameters.AddWithValue("@TakeRecords", 10);
                    //}
                    //else
                    //{
                    //    sqlComm.Parameters.AddWithValue("@TakeRecords", 0);
                    //}


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.MedicineId))
                        sqlComm.Parameters.AddWithValue("@MedicineId", filter.MedicineId);
                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyDashboardPatientDetailDTO> lst = ds.Tables[0].ToList<PharmacyDashboardPatientDetailDTO>();

                    return lst;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<List<MedicineStockOfflineDTO>> getPharmacyDashboardOflineMedicineStock(MedicineIssuedFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    var MimsMedicine = await _uowMimsMedicineDatum.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .Select(x => new MedicineStockOfflineDTO
                    {
                        MedicineName = x.MedicineName,
                        MedicineTypeName = x.MedicineTypeName,
                        WardName = x.WardName,
                        AvailableQuantity = x.AvailableQuantity,
                        TotalQuantity = x.TotalQuantity,
                        UnitPrice = x.UnitPrice,
                        isOffline = _isActiveOffline

                    }).ToListAsync();
                    return MimsMedicine;
                }
                catch (Exception ex)
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
        #region Pharmacy Dashboard old
        public async Task<List<PatientVisitPharmacyCountByUser>> GetPatientVisitPharmacyCountByUser(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientPharmacyDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.MedicineDispatchedCreatedBy.ToString()!))

                .GroupBy(x => new { x.MedicineDispatchedCreatedBy, x.PatientVisitCreatedByName }).
                Select(x => new PatientVisitPharmacyCountByUser
                {
                    name = x.Key.PatientVisitCreatedByName,
                    extra = x.Key.MedicineDispatchedCreatedBy,
                    value = x.Select(x => x.PatientId).Distinct().Count()
                }).OrderBy(x => x.name).ToListAsync();

            return list;
        }




        public async Task<List<PharmacyInternalExternalMedicineAccumulateQuantity>> GetPharmacyInternalExternalMedicineAccumulateQuantity(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyInternalExternalAccumulateQuantity", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    if (filter.isPaginate)
                    {
                        sqlComm.Parameters.AddWithValue("@TakeRecords", 10);
                    }
                    else
                    {
                        sqlComm.Parameters.AddWithValue("@TakeRecords", 0);
                    }


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyInternalExternalMedicineAccumulateQuantity> lst = ds.Tables[0].ToList<PharmacyInternalExternalMedicineAccumulateQuantity>();

                    return lst;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        #region Get Patient Wise Medicine Report
        public async Task<List<PharmacyMedicinePatientWiseReport>> getPharmacyInternalExternalAccumulateQuantityPatientWiseReport(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyInternalExternalAccumulateQuantityPatientWiseReport", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.Date.ToString() : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.Date.ToString() : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    if (filter.isPaginate)
                    {
                        sqlComm.Parameters.AddWithValue("@TakeRecords", 10);
                    }
                    else
                    {
                        sqlComm.Parameters.AddWithValue("@TakeRecords", 0);
                    }


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyMedicinePatientWiseReport> lst = ds.Tables[0].ToList<PharmacyMedicinePatientWiseReport>();

                    return lst;
                }
                catch (Exception ex)
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
        //public async Task<PharmacyDashboardCardCount> GetPharmacyDashboardCardCount(DashboardFilter filter)
        //{

        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("SPPharmacyInternalExternalAccumulateQuantity", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;
        //            sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.Date.ToString() : filter.StartDate.Value.Date.ToString());// DateTime.Now.Date.ToString());
        //            sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.Date.ToString() : filter.EndDate.Value.Date.ToString());


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
        //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
        //                sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
        //                sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

        //            if (!string.IsNullOrEmpty(filter.User))
        //                sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<PharmacyInternalExternalMedicineAccumulateQuantity> lst = ds.Tables[0].ToList<PharmacyInternalExternalMedicineAccumulateQuantity>();


        //            PharmacyDashboardCardCount responseObj = new PharmacyDashboardCardCount();

        //            if (lst.Count() > 0)
        //            {
        //                responseObj.InternalMedicineCount = lst.Sum(x => x.InternalMedicineQuantity).Value;
        //                responseObj.ExternalMedicineCount = lst.Sum(x => x.ExternalMedicineQuantity).Value;

        //            }


        //            List<string> user = new List<string>();
        //            if (filter.User != null)
        //                user = filter!.User!.Split(',').ToList();

        //            var list = await _uowPatient.GetDbContext().ViewPatientPharmacyDetails
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
        //                 .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
        //                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
        //                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value.Date <= filter.EndDate!.Value.Date)
        //                 .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.MedicineDispatchedCreatedBy.ToString()!))

        //                 .GroupBy(x => 1).
        //                 Select(x => new PharmacyDashboardCardCount
        //                 {
        //                     PatientsCount = x.Select(x => x.PatientId).Distinct().Count()
        //                 }).FirstOrDefaultAsync();

        //            responseObj.PatientsCount = list != null ? list.PatientsCount : 0;







        //            return responseObj;
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

        // Get Pharmacy Dashboard
        public async Task<PharmacyCardsCount> GetPharmacyDashboardCardCount(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPharmacyPatientCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PharmacyCardsCount> lst = ds.Tables[0].ToList<PharmacyCardsCount>();
                    return lst[0];
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
        public async Task<PharmacyDashboardInternalExternalStats> GetPharmacyDashboardInternalExternalStats(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            //Internal Medicine Stats
            var list1 = await _uowPatient.GetDbContext().ViewPatientPharmacyDetails.Where(x => x.MedicineId > 0)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                        .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.MedicineDispatchedCreatedBy.ToString()!))

                        .GroupBy(x => 1)
                        .Select(x => new
                        {
                            MedicineCount = x.Select(x => x.MedicineId).Distinct().Count(),
                            MedicintQuantity = x.Sum(x => x.QuantityDispatch)
                        }).FirstOrDefaultAsync();

            //External Medicine Stats
            var list2 = await _uowPatient.GetDbContext().ViewPatientPharmacyDetails.Where(x => x.MedicineId == 0 || x.QuantityPrescribed > x.QuantityDispatch)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.MedicineDispatchedCreatedBy.ToString()!))

                    .GroupBy(x => 1)
                    .Select(x => new
                    {
                        MedicineCount = x.Select(x => x.MedicineId).Distinct().Count(),
                        MedicintQuantity = x.Sum(x => x.QuantityPrescribed - x.QuantityDispatch)
                    }).FirstOrDefaultAsync();

            //Internal Medicine Patient COunt
            var list3 = await _uowPatient.GetDbContext().ViewPatientPharmacyDetails.Where(x => x.MedicineId > 0)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                        .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.MedicineDispatchedCreatedBy.ToString()!))

                        .GroupBy(x => 1)
                        .Select(x => new
                        {
                            PatientsCount = x.Select(x => x.PatientId).Distinct().Count()
                        }).FirstOrDefaultAsync();

            //External Medicine Patient COunt
            var list4 = await _uowPatient.GetDbContext().ViewPatientPharmacyDetails.Where(x => x.MedicineId == 0 || x.QuantityPrescribed > x.QuantityDispatch)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientVisitCreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientVisitCreatedOn!.Value <= filter.EndDate!.Value)
                        .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.MedicineDispatchedCreatedBy.ToString()!))

                         .GroupBy(x => 1)
                        .Select(x => new
                        {
                            PatientsCount = x.Select(x => x.PatientId).Distinct().Count()
                        }).FirstOrDefaultAsync();

            PharmacyDashboardInternalExternalStats responseObj = new PharmacyDashboardInternalExternalStats();

            responseObj.InternalMedicineCount = list1 != null ? list1.MedicineCount : 0;
            responseObj.InternalMedicineQuantity = list1 != null ? list1.MedicintQuantity : 0;
            responseObj.InternalMedicinePatientsCount = list3 != null ? list3.PatientsCount : 0;

            responseObj.ExternalMedicineCount = list2 != null ? list2.MedicineCount : 0;
            responseObj.ExternalMedicineQuantity = list2 != null ? list2.MedicintQuantity : 0;
            responseObj.ExternalMedicinePatientsCount = list4 != null ? list4.PatientsCount : 0;

            return responseObj;
        }


        #endregion

        #region Pathology Login user Counts
        public async Task<LabDashboardLoginUserTestAllCountsDTO> getLabDashboardLoginUserTestAllCounts(DashboardFilter filter)
        {
            LabDashboardLoginUserTestAllCountsDTO labuserDashboard = new LabDashboardLoginUserTestAllCountsDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardLoginUserTestAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<LabDashboardLoginUserTestAllCountsDTO> lst = ds.Tables[0].ToList<LabDashboardLoginUserTestAllCountsDTO>();
                    return lst[0];

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
        #region Pathology Dashboard All Counts
        public async Task<LabDashboardTestViewModelDTO> getLabDashboardTestAllCounts(DashboardFilter filter)
        {
            LabDashboardTestViewModelDTO labDashboard = new LabDashboardTestViewModelDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardTestAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    labDashboard.AllCounts = ds.Tables[0].ToList<LabDashboardTestAllCountsDTO>();
                    labDashboard.SampleCollected = ds.Tables[1].ToList<SampleCollected>();
                    return labDashboard;

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
        public async Task<LabPatientDetailsWithPaginationdto> getLabDashboardTestAllList(DashboardFilter filter)
        {
            LabPatientDetailsWithPaginationdto PathologyList = new LabPatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardTestAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    PathologyList.TotalRecord = ds.Tables[0].ToList<LabPatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    PathologyList.PatientList = ds.Tables[1].ToList<LabPatientDetail>();

                    return PathologyList;

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
        public async Task<LabDashboardFromStartTillNowAllCountsDTO> getLabDashboardTestTimeFromStartTillNowAllCounts(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardTestTimeFromStartTillNowAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<LabDashboardFromStartTillNowAllCountsDTO> lst = ds.Tables[0].ToList<LabDashboardFromStartTillNowAllCountsDTO>();
                    return lst[0];
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
        public async Task<LabPatientDetailsWithPaginationdto> getLabDashboardTestTimeFromStartTillNowAllList(DashboardFilter filter)
        {
            LabPatientDetailsWithPaginationdto PathologyfromstartList = new LabPatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardTestTimeFromStartTillNowAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    PathologyfromstartList.TotalRecord = ds.Tables[0].ToList<LabPatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    PathologyfromstartList.PatientList = ds.Tables[1].ToList<LabPatientDetail>();

                    return PathologyfromstartList;

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

        public async Task<List<InternalExternalCountByLabTestDTO>> getLabDashboardTop20LabTestRecommended(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardTop20LabTestRecommended", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<InternalExternalCountByLabTestDTO> lst = ds.Tables[0].ToList<InternalExternalCountByLabTestDTO>();
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
        #region Pathology
        public async Task<List<PatientVisitLabTestSampleCollectedCountByUser>> GetPatientVisitPathologySampleCollectedCountByUser(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                .Where(x => x.IsSampleCollected == true)
                .GroupBy(x => new { x.SampleCollectedBy, x.SampleCollectedByName }).
                Select(x => new PatientVisitLabTestSampleCollectedCountByUser
                {
                    name = x.Key.SampleCollectedByName,
                    extra = x.Key.SampleCollectedBy,
                    value = x.Count()
                }).OrderBy(x => x.name).ToListAsync();


            return list;
        }
        public async Task<List<PatientVisitLabTestReportGeneratedCountByUser>> GetPatientVisitLabTestReportGeneratedCountByUser(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                .Where(x => x.IsReportGenerated == true)
                .GroupBy(x => new { x.ReportGeneratedBy, x.ReportGeneratedByName }).
                Select(x => new PatientVisitLabTestReportGeneratedCountByUser
                {
                    name = x.Key.ReportGeneratedByName,
                    extra = x.Key.ReportGeneratedBy,
                    value = x.Count()
                }).OrderBy(x => x.name).ToListAsync();


            return list;
        }
        public async Task<List<PatientVisitPathologyCountByGender>> GetPatientVisitPathologyCountByGender(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext()
                    .ViewPatientLabTestDetails
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                    .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                  //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.ReportGeneratedBy.ToString()!))
                  .GroupBy(x => new { x.Gender })
                  .Select(x => new PatientVisitPathologyCountByGender
                  {
                      name = x.Key.Gender,
                      value = x.Select(x => x.PatientId).Distinct().Count(),
                  })
                  .OrderBy(x => x.name)
                  .ToListAsync();

            var qryProfileList = (from _profiles in _uowPatient.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                  join _profileType in _uowPatient.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Gender) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                  select new PatientVisitPathologyCountByGender
                                  {
                                      name = _profiles.Name,
                                      value = 0
                                  });
            var genderList = await qryProfileList.ToListAsync().ConfigureAwait(false);

            foreach (var item in genderList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                        item.value = item2.value;
                }
            }

            return genderList.OrderBy(x => x.name).ToList();

        }
        public async Task<PathologyDashboardStats> GetPathologyDashboardStats(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var _hmisstartdate = await _uowPatient.GetDbContext().ViewPatientOpenVisits.FirstOrDefaultAsync();
            filter.StartDate = _hmisstartdate.VisitDate;

            var responseObj = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                        //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.ReportGeneratedBy.ToString()!))

                        .GroupBy(x => 1)
                        .Select(x => new PathologyDashboardStats
                        {
                            LabTestCount = x.Select(x => x.LabTestId).Count(),
                            PatientsCount = x.Select(x => x.PatientId).Distinct().Count(),
                            ResultPedningMoreThan3Days = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated != true && DateTime.Now.Date.AddDays(-3) >= x.CreatedOn.Value ? 1 : 0),
                            ResultPedningMoreThan7Days = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated != true && DateTime.Now.Date.AddDays(-7) >= x.CreatedOn.Value ? 1 : 0),
                            ResultPedningMoreThan15Days = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated != true && DateTime.Now.Date.AddDays(-15) >= x.CreatedOn.Value ? 1 : 0),
                            ResultPedningMoreThan30Days = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated != true && DateTime.Now.Date.AddDays(-15) >= x.CreatedOn.Value ? 1 : 0)
                        }).FirstOrDefaultAsync();



            return responseObj;
        }

        public async Task<PathologyCardsCount> getPathologyDashboardCardCount(DashboardFilter filter)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabDashboardCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PathologyCardsCount> lst = ds.Tables[0].ToList<PathologyCardsCount>();
                    return lst[0];
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
        public async Task<List<LabTestCountVsPatientCount>> GetTop20RecommendedLabTestCount(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                        .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientLabTestCreatedBy.ToString()!))

                        .GroupBy(x => new { x.LabTestName, x.LabType })
                        .Select(x => new LabTestCountVsPatientCount
                        {
                            LabTestName = x.Key.LabTestName,
                            LabTestCount = x.Select(x => x.PatientId).Distinct().Count(),
                            LabType = x.Key.LabType,
                        }).OrderByDescending(x => x.LabTestCount).Take(20).ToListAsync();



            return list;
        }
        public async Task<List<PatientVisitPathologyCountByLabTest>> GetPatientVisitPathologyCountByLabTest(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                        //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.ReportGeneratedBy.ToString()!))

                        .GroupBy(x => new { x.LabTestName })
                        .Select(x => new PatientVisitPathologyCountByLabTest
                        {
                            name = x.Key.LabTestName,
                            value = x.Select(x => x.PatientId).Distinct().Count()
                        }).OrderBy(x => x.name).ToListAsync();

            var qryLabTests = (from _tests in _uowPatient.GetDbContext().LabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)

                               select new PatientVisitPathologyCountByLabTest
                               {
                                   name = _tests.Name,
                                   value = 0
                               });

            var labTestList = await qryLabTests.ToListAsync().ConfigureAwait(false);

            foreach (var item in labTestList)
            {
                foreach (var item2 in list)
                {
                    if (item.name == item2.name)
                        item.value = item2.value;
                }
            }

            return labTestList;
        }
        //public async Task<List<PatientVisitPathologyCountByDepartment>> GetPatientVisitPathologyCountByDepartment(DashboardFilter filter)
        //{
        //    List<string> user = new List<string>();
        //    if (filter.User != null)
        //        user = filter!.User!.Split(',').ToList();

        //    var list = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
        //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
        //                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
        //                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
        //                //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.ReportGeneratedBy.ToString()!))

        //                .GroupBy(x => new { x.DepartementLookupId, x.Department })
        //                .Select(x => new PatientVisitPathologyCountByDepartment
        //                {
        //                    name = x.Key.Department,
        //                    value = x.Count()
        //                }).OrderBy(x => x.name).ToListAsync();

        //    var qryDepartments = (from _HfDepartments in _uowPatient.GetDbContext().HfDepartments
        //                          .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
        //                          .Include(x => x.DepartmentLookup)
        //                          .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
        //                          select new PatientVisitPathologyCountByDepartment
        //                          {
        //                              name = _HfDepartments.DepartmentLookup.Name,
        //                              value = 0
        //                          });

        //    var departmentList = await qryDepartments.ToListAsync().ConfigureAwait(false);

        //    foreach (var item in departmentList)
        //    {
        //        foreach (var item2 in list)
        //        {
        //            if (item.name == item2.name)
        //                item.value = item2.value;
        //        }
        //    }

        //    return departmentList;
        //}
        public async Task<PathologyCountByStatus> GetPathologyCountByStatus(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var obj = await _uowPatient.GetDbContext().ViewPatientLabTestDetails
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacilityProvinceId == filter.ProvinceId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacilityDivisionId == filter.DivisionId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacilityDistrictId == filter.DistrictId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacilityTehsilId == filter.TehsilId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                        .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
                        //.WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.ReportGeneratedBy.ToString()!))

                        .GroupBy(x => 1)
                        .Select(x => new PathologyCountByStatus
                        {
                            SampleNotCollected = x.Sum(x => x.IsSampleCollected != true && x.IsReportGenerated != true ? 1 : 0),
                            ResultAwaited = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated != true ? 1 : 0),
                            //ResultAwaited = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated != true ? 1 : 0),
                            ReportGenerated = x.Sum(x => x.IsSampleCollected == true && x.IsReportGenerated == true ? 1 : 0),
                        }).FirstOrDefaultAsync();

            PathologyCountByStatus responseObj = new PathologyCountByStatus();
            if (obj != null)
                responseObj = obj;

            return responseObj;
        }

        #endregion

        #region GetSecretaryDashboardCardCount
        //public async Task<SecretaryDashboardCardCount> GetSecretaryDashboardCardCount(DashboardFilter filter)
        //{

        //    List<string> user = new List<string>();
        //    if (filter.User != null)
        //        user = filter!.User!.Split(',').ToList();

        //    SecretaryDashboardCardCount objResponse = new SecretaryDashboardCardCount();

        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("SPDashboardPatientVisitFlow", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;
        //            sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.Date.ToString() : filter.StartDate.Value.Date.ToString());// DateTime.Now.Date.ToString());
        //            sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.Date.ToString() : filter.EndDate.Value.Date.ToString());
        //            //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
        //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
        //                sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
        //                sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


        //            //if (!string.IsNullOrEmpty(filter.User))
        //            //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<SecretaryDashboardCardCount> lst = ds.Tables[0].ToList<SecretaryDashboardCardCount>();
        //            objResponse = lst[0];


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
        //    return objResponse;

        //}

        #endregion



        #region TbDashboard


        public async Task<ViewPagerDto<DbModel.ViewTbRegisteredPatient>> GetTbPatientsRegistered(TbDashboardFilter filter)
        {
            var finalList = _uowPatient.GetDbContext().ViewTbRegisteredPatients
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewTbRegisteredPatient>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewTbRegisteredPatient>
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
        public async Task<ViewPagerDto<DbModel.ViewTbPatientCount>> GetTbPatientsDiagnosed(TbDashboardFilter filter)
        {
            var finalList = _uowPatient.GetDbContext().ViewTbPatientCounts
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value.Date)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewTbPatientCount>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewTbPatientCount>
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



        public async Task<ViewPagerDto<DbModel.ViewTbPatientCount>> GetTbPatientsConfirmed(TbDashboardFilter filter)
        {
            var finalList = _uowPatient.GetDbContext().ViewTbPatientCounts
              .Where(x => x.IsConfirmed == true)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewTbPatientCount>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewTbPatientCount>
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




        public async Task<ViewPagerDto<DbModel.ViewTbPatientCount>> GetTbPatientsPresumptive(TbDashboardFilter filter)
        {
            var finalList = _uowPatient.GetDbContext().ViewTbPatientCounts
              .Where(x => x.IsConfirmed == false)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewTbPatientCount>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewTbPatientCount>
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

        public async Task<ViewPagerDto<DbModel.ViewTbissuedMedicine>> GetTbIssuedMedicine(TbDashboardFilter filter)
        {
            var finalList = _uowPatient.GetDbContext().ViewTbissuedMedicines
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewTbissuedMedicine>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewTbissuedMedicine>
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

        public async Task<ViewPagerDto<SPTbMedicineDeliveryDataResponseDto>> GetTbMedicineToBeDelivered(TbDashboardMedicineDeliveryFilter filter)
        {
            SPTbMedicineDeliveryDataResponseDto responseSP = new SPTbMedicineDeliveryDataResponseDto();
            using (var db = new HmisAuthContext())
            {
                List<SPTbMedicineDeliveryDataResponseDto> lst = new List<SPTbMedicineDeliveryDataResponseDto>();
                var responseObject = new ViewPagerDto<SPTbMedicineDeliveryDataResponseDto>();
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    //var _uowUser = new UnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetTbMedicineToBeDelivered]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                    //    sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.CNIC))
                        sqlComm.Parameters.AddWithValue("@CNIC", filter.CNIC);

                    //if (!string.IsNullOrEmpty(filter.MrNo))
                    //    sqlComm.Parameters.AddWithValue("@MrNo", filter.MrNo);

                    if (!string.IsNullOrEmpty(filter.MobileNo))
                        sqlComm.Parameters.AddWithValue("@MobileNo", filter.MobileNo);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ListType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                        sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                        sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    var count = ds.Tables[0].ToList<SPTbMedicineDeliveryDataResponseTotalDto>();
                    lst = ds.Tables[1].ToList<SPTbMedicineDeliveryDataResponseDto>();

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
        }

        public async Task<SPTbMedicineDeliveryDataResponseDto> GetTbMedicineToBeDeliveredById(int? PatientId)
        {
            //PatientId = 491;
            //SPTbMedicineDeliveryDataResponseDto responseSP = new SPTbMedicineDeliveryDataResponseDto();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetTbMedicineToBeDeliveredById]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(PatientId))
                        sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SPTbMedicineDeliveryDataResponseDto> lst = ds.Tables[0].ToList<SPTbMedicineDeliveryDataResponseDto>();

                    var patientData = lst[0];

                    patientData.IsMedicine = _ShowTbMedicineOnSlip;

                    return patientData;
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

        public async Task<SPTbMedicineDeliveryDataResponseDto> TbMedicineDeliveryStatusUpdate(MedicineDeliveryStatusUpdateDto input)
        {
            //input.Id = 489;
            //SPTbMedicineDeliveryDataResponseDto responseSP = new SPTbMedicineDeliveryDataResponseDto();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                   
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[SPTbMedicineDeliveryStatusUpdate]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(input.Id))
                        sqlComm.Parameters.AddWithValue("@PatientId", input.Id);

                    if (!AppCommonMethod.IsNullorZeroInt(input.DeliveryStatus))
                        sqlComm.Parameters.AddWithValue("@DeliveryStatus", input.DeliveryStatus);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SPTbMedicineDeliveryDataResponseDto> lst = ds.Tables[0].ToList<SPTbMedicineDeliveryDataResponseDto>();

                    var patientData = lst[0];

                    patientData.IsMedicine = _ShowTbMedicineOnSlip;

                    if(_SendMsgForTbMedicine && input.DeliveryStatus == (int)TbMedicineDeliveryStatusEnum.Dispatch)
                        await SendSms(patientData);

                    return patientData;
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

        public async Task<List<TbDashboardDTO>> GetTbMedicineDeliveryDashboardCardCount(DashboardFilter filter)
        {
            List<TbDashboardDTO> objResponse = new List<TbDashboardDTO>();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPTbMedicineDeliveryDashboardCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<TbDashboardDTO> list = ds.Tables[0].ToList<TbDashboardDTO>();

                    objResponse = list.OrderBy(x => x.Index).ToList();

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
            return objResponse;

        }


        public async Task<ViewPagerDto<DbModel.ViewAdviseLabTest>> GetTbAdvisedTest(TbDashboardFilter filter)
        {
            var finalList = _uowPatient.GetDbContext().ViewAdviseLabTests
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMTestAdvised), x => x.TestName == CommonStringConstant.SSM)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRTestAdvised), x => x.TestName == CommonStringConstant.CXR)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.GeneXpertTestAdvised), x => x.TestName == CommonStringConstant.XPert)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVTestAdvised), x => x.TestName == CommonStringConstant.HIVScreening)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMPending), x => x.TestName == CommonStringConstant.SSM && x.IsReportGenerated == null)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRPending), x => x.TestName == CommonStringConstant.CXR && x.IsReportGenerated == null)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.XpertPending), x => x.TestName == CommonStringConstant.XPert && x.IsReportGenerated == null)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVPending), x => x.TestName == CommonStringConstant.HIVScreening && x.IsReportGenerated == null)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewAdviseLabTest>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewAdviseLabTest>
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

        public async Task<ViewPagerDto<DbModel.ViewLabTestResult>> GetTbTestResults(TbDashboardFilter filter)
        {

            var finalList = _uowPatient.GetDbContext().ViewLabTestResults
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMPositive), x => x.TestName == CommonStringConstant.SSM && x.TestResultName == CommonStringConstant.Result && x.Result == CommonStringConstant.PositiveForAFB)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMNegative), x => x.TestName == CommonStringConstant.SSM && x.TestResultName == CommonStringConstant.Result && x.Result == CommonStringConstant.NegativeForAFB)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRPositive), x => x.TestName == CommonStringConstant.CXR && x.TestResultName == CommonStringConstant.SuggestedForTB && x.Result == CommonStringConstant.Yes)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRNegative), x => x.TestName == CommonStringConstant.CXR && x.TestResultName == CommonStringConstant.SuggestedForTB && x.Result == CommonStringConstant.No)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.XpertPositive), x => x.TestName == CommonStringConstant.XPert && x.TestResultName == CommonStringConstant.Result && x.Result == CommonStringConstant.MTBPositive)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.XpertNegative), x => x.TestName == CommonStringConstant.XPert && x.TestResultName == CommonStringConstant.Result && x.Result == CommonStringConstant.MTBNegative)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.ResistanceDetected), x => x.TestName == CommonStringConstant.XPert && x.TestResultName == CommonStringConstant.Result && x.Result == CommonStringConstant.MTBPositive)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.RifampicinResistanceDetected), x => x.TestName == CommonStringConstant.XPert && x.TestResultName == CommonStringConstant.RifampicinResistant && x.Result == CommonStringConstant.MTBDetected)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.RifampicinResistanceNotDetected), x => x.TestName == CommonStringConstant.XPert && x.TestResultName == CommonStringConstant.RifampicinResistant && x.Result == CommonStringConstant.MTBNotDetected)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.Error), x => x.TestName == CommonStringConstant.XPert && x.TestResultName == CommonStringConstant.Result && x.Result == CommonStringConstant.Error)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVReactive), x => x.TestName == CommonStringConstant.HIVScreening && x.TestResultName == CommonStringConstant.HIVResult && x.Result == CommonStringConstant.Reactive)
              .WhereIf(filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVNonReactive), x => x.TestName == CommonStringConstant.HIVScreening && x.TestResultName == CommonStringConstant.HIVResult && x.Result == CommonStringConstant.NonReactive)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
              .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
              .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value <= filter.EndDate!.Value)
              .OrderByDescending(x => x.CreatedOn);




            var pagedList = await PagedListDto<DbModel.ViewLabTestResult>.ToPagedListAsync(
             finalList,
             filter.PageNumber,
             filter.PageSize
             );


            var responseObject = new ViewPagerDto<DbModel.ViewLabTestResult>
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



        public async Task<ViewPagerDto<DbModel.ViewTbPatientCount>> GetTbPatientsData(TbDashboardFilter filter)
        {
            if (filter.TbPatientTypeContstant == CommonStringConstant.TotalDiagnose)
            {
                var responseObject = await GetTbPatientsDiagnosed(filter);

                return responseObject;
            }
            else if (filter.TbPatientTypeContstant == CommonStringConstant.TotalConfirmedPatients)
            {
                var responseObject = await GetTbPatientsConfirmed(filter);

                return responseObject;
            }
            else if (filter.TbPatientTypeContstant == CommonStringConstant.TotalNotConfirmed)
            {
                var responseObject = await GetTbPatientsPresumptive(filter);

                return responseObject;
            }
            return null;

        }

        public async Task<List<TbDashboardDTO>> GetTbDashboardCardCount(DashboardFilter filter)
        {
            List<TbDashboardDTO> objResponse = new List<TbDashboardDTO>();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPTbDashboardCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<TbDashboardDTO> list = ds.Tables[0].ToList<TbDashboardDTO>();

                    objResponse = list.OrderBy(x => x.Index).ToList();

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
            return objResponse;

        }


        #endregion


        #region OldTBDashboard

        public async Task<List<TbDashboardDTO>> GetOldTbDashboardCardCount(DashboardFilter filter)
        {

            List<TbDashboardDTO> objResponse = new List<TbDashboardDTO>();

            using (var context = new HmisAuthContext())
            {
                //var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                SqlConnection dbConnection = (SqlConnection)context.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SP_HMISTbDashboardCounts", dbConnection);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);


                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);


                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    List<TbDashboardDTO> list = ds.Tables[0].ToList<TbDashboardDTO>();
                    objResponse = list.OrderBy(x => x.Index).ToList();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return objResponse;

        }

        //public async Task<ViewPagerDto<ViewPatienListOldTbDto>> GetPatientListOldTb(TbDashboardFilter filter)
        //{
        //    var finalList = new HmisAuthContext().ViewPatientListDashboards
        //               .WhereIf(!string.IsNullOrEmpty(filter.HealthFacilityCode), x => x.HealthFacilityCode == filter.HealthFacilityCode)
        //               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientCreationDate!.Value.Date >= filter.StartDate!.Value.Date)
        //               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientCreationDate!.Value.Date <= filter.EndDate!.Value.Date)
        //               .Select(x => new ViewPatienListOldTbDto
        //               {
        //                   PatientId = x.PatientId,
        //                   HealthFacilityCode = x.HealthFacilityCode,
        //                   DistrictCode = x.DistrictCode,
        //                   DivisionCode = x.DivisionCode,
        //                   TehsilCode = x.TehsilCode,
        //                   Cnic = x.Cnic,
        //                   ContactNo = x.ContactNo,
        //                   HealthFacilityDistrict = x.HealthFacilityDistrict,
        //                   HealthFacilityTehsil = x.HealthFacilityTehsil,
        //                   HealthFacilityDivision = x.HealthFacilityDivision,
        //                   MrNo = x.MrNo,
        //                   Name = x.Name
        //               });

        //    var pagedList = await PagedListDto<ViewPatienListOldTbDto>.ToPagedListAsync(
        //           finalList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewPatienListOldTbDto>
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

        //public async Task<ViewPagerDto<ViewPatienListOldTbDto>> GetPatientSampleListOldTb(TbDashboardFilter filter)
        //{
        //    dynamic finalList;

        //    finalList = new HmisAuthContext().ViewPatientSampleListDashboards
        //               .WhereIf(!string.IsNullOrEmpty(filter.HealthFacilityCode), x => x.HealthFacilityCode == filter.HealthFacilityCode)
        //               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.PatientCreationDate!.Value.Date >= filter.StartDate!.Value.Date)
        //               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.PatientCreationDate!.Value.Date <= filter.EndDate!.Value.Date)
        //               .Select(x => new ViewPatienListOldTbDto
        //               {
        //                   PatientId = x.PatientId,
        //                   HealthFacilityCode = x.HealthFacilityCode,
        //                   DistrictCode = x.DistrictCode,
        //                   DivisionCode = x.DivisionCode,
        //                   TehsilCode = x.TehsilCode,
        //                   Cnic = x.Cnic,
        //                   ContactNo = x.ContactNo,
        //                   HealthFacilityDistrict = x.HealthFacilityDistrict,
        //                   HealthFacilityTehsil = x.HealthFacilityTehsil,
        //                   HealthFacilityDivision = x.HealthFacilityDivision,
        //                   MrNo = x.MrNo,
        //                   Name = x.Name
        //               });

        //    var pagedList = await PagedListDto<ViewPatienListOldTbDto>.ToPagedListAsync(
        //           finalList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewPatienListOldTbDto>
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

        #endregion

        #region HealthCertifiacte
        public async Task<List<HealthCertificateDTO>> GetHeatlCertificateCountByHealthFacilityCode(DashboardFilter filter)
        {
            string hfCode = "";
            if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
            {
                hfCode = filter.HealthFacilityCode;
            }
            else if (!string.IsNullOrEmpty(filter.TehsilCode))
            {
                hfCode = filter.TehsilCode;
            }
            else if (!string.IsNullOrEmpty(filter.DistrictCode))
            {
                hfCode = filter.DistrictCode;
            }
            else if (!string.IsNullOrEmpty(filter.DivisionCode))
            {
                hfCode = filter.DivisionCode;
            }



            var list = await _healthCertificateService.GetHealthCertificateCountForHMIS(hfCode);

            return list.data;

        }
        #endregion

        #region MedicoLegal
        public async Task<List<MedicoLegalDTO>> GetMedicoLegalCountForHMISByHealthFacilityCode(DashboardFilter filter)
        {
            string hfCode = "";
            if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
            {
                hfCode = filter.HealthFacilityCode;
            }
            else if (!string.IsNullOrEmpty(filter.TehsilCode))
            {
                hfCode = filter.TehsilCode;
            }
            else if (!string.IsNullOrEmpty(filter.DistrictCode))
            {
                hfCode = filter.DistrictCode;
            }
            else if (!string.IsNullOrEmpty(filter.DivisionCode))
            {
                hfCode = filter.DivisionCode;
            }


            var list = await _medicoLegalService.GetMedicoLegalCountForHMIS(hfCode);

            return list.data;

        }
        #endregion

        #region HealthCouncil
        public async Task<List<HealthCouncilDTO>> GetHealthCouncilCountForHMISByHealthFacilityCode(DashboardFilter filter)
        {
            string hfCode = "";
            if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
            {
                hfCode = filter.HealthFacilityCode;
            }
            else if (!string.IsNullOrEmpty(filter.TehsilCode))
            {
                hfCode = filter.TehsilCode;
            }
            else if (!string.IsNullOrEmpty(filter.DistrictCode))
            {
                hfCode = filter.DistrictCode;
            }
            else if (!string.IsNullOrEmpty(filter.DivisionCode))
            {
                hfCode = filter.DivisionCode;
            }


            var list = await _healthCouncilService.GetHealthCouncilCountForHMIS(hfCode);

            return list.data;

        }

        #endregion

        #region MIMS
        public async Task<List<MedicineAvailableDto>> GetMIMSCountForHMISByHealthFacilityCode(DashboardFilter filter)
        {

            var _uowHealthFacility = new TransUnitOfWork<DbModel.HealthFacility>(_uowPatient.GetDbContext());
            var _uowDepartment = new TransUnitOfWork<DbModel.DepartmentLookup>(_uowPatient.GetDbContext());


            var healthFacility = await _uowHealthFacility.Repository.GetALL(x => x.HealthFacilityId == filter.HealthFacilityId).FirstOrDefaultAsync();

            var department = await _uowDepartment.Repository.GetALL(x => x.DepartmentLookupId == filter.DepartmentId).FirstOrDefaultAsync();


            string hfCode = "null";
            if (healthFacility != null)
            {
                if (!string.IsNullOrEmpty(healthFacility.HrId.ToString()))
                {
                    //hfCode = filter.HealthFacilityCode;
                    hfCode = Convert.ToString(healthFacility!.HrId);
                }
            }

            else if (!string.IsNullOrEmpty(filter.TehsilCode))
            {
                hfCode = filter.TehsilCode;
            }
            else if (!string.IsNullOrEmpty(filter.DistrictCode))
            {
                hfCode = filter.DistrictCode;
            }
            else if (!string.IsNullOrEmpty(filter.DivisionCode))
            {
                hfCode = filter.DivisionCode;
            }


            if (hfCode == null || hfCode == "")
                return new List<MedicineAvailableDto>();

            ResponseMedicineAvailableDto responseData = new ResponseMedicineAvailableDto();

            if (department == null)
                return responseData.Data;
            else
            {
                responseData = await _mimsService.GetMedicineAvailableQuantityByHealthFacilityForDashboard(_mimsBaseUrl, hfCode, Convert.ToString(department.MimsDepartmentId));
            }
            if (responseData.Data == null)
            {
                return new List<MedicineAvailableDto>();
            }
            return responseData.Data;

        }
        public async Task<LastUpdatedDateMIMsDTO> GetLastUpdatedDateOfMIMS(DashboardFilter filter)
        {

            var _uowHealthFacility = new TransUnitOfWork<HealthFacility>(_uowPatient.GetDbContext());
            var _uowDepartment = new TransUnitOfWork<DbModel.DepartmentLookup>(_uowPatient.GetDbContext());


            var healthFacility = await _uowHealthFacility.Repository.GetALL(x => x.HealthFacilityId == filter.HealthFacilityId).FirstOrDefaultAsync();

            var department = await _uowDepartment.Repository.GetALL(x => x.DepartmentLookupId == filter.DepartmentId).FirstOrDefaultAsync();


            string hfCode = "null";
            if (healthFacility != null)
            {
                if (!string.IsNullOrEmpty(healthFacility.HrId.ToString()))
                {
                    //hfCode = filter.HealthFacilityCode;
                    hfCode = Convert.ToString(healthFacility!.HrId);
                }
            }

            else if (!string.IsNullOrEmpty(filter.TehsilCode))
            {
                hfCode = filter.TehsilCode;
            }
            else if (!string.IsNullOrEmpty(filter.DistrictCode))
            {
                hfCode = filter.DistrictCode;
            }
            else if (!string.IsNullOrEmpty(filter.DivisionCode))
            {
                hfCode = filter.DivisionCode;
            }


            if (hfCode == null || hfCode == "")
                return new LastUpdatedDateMIMsDTO { };

            LastUpdatedDateMIMsDTO responseData = new LastUpdatedDateMIMsDTO();
            responseData = await _mimsService.GetLastUpdatedDateOfMIMS(_mimsBaseUrl, hfCode);
            return responseData;

        }

        #endregion

        #region AIDS
        public async Task<List<AidsDTO>> GetAidsCountForHMISByDateRange(DashboardFilter filter)
        {

            var list = await _aidsService.GetAidsCountForHMISByDateRange(filter.StartDate.Value.AddDays(-7), filter.EndDate);

            return list.table;

        }

        #endregion

        #region TBScreening
        public async Task<ResponseTBScreeningDto> GetTBScreeningCountForHMISByHealthFacilityCode(DashboardFilter filter)
        {
            string hfCode = "";
            if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
            {
                hfCode = filter.HealthFacilityCode;
            }
            else if (!string.IsNullOrEmpty(filter.TehsilCode))
            {
                hfCode = filter.TehsilCode;
            }
            else if (!string.IsNullOrEmpty(filter.DistrictCode))
            {
                hfCode = filter.DistrictCode;
            }
            else if (!string.IsNullOrEmpty(filter.DivisionCode))
            {
                hfCode = filter.DivisionCode;
            }
            else
            {
                hfCode = "0";

            }
            EMRProgressReportDto _eMRProgressReportDto = new EMRProgressReportDto();
            _eMRProgressReportDto.healthfacilityCode = hfCode;
            _eMRProgressReportDto.FromDate = DateTime.Today.AddDays(-1);
            _eMRProgressReportDto.ToDate = DateTime.Today;
            _eMRProgressReportDto.IsForExcel = true;
            _eMRProgressReportDto.tempFlag = true;
            _eMRProgressReportDto.testID = new List<int>(0);
            _eMRProgressReportDto.testResult = new List<string>();



            var responseTBScreening = await _tbScreeningService.GetEMRProgressReport(_eMRProgressReportDto);

            return responseTBScreening;

        }
        #endregion

        #region Prescription
        public async Task<PatientCountWithInterExternalMedicines> getPatientCountWithInterExternalMedicines(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientCountWithInterExternalMedicines", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientCountWithInterExternalMedicines> lst = ds.Tables[0].ToList<PatientCountWithInterExternalMedicines>();
                    return lst[0];
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
        public async Task<PatientCountWithInterExternalMedicines> getPatientCountWithInterExternalMedicinesByDoctor(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientCountWithInterExternalMedicines", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientCountWithInterExternalMedicines> lst = ds.Tables[0].ToList<PatientCountWithInterExternalMedicines>();
                    return lst[0];
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
        public async Task<List<PatientDetaildto>> GetPatientPrescriptionIssued(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientPerscribed", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientPrescriptionIssuedDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientPerscribed", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientServedByDoctor(DashboardFilter filter)
        {

            //List<string> user = new List<string>();
            //if (filter.User != null)
            //    user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientServed", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalPharmacyDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientInternalPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalPharmacy(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientInternalPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientExternalPharmacy(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientExternalPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientExternalPharmacyDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientExternalPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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

        public async Task<List<PatientDetaildto>> GetPatientInternalExternalPharmacy(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientInternalExternalPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalExternalPharmacyDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientInternalExternalPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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

        public async Task<List<PatientDetaildto>> GetMedicineIssueListPharmacy(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetMedicineIssueListPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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

        public async Task<List<PatientDetaildto>> GetInQueueListPharmacy(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetInQueueListPharmacy", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        #region Lab
        public async Task<List<PatientDetaildto>> GetPatientTotalLabs(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientTotalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));



                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientInternalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientExternalLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientExternalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalExternalLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientinternalExternalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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

        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<PatientDetaildto>> GetPatientTotalLabDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
            var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientTotalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                    {
                        if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                            sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);
                        if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                            sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalLabDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientInternalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                    {
                        //sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                        //sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    }
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientExternalLabDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new TransUnitOfWork<DbModel.User>(_uowPatientDiagnose.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientExternalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                    {
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<List<PatientDetaildto>> GetPatientInternalExternalLabDoctor(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientDetaildto objResponse = new PatientDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPatientinternalExternalLab", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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

        public async Task<List<PatientLabDetaildto>> GetInQueuelistLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientLabDetaildto objResponse = new PatientLabDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabInQueue", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientLabDetaildto> lst = ds.Tables[0].ToList<PatientLabDetaildto>();



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

        public async Task<List<PatientLabDetaildto>> GetPendingReportListLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientLabDetaildto objResponse = new PatientLabDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabReportPending", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientLabDetaildto> lst = ds.Tables[0].ToList<PatientLabDetaildto>();



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

        public async Task<List<PatientLabDetaildto>> GetReportGernatedListLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientLabDetaildto objResponse = new PatientLabDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabReportGenerated", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientLabDetaildto> lst = ds.Tables[0].ToList<PatientLabDetaildto>();



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

        public async Task<List<PatientLabDetaildto>> GetSampleCollectedListLab(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientLabDetaildto objResponse = new PatientLabDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabSampleCollected", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientLabDetaildto> lst = ds.Tables[0].ToList<PatientLabDetaildto>();



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

        public async Task<List<PatientLabDetaildto>> GetInternalLabVisitList(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            PatientLabDetaildto objResponse = new PatientLabDetaildto();
            DctDashboardCardCount DocResponse = new DctDashboardCardCount();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPLabRecommended", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);


                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientLabDetaildto> lst = ds.Tables[0].ToList<PatientLabDetaildto>();



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

        #region SehtSahulatCardDashboard
        public async Task<SehtSahulatCardDashboardCountDTO> getSehatSahulatCardDashboardCount(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            SehtSahulatCardDashboardCountDTO objResponse = new SehtSahulatCardDashboardCountDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("dashboard.SPSehtSahulatCardDashboardCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    List<SehtSahulatCardDashboardCountDTO> lst = ds.Tables[0].ToList<SehtSahulatCardDashboardCountDTO>();
                    objResponse = lst[0];



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
            return objResponse;

        }

        public async Task<PatientDetailsWithPaginationdto> getSehatSahulatCardDashboardList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto ssList = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("dashboard.SPSehtSahulatCardDashboardList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    ssList.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    ssList.PatientList = ds.Tables[1].ToList<PatientDetailList>();
                    return ssList;

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

        #region DrugAddcicts Dashboard
        public async Task<DrugAddictsCommonDTO> getDrugAddcictsDashboardCounts(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            DrugAddictsCommonDTO objResponse = new DrugAddictsCommonDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("da.SPDrugAddcictsDashboardCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    List<DrugAddictsCommonDTO> lst = ds.Tables[0].ToList<DrugAddictsCommonDTO>();
                    objResponse = lst[0];



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
            return objResponse;

        }
        public async Task<PatientDetailDrugAddictsDTO> getDrugAddcictsDashboardListings(DashboardFilter filter)
        {
            PatientDetailDrugAddictsDTO daResponse = new PatientDetailDrugAddictsDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("da.SPDrugAddcictsDashboardListings", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<PatientDetailDrugAddictDTO> lst = ds.Tables[0].ToList<PatientDetailDrugAddictDTO>();

                    daResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailDrugAddictsDTO>().FirstOrDefault().TotalRecord;
                    daResponse.PatientList = ds.Tables[1].ToList<PatientDetailDrugAddictList>();
                    return daResponse;

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

        #region DrugAddcicts Diseases
        public async Task<DrugAddictsDiseasesDTO> getDrugAddcictsDashboardDieasesCounts(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            DrugAddictsDiseasesDTO objResponse = new DrugAddictsDiseasesDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("da.SPDrugAddcictsDashboardDieasesCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.Date.ToString() : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.Date.ToString() : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    List<DrugAddictsDiseasesDTO> lst = ds.Tables[0].ToList<DrugAddictsDiseasesDTO>();
                    objResponse = lst[0];



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
            return objResponse;

        }
        public async Task<PatientDetailDrugAddictDiseaseDTO> getDrugAddcictsDashboardDieasesListings(DashboardFilter filter)
        {
            PatientDetailDrugAddictDiseaseDTO dasResponse = new PatientDetailDrugAddictDiseaseDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("da.SPDrugAddcictsDashboardDieasesListings", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    dasResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailDrugAddictDiseaseDTO>().FirstOrDefault().TotalRecord;
                    dasResponse.PatientList = ds.Tables[1].ToList<PatientDetailDrugAddictDiseaseList>();
                    return dasResponse;


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

        #region DrugAddcicts To Social Welfare
        public async Task<DrugAddictsSocialRefferedDTO> getDrugAddcictsToSocialWelfareCounts(DashboardFilter filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            DrugAddictsSocialRefferedDTO objResponse = new DrugAddictsSocialRefferedDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("da.SPDrugAddcictsToSocialWelfareCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    List<DrugAddictsSocialRefferedDTO> lst = ds.Tables[0].ToList<DrugAddictsSocialRefferedDTO>();
                    objResponse = lst[0];



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
            return objResponse;

        }
        public async Task<PatientDetailDrugAddictsSocialWelfareDTO> getDrugAddcictsToSocialWelfareListings(DashboardFilter filter)
        {
            PatientDetailDrugAddictsSocialWelfareDTO daswResponse = new PatientDetailDrugAddictsSocialWelfareDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("da.SPDrugAddcictsToSocialWelfareListings", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<PatientDetailDrugAddictSocialWelfareDTO> lst = ds.Tables[0].ToList<PatientDetailDrugAddictSocialWelfareDTO>();

                    daswResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailDrugAddictsSocialWelfareDTO>().FirstOrDefault().TotalRecord;
                    daswResponse.PatientList = ds.Tables[1].ToList<PatientDetailDrugAddictSocialWelfareList>();
                    return daswResponse;




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

        #region HCP Dashboard
        public async Task<HCPOPDDashboardAllCountsDTO> getHCPDashboardAllCounts(DashboardFilter filter)
        {
            HCPOPDDashboardAllCountsDTO HCPDashboard = new HCPOPDDashboardAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHCPDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HCPOPDDashboardAllCountsDTO> lst = ds.Tables[0].ToList<HCPOPDDashboardAllCountsDTO>();
                    return lst[0];
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
        public async Task<HCPOPDDashboardAllCountsUpdatedDTO> getHCPDashboardAllCountsUpdated(DashboardFilter filter)
        {
            HCPOPDDashboardAllCountsUpdatedDTO HCPDashboard = new HCPOPDDashboardAllCountsUpdatedDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHCPDashboardAllCountsUpdated", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HCPOPDDashboardAllCountsUpdatedDTO> lst = ds.Tables[0].ToList<HCPOPDDashboardAllCountsUpdatedDTO>();
                    return lst[0];
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
        
        public async Task<List<PatientDetaildto>> getHCPDashboardAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHCPDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        public async Task<PatientDetailsWithPaginationdto> getHCPDashboardAllListUpdated(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto HCPResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHCPDashboardAllListUpdated", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    HCPResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    HCPResponse.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return HCPResponse;

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
        public async Task<HCPpreviousDashboardIndicatorsCountsDto> GetHCPpreviousDashboardIndicatorsCounts()
        {
            HCPpreviousDashboardMainDto HCPpreviousDashboardMain = new HCPpreviousDashboardMainDto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    //SqlCommand sqlComm = new SqlCommand("SPGetHCPpreviousDashboardIndicatorsCounts", (SqlConnection)conn);
                    SqlCommand sqlComm = new SqlCommand("SPGetOldDashboardIndicatorCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    ////sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    //    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    //    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    //    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    //if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    //    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    //if (!string.IsNullOrEmpty(filter.listType))
                    //    sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    HCPpreviousDashboardMain.HCPTotalSamples = ds.Tables[0].ToList<HCPTotalSamplesDto>().FirstOrDefault();
                    HCPpreviousDashboardMain.HCPScreening = ds.Tables[1].ToList<HCPScreeningDto>().FirstOrDefault();
                    HCPpreviousDashboardMain.HCPVaccination = ds.Tables[2].ToList<HCPVaccinationDto>().FirstOrDefault();
                    HCPpreviousDashboardMain.HCPTreatment = ds.Tables[3].ToList<HCPTreatmentDto>().FirstOrDefault();
                    HCPpreviousDashboardMain.HCPSvrEligible = ds.Tables[4].ToList<HCPSvrEligibleDto>().FirstOrDefault();

                    HCPpreviousDashboardIndicatorsCountsDto HCPpreviousDashboardIndicatorsCounts = new HCPpreviousDashboardIndicatorsCountsDto();

                    HCPpreviousDashboardIndicatorsCounts.PreDiagnosed = HCPpreviousDashboardMain.HCPScreening.PreDiagnosed;
                    HCPpreviousDashboardIndicatorsCounts.NewPatients = HCPpreviousDashboardMain.HCPScreening.NewPatients;
                    HCPpreviousDashboardIndicatorsCounts.HCVScreenedPositive = HCPpreviousDashboardMain.HCPScreening.HCVScreenedPositive;
                    HCPpreviousDashboardIndicatorsCounts.HCVScreenedNegative = HCPpreviousDashboardMain.HCPScreening.HCVScreenedNegative;
                    HCPpreviousDashboardIndicatorsCounts.HBVScreenedPositive = HCPpreviousDashboardMain.HCPScreening.HBVScreenedPositive;
                    HCPpreviousDashboardIndicatorsCounts.HBVScreenedNegative = HCPpreviousDashboardMain.HCPScreening.HBVScreenedNegative;
                    HCPpreviousDashboardIndicatorsCounts.VaccinationDose1Administered = HCPpreviousDashboardMain.HCPVaccination.VaccinationDose1Administered;
                    HCPpreviousDashboardIndicatorsCounts.VaccinationDose2Administered = HCPpreviousDashboardMain.HCPVaccination.VaccinationDose2Administered;
                    HCPpreviousDashboardIndicatorsCounts.VaccinationDose3Administered = HCPpreviousDashboardMain.HCPVaccination.VaccinationDose3Administered;

                    HCPpreviousDashboardIndicatorsCounts.SampleCollected = HCPpreviousDashboardMain.HCPTotalSamples.SampleCollected;
                    HCPpreviousDashboardIndicatorsCounts.SampleRejected = HCPpreviousDashboardMain.HCPTotalSamples.SampleRejected;
                    HCPpreviousDashboardIndicatorsCounts.InProcessSamples = HCPpreviousDashboardMain.HCPTotalSamples.InProcessSamples;

                    HCPpreviousDashboardIndicatorsCounts.HCVDetected = HCPpreviousDashboardMain.HCPTotalSamples.HCVDetected;
                    HCPpreviousDashboardIndicatorsCounts.HCVNotDetected = HCPpreviousDashboardMain.HCPTotalSamples.HCVNotDetected;
                    HCPpreviousDashboardIndicatorsCounts.HCVReSample = HCPpreviousDashboardMain.HCPTotalSamples.HCVReSample;

                    HCPpreviousDashboardIndicatorsCounts.HBVDetected = HCPpreviousDashboardMain.HCPTotalSamples.HBVDetected;
                    HCPpreviousDashboardIndicatorsCounts.HBVNotDetected = HCPpreviousDashboardMain.HCPTotalSamples.HBVNotDetected;
                    HCPpreviousDashboardIndicatorsCounts.HBVReSample = HCPpreviousDashboardMain.HCPTotalSamples.HBVReSample;

                    HCPpreviousDashboardIndicatorsCounts.SdHcvEnrolledInTreatment = HCPpreviousDashboardMain.HCPTreatment.SdHcvEnrolledInTreatment;
                    HCPpreviousDashboardIndicatorsCounts.SvHcvEnrolledInTreatment = HCPpreviousDashboardMain.HCPTreatment.SvHcvEnrolledInTreatment;
                    HCPpreviousDashboardIndicatorsCounts.SdrHcvEnrolledInTreatment = HCPpreviousDashboardMain.HCPTreatment.SdrHcvEnrolledInTreatment;

                    HCPpreviousDashboardIndicatorsCounts.EntecaHbvEnrolledInTreatment = HCPpreviousDashboardMain.HCPTreatment.EntecaHbvEnrolledInTreatment;
                    HCPpreviousDashboardIndicatorsCounts.TenofoHbvEnrolledInTreatment = HCPpreviousDashboardMain.HCPTreatment.TenofoHbvEnrolledInTreatment;

                    HCPpreviousDashboardIndicatorsCounts.SVRSampleCollected = HCPpreviousDashboardMain.HCPTotalSamples.SVRSampleCollected;
                    HCPpreviousDashboardIndicatorsCounts.SVRSampleProcessed = HCPpreviousDashboardMain.HCPTotalSamples.SVRSampleProcessed;
                    HCPpreviousDashboardIndicatorsCounts.CuredPatient = HCPpreviousDashboardMain.HCPTotalSamples.CuredPatient;
                    HCPpreviousDashboardIndicatorsCounts.RelapsedPatient = HCPpreviousDashboardMain.HCPTotalSamples.RelapsedPatient;
                    HCPpreviousDashboardIndicatorsCounts.EligibleForSVR = HCPpreviousDashboardMain.HCPSvrEligible.EligibleForSVR;

                    return HCPpreviousDashboardIndicatorsCounts;

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


        #region DSR Dashboard
        public async Task<DSRDashboardDataDTO> getDSRReportAllCounts(DashboardFilter filter)
        {
            DSRDashboardDataDTO DSRDashboard = new DSRDashboardDataDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("DSR_QUERY", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    DSRDashboard.AllCounts = ds.Tables[0].ToList<DSRDashboardReportDTO>();
                    DSRDashboard.TopFourMedicine = ds.Tables[1].ToList<DSRDashboardTopFourMedicineDTO>();
                    return DSRDashboard;




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

        //#region Paraplegic Dashboard
        //public async Task<ParaplegicDashboardAllCountsDTO> getParaplegicDashboardAllCounts(DashboardFilter filter)
        //{
        //    ParaplegicDashboardAllCountsDTO HCPDashboard = new ParaplegicDashboardAllCountsDTO();
        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("SPParaplegicDashboardAllCounts", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;
        //            sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
        //            sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
        //            sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
        //                sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
        //                sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
        //                sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
        //                sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
        //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
        //                sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
        //                sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

        //            if (!string.IsNullOrEmpty(filter.User))
        //                sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<ParaplegicDashboardAllCountsDTO> lst = ds.Tables[0].ToList<ParaplegicDashboardAllCountsDTO>();
        //            return lst[0];
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
        //public async Task<List<PatientDetaildto>> getParaplegicDashboardAllList(DashboardFilter filter)
        //{
        //    PatientDetaildto objResponse = new PatientDetaildto();

        //    using (var db = new HmisAuthContext())
        //    {
        //        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("SPParaplegicDashboardAllList", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;
        //            sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
        //            sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
        //            //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
        //                sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
        //                sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
        //                sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


        //            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
        //                sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
        //                sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
        //                sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

        //            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
        //                sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

        //            if (!string.IsNullOrEmpty(filter.listType))
        //                sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

        //            if (!string.IsNullOrEmpty(filter.User))
        //                sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));


        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();



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
        //#endregion




        #region Eye Infection Dashboard

        public async Task<List<DashboardDTO>> GetEyeInfectionDashboardCardCount(DashboardFilter filter)
        {
            List<DashboardDTO> objResponse = new List<DashboardDTO>();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPEyeBlindessPatientCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DashboardDTO> list = ds.Tables[0].ToList<DashboardDTO>();

                    objResponse = list.OrderBy(x => x.Index).ToList();

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
            return objResponse;

        }
        #endregion

        #region Dental Dashboard
        public async Task<DentalCountsAndChartDataDTO> getDentalDashboardAllCounts(DashboardFilter filter)
        {
            DentalCountsAndChartDataDTO dentalDashboard = new DentalCountsAndChartDataDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDentalDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    sqlComm.Parameters.AddWithValue("@TakeRecords", 20);


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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    dentalDashboard.AllCounts = ds.Tables[0].ToList<DentalDashboardAllCountsDTO>();
                    dentalDashboard.DentalProcedures = ds.Tables[1].ToList<DentalChartDataDTO>();
                    return dentalDashboard;
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
        public async Task<PatientDetailsWithPaginationdto> getDentalDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto DentalResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDentalDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!string.IsNullOrEmpty(filter.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", filter.DivisionCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", filter.DistrictCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!string.IsNullOrEmpty(filter.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", filter.TehsilCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", filter.HealthFacilityCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!string.IsNullOrEmpty(filter.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", filter.HealthFacilityTypeCode);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@listType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    DentalResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    DentalResponse.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return DentalResponse;

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

        public async Task<DentalProcedureDetailListingDTO> getDentalDashboardAllListProcedures(DashboardFilter filter)
        {
            DentalProcedureDetailListingDTO procedureResponse = new DentalProcedureDetailListingDTO();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPDentalDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<DentalProcedureDetaildto> lst = ds.Tables[0].ToList<DentalProcedureDetaildto>();

                    procedureResponse.TotalRecord = ds.Tables[0].ToList<DentalProcedureDetailListingDTO>().FirstOrDefault().TotalRecord;
                    procedureResponse.PatientList = ds.Tables[1].ToList<DentalProcedureDetailList>();

                    return procedureResponse;

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

        #region PhysioTherapy Dashboard
        public async Task<PhysioTherapyDashboardAllCountsDTOObj> getPhysioTherapyDashboardAllCounts(DashboardFilter filter)
        {
            PhysioTherapyDashboardAllCountsDTOObj listDashboard = new PhysioTherapyDashboardAllCountsDTOObj();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPhysioTherapyDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    listDashboard.AllCounts = ds.Tables[0].ToList<PhysioTherapyDashboardAllCountsDTO>();
                    listDashboard.ExercisePlan = ds.Tables[1].ToList<ChartPhysioDto>();
                    listDashboard.ModalityList = ds.Tables[2].ToList<ChartPhysioDto>();
                    listDashboard.ReportPatientWise = ds.Tables[3].ToList<PhysioReportReportPatientWiseDto>();
                    return listDashboard;
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
        public async Task<PatientDetailsWithPaginationdto> getPhysioTherapyDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto PhysioTherapyResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPhysioTherapyDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                   

                    PhysioTherapyResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    PhysioTherapyResponse.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return PhysioTherapyResponse;


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

        #region speech Therapy Dashboard
        public async Task<SpeechTherapyDashboardAllCountsDTO> getSpeechTherapyDashboardAllCounts(DashboardFilter filter)
        {
            SpeechTherapyDashboardAllCountsDTO speechdashboard = new SpeechTherapyDashboardAllCountsDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPSpeechTherapyDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<SpeechTherapyDashboardAllCountsDTO> lst = ds.Tables[0].ToList<SpeechTherapyDashboardAllCountsDTO>();
                    speechdashboard.AllCounts = ds.Tables[0].ToList<SpeechTherapyDashboardAllCounts>();
                    speechdashboard.SpeechPatientReport = ds.Tables[1].ToList<DetailSpeechPatientReport>();
                    return speechdashboard;

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
        public async Task<PatientDetailsWithPaginationdto> getSpeechTherapyDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto speechTherapyResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPSpeechTherapyDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    speechTherapyResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    speechTherapyResponse.PatientList = ds.Tables[1].ToList<PatientDetailList>();

                    return speechTherapyResponse;
                    

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

        #region Nutrition Dashboard
        public async Task<NutritionDashboardDTO> getNutritionDashboardAllCounts(DashboardFilter filter)
        {
            NutritionDashboardDTO NutritionDashboard = new NutritionDashboardDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPNutritionDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    NutritionDashboard.AllCounts = ds.Tables[0].ToList<NutritionDashboardCountsDTO>();
                    NutritionDashboard.BMI = ds.Tables[1].ToList<ChartNutritionDto>();
                    NutritionDashboard.NutritionalRisk = ds.Tables[2].ToList<ChartNutritionDto>();
                    NutritionDashboard.ExaminationFindings = ds.Tables[3].ToList<ChartNutritionDto>();
                    NutritionDashboard.Comorbidity = ds.Tables[4].ToList<ChartNutritionDto>();
                    NutritionDashboard.Malnutrition = ds.Tables[5].ToList<ChartNutritionDto>();
                    NutritionDashboard.PatientReport = ds.Tables[6].ToList<DetailPatientReport>();
                    return NutritionDashboard;

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
        public async Task<PatientDetailsWithPaginationdto> getNutritionDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto nutritionResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPNutritionDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();
                    nutritionResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    nutritionResponse.PatientList = ds.Tables[1].ToList<PatientDetailList>();
                    return nutritionResponse;

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

        #region Psychology Dashboard
        public async Task<PsychologyDashboardDTO> getPsychologyDashboardAllCounts(DashboardFilter filter)
        {
            PsychologyDashboardDTO PsychologyDashboard = new PsychologyDashboardDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPsychologyDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    PsychologyDashboard.AllCounts = ds.Tables[0].ToList<PsychologyDashboardCountsDTO>();
                    PsychologyDashboard.Appearance = ds.Tables[1].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Orientation = ds.Tables[2].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Speech = ds.Tables[3].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.ThroughProcess = ds.Tables[4].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.ThroughContent = ds.Tables[5].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.PerceptualProcess = ds.Tables[6].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Insight = ds.Tables[7].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Judgment = ds.Tables[8].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Mood = ds.Tables[9].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Affect = ds.Tables[10].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.Memory = ds.Tables[11].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.EstimatedIntellectualFunctioning = ds.Tables[12].ToList<ChartPsychologyDto>();
                    PsychologyDashboard.CognitiveDeficits = ds.Tables[13].ToList<ChartPsychologyDto>();
                    return PsychologyDashboard;

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
        public async Task<PatientDetailsWithPaginationdto> getPsychologyDashboardAllList(DashboardFilter filter)
        {
            PatientDetailsWithPaginationdto psychoResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPPsychologyDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();

                    psychoResponse.TotalRecord = ds.Tables[0].ToList<PatientDetailsWithPaginationdto>().FirstOrDefault().TotalRecord;
                    psychoResponse.PatientList = ds.Tables[1].ToList<PatientDetailList>();
                    return psychoResponse;

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

        #region HealthFacility wise Disease List
        public async Task<List<ResHFWiseDiseaseListCount>> getHealthFacilitywiseDiseaseListCount(DashboardFilter filter)
        {
            //PatientDetailsWithPaginationdto psychoResponse = new PatientDetailsWithPaginationdto();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[SPDashboardHealthFacilityWiseDiseaseList]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    //List<PatientDetaildto> lst = ds.Tables[0].ToList<PatientDetaildto>();

                    List<ResHFWiseDiseaseListCount> res = ds.Tables[0].ToList<ResHFWiseDiseaseListCount>();
                    return res;

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

        public async Task<bool> SendSms(SPTbMedicineDeliveryDataResponseDto data)
        {
            //int otp = AppCommonMethod.GenerateRandom4DigitNumber();
            //var otpCode = await SaveOtp(otp, userObj.UserId);
            var body = "";
            if (data.Gender == "Female")
                body = $"محترمہ جناب {data.PatientName}\n\n";
            else
                body = $"محترم جناب {data.PatientName}\n\n";
            body += "آپ کو وزیر اعلٰی پنجاب محترمہ مریم نواز شریف صاحبہ کی خصوصی ہدایت پر TB کی مندرجہ ذیل ادویات مہینہ وار مفت فراہم کی جا رہی ہیں۔ کسی بھی قسم کی طبی مشاورت کی لیے اپنے قریبی Clinic TB پر تشریف لے جائیں۔\n\n";
            
            body += $"- {data.NameOfMedicineAdvised} (Qty: {data.Quantity})\n";
            if(!string.IsNullOrEmpty(data.AdditionalMedicineAdvised))
                body += $"- {data.AdditionalMedicineAdvised} (Qty: {data.Quantity})\n";
            body += "\nمنجاب:-\n";
            body += "محکمہ پرائمری اینڈ سیکنڈری ہیلتھ کیئر ڈیپارٹمنٹ لاہور";

            SendSMSDto smsObj = new SendSMSDto()
            {
                Receiver = data.PhoneNo1!.Replace("-", ""),
                Body = body
            };

            _smsService.SendSMS(smsObj);

            return true;

        }
        public async Task<string> GetHfCode(DashboardBASFilter filter)
        {
            var hfcode = "";





            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                //var _uowUser = new TransUnitOfWork<HealthFacility>(uow.GetDbContext());
                var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId).FirstOrDefault();
                if (HealthFacility != null)
                {
                    hfcode = HealthFacility.HealthFacilityCode;
                }

            }
            else if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
                            .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId).FirstOrDefault();
                if (HealthFacility != null)
                {
                    hfcode = HealthFacility.TehsilCode;
                }
            }
            else if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
                            .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId).FirstOrDefault();
                if (HealthFacility != null)
                {
                    hfcode = HealthFacility.DistrictCode;
                }
            }
            else if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId).FirstOrDefault();
                if (HealthFacility != null)
                {
                    hfcode = HealthFacility.DivisionCode;
                }
            }
            //else 
            //{
            //    hfcode = "0";
            //}


            //else if (AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            //{
            //    if (AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            //    {
            //        if (AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            //        {
            //            if (AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            //            {
            //                var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
            //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId).FirstOrDefault();
            //                if (HealthFacility != null)
            //                {
            //                    hfcode = HealthFacility.TehsilCode;
            //                }
            //            }
            //            else
            //            {
            //                var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
            //                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId).FirstOrDefault();
            //                if (HealthFacility != null)
            //                {
            //                    hfcode = HealthFacility.DistrictCode;
            //                }
            //            }

            //        }
            //        else
            //        {
            //            var HealthFacility = _uowPatient.GetDbContext().ViewHfLocations
            //            .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId).FirstOrDefault();
            //            if (HealthFacility != null)
            //            {
            //                hfcode = HealthFacility.DivisionCode;
            //            }


            //        }

            //    }
            //    else
            //    {
            //        hfcode = "0";
            //    }

            //}
            else
            {
                hfcode = "0";
            }
            return hfcode;

        }

        #endregion
    }
}
