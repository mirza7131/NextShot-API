
using CRReports.Common;
using CRReports.Data;
using CRReports.Models.Dto;
using CRReports.Services;
//using CRReports.Utilities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CRReports.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {
        DashboardService _dashboardReplicationService = new DashboardService();
        DashboardTransService _dashboardTransactionService = new DashboardTransService();


        #region Dashboard Registration New 
        [HttpPost]
        [ActionName("getRegistrationDashboardAllReport")]
        public HttpResponseMessage getRegistrationDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;

            string StoreProcedureName = "SPRegistrationDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getRegistrationDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Dashboard Vitals New 
        [HttpPost]
        [ActionName("getVitalDashboardAllReport")]
        public HttpResponseMessage getVitalDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPVitalDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getVitalDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion
        #region Old Dashboard
        [HttpPost]
        [ActionName("GetPatientCrystalReport")]
        public HttpResponseMessage GetPatientCrystalReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "";
            //if (model.listType == "patientInQueue" || model.listType == "InQueuelist")
            //{
            //    model.listType = "In Queue";
            //    StoreProcedureName = "SPGetInQueueListPharmacy";

            //}
            //else if (model.listType == "DoctorQueueList")
            //{
            //    model.listType = "In Queue";
            //    StoreProcedureName = "SPDocInQueuePatient";
            //    var dbuser = entities.Users.Where(x => x.UserId.ToString() == model.User.FirstOrDefault().ToString()).FirstOrDefault();
            //    if (dbuser != null)
            //    {
            //        model.DepartmentId = dbuser.DepartmentId;
            //        model.SectionId = dbuser.SectionId;
            //        model.User = null;
            //    }
            //}
            //else if (model.listType == "PrescriptionIssued" || model.listType == "Prescribed" || model.listType == "MyPrescriptionIssued")
            //{
            //    model.listType = "Perscribed Patient Report";
            //    StoreProcedureName = "SPPatientPerscribed";
            //}
            //else if (model.listType == "InternalPharmacy" || model.listType == "MyInternalPharmacy")
            //{
            //    model.listType = "Doctor Internal Pharmacy Patient Report";
            //    StoreProcedureName = "SPPatientInternalPharmacy";
            //}
            //else if (model.listType == "ExternalPharmacy" || model.listType == "MyExternalPharmacy")
            //{
            //    model.listType = "Doctor External Pharmacy Patient Report";
            //    StoreProcedureName = "SPPatientExternalPharmacy";
            //}
            //else if (model.listType == "InternalExternalPharmacy" || model.listType == "MyInternalExternalPharmacy")
            //{
            //    model.listType = "Doctor Internal & External Pharmacy Patient Report";
            //    StoreProcedureName = "SPPatientInternalExternalPharmacy";
            //}
            //else if (model.listType == "TotalLab" || model.listType == "Facility Total LAB Patient Report" || model.listType == "MyTotalLab")
            //{

            //    model.listType = "Doctor Total LAB Patient Report";
            //    StoreProcedureName = "SPPatientTotalLab";

            //}
            //else if (model.listType == "InternalLab" || model.listType == "Facility LAB Patient Report" || model.listType == "MyInternalLab")
            //{
            //    model.listType = "Doctor Facility LAB Patient Report";
            //    StoreProcedureName = "SPPatientInternalLab";

            //}
            //else if (model.listType == "ExternalLab" || model.listType == "External LAB Patient Report" || model.listType == "MyExternalLab")
            //{
            //    model.listType = "Doctor External LAB Patient Report";
            //    StoreProcedureName = "SPPatientExternalLab";
            //}
            //else if (model.listType == "InternalExternalLab" || model.listType == "Facility &  External LAB Patient Report" || model.listType == "MyInternalExternalLab")
            //{
            //    model.listType = "Doctor Facility &  External LAB Patient Report";
            //    StoreProcedureName = "SPPatientinternalExternalLab";
            //}
            //else if (model.listType == "MyServed")
            //{
            //    model.listType = "Served Patient Report";
            //    StoreProcedureName = "SPPatientServed";

            //}
            //else if (model.listType == "MedicineIssueList")
            //{
            //    model.listType = "Medicine Issue Report";
            //    StoreProcedureName = "SPGetMedicineIssueListPharmacy";

            //}
            //else if (model.listType == "TokenIssue" || model.listType == "OverAllReg")
            //{
            //    model.listType = "Token Issue/Registrations List";
            //    StoreProcedureName = "SPDashboardPatientTokenList";

            //}
            //else if (model.listType == "Newregistration")
            //{
            //    model.listType = "New Registration List";
            //    StoreProcedureName = "SPNewRegistrationList";
            //}
            //else if (model.listType == "reVisit")
            //{
            //    model.listType = "Re-Visit Report";
            //    StoreProcedureName = "SPRevistPatientList";
            //}
            //else if (model.listType == "vitalrefered")
            //{
            //    model.listType = "Vital Refered Report";
            //    StoreProcedureName = "SPVitalPatientList";
            //}
            //else if (model.listType == "vitalcollected")
            //{
            //    model.listType = "Vital Collected Report";
            //    StoreProcedureName = "SPVitalCollectedPatientList";
            //}
            //else if (model.listType == "InternalLabVisitList")
            //{
            //    model.listType = "Test Recommended Patients";
            //    StoreProcedureName = "SPPatientInternalLab";
            //}
            //else if (model.listType == "visit" || model.listType == "registration")
            //{
            //    model.listType = "Token Issued/Registrations";
            //    StoreProcedureName = "SPTokenIssued";
            //}
            //else if (model.listType == "DocHFInQueuelist")
            //{
            //    model.listType = "In-Queue";
            //    StoreProcedureName = "SPDocInQueuePatient";
            //}
            ////else if (model.listType == "SampleCollectedListLab")
            ////{
            ////    model.listType = "Sample Collected Patients";
            ////    StoreProcedureName = "SPLabSampleCollected";
            ////}
            ////else if (model.listType == "InQueuelistLab")
            ////{
            ////    model.listType = "In Queue List";
            ////    StoreProcedureName = "SPLabInQueue";
            ////}
            ////else if (model.listType == "ReportGernatedListLab")
            ////{
            ////    model.listType = "Report Generated Patients";
            ////    StoreProcedureName = "SPLabReportGenerated";
            ////}
            ////else if (model.listType == "PendingReportListLab")
            ////{
            ////    model.listType = "Pendning Reports";
            ////    StoreProcedureName = "SPLabReportPending";
            ////}
            //else
            //{
            //    return null;
            //}
            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetPatientCrystalReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("GetPatientLabsCrystalReport")]
        public HttpResponseMessage GetPatientLabsCrystalReport(DashboardFilter model)
        {

            if (model == null)
                return null;

            string StoreProcedureName = "";
            
            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "PatientLab_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetPatientLabsCrystalReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region Doctor Dashboard New 
        [HttpPost]
        [ActionName("getDoctorDashboardPatientAllReport")]
        public HttpResponseMessage getDoctorDashboardPatientAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPDoctorDashboardPatientAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            //string reportFileName = "PatientReport.rpt";
            string reportFileName = "DoctorWisePatientReport.rpt";

            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getDoctorDashboardPatientAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getDoctorDashboardHFPatientAllReport")]
        public HttpResponseMessage getDoctorDashboardHFPatientAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPDoctorDashboardPatientAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "DoctorWisePatientReport.rpt";   
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getDoctorDashboardHFPatientAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = "";
            if (model.listType == "DiseaseWiseCount")
            {
                ReportTitle = "Disease - " + model.conditionType;
            }
            else
            {
                ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Pharmacy Dashboard New 

        [HttpPost]
        [ActionName("getPharmacyDashboardAllReport")]
        public HttpResponseMessage getPharmacyDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPPharmacyDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getPharmacyDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Lab Dashboard New 

        [HttpPost]
        [ActionName("getLabDashboardTestAllReport")]
        public HttpResponseMessage getLabDashboardTestAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPLabDashboardTestAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getLabDashboardTestAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getLabDashboardTestTimeFromStartTillNowAllReport")]
        public HttpResponseMessage getLabDashboardTestTimeFromStartTillNowAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPLabDashboardTestTimeFromStartTillNowAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getLabDashboardTestTimeFromStartTillNowAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region UHI Reports
        [HttpPost]
        [ActionName("getSehatSahulatCardDashboardAllReport")]
        public HttpResponseMessage getSehatSahulatCardDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "dashboard.SPSehtSahulatCardDashboardList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getSehatSahulatCardDashboardAllReport(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            //if (!AppCommonMethod.IsNullorZeroInt(model.HealthFacilityId))
            //{
            //    healthFacilityName = entities.HealthFacility.Where(x => x.HealthFacilityId == model.HealthFacilityId).FirstOrDefault().Name;
            //}
            //else if (!AppCommonMethod.IsNullorZeroInt(model.ProvinceId))
            //{
            //    if (!AppCommonMethod.IsNullorZeroInt(model.DivisionId))
            //    {
            //        if (!AppCommonMethod.IsNullorZeroInt(model.DistrictId))
            //        {
            //            if (!AppCommonMethod.IsNullorZeroInt(model.TehsilId))
            //            {
            //                healthFacilityName = entities.Tehsil.Where(x => x.TehsilId == model.TehsilId).FirstOrDefault().Name;
            //            }
            //            else
            //            {
            //                healthFacilityName = entities.District.Where(x => x.DistrictId == model.DistrictId).FirstOrDefault().Name;
            //            }

            //        }
            //        else
            //        {
            //            healthFacilityName = entities.Division.Where(x => x.DivisionId == model.DivisionId).FirstOrDefault().Name;
            //        }

            //    }
            //    else
            //    {
            //        healthFacilityName = entities.Province.Where(x => x.ProvinceId == model.ProvinceId).FirstOrDefault().Name;

            //    }

            //}
            //else
            //{
            //    healthFacilityName = healthFacilityName = entities.Province.Where(x => x.ProvinceId == 1).FirstOrDefault().Name;
            //}
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion
        #region MedicineReportCounts
        [HttpPost]
        [ActionName("GetMedicineIssuedCrystalReportMedicineWise")]
        public HttpResponseMessage GetMedicineIssuedCrystalReportMedicineWise(DashboardFilter model)
        {

            if (model == null)
                return null;

            string StoreProcedureName = "SPMedicineIssuedCrystalReportMedicineWise";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "PatientLab_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).MedicineIssuedCrystalReportMedicineWise(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd MMMM yyyy  hh:mm"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region DrugAddicts Dashbaord Listings
        [HttpPost]
        [ActionName("getReportDrugAddictsDashboardListings")]
        public HttpResponseMessage getReportDrugAddictsDashboardListings(DashboardFilter model)
        {

            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "DrugAddictDashboardReport.rpt";
            string exportFilename = "DrugAddict_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getReportDrugAddictsDashboardListings(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region DrugAddicts  Dashbaord Diseases
        [HttpPost]
        [ActionName("getReportDrugAddictsDiseasesListings")]
        public HttpResponseMessage getReportDrugAddictsDiseasesListings(DashboardFilter model)
        {

            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "DrugAddcictDashboardDieases.rpt";
            string exportFilename = "DrugAddictDisease_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getReportDrugAddictsDiseasesListings(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region DrugAddicts To SocialWelfare Dashbaord Diseases
        [HttpPost]
        [ActionName("getReportDrugAddictsSocialWelfareListings")]
        public HttpResponseMessage getReportDrugAddictsSocialWelfareListings(DashboardFilter model)
        {

            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "DrugAddcictSocialWelfare.rpt";
            string exportFilename = "DrugAddictSocialWelfare_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getReportDrugAddictsSocialWelfareListings(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region IPD Admissions Registration New 
        [HttpPost]
        [ActionName("getIPDAdmissionDashboardAllReport")]
        public HttpResponseMessage getIPDAdmissionDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDAdmissionDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDAdmissionDashboardAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion
        #region IPD Dashboard Vitals New 
        [HttpPost]
        [ActionName("getIPDVitalDashboardAllReport")]
        public HttpResponseMessage getIPDVitalDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDVitalDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDVitalDashboardAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region IPD Doctor Dashboard New 
        [HttpPost]
        [ActionName("getIPDDoctorDashboardPatientAllReport")]
        public HttpResponseMessage getIPDDoctorDashboardPatientAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDDoctorDashboardPatientAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDDoctorDashboardPatientAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getIPDDoctorHFDashboardPatientAllReport")]
        public HttpResponseMessage getIPDDoctorHFDashboardPatientAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDDoctorDashboardPatientAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDDoctorHFDashboardPatientAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region IPD Pharmacy Dashboard  

        [HttpPost]
        [ActionName("getIPDPharmacyDashboardAllReport")]
        public HttpResponseMessage getIPDPharmacyDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDPharmacyDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDPharmacyDashboardAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region IPD Lab Dashboard  

        [HttpPost]
        [ActionName("getIPDLabDashboardTestAllReport")]
        public HttpResponseMessage getIPDLabDashboardTestAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDLabDashboardTestAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDLabDashboardTestAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getIPDLabDashboardTestTimeFromStartTillNowAllReport")]
        public HttpResponseMessage getIPDLabDashboardTestTimeFromStartTillNowAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIPDLabDashboardTestTimeFromStartTillNowAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDLabDashboardTestTimeFromStartTillNowAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region HCP Dashboard  

        [HttpPost]
        [ActionName("getHCPDashboardAllReport")]
        public HttpResponseMessage getHCPDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPHCPDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getHCPDashboardAllReport(model, StoreProcedureName);

            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Paraplegic Dashboard 

        [HttpPost]
        [ActionName("getParaplegicDashboardAllReport")]
        public HttpResponseMessage getParaplegicDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getParaplegicDashboardAllReport(model, StoreProcedureName);

            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }


        [HttpPost]
        [ActionName("getIndivisualOpdDashboardAllReport")]
        public HttpResponseMessage getIndivisualOpdDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPIndivisualOpdDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getParaplegicDashboardAllReport(model, StoreProcedureName);

            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);

            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Paraplegic all Dashboard
        #region Dashboard Registration New 
        [HttpPost]
        [ActionName("getParaplegicRegistrationDashboardAllReport")]
        public HttpResponseMessage getParaplegicRegistrationDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicRegistrationDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getRegistrationDashboardAllReport(model, StoreProcedureName);
            
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion
        #region Dashboard Vitals New 
        [HttpPost]
        [ActionName("getParaplegicVitalDashboardAllReport")]
        public HttpResponseMessage getParaplegicVitalDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicVitalDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getVitalDashboardAllReport(model, StoreProcedureName);
            
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Doctor Dashboard New 
        [HttpPost]
        [ActionName("getParaplegicDoctorDashboardPatientAllReport")]
        public HttpResponseMessage getParaplegicDoctorDashboardPatientAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicDoctorDashboardPatientAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getDoctorDashboardPatientAllReport(model, StoreProcedureName);
            

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getParaplegicDoctorDashboardHFPatientAllReport")]
        public HttpResponseMessage getParaplegicDoctorDashboardHFPatientAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicDoctorDashboardPatientAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getDoctorDashboardHFPatientAllReport(model, StoreProcedureName);
            

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Pharmacy Dashboard New 

        [HttpPost]
        [ActionName("getParaplegicPharmacyDashboardAllReport")]
        public HttpResponseMessage getParaplegicPharmacyDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicPharmacyDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getPharmacyDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Lab Dashboard New 

        [HttpPost]
        [ActionName("getParaplegicLabDashboardTestAllReport")]
        public HttpResponseMessage getParaplegicLabDashboardTestAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicLabDashboardTestAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getLabDashboardTestAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getParaplegicLabDashboardTestTimeFromStartTillNowAllReport")]
        public HttpResponseMessage getParaplegicLabDashboardTestTimeFromStartTillNowAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPParaplegicLabDashboardTestTimeFromStartTillNowAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PathologyReports.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getLabDashboardTestTimeFromStartTillNowAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion
        #endregion

        #region OpdStock
        public HttpResponseMessage DownloadOpdReport(OpdStockViewModel model)
        {
            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "MedicineStockReport.rpt";
            string exportFilename = "MedicineOpdStock_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            //var PatientList = SwitchConnectionBetweenTransAndReplication(model).getRegistrationDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model.DashboardFilter).GetHealthFacilityName(model.DashboardFilter);

            string ReportTitle = AppCommonMethod.ReportTittle(model.DashboardFilter.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(model.MedicineList);
            rd.SetParameterValue("FromDate", model.DashboardFilter.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.DashboardFilter.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("QuantityGrandTotal", model.OverAllQuantitySum);
            rd.SetParameterValue("PriceGrandTotal", model.OverAllPriceSum);

            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region Medicine Dispense report
        public HttpResponseMessage OPDMedicineDispenseReport(MedicineDispenseViewModel model)
        {
            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "MedicineDispenseReport.rpt";
            string exportFilename = "Medicine_Dispense_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            //var PatientList = SwitchConnectionBetweenTransAndReplication(model).getRegistrationDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model.DashboardFilter).GetHealthFacilityName(model.DashboardFilter);

            string ReportTitle = AppCommonMethod.ReportTittle(model.DashboardFilter.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(model.MedicineList);
            rd.SetParameterValue("FromDate", model.DashboardFilter.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.DashboardFilter.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("QuantityGrandTotal", model.OverAllQuantitySum);
            rd.SetParameterValue("PriceGrandTotal", model.OverAllPriceSum);

            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion
        #region Dental Dashboard 

        [HttpPost]
        [ActionName("getDentalDashboardAllReport")]
        public HttpResponseMessage getDentalDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPDentalDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getIPDPharmacyDashboardAllReport(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        [HttpPost]
        [ActionName("getDentalDashboardAllReportProcedures")]
        public HttpResponseMessage getDentalDashboardAllReportProcedures(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPDentalDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getDentalDashboardAllReportProcedures(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }

        #endregion

        #region Physiotherapy Dashboard 

        [HttpPost]
        [ActionName("getPhysioTherapyDashboardAllReport")]
        public HttpResponseMessage getPhysioTherapyDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPPhysioTherapyDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getPhysioTherapyDashboardAllReport(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }


        #endregion

        #region paraplegic OpdStock
        public HttpResponseMessage ParaplegicDownloadOpdReport(OpdStockViewModel model)
        {
            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "MedicineStockReport.rpt";
            string exportFilename = "MedicineOpdStock_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            //var PatientList = SwitchConnectionBetweenTransAndReplication(model).getRegistrationDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model.DashboardFilter).GetHealthFacilityName(model.DashboardFilter);

            string ReportTitle = AppCommonMethod.ReportTittle(model.DashboardFilter.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(model.MedicineList);
            rd.SetParameterValue("FromDate", model.DashboardFilter.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.DashboardFilter.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("QuantityGrandTotal", model.OverAllQuantitySum);
            rd.SetParameterValue("PriceGrandTotal", model.OverAllPriceSum);

            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion

        #region Paraplegic Medicine Dispense report
        public HttpResponseMessage ParaplegicOPDMedicineDispenseReport(MedicineDispenseViewModel model)
        {
            if (model == null)
                return null;

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "MedicineDispenseReport.rpt";
            string exportFilename = "Medicine_Dispense_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            //var PatientList = SwitchConnectionBetweenTransAndReplication(model).getRegistrationDashboardAllReport(model, StoreProcedureName);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model.DashboardFilter).GetHealthFacilityName(model.DashboardFilter);

            string ReportTitle = AppCommonMethod.ReportTittle(model.DashboardFilter.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(model.MedicineList);
            rd.SetParameterValue("FromDate", model.DashboardFilter.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.DashboardFilter.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("QuantityGrandTotal", model.OverAllQuantitySum);
            rd.SetParameterValue("PriceGrandTotal", model.OverAllPriceSum);

            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }
        #endregion


        #region Speech Dashboard 

        [HttpPost]
        [ActionName("getSpeechTherapyDashboardAllReport")]
        public HttpResponseMessage getSpeechTherapyDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPSpeechTherapyDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getSpeechTherapyDashboardAllReport(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }


        #endregion

        #region Speech Dashboard 

        [HttpPost]
        [ActionName("getNutritionDashboardAllReport")]
        public HttpResponseMessage getNutritionDashboardAllReport(DashboardFilter model)
        {

            if (model == null)
                return null;
            string StoreProcedureName = "SPNutritionDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getNutritionDashboardAllReport(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }


        #endregion

        #region Speech Dashboard 

        [HttpPost]
        [ActionName("getPsychologyDashboardAllReport")]
        public HttpResponseMessage getPsychologyDashboardAllReport(DashboardFilter model)
        {
            if (model == null)
                return null;
            string StoreProcedureName = "SPPsychologyDashboardAllList";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "PatientReport.rpt";
            string exportFilename = "Patient_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).getPsychologyDashboardAllReport(model, StoreProcedureName);

            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);
            string ReportTitle = AppCommonMethod.ReportTittle(model.listType);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ReportTitle", ReportTitle);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;
        }


        #endregion


        #region TB


        [HttpPost]
        [ActionName("GetTbPatientsRegistered")]
        public HttpResponseMessage GetTbPatientsRegistered(DashboardFilter model)
        {

            if (model == null)
                return null;

           
            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "TBDashboardReport" +
                ".rpt";
            string exportFilename = model.TbPatientTypeContstant + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetTbPatientsRegistered(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            model.listType = model.TbPatientTypeContstant + " Report";
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;

        }



        [HttpPost]
        [ActionName("GetTbPatientsData")]
        public HttpResponseMessage GetTbPatientsData(DashboardFilter model)
        {
            if (model == null)
                return null;


            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "TBDashboardReport" +
                ".rpt";
            string exportFilename = model.TbPatientTypeContstant + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetTbPatientsData(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            model.listType = model.TbPatientTypeContstant + " Report";
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;
        }



        [HttpPost]
        [ActionName("GetTbAdvisedTest")]
        public HttpResponseMessage GetTbAdvisedTest(DashboardFilter model)
        {
            if (model == null)
                return null;


            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "TBDashboardReport" +
                ".rpt";
            string exportFilename = model.TbPatientTypeContstant + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetTbAdvisedTest(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            model.listType = model.TbPatientTypeContstant + " Report";
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;
        }



        [HttpPost]
        [ActionName("GetTbTestResults")]
        public HttpResponseMessage GetTbTestResults(DashboardFilter model)
        {
            if (model == null)
                return null;


            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "TBDashboardReport" +
                ".rpt";
            string exportFilename = model.TbPatientTypeContstant + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetTbTestResults(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            model.listType = model.TbPatientTypeContstant + " Report";
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;
        }




        [HttpPost]
        [ActionName("GetTbIssuedMedicine")]
        public HttpResponseMessage GetTbIssuedMedicine(DashboardFilter model)
        {
            if (model == null)
                return null;


            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "TBDashboardReport" +
                ".rpt";
            string exportFilename = model.TbPatientTypeContstant + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = SwitchConnectionBetweenTransAndReplication(model).GetTbIssuedMedicine(model);
            healthFacilityName = SwitchConnectionBetweenTransAndReplication(model).GetHealthFacilityName(model);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            rd.SetDataSource(PatientList);
            rd.SetParameterValue("FromDate", model.StartDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            rd.SetParameterValue("ToDate", model.EndDate.Value.ToString("dd-MM-yyyy hh:mm tt"));
            model.listType = model.TbPatientTypeContstant + " Report";
            rd.SetParameterValue("ReportTitle", model.listType);
            rd.SetParameterValue("ReportGeneratedTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt:ss"));
            rd.SetParameterValue("HealthFacility", healthFacilityName);
            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;
        }
        #endregion

        #region Select Service To Response between Transaction and replication
        [HttpGet]
        public dynamic SwitchConnectionBetweenTransAndReplication(dynamic model)
        {
            if (model.SwitchConnection)
            {
                return _dashboardTransactionService;
            }
            else
            {
                return _dashboardReplicationService;

            }
        }
        #endregion
    }
}