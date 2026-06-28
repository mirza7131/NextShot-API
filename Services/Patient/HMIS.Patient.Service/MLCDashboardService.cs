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
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DbModels_TB;
using HMIS.Patient.Domain.Models.DTO.Common;
using HMIS.Patient.Domain.Models.DTO.DashboardDto;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.AdminReferedDashboard;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.DoctorDashboard;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.PathologyDashboardPathologyDashboard;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.PatientRegistrationDashboard;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.PharmacyDashboard;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.VitalDashboard;
using HMIS.Patient.Domain.Models.DTO.MLCDashboardDTO;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientVisitFlowDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using static HMIS.Aggregator.API.Models.MIMS.MedicineAvailableDto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using HealthFacility = HMIS.Patient.Domain.Models.DbModels.HealthFacility;

namespace HMIS.Patient.Service
{
    public class MLCDashboardService
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
        private readonly PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;
        private UnitOfWork<PatientDiagnose> _uowPatientDiagnose;
        private readonly UnitOfWork<PatientOpenVisit> _uowPatientOpenVisit;
        private readonly TBScreeningService _tbScreeningService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public MLCDashboardService(
            TokenService tokenService,
            IMapper mapper,

        UnitOfWork<DbModel.Patient> uowPatient,
            HealthCertificateService healthCertificateService,
            MedicoLegalService medicoLegalService,
            HealthCouncilService healthCouncilService,
            MIMSService mimsService,
            AidsService aidsService,
            TBScreeningService tbScreeningService,
            UnitOfWork<PatientDiagnose> uowPatientDiagnose,
             UnitOfWork<PatientOpenVisit> uowPatientOpenVisit,
            PatientDiagnoseService<PatientDiagnose> patientDiagnoseService,
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
            _tbScreeningService = tbScreeningService;
            _patientDiagnoseService = patientDiagnoseService;
            _uowPatientDiagnose = uowPatientDiagnose;
            _uowPatientOpenVisit = uowPatientOpenVisit;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
        }

        #endregion

        #region MLC Dashboard Counts 
        public async Task<MedicolegalDashboardDTO> getMedicoLegalDashboardAllCounts(DashboardFilter filter)
        {
            MedicolegalDashboardDTO MedicolegalDashboard = new MedicolegalDashboardDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("mlc.SPMedicoLegalDashboardAllCounts", (SqlConnection)conn);
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

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    MedicolegalDashboard.AllCounts = ds.Tables[0].ToList<MLCDashboardCountsDTO>();
                    MedicolegalDashboard.HFWiseCount = ds.Tables[1].ToList<MedicolegalHFWiseCountDTO>();
                    MedicolegalDashboard.DayWiseCount = ds.Tables[2].ToList<MedicolegalDayWiseCountDTO>();
                    MedicolegalDashboard.MonthWiseCount = ds.Tables[3].ToList<MedicolegalMonthWiseCountDTO>();
                    MedicolegalDashboard.SeasonWiseCount = ds.Tables[4].ToList<MedicolegalSeasonWiseCountDTO>();
                    return MedicolegalDashboard;
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

        #region EMC Dashboard Counts 
        public async Task<EMCDashboardDTO> getEMCDashboardAllCounts(DashboardFilter filter)
        {
            EMCDashboardDTO EMCDashboard = new EMCDashboardDTO();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("emc.SPEMCDashboardAllCounts", (SqlConnection)conn);
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

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    EMCDashboard.AllCounts = ds.Tables[0].ToList<EMCDashboardCountsDTO>();
                    EMCDashboard.HFWiseCount = ds.Tables[1].ToList<EMCHFWiseCountDTO>();
                    EMCDashboard.DayWiseCount = ds.Tables[2].ToList<EMCDayWiseCountDTO>();
                    EMCDashboard.MonthWiseCount = ds.Tables[3].ToList<EMCMonthWiseCountDTO>();
                    EMCDashboard.SeasonWiseCount = ds.Tables[4].ToList<EMCSeasonWiseCountDTO>();
                    return EMCDashboard;
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
