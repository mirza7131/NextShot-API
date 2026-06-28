using AppCommonMethods;
using AutoMapper;

using HMIS.Aggregator.API.Services;
using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Domain.Models.DTO.MobileAppDashboard;
using HMIS.Dashboard.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using DbModel = HMIS.Dashboard.Domain.Models.DbModels;
using HealthFacility = HMIS.Dashboard.Domain.Models.DbModels.HealthFacility;

namespace HMIS.Dashboard.Service
{
    public class MobileAppService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<DbModel.Patient> _uowPatient;
        private readonly HealthCertificateService _healthCertificateService;
        private readonly MedicoLegalService _medicoLegalService;
        private readonly HealthCouncilService _healthCouncilService;
        private readonly MIMSService _mimsService;
        private readonly AidsService _aidsService;
        //private readonly PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;
        private UnitOfWork<DbModel.PatientDiagnose> _uowPatientDiagnose;
        private UnitOfWork<DbModel.MimsMedicineDatum> _uowMimsMedicineDatum;
        private readonly UnitOfWork<DbModel.PatientOpenVisit> _uowPatientOpenVisit;
        private readonly TBScreeningService _tbScreeningService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly bool _isActiveOffline;
        
        #endregion

        #region Constructor

        public MobileAppService(
            TokenService tokenService,
            IMapper mapper,

        UnitOfWork<DbModel.Patient> uowPatient,
            HealthCertificateService healthCertificateService,
            MedicoLegalService medicoLegalService,
            HealthCouncilService healthCouncilService,
            MIMSService mimsService,
            AidsService aidsService,
            TBScreeningService tbScreeningService,
            UnitOfWork<DbModel.PatientDiagnose> uowPatientDiagnose,
             UnitOfWork<DbModel.PatientOpenVisit> uowPatientOpenVisit,
              UnitOfWork<DbModel.MimsMedicineDatum> uowMimsMedicineDatum,
            //PatientDiagnoseService<PatientDiagnose> patientDiagnoseService,
            IConfiguration config
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

            _isActiveOffline = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                           config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") : false;
        }

        #endregion

        #region Mobile App Dashboard New 
        public async Task<MobileAppHfWsieCounts> getHMISAllCountsForApp(MobileAppDashboardDTOs model)
        {
            MobileAppDashboardFilter IntDashboardModel = MappIDDashboardFilters(model);
            MobileAppHfWsieCounts HFWiseList = new MobileAppHfWsieCounts();
            using (var db = new HmisRepContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPHMISAllCountsForApp", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@StartDate", IntDashboardModel.StartDate == null ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : IntDashboardModel.StartDate.Value.ToString("yyyy-MM-dd"));// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", IntDashboardModel.EndDate == null ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : IntDashboardModel.EndDate.Value.ToString("yyyy-MM-dd"));
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);
                    if (!AppCommonMethod.IsNullorZeroInt(IntDashboardModel.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", IntDashboardModel.ProvinceId);
                    if (!string.IsNullOrEmpty(IntDashboardModel.DivisionCode))
                        sqlComm.Parameters.AddWithValue("@DivisionCode", IntDashboardModel.DivisionCode);
                    if (!string.IsNullOrEmpty(IntDashboardModel.DistrictCode))
                        sqlComm.Parameters.AddWithValue("@DistrictCode", IntDashboardModel.DistrictCode);
                    if (!string.IsNullOrEmpty(IntDashboardModel.TehsilCode))
                        sqlComm.Parameters.AddWithValue("@TehsilCode", IntDashboardModel.TehsilCode);
                    if (!string.IsNullOrEmpty(IntDashboardModel.HealthFacilityTypeCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeCode", IntDashboardModel.HealthFacilityTypeCode);
                    if (!string.IsNullOrEmpty(IntDashboardModel.HealthFacilityCode))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityCode", IntDashboardModel.HealthFacilityCode);
                    if (!AppCommonMethod.IsNullorZeroInt(IntDashboardModel.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", IntDashboardModel.HealthFacilityId);

                    //if (model!.filter!.Length > 0)
                    //{
                    //    const string quote = "\'";
                    //    for (int i = 0; i < model!.filter.Length; i++)
                    //    {
                    //        model!.filter[i] = quote + model!.filter[i] + quote;
                    //    }
                    //    var str = string.Join(",", model!.filter);
                    //    sqlComm.Parameters.AddWithValue("@DepartmentsIn", str);
                    //}

                    if (!AppCommonMethod.IsNullorZeroInt(IntDashboardModel.HrId))
                        sqlComm.Parameters.AddWithValue("@HrId", IntDashboardModel.HrId);

                    if (!AppCommonMethod.IsNullorZeroInt(IntDashboardModel.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", IntDashboardModel.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(IntDashboardModel.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", IntDashboardModel.SectionId);

                    if (!string.IsNullOrEmpty(IntDashboardModel.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", IntDashboardModel!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    //if (model.isPaginate == true)
                    //{
                    //    sqlComm.Parameters.AddWithValue("@isPaginate", model.isPaginate);
                    //    sqlComm.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                    //    sqlComm.Parameters.AddWithValue("@PageSize", model.PageSize);
                    //}
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<MobileAppHfWsieCounts> lst = ds.Tables[1].ToList<MobileAppHfWsieCounts>();
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
        #region Helper Function
        public MobileAppDashboardFilter MappIDDashboardFilters(MobileAppDashboardDTOs model)
        {
            MobileAppDashboardFilter IntDashboardModel = new MobileAppDashboardFilter();
            IntDashboardModel.StartDate = model.fromDate;
            IntDashboardModel.EndDate = model.toDate;
            IntDashboardModel.DivisionCode = model.divisionCode;
            IntDashboardModel.DistrictCode = model.districtCode;
            IntDashboardModel.TehsilCode = model.tehsilCode;
            IntDashboardModel.HealthFacilityTypeCode = model.hfTypeCode;
            IntDashboardModel.HealthFacilityCode = model.hfCode;
            IntDashboardModel.HealthFacilityId = model.hfId;

            return IntDashboardModel;
        }
        #endregion
    }
}
