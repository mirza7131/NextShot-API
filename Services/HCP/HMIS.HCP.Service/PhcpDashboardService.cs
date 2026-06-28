using AppCommonMethods;
using AutoMapper;
using AutoMapper.Internal;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.HCP.Domain.Models.OldDbModels;
using HMIS.HCP.Domain.Models.OldDto;
using HMIS.HCP.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HMIS.HCP.Service
{
    public class PhcpDashboardService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<TblPatient> _uowTblPatient;

        #endregion

        #region Constructor

        public PhcpDashboardService(TokenService tokenService, UnitOfWork<TblPatient> uowTblPatient, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowTblPatient = uowTblPatient;
        }

        #endregion

        //public async Task<DataSet> GetTotalPatientCount()
        //{
        //    using (var db = new PhcpContext())
        //    {
        //        var options = new JsonSerializerOptions
        //        {
        //            ReferenceHandler = ReferenceHandler.Preserve,
        //            Converters = { new TypeConverter(), new CultureInfoConverter() }
        //        };
        //        var conn = db.Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("sp_getHCPDashboardCounts", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));

        //            // Serialize the dataset to JSON with the configured options
        //            var jsonString = JsonSerializer.Serialize(ds, options);

        //            // Use the deserialized dataset for further processing if needed
        //            var deserializedDataSet = JsonSerializer.Deserialize<DataSet>(jsonString, options);


        //            //List<PatientModel> lst = ds.Tables[0].ToList<PatientModel>();
        //            //List<PatientModel1> lst1 = ds.Tables[1].ToList<PatientModel1>();
        //            ////var abc= lst.AddRange(lst1);
        //            //var list = lst.ToList();
        //            //var list1 = lst1.ToList();
        //            //var newlist = list.Concat(list1).ToList();
        //            return ds;

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

        public async Task<List<PatientRegistrationModel>> GetPatientRegistrationCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_getPatientRegistrationCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientRegistrationModel> lst = ds.Tables[0].ToList<PatientRegistrationModel>();
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
        public async Task<List<VaccinationAdministeredModel>> GetVaccinationAdministeredCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetVaccinationAdministeredCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<VaccinationAdministeredModel> lst = ds.Tables[0].ToList<VaccinationAdministeredModel>();
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
        //Sample Received Card (Accepted and Rejected Sample Count)
        public async Task<List<SampleReceivedACCnREJCountModel>> GetSampleReceivedCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSampleReceivedCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SampleReceivedACCnREJCountModel> lst = ds.Tables[0].ToList<SampleReceivedACCnREJCountModel>();
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
        ////Sample Received Card (InProcess Sample Count)
        //public async Task<List<SampleReceivedInProcessCountModel>> GetSampleReceivedInProcessCount()
        //{
        //    using (var db = new PhcpContext())
        //    {
        //        var conn = db.Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("sp_GetSampleReceivedInProcessCount", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<SampleReceivedInProcessCountModel> lst = ds.Tables[0].ToList<SampleReceivedInProcessCountModel>();
        //            return lst.ToList();

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
        //public async Task<List<HCVSampleProcessDetectnReSampleModel>> GetHCVSampleProcessDetectnNotDetect()
        //{
        //    using (var db = new PhcpContext())
        //    {
        //        var conn = db.Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("sp_GetHCVSampleProcessDetectnReSample", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<HCVSampleProcessDetectnReSampleModel> lst = ds.Tables[0].ToList<HCVSampleProcessDetectnReSampleModel>();
        //            return lst.ToList();

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
        public async Task<List<HCVSampleProcessNotDetectedModel>> GetHCVSampleProcessNotDetected(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHCVSampleProcessNotDetected", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HCVSampleProcessNotDetectedModel> lst = ds.Tables[0].ToList<HCVSampleProcessNotDetectedModel>();
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
        public async Task<List<HBVSampleProcessedModel>> GetHBVSampleProcessed(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHBVSampleProcessed", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HBVSampleProcessedModel> lst = ds.Tables[0].ToList<HBVSampleProcessedModel>();
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

        public async Task<List<HCVEnrolledInTreatmentModel>> GetHCVEnrolledInTreatment(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHCVEnrolledInTreatment", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HCVEnrolledInTreatmentModel> lst = ds.Tables[0].ToList<HCVEnrolledInTreatmentModel>();
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
        //public async Task<List<GetHCVEnrolledInTreatment_SRModel>> GetHCVEnrolledInTreatment_SR()
        //{
        //    using (var db = new PhcpContext())
        //    {
        //        var conn = db.Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("sp_GetHCVEnrolledInTreatment_SR", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<GetHCVEnrolledInTreatment_SRModel> lst = ds.Tables[0].ToList<GetHCVEnrolledInTreatment_SRModel>();
        //            return lst.ToList();

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
        public async Task<List<RelapsednCuredCountsModel>> GetRelapsednCuredCounts(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetRelapsednCuredCounts", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<RelapsednCuredCountsModel> lst = ds.Tables[0].ToList<RelapsednCuredCountsModel>();
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
        public async Task<List<EligibleForSVRCountModel>> GetEligibleForSVRCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetEligibleForSVRCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<EligibleForSVRCountModel> lst = ds.Tables[0].ToList<EligibleForSVRCountModel>();
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
        public async Task<List<SampleCollectedSVRModel>> GetSampleCollectedSVRCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSampleCollectedSVRCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SampleCollectedSVRModel> lst = ds.Tables[0].ToList<SampleCollectedSVRModel>();
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
        public async Task<List<SVRSampleProcessedModel>> GetSampleProcessedSVRCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSampleProcessedSVRCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SVRSampleProcessedModel> lst = ds.Tables[0].ToList<SVRSampleProcessedModel>();
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
        public async Task<List<HBVTelbuvidineTreatmentCountModel>> GetHBVTreatmentCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHBVTreatmentCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HBVTelbuvidineTreatmentCountModel> lst = ds.Tables[0].ToList<HBVTelbuvidineTreatmentCountModel>();
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
        //public async Task<List<HBVTenofovirTreatmentCountModel>> GetHBVTenofovirTreatmentCount()
        //{
        //    using (var db = new PhcpContext())
        //    {
        //        var conn = db.Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("sp_GetHBVTenofovirTreatmentCount", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<HBVTenofovirTreatmentCountModel> lst = ds.Tables[0].ToList<HBVTenofovirTreatmentCountModel>();
        //            return lst.ToList();

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
        //public async Task<List<HBVEntecavirTreatmentCountModel>> GetHBVEntecavirTreatmentCount()
        //{
        //    using (var db = new PhcpContext())
        //    {
        //        var conn = db.Database.GetDbConnection();
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            SqlCommand sqlComm = new SqlCommand("sp_GetHBVEntecavirTreatmentCount", (SqlConnection)conn);
        //            sqlComm.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));
        //            List<HBVEntecavirTreatmentCountModel> lst = ds.Tables[0].ToList<HBVEntecavirTreatmentCountModel>();
        //            return lst.ToList();

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
        //public async Task<List<PatientsListCountHFWiseModel>> GetPatientsListCountHFWise(DashboardFilter filter)
        public async Task<List<PatientsListCountHFWiseModel>> GetPatientsListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetPatientsListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientsListCountHFWiseModel> lst = ds.Tables[0].ToList<PatientsListCountHFWiseModel>();
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
        public async Task<List<ScreeningPatientsListCountHFWiseModel>> GetScreeningPatientsListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetScreeningPatientsListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ScreeningPatientsListCountHFWiseModel> lst = ds.Tables[0].ToList<ScreeningPatientsListCountHFWiseModel>();
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
        public async Task<List<VaccinatedPatientsListCountHFWiseModel>> GetVaccinatedPatientsListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetVaccinatedPatientsListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<VaccinatedPatientsListCountHFWiseModel> lst = ds.Tables[0].ToList<VaccinatedPatientsListCountHFWiseModel>();
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
        public async Task<List<SampleReceivedACCnREJListCountHFWiseModel>> GetSampleReceivedACCnREJListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSampleReceivedACCnREJListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SampleReceivedACCnREJListCountHFWiseModel> lst = ds.Tables[0].ToList<SampleReceivedACCnREJListCountHFWiseModel>();
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
        public async Task<List<HCVSampleProcessedListCountHFWiseModel>> GetHCVSampleProcessedListCountHFWise([FromQuery] DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHCVSampleProcessedListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HCVSampleProcessedListCountHFWiseModel> lst = ds.Tables[0].ToList<HCVSampleProcessedListCountHFWiseModel>();
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
        public async Task<List<HBVSampleProcessedListCountHFWiseModel>> GetHBVSampleProcessedListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHBVSampleProcessedListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HBVSampleProcessedListCountHFWiseModel> lst = ds.Tables[0].ToList<HBVSampleProcessedListCountHFWiseModel>();
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
        public async Task<List<GetSVRSampleCollectedListCountHFWiseModel>> GetSVRSampleCollectedListCountHFWise([FromQuery] DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSVRSampleCollectedListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetSVRSampleCollectedListCountHFWiseModel> lst = ds.Tables[0].ToList<GetSVRSampleCollectedListCountHFWiseModel>();
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
        public async Task<List<SVRSampleProcessedListCountHFWiseModel>> GetSVRSampleProcessedListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSVRSampleProcessedListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SVRSampleProcessedListCountHFWiseModel> lst = ds.Tables[0].ToList<SVRSampleProcessedListCountHFWiseModel>();
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
        public async Task<List<CurednRelapseListCountHFWiseModel>> GetCurednRelapseListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetCurednRelapseListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<CurednRelapseListCountHFWiseModel> lst = ds.Tables[0].ToList<CurednRelapseListCountHFWiseModel>();
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
        public async Task<List<EligibleForSVRListCountHFWiseModel>> GetEligibleForSVRListCountHFWise(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetEligibleForSVRListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<EligibleForSVRListCountHFWiseModel> lst = ds.Tables[0].ToList<EligibleForSVRListCountHFWiseModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetRegPatientsWithPatientTypeLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetRegPatientsWithPatientTypeLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                        sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetScreenedPatientsLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_getScreenedPatientsLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetVaccinatedPatientsLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetVaccinatedPatientsLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetTotalSampleReceivedLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetTotalSampleReceivedLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetHCVSampleProcessedLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHCVSampleProcessedLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetHBVSampleProcessedLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHBVSampleProcessedLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<GetRegPatientsWithPatientTypeLLModel>> GetSVRSampleLL(RegisteredPatientDto filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetSVRSampleLL", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientType", filter.PatientType);
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetRegPatientsWithPatientTypeLLModel> lst = ds.Tables[0].ToList<GetRegPatientsWithPatientTypeLLModel>();
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
        public async Task<List<PatientHistoryModel>> GetPatientHistory(long PatientId)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetPatientHistory", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);
                    //sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.FacilityId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<PatientHistoryModel> lst = ds.Tables[0].ToList<PatientHistoryModel>();
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
        public async Task<ScreeningCountOfDistrictDto> GetScreeningCountOfDistrict(long DistrictId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("dbo.SPGetScreeningCountOfDistrict", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@DistrictId", DistrictId);

                    sqlComm.CommandTimeout = 600;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ScreeningCountOfDistrictDto> lst = ds.Tables[0].ToList<ScreeningCountOfDistrictDto>();
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
        public async Task<List<HCVEnrolledInTreatmentHfWiseCountModel>> GetHCVEnrolledInTreatmentHfWiseCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHCVEnrolledInTreatmentHfWiseCount", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HCVEnrolledInTreatmentHfWiseCountModel> lst = ds.Tables[0].ToList<HCVEnrolledInTreatmentHfWiseCountModel>();
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
        public async Task<List<HBVEnrolledInTreatmentHfWiseCountModel>> GetHBVEnrolledInTreatmentHfWiseCount(DashboardFilter filter)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("sp_GetHBVTreatmentCountListCountHFWise", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<HBVEnrolledInTreatmentHfWiseCountModel> lst = ds.Tables[0].ToList<HBVEnrolledInTreatmentHfWiseCountModel>();
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
        public async Task<object> GetPatientVitals(long PatientId)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    var PatientVitals = db.TblPatientVitals.Where(x=>x.Pid == PatientId).OrderBy(x=>x.Id).LastOrDefault();
                    return PatientVitals;

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
        public async Task<object> GetPatientAssessment(long PatientId)
        {
            using (var db = new PhcpContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    var PatientVitals = db.TblPatientAssessments.Where(x=>x.PatientId == PatientId).OrderBy(x=>x.Id).LastOrDefault();
                    return PatientVitals;

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
    }
    public class PatientRegistrationModel
    {
        public int? TotalPatients { get; set; }
        public int? NewPatientCount { get; set; }
        public int? PreDiagnosedPatient { get; set; }
        public int? TotalScreening { get; set; }
        public int? HCVPositive { get; set; }
        public int? HCVNegative { get; set; }
        public int? HBVPositive { get; set; }
        public int? HBVNegative { get; set; }
    }
    public class VaccinationAdministeredModel
    {
        public int? VaccinationFirstDose { get; set; }
        public int? VaccinationSecondDose { get; set; }
        public int? VaccinationThirdDose { get; set; }
    }
    public class SampleReceivedACCnREJCountModel
    {
        public int? AcceptedSample { get; set; }
        public int? RejectedSample { get; set; }
        public int? InProcessSample { get; set; }
    }
    //public class SampleReceivedInProcessCountModel
    //{
    //    public int? InProcessSample { get; set; }
    //}
    //public class HCVSampleProcessDetectnReSampleModel
    //{
    //    public int? TotalDetectedSampleHCV { get; set; }
    //    public int? TotalResampleHCV { get; set; }
    //}
    public class HCVSampleProcessNotDetectedModel
    {
        public int? TotalDetectedSampleHCV { get; set; }
        public int? TotalNotDetectedHCV { get; set; }
        public int? TotalResampleHCV { get; set; }
    }
    public class HBVSampleProcessedModel
    {
        public int? TotalDetectedSampleHBV { get; set; }
        public int? TotalNotDetectedHBV { get; set; }
        public int? TotalResampleHBV { get; set; }
    }
    public class HCVEnrolledInTreatmentModel
    {
        public int? HCVsd12 { get; set; }
        public int? HCVsrd24 { get; set; }
        public int? HCVsr24 { get; set; }
    }
    //public class GetHCVEnrolledInTreatment_SRModel
    //{
    //    public int? HCVsr24 { get; set; }
    //}
    public class RelapsednCuredCountsModel
    {
        public int? RelapsedPatient { get; set; }
        public int? CuredPatient { get; set; }
    }
    public class EligibleForSVRCountModel
    {
        public int? EligibleForSVR { get; set; }
    }
    public class SampleCollectedSVRModel
    {
        public int? SVRSampleCollected { get; set; }
    }
    public class SVRSampleProcessedModel
    {
        public int? SVRSampleProcessed { get; set; }
    }
    public class HBVTelbuvidineTreatmentCountModel
    {
        public int? HBVTelbuvidine { get; set; }
        public int? HBVTenofovir { get; set; }
        public int? HBVEntecavir { get; set; }
    }
    //public class HBVTenofovirTreatmentCountModel
    //{
    //    public int? HBVTenofovir { get; set; }
    //}
    //public class HBVEntecavirTreatmentCountModel
    //{
    //    public int? HBVEntecavir { get; set; }
    //}
    public class PatientsListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? TotalRegisteredPatients { get; set; }
        public int? NewPatientCount { get; set; }
        public int? PreDiagnosedPatient { get; set; }
    }
    public class ScreeningPatientsListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? HCVPositive { get; set; }
        public int? HCVNegative { get; set; }
        public int? HBVPositive { get; set; }
        public int? HBVNegative { get; set; }
    }
    public class VaccinatedPatientsListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? VaccinationFirstDose { get; set; }
        public int? VaccinationSecondDose { get; set; }
        public int? VaccinationThirdDose { get; set; }
    }
    public class SampleReceivedACCnREJListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? AcceptedSample { get; set; }
        public int? RejectedSample { get; set; }
        public int? InProcessSample { get; set; }
    }
    public class HCVSampleProcessedListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? DetectedSampleHCV { get; set; }
        public int? NotDetectedHCV { get; set; }
        public int? ResampleHCV { get; set; }
    }
    public class HBVSampleProcessedListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? DetectedSampleHBV { get; set; }
        public int? NotDetectedHBV { get; set; }
        public int? ResampleHBV { get; set; }
    }
    public class GetSVRSampleCollectedListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? SVRSampleCollected { get; set; }
    }
    public class SVRSampleProcessedListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? SVRSampleProcessed { get; set; }
    }
    public class CurednRelapseListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? RelapsedPatient { get; set; }
        public int? CuredPatient { get; set; }
    }
    public class EligibleForSVRListCountHFWiseModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? EligibleForSVR { get; set; }
    }
    public class HCVEnrolledInTreatmentHfWiseCountModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? HCVsr24 { get; set; }
        public int? HCVsd12 { get; set; }
        public int? HCVsrd24 { get; set; }
    }
    public class HBVEnrolledInTreatmentHfWiseCountModel
    {
        public string? division_name { get; set; }
        public string? district_name { get; set; }
        public string? tehsil_name { get; set; }
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? HBVEntecavir { get; set; }
        public int? HBVTenofovir { get; set; }
        public int? HBVTelbuvidine { get; set; }
    }
    public class GetRegPatientsWithPatientTypeLLModel
    {
        public int? id { get; set; }
        public string? hf_name { get; set; }
        public int? PatientId { get; set; }
        public string? mrn_no { get; set; }
        public string? patient_name { get; set; }
        public Single? patient_age { get; set; }
        public string? gender_name { get; set; }
        public string? self_cnic { get; set; }
    }
    public class ScreeningCountOfDistrictDto
    {
        public int? HCVScreenedNegative { get; set; }
        public int? HBVScreenedNegative { get; set; }
        public int? HCVScreenedPositive { get; set; }
        public int? HBVScreenedPositive { get; set; }
        public int? TodayScreened { get; set; }
    }
    public class PatientHistoryModel
    {
        public int? id { get; set; }
        public string? passport { get; set; }
        public string? is_edit_assessment { get; set; }
        public string? patient_from { get; set; }
        public string? is_type_change { get; set; }
        public string? is_medicine_disburs_form_submitted { get; set; }
        public string? mrn_no { get; set; }
        public string? is_old_regime { get; set; }
        public string? is_old_regime_tcs { get; set; }
        public int? no_of_medicine_delivered { get; set; }
        public string? medicine_delivery_status { get; set; }
        public string? reg_no { get; set; }
        public string? patient_type { get; set; }
        public string? patient_name { get; set; }
        public string? father_name { get; set; }
        //public string? patient_address { get; set; }
        public string? self_cnic { get; set; }
        public string? next_of_kin_cnic { get; set; }
        public Single? patient_age { get; set; }
        public string? gender { get; set; }
        public string? previous_hbv { get; set; }
        public DateTime? registration_date { get; set; }
        public int? next_status { get; set; }
        public string? is_conseled_n_closed { get; set; }
        public string? is_closed { get; set; }
        public string? collect_sample { get; set; }
        public string? vaccinate { get; set; }
        public string? is_refered { get; set; }
        public string? is_sample { get; set; }
        public string? is_vacinate { get; set; }
        public string? is_treatment { get; set; }
        public string? is_assesment { get; set; }
        public string? is_discharge { get; set; }
        public string? is_vital { get; set; }
        public string? is_register { get; set; }
        public string? relation { get; set; }
        public string? contact_no_self { get; set; }
        public string? is_terminate { get; set; }
        public string? pat_transit_bit { get; set; }
        public string? is_gi_referred { get; set; }
        public string? is_gi_received { get; set; }
        public string? stage { get; set; }
        public int? hcv_medicine_duration { get; set; }
        public int? hbv_medicine_duration { get; set; }
        public DateTime? hcv_frist_medicine_date { get; set; }
        public int? no_of_doses_taken { get; set; }
        public string? hbv_baseline_stage { get; set;}
        public string? is_patient_transfer_status { get; set; }
        public string? ex_hospital_name { get; set; }
        public string? hcv_baseline_stage { get; set; }
        public string? is_legacy_svr { get; set; }
        public string? is_svr_form_submitted { get; set; }
        public string? is_svr_sample { get; set; }
        public DateTime? first_baseline_date { get; set; }
        public DateTime? last_follow_up_date { get; set; }
        public string? hbv_screening_result { get; set; }
        public string? hcv_screening_result { get; set; }
        public string? previous_hcv { get; set; }
        public string? rapid_testing { get; set; }
        public string? close_case { get; set; }
        public string? is_legacy_data { get; set; }
        public string? is_follow_up_on { get; set; }
        public string? completed_vacination_hbv { get; set; }
        public int? no_of_followups { get; set; }
        public int? no_of_hbv_medicine_delivered { get; set; }
        public int? xFactor { get; set; }
        public int? yFactor { get; set; }
        public int? zFactor { get; set; }
        public string? pcr_confirmation_hbv { get; set; }
        public string? pcr_confirmation_hcv { get; set; }
        public int? no_of_hcv_medicine_delivered { get; set; }
        public int? no_of_hcv_followups { get; set; }
        public int? no_of_hbv_followups { get; set; }
        public int? aFactor { get; set; }
        public int? bFactor { get; set; }
        public int? cFactor { get; set; }
        public int? dFactor { get; set; }
        public int? eFactor { get; set; }
        public int? fFactor { get; set; }
        public DateTime? HCV_first_medicine_delivery { get; set; }
        public DateTime? HBV_first_medicine_delivery { get; set; }
        public DateTime? patient_dob { get; set; }
        public string? finger_print1 { get; set; }
        public string? finger_print2 { get; set; }
        public string? patient_stage { get; set; }
        public int? lost_followup_id { get; set; }
        public string? Reason { get; set; }
        public string? is_pregnant { get; set; }
        public int? marital_status { get; set; }
        public string? is_annual_pcr { get; set; }
        public int? user_id { get; set; }
        public string? user_hospital_name { get; set; }
}
}
