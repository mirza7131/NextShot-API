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
using HMIS.MEAs.Domain.Models.DTO.Dashboard;
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
using HMIS.MEAs.Domain.Models.DTO.Visists;

namespace HMIS.MEAs.Service
{
    public class DashboardService
    {
        #region Class Fields & Propertities
        private readonly IMapper _mapper;
        private readonly UnitOfWork<ViewTodayVisit> _ViewTodayVisit;
        private readonly UnitOfWork<RepeatVisitPercentage> _RepeatVisitPercentage;
        #endregion
        #region Constructor
        public DashboardService(
           
            UnitOfWork<ViewTodayVisit> viewTodayVisit,
            IMapper mapper,
            UnitOfWork<RepeatVisitPercentage> repeatVisitPercentage)
        {
            _mapper = mapper;
            _ViewTodayVisit = viewTodayVisit;
            _RepeatVisitPercentage = repeatVisitPercentage;
        }
        #endregion
        #region CUD Opertations
        #endregion
        #region Read Operations

        public async Task<List<GetUserVisitsDTO>> GetCurrentMonthVisits()
        {
            using (var db = new MeasallContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("GetCurrentMonthVisits", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                    await Task.Run(() => da.Fill(ds));

                    List<GetUserVisitsDTO> lst = ds.Tables[0].ToList<GetUserVisitsDTO>();

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
        public async Task<List<LastVisitListDTO>> Get_HF_LastVisit(string hfmiscode)
        {
            using (var db = new MeasallContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("Get_HF_LastVisit", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@hfmiscode", hfmiscode);
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                    await Task.Run(() => da.Fill(ds));

                    List<LastVisitListDTO> lst = ds.Tables[0].ToList<LastVisitListDTO>();

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
      

        public async Task<List<UserComplianceCoverageDTO>> GetMEACoverageCompliance()
        {
            using (var db = new MeasallContext())  
            {
                var conn = db.Database.GetDbConnection();  
                try
                {
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("GetUserVisitsComplianceCoverage", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                    await Task.Run(() => da.Fill(ds));

                    List<UserComplianceCoverageDTO> lst = ds.Tables[0].ToList<UserComplianceCoverageDTO>();

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
        public async Task<List<ViewTodayVisit>> GetTodaysVisitDetails(int hft)
        {
            using (var db = new MeasallContext())
            {
                var connection = db.Database.GetDbConnection();
                try
                {
                    await connection.OpenAsync();

                    var visits = db.ViewTodayVisits.AsQueryable();

                    var count = visits.Count();

                    return visits.ToList();

                    ;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<List<DashboardCountsDTO>> GetDashboardCounts()
        {
            using (var db = new MeasallContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("GetDashboardCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = sqlComm
                    };

                    await Task.Run(() => da.Fill(ds));

                    List<DashboardCountsDTO> lst = ds.Tables[0].ToList<DashboardCountsDTO>();

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

       
        public async Task<GetMEAsCoverageAndComplianceDTO> GetMEAsCoverageAndCompliance(SearchDashboardDTO search)
        {
            DateTime date = new DateTime(search.year, search.month, 1);

            string monthName = date.ToString("MMMM");
            //string monthName = DateTime.Now.ToString("MMMM");
            string compliancePercent = "";
            var complaincePercentage = _RepeatVisitPercentage.Repository.GetALL(x => x.IsActive == true && x.Year == search.year.ToString() && x.Month == monthName).FirstOrDefault();
            if (complaincePercentage != null)
            {
                compliancePercent = (100 + complaincePercentage.RepeatVisitPercent).ToString() + "%";
            }
            else
            {
                compliancePercent = "100%";
            }
            List<UserComplianceCoverageDTO> coverageComplianceList = await GetMEACoverageCompliance();
            GetMEAsCoverageAndComplianceDTO response = new GetMEAsCoverageAndComplianceDTO
            {
                CompliancePercentage = compliancePercent,
                CoverageComplianceList = coverageComplianceList
            };

            return response;
        }
        #endregion
        #region Helper
        #endregion
    }




}
