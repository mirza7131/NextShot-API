using AppCommonMethods;
using AutoMapper;
using CommonMessages;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using HMIS.Dashboard.Domain.Models.DbModels;
//using HMIS.Dashboard.Domain.Models.DbModels_TB;
using HMIS.Dashboard.Domain.Models.DTO.Common;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.AdminReferedDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.DoctorDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PathologyDashboardPathologyDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PatientRegistrationDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PharmacyDashboard;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.VitalDashboard;
using HMIS.Dashboard.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using DbModel = HMIS.Dashboard.Domain.Models.DbModels;
using HealthFacility = HMIS.Dashboard.Domain.Models.DbModels.HealthFacility;
//using ZXing;
//using ZXing.Common;
//using System.Drawing;
//using System.Drawing.Imaging;

namespace HMIS.Dashboard.Service
{
    public class ParaplegicDashboardService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<DbModel.Patient> _uowPatient;
        private readonly MIMSService _mimsService;
        //private readonly PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;
        //private readonly UnitOfWork<PatientOpenVisit> _uowPatientOpenVisit;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public ParaplegicDashboardService(
            TokenService tokenService,
            IMapper mapper,

        UnitOfWork<DbModel.Patient> uowPatient,
            //HealthCertificateService healthCertificateService,
            //HealthCouncilService healthCouncilService,
            MIMSService mimsService,
            //AidsService aidsService,
            //TBScreeningService tbScreeningService,
            //UnitOfWork<PatientDiagnose> uowPatientDiagnose,
             //UnitOfWork<PatientOpenVisit> uowPatientOpenVisit,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatient = uowPatient;
            _mimsService = mimsService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
        }

        #endregion

        #region Registration Dashboard New 
        public async Task<RegistrationDashboardAllCountsDTO> getParaplegicRegistrationDashboardAllCounts(DashboardFilter filter)
        {
            DoctorDashboardPatientAllCountsDTO doctorDashboard = new DoctorDashboardPatientAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicRegistrationDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<RegistrationDashboardAllCountsDTO> lst = ds.Tables[0].ToList<RegistrationDashboardAllCountsDTO>();
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

        public async Task<List<PatientDetaildto>> getParaplegicRegistrationDashboardAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicRegistrationDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        public async Task<List<OPDSectionWiseTokenCountDTO>> getParaplegicRegDashboardOPDSectionWiseTokenCount(DashboardFilter filter)
        {

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicRegDashboardOPDSectionWiseTokenCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

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
        public async Task<List<PatientVisitCountByUser>> getParaplegicPatientVisitCountByUser(DashboardFilter filter)
        {
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicGetPatientVisitCountByUserEntries", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

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

        #endregion
        #region Vital Dashboard New 
        public async Task<VitalDashboardAllCountsDTO> getParaplegicVitalDashboardAllCounts(DashboardFilter filter)
        {
            VitalDashboardAllCountsDTO doctorDashboard = new VitalDashboardAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicVitalDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

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

        public async Task<List<PatientDetaildto>> getParaplegicVitalDashboardAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicVitalDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        public async Task<List<PatientVisitCountByUser>> getParaplegicPatientVisitVitalCountByUser(DashboardFilter filter)
        {
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicGetPatientVisitVitalCountByUserEntries", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

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
        #endregion
        #region MIMS
        public async Task<List<MedicineAvailableDto>> GetParaplegicMIMSCountForHMISByHealthFacilityCode(DashboardFilter filter)
        {

            var _uowHealthFacility = new UnitOfWork<HealthFacility>(_uowPatient.GetDbContext());
            var _uowDepartment = new UnitOfWork<DepartmentLookup>(_uowPatient.GetDbContext());


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

        #endregion
        #region Doctor Dashboard
        public async Task<DoctorDashboardPatientAllCountsDTO> getParaplegicDoctorDashboardPatientAllCounts(DashboardFilter filter)
        {
            DoctorDashboardPatientAllCountsDTO doctorDashboard = new DoctorDashboardPatientAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new UnitOfWork<User>(_uowPatient.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicDoctorDashboardPatientAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DoctorDashboardPatientAllCountsDTO> lst = ds.Tables[0].ToList<DoctorDashboardPatientAllCountsDTO>();
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
        #endregion Doctor Dashboard Counts
        #region Doctor Dashboard HF
        public async Task<DoctorDashboardPatientAllCountsDTO> getParaplegicDoctorDashboardHFPatientAllCounts(DashboardFilter filter)
        {
            DoctorDashboardPatientAllCountsDTO doctorDashboard = new DoctorDashboardPatientAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicDoctorDashboardPatientAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DoctorDashboardPatientAllCountsDTO> lst = ds.Tables[0].ToList<DoctorDashboardPatientAllCountsDTO>();
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
        public async Task<List<PatientDetaildto>> getParaplegicDoctorDashboardPatientAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var _uowUser = new UnitOfWork<User>(_uowPatient.GetDbContext());
                    var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicDoctorDashboardPatientAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

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
        public async Task<List<PatientDetaildto>> getParaplegicDoctorDashboardHFPatientAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicDoctorDashboardPatientAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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
        #endregion
        #region Pharmacy Dashboard new
        public async Task<PharmacyDashboardPatientAllCountsDTO> getParaplegicPharmacyDashboardAllCounts(DashboardFilter filter)
        {
            PharmacyDashboardPatientAllCountsDTO doctorDashboard = new PharmacyDashboardPatientAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicPharmacyDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        public async Task<List<PatientDetaildto>> getParaplegicPharmacyDashboardAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicPharmacyDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        public async Task<List<PharmacyDashboardMedcineIssuedReport>> GetParaplegicPharmacyDashboardMedcineIssuedReport(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicPharmacyDashboardMedcineIssuedReport", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        public async Task<List<PharmacyDashboardPatientDetailDTO>> getParaplegicPharmacyDashboardMedcineIssuedPatientDetailList(MedicineIssuedFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicPharmacyDashboardMedcineIssuedPatientDetailReport", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        #region Get Patient Wise Medicine Report
        public async Task<List<PharmacyMedicinePatientWiseReport>> getParaplegicPharmacyInternalExternalAccumulateQuantityPatientWiseReport(DashboardFilter filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();
            //if (filter.StartDate == filter.EndDate)
            //{
            //    filter.EndDate = filter.EndDate.Value.AddDays(1);
            //}
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicPharmacyInternalExternalAccumulateQuantityPatientWiseReport", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.Date.ToString() : filter.StartDate.Value.Date.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.Date.ToString() : filter.EndDate.Value.Date.ToString());
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
        #endregion

        #region Pathology Dashboard All Counts
        public async Task<LabDashboardTestViewModelDTO> getParaplegicLabDashboardTestAllCounts(DashboardFilter filter)
        {
            LabDashboardTestViewModelDTO labDashboard = new LabDashboardTestViewModelDTO();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicLabDashboardTestAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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
        public async Task<List<PatientLabDetaildto>> getParaplegicLabDashboardTestAllList(DashboardFilter filter)
        {
            PatientLabDetaildto objResponse = new PatientLabDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicLabDashboardTestAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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
        public async Task<LabDashboardFromStartTillNowAllCountsDTO> getParaplegicLabDashboardTestTimeFromStartTillNowAllCounts(DashboardFilter filter)
        {
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicLabDashboardTestTimeFromStartTillNowAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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
        public async Task<List<PatientLabDetaildto>> getParaplegicLabDashboardTestTimeFromStartTillNowAllList(DashboardFilter filter)
        {
            PatientLabDetaildto objResponse = new PatientLabDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicLabDashboardTestTimeFromStartTillNowAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        public async Task<List<InternalExternalCountByLabTestDTO>> getParaplegicLabDashboardTop20LabTestRecommended(DashboardFilter filter)
        {
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicLabDashboardTop20LabTestRecommended", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

        #region Paraplegic Dashboard
        public async Task<ParaplegicDashboardAllCountsDTO> getParaplegicDashboardAllCounts(DashboardFilter filter)
        {
            ParaplegicDashboardAllCountsDTO HCPDashboard = new ParaplegicDashboardAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicDashboardAllCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ParaplegicDashboardAllCountsDTO> lst = ds.Tables[0].ToList<ParaplegicDashboardAllCountsDTO>();
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
        public async Task<List<PatientDetaildto>> getParaplegicDashboardAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPParaplegicDashboardAllList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
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
        #endregion
        #region  Indivisual Opd Dashboard
        public async Task<ParaplegicDashboardAllCountsDTO> getIndivisualOpdDashboardAllCounts(DashboardFilter filter)
        {
            ParaplegicDashboardAllCountsDTO HCPDashboard = new ParaplegicDashboardAllCountsDTO();
            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPIndivisualOpdDashboardAllCounts", (SqlConnection)conn);
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ParaplegicDashboardAllCountsDTO> lst = ds.Tables[0].ToList<ParaplegicDashboardAllCountsDTO>();
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
        public async Task<List<PatientDetaildto>> getIndivisualOpdDashboardAllList(DashboardFilter filter)
        {
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisRepContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPIndivisualOpdDashboardAllList", (SqlConnection)conn);
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
        #endregion


    }
}
