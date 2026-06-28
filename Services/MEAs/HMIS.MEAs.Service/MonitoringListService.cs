using HMIS.MEAs.Domain.Models.DbModels;
using DbRegion = HMIS.MEAs.Domain.Models.DbModels.Region;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.MEAs.Domain.Models.DTO.Common;
using HMIS.MEAs.Domain.Repositories._UOW;
using AppCommonMethods;
using HMIS.MEAs.Domain.Models.DTO.Monitoring;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using JWTAuthentication;
using HMIS.Aggregator.API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.Win32;
using System.Drawing;
using HMIS.MEAs.Domain.Models.DTO;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace HMIS.MEAs.Service
{
    public class MonitoringListService
    {
        #region Class Fields & Propertities

        private readonly JWTAuthentication.TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<MonitoringChild> _MonitringChikd;
        private readonly UnitOfWork<Module> _Module;
        private readonly UnitOfWork<Indicator> _Indicator;


        #endregion
        #region Constructor

        public MonitoringListService(
           JWTAuthentication.TokenService tokenService,
           UnitOfWork<MonitoringChild> monitoringChild,
            UnitOfWork<Module> module,
             UnitOfWork<Indicator> indicator,
           IMapper mapper)
        {
            _tokenService = tokenService;
          
            _mapper = mapper;
            _MonitringChikd = monitoringChild;
            _Module = module;
            _Indicator = indicator;
          
        }
        #endregion
        #region CUD Opertations

        public async Task<List<MonitoringListDTO>> GetMonitoringList(SearchDTO searchDTO)
        {
            using (var db = new MeasallContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("GetMonitoringList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@moduleId", searchDTO.ModuleId);
                    sqlComm.Parameters.AddWithValue("@divisionId", searchDTO.divisionId);
                    sqlComm.Parameters.AddWithValue("@districtId", searchDTO.districtId);
                    sqlComm.Parameters.AddWithValue("@tehsilId", searchDTO.tehsilId);
                    sqlComm.Parameters.AddWithValue("@zoneID", searchDTO.ZoneId);
                    sqlComm.Parameters.AddWithValue("@hfTypeId", searchDTO.HfTypeId);
                    sqlComm.Parameters.AddWithValue("@shiftId", searchDTO.ShiftId);
                    sqlComm.Parameters.AddWithValue("@fromDate", searchDTO.fromDate?.ToString("dd/MM/yyyy"));
                    sqlComm.Parameters.AddWithValue("@toDate", searchDTO.fromDate?.ToString("dd/MM/yyyy"));
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                    await Task.Run(() => da.Fill(ds));

                    List<MonitoringListDTO> lst = ds.Tables[0].ToList<MonitoringListDTO>();

                    return lst;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
        }
        public async Task<List<tbl_BedsChildList>> HFBedSurveyGetMasterDetailById(int monitoringId)
        {
            try
            {
                List<tbl_BedsChildList> monitoringMastAll = new List<tbl_BedsChildList>();
                monitoringMastAll = GetHFMonitoringMaster(monitoringId);

                return monitoringMastAll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static List<tbl_BedsChildList> GetHFMonitoringMaster(int monitoringMasterId)
        {
           // List<tbl_BedsChildList> result = new List<tbl_BedsChildList>();

            using(var db = new MeasallContext())
            {
                var conn = db.Database.GetDbConnection();

                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("MonitoringMasterDetail", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@ActivityName", "HFMonitoringMaster");
                    sqlComm.Parameters.AddWithValue("@MasterId", monitoringMasterId);
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                     Task.Run(() => da.Fill(ds));

                    List<tbl_BedsChildList> lst = ds.Tables[0].ToList<tbl_BedsChildList>();

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

        public async Task<List<HFBedSurveyMonitoringListDTO>> HFBedSurveyGetMonitoringList(SearchDTO searchDTO)
        {
            using (var db = new MeasallContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("GetMonitoringList", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@moduleId", searchDTO.ModuleId);
                    sqlComm.Parameters.AddWithValue("@divisionId", searchDTO.divisionId);
                    sqlComm.Parameters.AddWithValue("@districtId", searchDTO.districtId);
                    sqlComm.Parameters.AddWithValue("@tehsilId", searchDTO.tehsilId);
                    sqlComm.Parameters.AddWithValue("@zoneID", searchDTO.ZoneId);
                    sqlComm.Parameters.AddWithValue("@hfTypeId", searchDTO.HfTypeId);
                    sqlComm.Parameters.AddWithValue("@shiftId", searchDTO.ShiftId);
                    sqlComm.Parameters.AddWithValue("@fromDate", searchDTO.fromDate?.ToString("dd/MM/yyyy"));
                    sqlComm.Parameters.AddWithValue("@toDate", searchDTO.fromDate?.ToString("dd/MM/yyyy"));
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                    await Task.Run(() => da.Fill(ds));

                    List<HFBedSurveyMonitoringListDTO> lst = ds.Tables[0].ToList<HFBedSurveyMonitoringListDTO>();

                    return lst;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
        }
        #endregion
        #region Read Operations
        #endregion
        #region Helper
        #endregion

    }
}
