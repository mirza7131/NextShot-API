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

namespace CRReports.Services
{
    public class DashboardService
    {
        ReplicationHmisEntities entities = new ReplicationHmisEntities();
        public List<PatientDetaildto> GetPatientCrystalReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            if (filter.listType == "patientInQueue" || filter.listType == "InQueuelist")
            {
                filter.listType = "In Queue";
                StoreProcedureName = "SPGetInQueueListPharmacy";

            }
            else if (filter.listType == "DoctorQueueList")
            {
                filter.listType = "In Queue";
                StoreProcedureName = "SPDocInQueuePatient";
                var dbuser = entities.Users.Where(x => x.UserId.ToString() == filter.User.FirstOrDefault().ToString()).FirstOrDefault();
                if (dbuser != null)
                {
                    filter.DepartmentId = dbuser.DepartmentId;
                    filter.SectionId = dbuser.SectionId;
                    filter.User = null;
                }
            }
            else if (filter.listType == "PrescriptionIssued" || filter.listType == "Prescribed" || filter.listType == "MyPrescriptionIssued")
            {
                filter.listType = "Perscribed Patient Report";
                StoreProcedureName = "SPPatientPerscribed";
            }
            else if (filter.listType == "InternalPharmacy" || filter.listType == "MyInternalPharmacy")
            {
                filter.listType = "Doctor Internal Pharmacy Patient Report";
                StoreProcedureName = "SPPatientInternalPharmacy";
            }
            else if (filter.listType == "ExternalPharmacy" || filter.listType == "MyExternalPharmacy")
            {
                filter.listType = "Doctor External Pharmacy Patient Report";
                StoreProcedureName = "SPPatientExternalPharmacy";
            }
            else if (filter.listType == "InternalExternalPharmacy" || filter.listType == "MyInternalExternalPharmacy")
            {
                filter.listType = "Doctor Internal & External Pharmacy Patient Report";
                StoreProcedureName = "SPPatientInternalExternalPharmacy";
            }
            else if (filter.listType == "TotalLab" || filter.listType == "Facility Total LAB Patient Report" || filter.listType == "MyTotalLab")
            {

                filter.listType = "Doctor Total LAB Patient Report";
                StoreProcedureName = "SPPatientTotalLab";

            }
            else if (filter.listType == "InternalLab" || filter.listType == "Facility LAB Patient Report" || filter.listType == "MyInternalLab")
            {
                filter.listType = "Doctor Facility LAB Patient Report";
                StoreProcedureName = "SPPatientInternalLab";

            }
            else if (filter.listType == "ExternalLab" || filter.listType == "External LAB Patient Report" || filter.listType == "MyExternalLab")
            {
                filter.listType = "Doctor External LAB Patient Report";
                StoreProcedureName = "SPPatientExternalLab";
            }
            else if (filter.listType == "InternalExternalLab" || filter.listType == "Facility &  External LAB Patient Report" || filter.listType == "MyInternalExternalLab")
            {
                filter.listType = "Doctor Facility &  External LAB Patient Report";
                StoreProcedureName = "SPPatientinternalExternalLab";
            }
            else if (filter.listType == "MyServed")
            {
                filter.listType = "Served Patient Report";
                StoreProcedureName = "SPPatientServed";

            }
            else if (filter.listType == "MedicineIssueList")
            {
                filter.listType = "Medicine Issue Report";
                StoreProcedureName = "SPGetMedicineIssueListPharmacy";

            }
            else if (filter.listType == "TokenIssue" || filter.listType == "OverAllReg")
            {
                filter.listType = "Token Issue/Registrations List";
                StoreProcedureName = "SPDashboardPatientTokenList";

            }
            else if (filter.listType == "Newregistration")
            {
                filter.listType = "New Registration List";
                StoreProcedureName = "SPNewRegistrationList";
            }
            else if (filter.listType == "reVisit")
            {
                filter.listType = "Re-Visit Report";
                StoreProcedureName = "SPRevistPatientList";
            }
            else if (filter.listType == "vitalrefered")
            {
                filter.listType = "Vital Refered Report";
                StoreProcedureName = "SPVitalPatientList";
            }
            else if (filter.listType == "vitalcollected")
            {
                filter.listType = "Vital Collected Report";
                StoreProcedureName = "SPVitalCollectedPatientList";
            }
            else if (filter.listType == "InternalLabVisitList")
            {
                filter.listType = "Test Recommended Patients";
                StoreProcedureName = "SPPatientInternalLab";
            }
            else if (filter.listType == "visit" || filter.listType == "registration")
            {
                filter.listType = "Token Issued/Registrations";
                StoreProcedureName = "SPTokenIssued";
            }
            else if (filter.listType == "DocHFInQueuelist")
            {
                filter.listType = "In-Queue";
                StoreProcedureName = "SPDocInQueuePatient";
            }
            //else if (model.listType == "SampleCollectedListLab")
            //{
            //    model.listType = "Sample Collected Patients";
            //    StoreProcedureName = "SPLabSampleCollected";
            //}
            //else if (model.listType == "InQueuelistLab")
            //{
            //    model.listType = "In Queue List";
            //    StoreProcedureName = "SPLabInQueue";
            //}
            //else if (model.listType == "ReportGernatedListLab")
            //{
            //    model.listType = "Report Generated Patients";
            //    StoreProcedureName = "SPLabReportGenerated";
            //}
            //else if (model.listType == "PendingReportListLab")
            //{
            //    model.listType = "Pendning Reports";
            //    StoreProcedureName = "SPLabReportPending";
            //}
            else
            {
                return null;
            }
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

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

        #region Registration Dashboard New
        public List<PatientDetaildto> getRegistrationDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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




        //public List<ViewTbPatientCounts> GetTbPatientsDiagnosed(DashboardFilter filter)
        //{
        //    using (var db = new ReplicationHmisEntities())
        //    {
        //        var conn = db.Database.Connection;
        //        try
        //        {
        //            var list = db.ViewTbPatientCounts.Where(x => x.IsConfirmed == false
        //                       || (filter.ProvinceId == null || x.ProvinceId == filter.ProvinceId)
        //                       || (filter.DivisionId == null || x.DivisionId == filter.DivisionId)
        //                       || (filter.DistrictId == null || x.DistrictId == filter.DistrictId)
        //                       || (filter.TehsilId == null || x.TehsilId == filter.TehsilId)
        //                       || (filter.DepartmentId == null || x.DepartementLookupId == filter.DepartmentId)
        //                       || (filter.StartDate == null || x.CreatedOn >= filter.StartDate)
        //                       || (filter.StartDate == null || x.CreatedOn <= filter.EndDate)
        //                       ).ToList();
        //             return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }

        //    }

        //}

        #region Vitals Dashboard New
        public List<PatientDetaildto> getVitalDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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
        #region Doctor Dashboard New
        public List<PatientDetaildto> getDoctorDashboardPatientAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            var dbuser = entities.Users.Where(x => x.UserId.ToString() == filter.User).FirstOrDefault();
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbuser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbuser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbuser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbuser.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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
        public List<PatientDetaildto> getDoctorDashboardHFPatientAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);
                    if (!string.IsNullOrEmpty(filter.DiseaseProfileId))
                        sqlComm.Parameters.AddWithValue("@DiseaseProfileId", filter.DiseaseProfileId);

                    if (!string.IsNullOrEmpty(filter.conditionType))
                        sqlComm.Parameters.AddWithValue("@conditionType", filter.conditionType);

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
        #region Pharmacy New
        public List<PatientDetaildto> getPharmacyDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region Lab New
        public List<PatientLabReportDetaildto> getLabDashboardTestAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientLabReportDetaildto objResponse = new PatientLabReportDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientLabReportDetaildto>(res, false).AsQueryable();
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
        public List<PatientLabReportDetaildto> getLabDashboardTestTimeFromStartTillNowAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientLabReportDetaildto objResponse = new PatientLabReportDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientLabReportDetaildto>(res, false).AsQueryable();
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
        #endregion
        public List<PatientLabReportDetaildto> GetPatientLabsCrystalReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            if (filter.listType == "InternalLabVisitList")
            {
                filter.listType = "Test Recommended Patients";
                StoreProcedureName = "SPLabRecommended";
            }
            else if (filter.listType == "SampleCollectedListLab")
            {
                filter.listType = "Sample Collected Patients";
                StoreProcedureName = "SPLabSampleCollected";
            }
            else if (filter.listType == "InQueuelistLab")
            {
                filter.listType = "In Queue List";
                StoreProcedureName = "SPLabInQueue";
            }
            else if (filter.listType == "ReportGernatedListLab")
            {
                filter.listType = "Report Generated Patients";
                StoreProcedureName = "SPLabReportGenerated";
            }
            else if (filter.listType == "PendingReportListLab")
            {
                filter.listType = "Pendning Reports";
                StoreProcedureName = "SPLabReportPending";
            }
            else
            {
                return null;
            }
            PatientLabReportDetaildto objResponse = new PatientLabReportDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientLabReportDetaildto>(res, false).AsQueryable();
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

        #region UHI Report
        public List<PatientDetaildto> getSehatSahulatCardDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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
        #region MedicineReportCounts
        public List<PatientLabReportDetaildto> MedicineIssuedCrystalReportMedicineWise(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientLabReportDetaildto objResponse = new PatientLabReportDetaildto();

            using (var db = new ReplicationHmisEntities())
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
                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientLabReportDetaildto>(res, false).AsQueryable();
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
        #endregion


        #region DrugAddicts Dashboard Listings
        public List<PatientDetailDrugAddictDTO> getReportDrugAddictsDashboardListings(DashboardFilter filter)
        {
            using (var db = new ReplicationHmisEntities())
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

                    sqlComm.CommandText = "da.SPDrugAddcictsDashboardListings";

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetailDrugAddictDTO>(res, false).AsQueryable();
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
        #endregion
        #region DrugAddicts Diseases Listings
        public List<PatientDetailDrugAddictDiseasesDTO> getReportDrugAddictsDiseasesListings(DashboardFilter filter)
        {
            using (var db = new ReplicationHmisEntities())
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

                    sqlComm.CommandText = "da.SPDrugAddcictsDashboardDieasesListings";

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetailDrugAddictDiseasesDTO>(res, false).AsQueryable();
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
        #endregion

        #region DrugAddicts Social Welfare Listings
        public List<PatientDetailDrugAddictSocialWelfareDTO> getReportDrugAddictsSocialWelfareListings(DashboardFilter filter)
        {
            using (var db = new ReplicationHmisEntities())
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

                    sqlComm.CommandText = "da.SPDrugAddcictsToSocialWelfareListings";

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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetailDrugAddictSocialWelfareDTO>(res, false).AsQueryable();
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
        #endregion

        #region IPD Doctor Dashboard
        public List<PatientDetaildto> getIPDDoctorDashboardPatientAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();

            var dbuser = entities.Users.Where(x => x.UserId.ToString() == filter.User).FirstOrDefault();
            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbuser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbuser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbuser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbuser.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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
        public List<PatientDetaildto> getIPDDoctorHFDashboardPatientAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region IPD Lab Dashboard

        public List<PatientLabReportDetaildto> getIPDLabDashboardTestAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientLabReportDetaildto objResponse = new PatientLabReportDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientLabReportDetaildto>(res, false).AsQueryable();
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
        public List<PatientLabReportDetaildto> getIPDLabDashboardTestTimeFromStartTillNowAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientLabReportDetaildto objResponse = new PatientLabReportDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    if (!string.IsNullOrEmpty(filter.User))
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientLabReportDetaildto>(res, false).AsQueryable();
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
        #endregion

        #region IPD Admission Dashboard 
        public List<PatientDetaildto> getIPDAdmissionDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region IPD Vitals Dashboard New
        public List<PatientDetaildto> getIPDVitalDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);
                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region Pharmacy New
        public List<PatientDetaildto> getIPDPharmacyDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region HCP Report
        public List<PatientDetaildto> getHCPDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region Paraplegic Report
        public List<PatientDetaildto> getParaplegicDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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
        public List<PatientDetaildto> getIndivisualOpdDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region Dental Report
        public List<PatientDetaildto> getDentalDashboardAllList(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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
        public List<PatientDetaildto> getDentalDashboardAllReportProcedures(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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

        #endregion

        #region PhysioTherapy Report
        public List<PatientDetaildto> getPhysioTherapyDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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
        #endregion


        #region Tb Dashboard Report

        public List<TbDashboardDTO> GetTbPatientsRegistered(DashboardFilter filter)
        {


            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
                "FROM ViewTbRegisteredPatient where 1=1";
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }
            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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


        public List<TbDashboardDTO> GetTbPatientsDiagnosed(DashboardFilter filter)
        {
            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
               "FROM ViewTbPatientCounts where 1=1";
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }
            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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


        public List<TbDashboardDTO> GetTbPatientsConfirmed(DashboardFilter filter)
        {
            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
                "FROM ViewTbPatientCounts where IsConfirmed = 1 ";
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }
            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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


        public List<TbDashboardDTO> GetTbPatientsPresumptive(DashboardFilter filter)
        {
            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
                "FROM ViewTbPatientCounts where IsConfirmed = 0 ";
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }
            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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


        public List<TbDashboardDTO> GetTbAdvisedTest(DashboardFilter filter)
        {
            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
              "FROM ViewAdviseLabTest where 1=1 ";

            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMTestAdvised))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.SSM + "'\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRTestAdvised))
            {
                sqlQuery += "\tand TestName =  '" + CommonStringConstant.CXR + "'\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.GeneXpertTestAdvised))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "'\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVTestAdvised))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.HIVScreening + "'\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMPending))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.SSM + "'\tand " + "IsReportGenerated is null\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRPending))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.CXR + "'\tand " + "IsReportGenerated is null\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.XpertPending))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "'\tand " + "IsReportGenerated is null\t";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVPending))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.HIVScreening + "'\tand " + "IsReportGenerated is null\t";
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }
            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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


        public List<TbDashboardDTO> GetTbTestResults(DashboardFilter filter)
        {
            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
              "FROM ViewLabTestResults where 1=1 ";

            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMPositive))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.SSM + "' and TestResultName = 'Result' and 'Result' = 'Positive For AFB' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.SSMNegative))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.SSM + "' and TestResultName = Result and 'Result' = 'Negative For AFB' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRPositive))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.CXR + "' and TestResultName = 'Suggestive For TB' and Result = 'Yes' "; ;
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.CXRNegative))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.CXR + "' and TestResultName = '" + CommonStringConstant.SuggestedForTB + "' and Result = '" + CommonStringConstant.No + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.XpertPositive))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "' and TestResultName = '" + CommonStringConstant.Result + "' and Result = '" + CommonStringConstant.MTBPositive + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.XpertNegative))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "' and TestResultName = '" + CommonStringConstant.Result + "' and Result = '" + CommonStringConstant.MTBNegative + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.RifampicinResistanceDetected))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "' and TestResultName = '" + CommonStringConstant.RifampicinResistant + "' and Result = '" + CommonStringConstant.MTBDetected + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.RifampicinResistanceNotDetected))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "' and TestResultName = '" + CommonStringConstant.RifampicinResistant + "' and Result = '" + CommonStringConstant.MTBNotDetected + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.Error))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.XPert + "' and TestResultName = '" + CommonStringConstant.Result + "' and Result = '" + CommonStringConstant.Error + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVReactive))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.HIVScreening + "' and TestResultName = '" + CommonStringConstant.HIVResult + "' and Result = '" + CommonStringConstant.Reactive + "' ";
            }
            if (filter.TbPatientTypeContstant.Contains(CommonStringConstant.HIVNonReactive))
            {
                sqlQuery += "\tand TestName = '" + CommonStringConstant.HIVScreening + "' and TestResultName = '" + CommonStringConstant.HIVResult + "' and Result = '" + CommonStringConstant.NonReactive + "' ";
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }

            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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


        public List<TbDashboardDTO> GetTbPatientsData(DashboardFilter filter)
        {
            if (filter.TbPatientTypeContstant == "Total Diagnose")
            {
                var responseObject = GetTbPatientsDiagnosed(filter);

                return responseObject;
            }
            else if (filter.TbPatientTypeContstant == "Total Confirmed Patients")
            {
                var responseObject = GetTbPatientsConfirmed(filter);

                return responseObject;
            }
            else if (filter.TbPatientTypeContstant == "Total Not Confirmed")
            {
                var responseObject = GetTbPatientsPresumptive(filter);

                return responseObject;
            }
            return null;

        }



        public List<TbDashboardDTO> GetTbIssuedMedicine(DashboardFilter filter)
        {

            string sqlQuery = "SELECT FullName,CNIC,MRNo,DivisionName,DistrictName,TehsilName,HealthFacilityName,CreatedOn " +
              "FROM ViewTbissuedMedicine where 1=1 ";
            if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
            {
                sqlQuery += "\tand ProvinceId = " + filter.ProvinceId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                sqlQuery += "\tand DivisionId = " + filter.DivisionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                sqlQuery += "\tand DistrictId = " + filter.DistrictId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                sqlQuery += "\tand TehsilId = " + filter.TehsilId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
            {
                sqlQuery += "\tand DepartmentId = " + filter.DepartmentId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
            {
                sqlQuery += "\tand SectionId = " + filter.SectionId;
            }
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                sqlQuery += "\tand HealthFacilityId = " + filter.HealthFacilityId;
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.StartDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) >= cast('" + filter.StartDate + "' as datetime)";
            }
            if (!AppCommonMethod.IsNullorEmptyDate(filter.EndDate))
            {
                sqlQuery += "\tand cast(CreatedOn as datetime) <= cast('" + filter.EndDate + "' as datetime)";
            }
            using (var db = new ReplicationHmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {
                    DataSet ds = new DataSet();
                    string cnnString = db.Database.Connection.ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand sqlComm = new SqlCommand(sqlQuery, cnn);
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<TbDashboardDTO>(res, false).AsQueryable();
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



        #endregion

        #region Speech Report
        public List<PatientDetaildto> getSpeechTherapyDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityTypeId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityTypeId", filter.HealthFacilityTypeId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.listType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

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

        #region Nutrition Report
        public List<PatientDetaildto> getNutritionDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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
        #endregion

        #region Psychology Report
        public List<PatientDetaildto> getPsychologyDashboardAllReport(DashboardFilter filter, string StoreProcedureName)
        {
            List<string> user = new List<string>();


            PatientDetaildto objResponse = new PatientDetaildto();

            using (var db = new ReplicationHmisEntities())
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
                        sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter.User.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));
                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", filter.User);
                    //add any parameters the stored procedure might require
                    cnn.Open();
                    var res = sqlComm.ExecuteReader();
                    var resultList = PropertyMapper.ToList<PatientDetaildto>(res, false).AsQueryable();
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
        #endregion
        #region Helper Function
        public string GetHealthFacilityName(DashboardFilter filter)
        {
            var healthFacilityName = "";
            if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
            {
                //var _uowUser = new UnitOfWork<HealthFacility>(uow.GetDbContext());
                healthFacilityName = entities.HealthFacility.Where(x => x.HealthFacilityId == filter.HealthFacilityId).FirstOrDefault().Name;

            }
            else if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
            {
                healthFacilityName = entities.Tehsil.Where(x => x.TehsilId == filter.TehsilId).FirstOrDefault().Name;
            }
            else if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
            {
                healthFacilityName = entities.District.Where(x => x.DistrictId == filter.DistrictId).FirstOrDefault().Name;
            }
            else if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
            {
                healthFacilityName = entities.Division.Where(x => x.DivisionId == filter.DivisionId).FirstOrDefault().Name;
            }
            else
            {
                healthFacilityName = healthFacilityName = entities.Province.Where(x => x.ProvinceId == 1).FirstOrDefault().Name;
            }
            return healthFacilityName;
        }



        #endregion
    }
}
