using CRReports.Common;
using CRReports.Data;
using CRReports.Models.Dto;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CommonMessages;
using System.Data.Entity;

namespace CRReports.Services
{
    public class EmcService
    {
        HmisEntities entities = new HmisEntities();
        public List<PatientDetaildto> GetPatientCrystalReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    //if (filter.listType == "Perscribed Patient Report" || filter.listType == "Facility Perscribed Patient Report")
                    //    StoreProcedureName = "SPPatientPerscribed";
                    //else if (filter.listType == "Served Patient Report")
                    //    StoreProcedureName = "SPPatientServed";
                    //else if (filter.listType == "Doctor Internal Pharmacy Patient Report" || filter.listType == "Facility Pharmacy Patient Report")
                    //    StoreProcedureName = "SPPatientInternalPharmacy";
                    //else if (filter.listType == "Doctor External Pharmacy Patient Report" || filter.listType == "External Pharmacy Patient Report")
                    //    StoreProcedureName = "SPPatientExternalPharmacy";

                    //else if (filter.listType == "" || filter.listType == "Facility & External Pharmacy Patient Report")
                    //    StoreProcedureName = "SPPatientInternalExternalPharmacy";

                    //else if (filter.listType == "Doctor Total LAB Patient Report" || filter.listType == "Facility Total LAB Patient Report")
                    //    StoreProcedureName = "SPPatientTotalLab";
                    //else if (filter.listType == "Doctor Facility LAB Patient Report" || filter.listType == "Facility LAB Patient Report")
                    //  StoreProcedureName = "SPPatientInternalLab";
                    //else if (filter.listType == "Doctor External LAB Patient Report" || filter.listType == "External LAB Patient Report")
                    //    StoreProcedureName = "SPPatientExternalLab";
                    //else if (filter.listType == "Doctor Facility &  External LAB Patient Report" || filter.listType == "Facility &  External LAB Patient Report")
                    //    StoreProcedureName = "SPPatientinternalExternalLab";

                    DataSet ds = new DataSet();


                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlComm.CommandText = StoreProcedureName;

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
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
                    var list = resultList.ToList();

                    return list;

                    //conn.Close();
                    //return list;
                    //ReportDocument rd = new ReportDocument();
                    //rd.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "UHIClaimsDetail.rpt"));
                    //rd.SetDataSource(list);
                    //rd.SetParameterValue("FromDate", filter.StartDate.Value.ToString("dd MMMM yyyy"));
                    //rd.SetParameterValue("ToDate", filter.EndDate.Value.ToString("dd MMMM yyyy"));
                    //filter.listType = "Perscribed Patient Report";

                    //var pdfStream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    //pdfStream.Seek(0, SeekOrigin.Begin);
                    //rd.Close();
                    //rd.Dispose();
                    //return File(pdfStream, "application/pdf", "PathologyDetail_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf");








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

        public List<MlePatientDto> GetMleSinglePatientReport(Guid PatientVisitId, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            MlePatientDto objResponse = new MlePatientDto();

            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    DataSet ds = new DataSet();


                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlComm.CommandText = StoreProcedureName;

                    sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);


                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<MlePatientDto>(res, false).AsQueryable();

                    var list = resultList.ToList();

                    return list;


                    //DataSet ds = new DataSet();
                    //SqlCommand sqlComm = new SqlCommand("mlc.GetMleSinglePatientReport", (SqlConnection)conn);
                    //sqlComm.CommandType = CommandType.StoredProcedure;
                    //sqlComm.Parameters.AddWithValue("@PatientId", patientId);

                    //SqlDataAdapter da = new SqlDataAdapter();
                    //da.SelectCommand = sqlComm;
                    //Task.Run(() => da.Fill(ds));
                    //List<MlePatientDto> lst = ds.Tables[0].ToList<MlePatientDto>();



                    //return lst;

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
        public DataSet GetDataFromStoredProcedure(Guid pid)
        {
            DataSet dataSet = new DataSet();

            try
            {
                var db = new HmisEntities();
                // Replace with your connection string
                string connectionString = db.Database.Connection.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("[mlc].[GetMleSinglePatientReport]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // If your stored procedure has parameters, you can add them here
                        command.Parameters.Add(new SqlParameter("@PatientId", pid));

                        // Create a data adapter to fill the dataset with the stored procedure's results
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as connection errors or invalid parameters
                // You can log or rethrow the exception as needed
            }

            return dataSet;
        }
        public List<MleSvPatientDto> GetMleSvSinglePatientReport(Guid PatientVisitId, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    DataSet ds = new DataSet();


                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlComm.CommandText = StoreProcedureName;

                    sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<MleSvPatientDto>(res, false).AsQueryable();
                    var list = resultList.ToList();

                    return list;

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

        public List<BirthCertificateDto> GetBirthCertificate(Guid patientVisitId, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    DataSet ds = new DataSet();


                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlComm.CommandText = StoreProcedureName;

                    sqlComm.Parameters.AddWithValue("@PatientVisitId", patientVisitId);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<BirthCertificateDto>(res, false).AsQueryable();
                    var list = resultList.ToList();

                    return list;

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
        public List<DeathCertificateDto> GetDeathCertificate(Guid patientVisitId, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    DataSet ds = new DataSet();


                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlComm.CommandText = StoreProcedureName;

                    sqlComm.Parameters.AddWithValue("@PatientVisitId", patientVisitId);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<DeathCertificateDto>(res, false).AsQueryable();
                    var list = resultList.ToList();

                    return list;

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

        //public async Task<GetSingleFitnessCertificateDtoWithQuestions> GetFitnessCertificate(Guid patientVisitId, string StoreProcedureName)
        //{
        //    List<string> user = new List<string>();

        //    using (var db = new HmisEntities())
        //    {
        //        var conn = db.Database.Connection;
        //        try
        //        {
        //            GetSingleFitnessCertificateDtoWithQuestions fitness = new GetSingleFitnessCertificateDtoWithQuestions();

        //            DataSet ds = new DataSet();


        //            string cnnString = db.Database.Connection.ConnectionString;
        //            SqlConnection cnn = new SqlConnection(cnnString);
        //            SqlCommand sqlComm = new SqlCommand();
        //            sqlComm.Connection = cnn;
        //            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

        //            sqlComm.CommandText = StoreProcedureName;

        //            sqlComm.Parameters.AddWithValue("@PatientVisitId", patientVisitId);

        //            cnn.Open();
        //            //var res = sqlComm.ExecuteReader();
        //            //var resultList = PropertyMapper.ToList<FitnessCertificateDto>(res, false).AsQueryable();
        //            //var list = await resultList.ToListAsync();

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = sqlComm;
        //            await Task.Run(() => da.Fill(ds));

        //            fitness.fitnessSingleRecord = ds.Tables[0].ToList<FitnessCertificateDto>();
        //            fitness.PsychologicalQuestionsAndAnswer = ds.Tables[1].ToList<PsychologyDto>();

        //            return fitness;

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

        public async Task<GetSingleFitnessCertificateDtoWithQuestions> GetFitnessCertificate(Guid patientVisitId, string StoreProcedureName)
        {
            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    GetSingleFitnessCertificateDtoWithQuestions fitness = new GetSingleFitnessCertificateDtoWithQuestions();

                    // Create DataSet to hold stored procedure result
                    DataSet ds = new DataSet();

                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlComm.CommandText = StoreProcedureName;
                    sqlComm.Parameters.AddWithValue("@PatientVisitId", patientVisitId);

                    cnn.Open();

                    // Create DataAdapter to fill DataSet
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    // Convert FitnessCertificateDto data
                    fitness.fitnessSingleRecord = ds.Tables[0].ToList<FitnessCertificateDto>();

                    // Convert PsychologicalQuestionsAndAnswer to DataTable
                    DataTable psychologyDataTable = ds.Tables[1];

                    // Assign DataTable to List<PsychologyDto>
                    List<PsychologyDto> psychologyList = new List<PsychologyDto>();
                    foreach (DataRow row in psychologyDataTable.Rows)
                    {
                        psychologyList.Add(new PsychologyDto
                        {
                            // Map properties from DataTable columns
                            Question = row["Question"].ToString(),
                            Answer = row["Answer"].ToString()
                            // Add more properties if needed
                        });
                    }

                    // Assign List<PsychologyDto> to fitness.PsychologicalQuestionsAndAnswer
                    fitness.PsychologicalQuestionsAndAnswer = psychologyList;

                    return fitness;
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

        public List<IcvCertificateDto> GetIcvCertificate(Guid patientVisitId, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    DataSet ds = new DataSet();


                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = cnn;
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlComm.CommandText = StoreProcedureName;

                    sqlComm.Parameters.AddWithValue("@PatientVisitId", patientVisitId);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<IcvCertificateDto>(res, false).AsQueryable();
                    var list = resultList.ToList();

                    return list;

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
}
