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
//using HMIS.Dashboard.Domain.Models.DbModels;
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
//using ZXing;
//using ZXing.Common;
//using System.Drawing;
//using System.Drawing.Imaging;

namespace HMIS.Dashboard.Service
{
    public class DashboardSyncService
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
        
        #endregion

        #region Constructor

        public DashboardSyncService(
            TokenService tokenService,
            IMapper mapper,

            TransUnitOfWork<DbModel.Patient> uowPatient,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatient = uowPatient;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;

            _isActiveOffline = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                           config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") : false;
        }

        #endregion

        #region CUD Operations

        #endregion

        #region Read Operations
        public async Task<List<DashboardDataSyncUtilityLogDto>> getDataSyncUtilityLog(SyncUtilityDashboardFilter filter)
        {
            DashboardDataSyncUtilityLogDto list = new DashboardDataSyncUtilityLogDto();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPDataSyncUtilityLog]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

                    //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));


                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);
                    
                    if (!AppCommonMethod.IsNullBool(filter.isAllList))
                        sqlComm.Parameters.AddWithValue("@isAllList", filter.isAllList);
                    


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DashboardDataSyncUtilityLogDto> lst = ds.Tables[0].ToList<DashboardDataSyncUtilityLogDto>();
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
        
    }
}
