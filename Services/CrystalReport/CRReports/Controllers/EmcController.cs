
using CRReports.Common;
using CRReports.Data;
using CRReports.Models.Dto;
using CRReports.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CRReports.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmcController : ApiController
    {
        EmcService _emcService = new EmcService();
        HmisEntities entities = new HmisEntities();

        string ImagePath = ConfigurationManager.AppSettings["imageUrl"] ?? "";
        string baseURL = ConfigurationManager.AppSettings["baseURL"] ?? "";
        //string ImagePath = "D:\\mlc\\person 2.jpg";


        //private readonly string _hrBaseUrl = "D:\\HMIS\\CDN\\wwwroot\\HMIS\\EMCMLEFORM\\";
        //private readonly string _hrBaseUrl = "\\\\192.168.0.42\\app-storage\\cdn-phis.pshealthpunjab.gov.pk\\wwwroot\\HMIS\\EMCMLEFORM";
        //private readonly string _hrBaseUrl = @"\\192.168.0.42\app-storage\cdn-phis.pshealthpunjab.gov.pk\wwwroot\HMIS\";

        #region EMC
        [HttpPost]
        [ActionName("GetMleSinglePatientReport")]
        public HttpResponseMessage GetMleSinglePatientReport(FilterDto filter)
        {

            //if (model == null)
            //    return null;
            string StoreProcedureName = "mlc.GetMleSinglePatientReport";

            var healthFacilityName = "";
            string report = "~/Reports";
            string reportFileName = "MLCReport.rpt";
            string exportFilename = "Mle_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = _emcService.GetMleSinglePatientReport(filter.PatientVisitId, StoreProcedureName);

            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);


            //DataSet dataSet = _emcService.GetDataFromStoredProcedure(patientId); // Fetch data from the stored procedure

            //rd.SetDataSource(dataSet.Tables[0]); // Bind the dataset to the report

            ////crystalReportViewer.ReportSource = rd;



            rd.SetDataSource(PatientList);

            if (filter.PaperType != "Security Paper")
            {
                rd.SetParameterValue("ReportTitle", "PRIMARY & SECONDARY HEALTHCARE DEPARTMENT\r\nGOVERNMENT OF PUNJAB");
                rd.SetParameterValue("ReportName", "MEDICO LEGAL EXAMINATION CERTIFICATE");
            }
            else {
                rd.SetParameterValue("ReportTitle", "  ");
                rd.SetParameterValue("ReportName", "  ");
            }

            if (PatientList[0].IsFinalReport == true)
            {
                rd.SetParameterValue("ProvisionalOrFinalReport", "Final Report");
            }
            else
            {
                rd.SetParameterValue("ProvisionalOrFinalReport", "Provisional Report");
            }

            rd.SetParameterValue("BookNo", PatientList[0].BookNo != null ? PatientList[0].BookNo : "-");

            rd.SetParameterValue("PoliceDocketOne", PatientList[0].PoliceDocketOne != null ? PatientList[0].PoliceDocketOne : "-");
            rd.SetParameterValue("PoliceDocketTwo", PatientList[0].PoliceDocketTwo != null ? PatientList[0].PoliceDocketTwo : "-");
            rd.SetParameterValue("PoliceDocketThree", PatientList[0].PoliceDocketThree != null ? PatientList[0].PoliceDocketThree : "-");
            rd.SetParameterValue("DesignationName", PatientList[0].DesignationName != null ? PatientList[0].DesignationName : "-");

            rd.SetParameterValue("SerialNo", PatientList[0].SerialNo != null ? PatientList[0].SerialNo : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList[0].HealthFacilityName != null ? PatientList[0].HealthFacilityName : "-");
            rd.SetParameterValue("MLCNo", PatientList[0].MLCNo != null ? PatientList[0].MLCNo : "-");
            rd.SetParameterValue("FullName", PatientList[0].FullName != null ? PatientList[0].FullName : "-");
            rd.SetParameterValue("Caste", PatientList[0].Caste != null ? PatientList[0].Caste : "-");
            rd.SetParameterValue("Relation", PatientList[0].Relation != null ? PatientList[0].Relation : "-");
            rd.SetParameterValue("AgeAndSex", (PatientList[0].Age != null ? PatientList[0].Age : "-") + " - " + (PatientList[0].Gender != null ? PatientList[0].Gender : "-"));
            rd.SetParameterValue("Age", PatientList[0].Age != null ? PatientList[0].Age : "-");
            rd.SetParameterValue("Gender", PatientList[0].Gender != null ? PatientList[0].Gender : "-");
            rd.SetParameterValue("MobileNo", PatientList[0].MobileNo != null ? PatientList[0].MobileNo : "-");
            rd.SetParameterValue("Occupation", PatientList[0].Occupation != null ? PatientList[0].Occupation : "-");
            rd.SetParameterValue("Address", PatientList[0].Address != null ? PatientList[0].Address : "-");
            rd.SetParameterValue("Occupation", PatientList[0].Occupation != null ? PatientList[0].Occupation : "-");
            rd.SetParameterValue("NICNo", PatientList[0].CNIC != null ? PatientList[0].CNIC : "-");
            rd.SetParameterValue("MlcRemark1", PatientList[0].MlcRemark1 != null ? PatientList[0].MlcRemark1 : "-");
            rd.SetParameterValue("MlcRemark2", PatientList[0].MlcRemark2 != null ? PatientList[0].MlcRemark2 : "-");
            rd.SetParameterValue("AccompaniedBy", PatientList[0].AccompaniesBy != null ? PatientList[0].AccompaniesBy : "-");
            rd.SetParameterValue("ArrivalDateTime", PatientList[0].ArrivalDateTime != null ? PatientList[0].ArrivalDateTime : "-");
            rd.SetParameterValue("ExaminationDateTime", PatientList[0].ExaminationDateTime != null ? PatientList[0].ExaminationDateTime : "-");
            rd.SetParameterValue("CourtOrder", PatientList[0].CourtOrder != null ? PatientList[0].CourtOrder : "-");
            var tempPoliceConstableName = PatientList[0].PoliceConstableName != null ? PatientList[0].PoliceConstableName : "-";
            var tempPoliceConstablePhoneNumber = PatientList[0].PoliceConstablePhoneNumber != null ? PatientList[0].PoliceConstablePhoneNumber : "-";

            rd.SetParameterValue("PoliceConstableName", tempPoliceConstableName);
            rd.SetParameterValue("PoliceConstablePhoneNumber", tempPoliceConstablePhoneNumber);


            rd.SetParameterValue("NameAndNoOfPoliceConstable", tempPoliceConstableName + " - " + tempPoliceConstablePhoneNumber);

            rd.SetParameterValue("AdmissionDateTime", PatientList[0].AdmitDateTime != null ? PatientList[0].AdmitDateTime : "-");
            rd.SetParameterValue("DischargeDateTime", PatientList[0].DischargeDateTime != null ? PatientList[0].DischargeDateTime : "-");
            rd.SetParameterValue("DateAndTimeOfReportSentToPolice", PatientList[0].SentDateTime != null ? PatientList[0].SentDateTime : "-");


            // Guardian
            var tempGuardianAddress = PatientList[0].Address != null ? PatientList[0].Address : "-";
            var tempGuardianMobileNo = PatientList[0].MobileNo != null ? PatientList[0].MobileNo : "-";
            // End GuardianGuardianAddress


            rd.SetParameterValue("GuardianAddress", tempGuardianAddress); // Patient Address
            rd.SetParameterValue("GuardianMobileNo", tempGuardianMobileNo); // Patient Phone No
            rd.SetParameterValue("GuardianAddressMobileNo", tempGuardianAddress + " - " + tempGuardianMobileNo);


            
            //rd.SetParameterValue("PatientSignature", PatientList[0].PatientSignature != null ? PatientList[0].PatientSignature : "-");

            rd.SetParameterValue("CaseAgainst", PatientList[0].CaseAgainst != null ? PatientList[0].CaseAgainst : "-");
            rd.SetParameterValue("IncidentPlace", PatientList[0].IncidentPlace != null ? PatientList[0].IncidentPlace : "-");

            if (PatientList[0].PatientSignatureImageUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientSignature);
                string tempUrlPath = baseURL + PatientList[0].PatientSignatureImageUrl;
            rd.SetParameterValue("PatientSignature", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PatientSignature", "");
            }
            //if (PatientList[0].PatientImageUrl != null)
            //{
            //    string lastPath = GetLastPath(PatientList[0].PatientImageUrl);
            //    string tempUrlPath = _hrBaseUrl + lastPath;
            //    rd.SetParameterValue("PatientImageUrl", tempUrlPath);
            //}
            //else
            //{
            //    rd.SetParameterValue("PatientImageUrl", "");
            //}

            //rd.SetParameterValue("PatientImageUrl", @"D:\mlc\person 2.jpg");
            string patientImage = null;
            if (PatientList[0].PatientImageUrl != null)
                patientImage = baseURL + PatientList[0].PatientImageUrl;
            else
                patientImage = ImagePath;
            //string newPath = patientImage.Replace(@"\\", @"\");
            //string newUrlPath = newPath.Replace(@"\\", @"\");

            rd.SetParameterValue("PatientImageUrl", @patientImage);


            rd.SetParameterValue("GuardianName", PatientList[0].GuardianName != null ? PatientList[0].GuardianName : "-");
            rd.SetParameterValue("GuardianCNIC", PatientList[0].GuardianCNIC != null ? PatientList[0].GuardianCNIC : "-");


            if (PatientList[0].PatientUnderAgeOrAdultFingerPrint != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientUnderAgeOrAdultFingerPrint);
                string tempUrlPath = baseURL + PatientList[0].PatientUnderAgeOrAdultFingerPrint;
                rd.SetParameterValue("PatientUnderAgeOrAdultFingerPrint", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PatientUnderAgeOrAdultFingerPrint", "");
            }
            //rd.SetParameterValue("PatientUnderAgeOrAdultFingerPrint", PatientList[0].PatientUnderAgeOrAdultFingerPrint != null ? PatientList[0].PatientUnderAgeOrAdultFingerPrint : "-");
            rd.SetParameterValue("BriefHistory", PatientList[0].History != null ? PatientList[0].History : "-");
            rd.SetParameterValue("ClothExamination", PatientList[0].ClothExamination != null ? PatientList[0].ClothExamination : "-");
            rd.SetParameterValue("GeneralPhysicalExamination", PatientList[0].GeneralPhysicalExamination != null ? PatientList[0].GeneralPhysicalExamination : "-");
            rd.SetParameterValue("InjuriesDescription", PatientList[0].InjuriesDescription != null ? PatientList[0].InjuriesDescription : "-");
            rd.SetParameterValue("AdvisedInvestigate", PatientList[0].AdvisedInvestigate != null ? PatientList[0].AdvisedInvestigate : "-");
            rd.SetParameterValue("LaboratoryInvestigation", PatientList[0].LaboratoryInvestigation != null ? PatientList[0].LaboratoryInvestigation : "-");
            rd.SetParameterValue("OpinionSpecialistOrXRayReport", PatientList[0].OpinionSpecialistOrXRayReport != null ? PatientList[0].OpinionSpecialistOrXRayReport : "-");
            rd.SetParameterValue("NatureOfInjuries", PatientList[0].NatureOfInjuries != null ? PatientList[0].NatureOfInjuries : "-");
            rd.SetParameterValue("PossibilityOfFabrication", PatientList[0].Fabrication != null ? PatientList[0].Fabrication : "-");
            rd.SetParameterValue("DurationOfInjuries", PatientList[0].DurationOfInjuries != null ? PatientList[0].DurationOfInjuries : "-");
            rd.SetParameterValue("KUOInjuries", PatientList[0].KuoInjuries != null ? PatientList[0].KuoInjuries : "-");


            if (PatientList[0].PoliceSignatureImageUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PoliceSignature);
                string tempUrlPath = baseURL + PatientList[0].PoliceSignatureImageUrl;
                rd.SetParameterValue("PoliceSignature", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PoliceSignature", "");
            }


            //rd.SetParameterValue("PoliceSignature", PatientList[0].PoliceSignature != null ? PatientList[0].PoliceSignature : "-");
            rd.SetParameterValue("WeaponPoison", PatientList[0].WeaponPoison != null ? PatientList[0].WeaponPoison : "-");
            rd.SetParameterValue("PoliceDistrict", PatientList[0].PoliceDistrict != null ? PatientList[0].PoliceDistrict : "-");

            int indexOfSlash = filter.CopyType.IndexOf('/');

            if (filter.CopyType.Contains("/"))
            {
                if (indexOfSlash != -1)
                {

                    // Get the substring after '/'
                    string contentAfterSlash = filter.CopyType.Substring(indexOfSlash + 1);

                    // Convert the substring to uppercase
                    string result = contentAfterSlash.ToUpper();

                    rd.SetParameterValue("CopyType", result);
                }
            }
            else
            {
                rd.SetParameterValue("CopyType", filter.CopyType.ToUpper());
            }

            //rd.SetParameterValue("PoliceFingerPrint", "E:\\Projects\\MLC\\a8e1688b-a190-4863-afdc-29295ca0e9f6.png");

            if (PatientList[0].PoliceFingerPrintUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PoliceFingerPrintUrl);
                string tempUrlPath = baseURL + PatientList[0].PoliceFingerPrintUrl;
                rd.SetParameterValue("PoliceFingerPrint", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PoliceFingerPrint", "");
            }

            //rd.SetParameterValue("PoliceFingerPrint", PatientList[0].PoliceFingerPrintUrl != null ? PatientList[0].PoliceFingerPrintUrl : "-");
            rd.SetParameterValue("MlcDate", PatientList[0].MlcDate != null ? PatientList[0].MlcDate : "-");

            if (PatientList[0].PatientFinalReportUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].PatientFinalReportUrl;
                rd.SetParameterValue("FinalReport", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("FinalReport", "");
            }

            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }


            //var qrCodeDirectoryPath = 

            //rd.SetParameterValue("FinalReport", PatientList[0].PatientFinalReportUrl != null ? PatientList[0].PatientFinalReportUrl : "-");


            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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

       

        
        //private async Task<string> QrCodeImagePath(string qrMsg)
        //{
        //    var body = new UploadDocumentDto
        //    {
        //        ProjectName = "Hmis",
        //        FolderName = "QrCodeEmcMle",
        //        Base64String = "data:image/jpg;base64," + Convert.ToBase64String(GenerateQrCode(qrMsg))
        //    };

        //    var jsonBody = JsonConvert.SerializeObject(body);
        //    var stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");


        //    HttpClient client = new HttpClient() { BaseAddress = new Uri("https://cdn-crystalreport.pshealthpunjab.gov.pk/") };



        //    var httpResponse = await client.PostAsync("api/UploadFile/Upload", stringContent);
        //    var content = await httpResponse.Content.ReadAsStringAsync();

        //    ResponseDTO response = JsonConvert.DeserializeObject<ResponseDTO>(content);
        //    if (response == null)
        //        throw new Exception("Error in uploading file!");



        //    if (string.IsNullOrEmpty(Convert.ToString(response.data)))
        //        throw new Exception("Error in uploading file!");

        //    string url = "";

        //    if (!string.IsNullOrEmpty(Convert.ToString(response.data)))
        //    {
        //        url = response.data.ToString().Replace("http", "https");
        //        url = url.Replace("cdn", "cdn-crystalreport");
        //    }

        //    if (response.status)
        //        return await Task.FromResult(Convert.ToString(url));
        //    else
        //        return null;


        //}


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        [HttpPost]
        [ActionName("GetMleSvSinglePatientReport")]
        public HttpResponseMessage GetMleSvSinglePatientReport(FilterDto filter)
        {

            //if (model == null)
            //    return null;
            string StoreProcedureName = "mlc.SpGetMleSvSinglePatientReport";

            var healthFacilityName = "-";
            string report = "~/Reports";
            string reportFileName = "MleSvReport.rpt";
            string exportFilename = "MleSv_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = _emcService.GetMleSvSinglePatientReport(filter.PatientVisitId, StoreProcedureName);
            
            
            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            //rd.SetDataSource(PatientList);
            //rd.SetParameterValue("FromDate", "-");
            //rd.SetParameterValue("ToDate", "-");
            //rd.SetParameterValue("ReportTitle", "MLC Report");

            if (filter.PaperType != "Security Paper")
            {
                rd.SetParameterValue("ReportTitle", "PRIMARY & SECONDARY HEALTHCARE DEPARTMENT\r\nGOVERNMENT OF PUNJAB");
                rd.SetParameterValue("ReportName", "MEDICO LEGAL EXAMINATION CERTIFICATE\r\nFOR FEMALE SURVIVORS OF SEXUAL VOILENCE");
            }
            else
            {
                rd.SetParameterValue("ReportTitle", "  ");
                rd.SetParameterValue("ReportName", "  ");
            }
            
            int indexOfSlash = filter.CopyType.IndexOf('/');

            // Check if '/' is found in the string

            if (filter.CopyType.Contains("/"))
            {
                if (indexOfSlash != -1)
                {

                    // Get the substring after '/'
                    string contentAfterSlash = filter.CopyType.Substring(indexOfSlash + 1);

                    // Convert the substring to uppercase
                    string result = contentAfterSlash.ToUpper();

                    rd.SetParameterValue("CopyType", result);
                }
            }
            else
            {
                rd.SetParameterValue("CopyType", filter.CopyType.ToUpper());
            }

            if (PatientList[0].IsFinalReport == true)
            {
                rd.SetParameterValue("ProvisionalOrFinalReport", "Final Report");
            }
            else
            {
                rd.SetParameterValue("ProvisionalOrFinalReport", "Provisional Report");
            }

            rd.SetParameterValue("CasualtyEmergencyNO", PatientList[0].EmergencyNo != null ? PatientList[0].EmergencyNo : "-");
   

        rd.SetParameterValue("PoliceDocketOne", PatientList[0].PoliceDocketOne != null ? PatientList[0].PoliceDocketOne : "-");
            rd.SetParameterValue("PoliceDocketTwo", PatientList[0].PoliceDocketTwo != null ? PatientList[0].PoliceDocketTwo : "-");
            rd.SetParameterValue("PoliceDocketThree", PatientList[0].PoliceDocketThree != null ? PatientList[0].PoliceDocketThree : "-");

            rd.SetParameterValue("DesignationName", PatientList[0].DesignationName != null ? PatientList[0].DesignationName : "-");

            rd.SetParameterValue("DaughterWifeOf", PatientList[0].DaughterWifeOf != null ? PatientList[0].DaughterWifeOf : "-");
            rd.SetParameterValue("MlcNo", PatientList[0].MLCNo != null ? PatientList[0].MLCNo : "-");
            rd.SetParameterValue("NameOfTheDoctor", PatientList[0].DoctorName != null ? PatientList[0].DoctorName : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList[0].HealthFacilityName != null ? PatientList[0].HealthFacilityName : "-");
            rd.SetParameterValue("Name", PatientList[0].PatientName != null ? PatientList[0].PatientName : "-");
            rd.SetParameterValue("Guardian", PatientList[0].GuardianName != null ? PatientList[0].GuardianName : "-");
            rd.SetParameterValue("Caste", PatientList[0].Caste != null ? PatientList[0].Caste : "-");
            rd.SetParameterValue("CNICNo", PatientList[0].CNIC != null ? PatientList[0].CNIC : "-");
            rd.SetParameterValue("TelephoneNo", PatientList[0].MobileNo != null ? PatientList[0].MobileNo : "-");
            rd.SetParameterValue("Address", PatientList[0].ParmanentAddress != null ? PatientList[0].ParmanentAddress : "-");
            rd.SetParameterValue("Age", PatientList[0].Age != null ? PatientList[0].Age : "-");
            rd.SetParameterValue("ArrivalDateTime", PatientList[0].ArrivalDateTime != null ? PatientList[0].ArrivalDateTime : "-");
            rd.SetParameterValue("ExaminationDateTime", PatientList[0].ExaminationDateTime != null ? PatientList[0].ExaminationDateTime : "-");
            rd.SetParameterValue("BoughtAccompaniedBy", PatientList[0].AccompaniedBy != null ? PatientList[0].AccompaniedBy : "-");
            rd.SetParameterValue("NoDateOfCountOrder", PatientList[0].CourtOrder != null ? PatientList[0].CourtOrder : "-");
            rd.SetParameterValue("IdentificationMarks1", PatientList[0].MLCSVRemark1 != null ? PatientList[0].MLCSVRemark1 : "-");
            rd.SetParameterValue("IdentificationMarks2", PatientList[0].MLCSVRemark2 != null ? PatientList[0].MLCSVRemark2 : "-");
            rd.SetParameterValue("DateTimeOfAdmission", PatientList[0].AdmissionDateTime != null ? PatientList[0].AdmissionDateTime : "-");
            rd.SetParameterValue("DateTimeOfIncidence", PatientList[0].IncidenceDateTim != null ? PatientList[0].IncidenceDateTim : "-");
            rd.SetParameterValue("LocationOfIncidence", PatientList[0].Location != null ? PatientList[0].Location : "-");
            rd.SetParameterValue("DischargeDateTime", PatientList[0].DischargeDateTime != null ? PatientList[0].DischargeDateTime : "-");
            rd.SetParameterValue("RelationToVictim", PatientList[0].RelationToVictim != null ? PatientList[0].RelationToVictim : "-");
            rd.SetParameterValue("RelevantDetailOfAssault", PatientList[0].AssaultDetail != null ? PatientList[0].AssaultDetail : "-");
            rd.SetParameterValue("PreviousSuchIncidence", PatientList[0].PreviousIncidence != null ? PatientList[0].PreviousIncidence : "-");
            rd.SetParameterValue("IfYesWhenDateYear", PatientList[0].PreviousIncidenceDateTime != null ? PatientList[0].PreviousIncidenceDateTime : "-");
            rd.SetParameterValue("DeatialFromOtherPartiesPoliceFamilyWitness", PatientList[0].DetailFromOther != null ? PatientList[0].DetailFromOther : "-");
            rd.SetParameterValue("RelevantMedicalSurgicalPsychiatricHistory", PatientList[0].RelevantMedicalSurgicalPsychiatricHistory != null ? PatientList[0].RelevantMedicalSurgicalPsychiatricHistory : "-");
            rd.SetParameterValue("RelevantGynecologicalHistory", PatientList[0].RelevantGynecologicalHistory != null ? PatientList[0].RelevantGynecologicalHistory : "-");
            rd.SetParameterValue("CurrentSymptoms", PatientList[0].CurrentSymptoms != null ? PatientList[0].CurrentSymptoms : "-");
            rd.SetParameterValue("NumberColourTextureSizeTypeOfClothes", PatientList[0].TypeOfCloths != null ? PatientList[0].TypeOfCloths : "-");
            rd.SetParameterValue("CutsTearsHolesBrokenButtonsZipper", PatientList[0].CutsTearsHoles != null ? PatientList[0].CutsTearsHoles : "-");
            rd.SetParameterValue("StainingWithBloodurineFeacesVomit", PatientList[0].BloodStaining != null ? PatientList[0].BloodStaining : "-");
            rd.SetParameterValue("StainingWithNonBiologicalMaterial", PatientList[0].NonBiologicalMaterialStaining != null ? PatientList[0].NonBiologicalMaterialStaining : "-");
            rd.SetParameterValue("Physique", PatientList[0].Physique != null ? PatientList[0].Physique : "-");
            rd.SetParameterValue("ConfidentWellOrientedInTimeAndSpace", PatientList[0].Confident != null ? PatientList[0].Confident  : "-");
            rd.SetParameterValue("ConfussedShyDepressedAgitateddCooperativeIntellectEmotionalState", PatientList[0].Confused != null ? PatientList[0].Confused : "-");
            rd.SetParameterValue("HeightAndWeight", PatientList[0].HeightWeight1 != null ? PatientList[0].HeightWeight1 : "-");
            rd.SetParameterValue("CharacteristicsOfInjuries", PatientList[0].CharacteristicsOfInjuries != null ? PatientList[0].CharacteristicsOfInjuries : "-");
            rd.SetParameterValue("Tears", PatientList[0].Tears != null ? PatientList[0].Tears : "-");
            rd.SetParameterValue("RuptureOfHymenIfPresentFreshOrOld", PatientList[0].Rupture != null ? PatientList[0].Rupture : "-");
            rd.SetParameterValue("EvidenceOfBleedingStainingWithTheBlood", PatientList[0].EvidenceBleed != null ? PatientList[0].EvidenceBleed : "-");
            rd.SetParameterValue("EvidenceOfSeminalStain", PatientList[0].EvidenceSeminal != null ? PatientList[0].EvidenceSeminal : "-");
            rd.SetParameterValue("ClothsHandedOverTo", PatientList[0].ClothsHandOverTo != null ? PatientList[0].ClothsHandOverTo : "-");
            rd.SetParameterValue("BloodHandedOverTo", PatientList[0].BloodHandOverTo != null ? PatientList[0].BloodHandOverTo : "-");
            rd.SetParameterValue("VaginalAndAnalSwabshandedOverTo", PatientList[0].VaginalHandOverTo != null ? PatientList[0].VaginalHandOverTo : "-");
            rd.SetParameterValue("OralSwabshandedOverTo", PatientList[0].OralHandOverTo != null ? PatientList[0].OralHandOverTo : "-");
            rd.SetParameterValue("GuardianName", PatientList[0].GuardianName != null ? PatientList[0].GuardianName : "-");
            rd.SetParameterValue("History", PatientList[0].History != null ? PatientList[0].History : "-");
            rd.SetParameterValue("GeneralPhysicalExamination", PatientList[0].GeneralPhysicalExamination != null ? PatientList[0].GeneralPhysicalExamination : "-");
            // Evidence Collected
            rd.SetParameterValue("ClothsDescription", PatientList[0].ClothsDescription != null ? PatientList[0].ClothsDescription : "-");
            rd.SetParameterValue("BloodDescription", PatientList[0].BloodDescription != null ? PatientList[0].BloodDescription : "-");
            rd.SetParameterValue("VaginalDescription", PatientList[0].VaginalDescription != null ? PatientList[0].VaginalDescription : "-");
            rd.SetParameterValue("OralDescription", PatientList[0].OralDescription != null ? PatientList[0].OralDescription : "-");
            rd.SetParameterValue("ClothExamination", PatientList[0].ClothExamination != null ? PatientList[0].ClothExamination : "-");

            rd.SetParameterValue("InvestigationAdvice", PatientList[0].InvestigationAdvice != null ? PatientList[0].InvestigationAdvice : "-");

            rd.SetParameterValue("Treatment", PatientList[0].Treatment != null ? PatientList[0].Treatment : "-");

            rd.SetParameterValue("Notes", PatientList[0].Notes != null ? PatientList[0].Notes : "-");
            
            rd.SetParameterValue("VictimDetail", PatientList[0].VictimDetail != null ? PatientList[0].VictimDetail : "-");




            rd.SetParameterValue("XRaysCTScanMRIIfRequired", PatientList[0].XRayReport != null ? PatientList[0].XRayReport : "-");
            rd.SetParameterValue("UltrasoundAbdomialPelvicCavity", PatientList[0].UltraSoundReport != null ? PatientList[0].UltraSoundReport : "-");
            rd.SetParameterValue("BloodInvestigation", PatientList[0].BloodReport != null ? PatientList[0].BloodReport : "-");
            rd.SetParameterValue("Reference", PatientList[0].Reference != null ? PatientList[0].Reference : "-");
            rd.SetParameterValue("CounsellingRefferal", PatientList[0].CounsellingRefferal != null ? PatientList[0].CounsellingRefferal : "-");
            rd.SetParameterValue("InitialOPinionRremarks", PatientList[0].FinalOpinion != null ? PatientList[0].FinalOpinion : "-");

            rd.SetParameterValue("NatureOfInguriesUndercrimianlAmendmentAct", PatientList[0].NatureOfInjuries != null ? PatientList[0].NatureOfInjuries : "-");
            rd.SetParameterValue("ProbaleDurationOfInguries", PatientList[0].DurationOfInjuries != null ? PatientList[0].DurationOfInjuries : "-");
            rd.SetParameterValue("KindOfWeaponPoison", PatientList[0].KindOfWeaponUse != null ? PatientList[0].KindOfWeaponUse : "-");

            rd.SetParameterValue("FinalOpinionSv", PatientList[0].FinalOpinion != null ? PatientList[0].FinalOpinion : "-");
            rd.SetParameterValue("KUOInjuryNote", PatientList[0].KUOInjuryNote != null ? PatientList[0].KUOInjuryNote : "-");
            rd.SetParameterValue("DoctorName", PatientList[0].DoctorName != null ? PatientList[0].DoctorName : "-");
            rd.SetParameterValue("IncidentPlace", PatientList[0].IncidentPlace != null ? PatientList[0].IncidentPlace : "-");
            rd.SetParameterValue("CaseAgainst", PatientList[0].CaseAgainst != null ? PatientList[0].CaseAgainst : "-");
            rd.SetParameterValue("XRayReason", PatientList[0].XRayReason != null ? PatientList[0].XRayReason : "-");
            rd.SetParameterValue("UltraSoundReason", PatientList[0].UltraSoundReason != null ? PatientList[0].UltraSoundReason : "-");
            rd.SetParameterValue("BloodReason", PatientList[0].BloodReason != null ? PatientList[0].BloodReason : "-");
            rd.SetParameterValue("NameOfOfficialAccompany", PatientList[0].NameOfOfficialAccompany != null ? PatientList[0].NameOfOfficialAccompany : "-");
            
            if (PatientList[0].GuardianSignatureImageUrl != null)
            {
                string tempUrlPath = baseURL + PatientList[0].GuardianSignatureImageUrl;
                rd.SetParameterValue("GuardianSignatureImageUrl", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("ManualReportImageUrl", "");
                rd.SetParameterValue("GuardianSignatureImageUrl", "");
            }

             if (PatientList[0].PatientSignatureImageUrl != null)
            {
                string tempUrlPath = baseURL + PatientList[0].PatientSignatureImageUrl;
                rd.SetParameterValue("PatientSignatureImageUrl", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PatientSignatureImageUrl", "");
            }


            if (PatientList[0].ManualReportImageUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].ManualReportImageUrl);
                string tempUrlPath = baseURL + PatientList[0].ManualReportImageUrl;
                rd.SetParameterValue("ManualReportImageUrl", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("ManualReportImageUrl", "");
            }

            if (PatientList[0].PatientFingerPrintImageUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFingerPrintImageUrl);
                string tempUrlPath = baseURL + PatientList[0].PatientFingerPrintImageUrl;
                rd.SetParameterValue("PatientFingerPrintImageUrl", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PatientFingerPrintImageUrl", "");
            }
            
            if (PatientList[0].PatientImageUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientImageUrl);
                string tempUrlPath = baseURL + PatientList[0].PatientImageUrl;
                rd.SetParameterValue("PatientImageUrl", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("PatientImageUrl", "");
            }



            if (PatientList[0].FinalReportImageUrl != null)
            {
                //string lastPath = GetLastPath(PatientList[0].FinalReportImageUrl);
                string tempUrlPath = baseURL + PatientList[0].FinalReportImageUrl;
                rd.SetParameterValue("FinalReportImageUrl", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("FinalReportImageUrl", "");
            }

            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        [ActionName("GetPostMortemSinglePatientReport")]
        public async Task<HttpResponseMessage> GetPostMortemSinglePatientReport(FilterDto filter)
        {

            //if (model == null)
            //    return null;
            string StoreProcedureName = "mlc.SpGetPostMortemSinglePatientReport";

            string report = "~/Reports";
            string reportFileName = "PostMortemReportOne.rpt";
            string exportFilename = "Pstmrtm_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            //var PatientList = _emcService.GetPostMortemSinglePatientReport(patientId, StoreProcedureName);




            PostMortemDto objResponse = new PostMortemDto();
            using (var db = new HmisEntities())
            {
                var conn = db.Database.Connection;
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand(StoreProcedureName, (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@PatientVisitId", filter.PatientVisitId);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    await Task.Run(() => da.Fill(ds));

                    objResponse.PostMortemReponseZero = ds.Tables[0].ToList<PostMortemInitialExaminationAndReportDto>();
                    objResponse.PostMortemReponseOne = ds.Tables[1].ToList<PostMortemIdentificationDto>();
                    //PatientList =  objResponse;
                }
                catch (Exception)
                {
                    throw;
                }  conn.Close();
                }


            if (objResponse.PostMortemReponseZero != null)
            {
                string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
                ReportDocument rd = new ReportDocument();
                rd.Load(reportPath);


                if (filter.PaperType != "Security Paper")
                {
                    rd.SetParameterValue("ReportTitle", "PRIMARY & SECONDARY HEALTHCARE DEPARTMENT\r\nGOVERNMENT OF PUNJAB");
                    rd.SetParameterValue("ReportName", "POST-MORTEM EXAMINATION REPORT");
                }
                else
                {
                    rd.SetParameterValue("ReportTitle", "  ");
                    rd.SetParameterValue("ReportName", "  ");
                }

                int indexOfSlash = filter.CopyType.IndexOf('/');

                if (filter.CopyType.Contains("/"))
                {
                    if (indexOfSlash != -1)
                    {

                        // Get the substring after '/'
                        string contentAfterSlash = filter.CopyType.Substring(indexOfSlash + 1);

                        // Convert the substring to uppercase
                        string result = contentAfterSlash.ToUpper();

                        rd.SetParameterValue("CopyType", result);
                    }
                }
                else
                {
                    rd.SetParameterValue("CopyType", filter.CopyType.ToUpper());
                }


                if (objResponse.PostMortemReponseZero[0].IsFinalReport == true)
                {
                    rd.SetParameterValue("ProvisionalOrFinalReport", "Final Report");
                }
                else
                {
                    rd.SetParameterValue("ProvisionalOrFinalReport", "Provisional Report");
                }


                rd.SetParameterValue("MlcNo", objResponse.PostMortemReponseZero[0].MLCNo != null ? objResponse.PostMortemReponseZero[0].MLCNo : "-");
                rd.SetParameterValue("BookNo", objResponse.PostMortemReponseZero[0].BookNo != null ? objResponse.PostMortemReponseZero[0].BookNo : "-");
                
                rd.SetParameterValue("DesignationName", objResponse.PostMortemReponseZero[0].DesignationName != null ? objResponse.PostMortemReponseZero[0].DesignationName : "-");
                
                rd.SetParameterValue("FatherHusbandName", objResponse.PostMortemReponseZero[0].FatherHusbandName != null ? objResponse.PostMortemReponseZero[0].FatherHusbandName : "-");
                //rd.SetParameterValue("SerialNo", objResponse.PostMortemReponseZero[0].SerialNo != null ? objResponse.PostMortemReponseZero[0].SerialNo : "-");
                rd.SetParameterValue("HealthFacilityName", objResponse.PostMortemReponseZero[0].HealthFacilityName != null ? objResponse.PostMortemReponseZero[0].HealthFacilityName : "-");
                rd.SetParameterValue("PmrNo", objResponse.PostMortemReponseZero[0].PMRNo != null ? objResponse.PostMortemReponseZero[0].PMRNo : "-");
                rd.SetParameterValue("PatientName", objResponse.PostMortemReponseZero[0].PatientName != null ? objResponse.PostMortemReponseZero[0].PatientName : "-");
                rd.SetParameterValue("Age", objResponse.PostMortemReponseZero[0].Age != null ? objResponse.PostMortemReponseZero[0].Age.ToString() + " years" : "-");
                rd.SetParameterValue("Gender", objResponse.PostMortemReponseZero[0].Gender != null ? objResponse.PostMortemReponseZero[0].Gender : "-");
                rd.SetParameterValue("Caste", objResponse.PostMortemReponseZero[0].Caste != null ? objResponse.PostMortemReponseZero[0].Caste : "-");
                rd.SetParameterValue("Relation", objResponse.PostMortemReponseZero[0].Relation != null ? objResponse.PostMortemReponseZero[0].Relation : "-");
                rd.SetParameterValue("Address", objResponse.PostMortemReponseZero[0].ParmanentAddress != null ? objResponse.PostMortemReponseZero[0].ParmanentAddress : "-");
                rd.SetParameterValue("GuardianName", objResponse.PostMortemReponseZero[0].GuardianName != null ? objResponse.PostMortemReponseZero[0].GuardianName : "-");
                rd.SetParameterValue("NameAndDesignation1", objResponse.PostMortemReponseZero[0].PolicePersonNameDesignation != null ? objResponse.PostMortemReponseZero[0].PolicePersonNameDesignation : "-");
                rd.SetParameterValue("NameAndDesignation2", objResponse.PostMortemReponseZero[0].PolicePeron2NameDesignation != null ? objResponse.PostMortemReponseZero[0].PolicePeron2NameDesignation : "-");
                rd.SetParameterValue("PoliceStation", objResponse.PostMortemReponseZero[0].PoliceStationName != null ? objResponse.PostMortemReponseZero[0].PoliceStationName : "-");
                rd.SetParameterValue("PoliceDistrict", objResponse.PostMortemReponseZero[0].PoliceDistrict != null ? objResponse.PostMortemReponseZero[0].PoliceDistrict : "-");



                // Assigning Identifiers
                if (objResponse.PostMortemReponseOne.Count > 0)
                {
                // Identifier 1
                rd.SetParameterValue("IdentifierName1", objResponse.PostMortemReponseOne[0].IdentifierName != null ? objResponse.PostMortemReponseOne[0].IdentifierName : "-");
                rd.SetParameterValue("IdentifierCNIC1", objResponse.PostMortemReponseOne[0].IdentifierCNIC != null ? objResponse.PostMortemReponseOne[0].IdentifierCNIC : "-");
                rd.SetParameterValue("IdentifierRelation1", objResponse.PostMortemReponseOne[0].IdentifierRelation != null ? objResponse.PostMortemReponseOne[0].IdentifierRelation : "-");
                rd.SetParameterValue("IdentifierComments1", objResponse.PostMortemReponseOne[0].IdentifierComments != null ? objResponse.PostMortemReponseOne[0].IdentifierComments : "-");
                }
                else
                {
                    rd.SetParameterValue("IdentifierName1", "-");
                    rd.SetParameterValue("IdentifierCNIC1", "-");
                    rd.SetParameterValue("IdentifierRelation1", "-");
                    rd.SetParameterValue("IdentifierComments1", "-");


                    rd.SetParameterValue("IdentifierName2", "-");
                    rd.SetParameterValue("IdentifierCNIC2", "-");
                    rd.SetParameterValue("IdentifierRelation2", "-");
                    rd.SetParameterValue("IdentifierComments2", "-");
                }
            if (objResponse.PostMortemReponseOne.Count > 1)
                {
                    // Identifier 2
                    rd.SetParameterValue("IdentifierName2", objResponse.PostMortemReponseOne[1].IdentifierName != null ? objResponse.PostMortemReponseOne[1].IdentifierName : "-");
                    rd.SetParameterValue("IdentifierCNIC2", objResponse.PostMortemReponseOne[1].IdentifierCNIC != null ? objResponse.PostMortemReponseOne[1].IdentifierCNIC : "-");
                    rd.SetParameterValue("IdentifierRelation2", objResponse.PostMortemReponseOne[1].IdentifierRelation != null ? objResponse.PostMortemReponseOne[1].IdentifierRelation : "-");
                    rd.SetParameterValue("IdentifierComments2", objResponse.PostMortemReponseOne[1].IdentifierComments != null ? objResponse.PostMortemReponseOne[1].IdentifierComments : "-");
                }
                else
                {
                    rd.SetParameterValue("IdentifierName2", "-");
                    rd.SetParameterValue("IdentifierCNIC2", "-");
                    rd.SetParameterValue("IdentifierRelation2", "-");
                    rd.SetParameterValue("IdentifierComments2", "-");
                }
                // End Identifiers

                //foreach (var item in objResponse.PostMortemReponseOne.Select((value, i) => new { i, value }))
                //{
                //    var value = item.value;
                //    var index = item.i;


                //    rd.SetParameterValue("IdentifierName" +index, value.IdentifierName != null ? value.IdentifierName : "-");
                //    rd.SetParameterValue("IdentifierCNIC" + index, value.IdentifierCNIC != null ? value.IdentifierCNIC : "-");
                //    rd.SetParameterValue("IdentifierRelation", value.IdentifierRelation != null ? value.IdentifierRelation : "-");
                //    rd.SetParameterValue("IdentifierComments" + index, value.IdentifierComments != null ? value.IdentifierComments : "-");




                //    //    foreach (var item in objResponse.PostMortemReponseOne)
                //    //{

                //    //    rd.SetParameterValue("IdentifierName" + , item.IdentifierName != null ? item.IdentifierName : "-");
                //    //    rd.SetParameterValue("IdentifierCNIC", item.IdentifierCNIC != null ? item.IdentifierCNIC : "-");
                //    //    rd.SetParameterValue("IdentifierRelation", item.IdentifierRelation != null ? item.IdentifierRelation : "-");
                //    //    rd.SetParameterValue("IdentifierComments", item.IdentifierComments != null ? item.IdentifierComments : "-");
                //}

                rd.SetParameterValue("DeathDateTime", objResponse.PostMortemReponseZero[0].DeathDateTime != null ? objResponse.PostMortemReponseZero[0].DeathDateTime.ToString() : "-");

                rd.SetParameterValue("DoctorName", objResponse.PostMortemReponseZero[0].DoctorName != null ? objResponse.PostMortemReponseZero[0].DoctorName : "-");
                rd.SetParameterValue("RecieveDeadBodyInDeathHouseDateTime", objResponse.PostMortemReponseZero[0].RecieveDateTime != null ? objResponse.PostMortemReponseZero[0].RecieveDateTime.ToString() : "-");
                rd.SetParameterValue("ReceivingCompleteDocumentsFromPoliceDateTime", objResponse.PostMortemReponseZero[0].DoctorVisitDateTime != null ? objResponse.PostMortemReponseZero[0].DoctorVisitDateTime.ToString() : "-");
                rd.SetParameterValue("ConductionOfAutospyDateTime", objResponse.PostMortemReponseZero[0].AutopsyDateTime != null ? objResponse.PostMortemReponseZero[0].AutopsyDateTime.ToString() : "-");
                rd.SetParameterValue("InformationFurnishedByPolice", objResponse.PostMortemReponseZero[0].PoliceInformation != null ? objResponse.PostMortemReponseZero[0].PoliceInformation : "-");
                rd.SetParameterValue("Radiological", objResponse.PostMortemReponseZero[0].Radiological != null ? objResponse.PostMortemReponseZero[0].Radiological : "-");
                rd.SetParameterValue("Ultrasound", objResponse.PostMortemReponseZero[0].UltraSound != null ? objResponse.PostMortemReponseZero[0].UltraSound : "-");

                //General Physical Appearance
                rd.SetParameterValue("PhysicalRemarks", objResponse.PostMortemReponseZero[0].PhysicalRemarks != null ? objResponse.PostMortemReponseZero[0].PhysicalRemarks : "-");
                rd.SetParameterValue("ClothRemarks", objResponse.PostMortemReponseZero[0].ClothRemarks != null ? objResponse.PostMortemReponseZero[0].ClothRemarks : "-");
                rd.SetParameterValue("NeckRemarks", objResponse.PostMortemReponseZero[0].NeckRemarks != null ? objResponse.PostMortemReponseZero[0].NeckRemarks : "-");
                rd.SetParameterValue("IncidentPlace", objResponse.PostMortemReponseZero[0].IncidentPlace != null ? objResponse.PostMortemReponseZero[0].IncidentPlace : "-");
                rd.SetParameterValue("MobileNo", objResponse.PostMortemReponseZero[0].MobileNo != null ? objResponse.PostMortemReponseZero[0].MobileNo : "-");
                rd.SetParameterValue("DescriptionOfInjuries", objResponse.PostMortemReponseZero[0].InjuriesRemarks != null ? objResponse.PostMortemReponseZero[0].InjuriesRemarks : "-");

                //II - CRANIUM AND SPINAL CARD
                rd.SetParameterValue("Scalp", objResponse.PostMortemReponseZero[0].Scalp != null ? objResponse.PostMortemReponseZero[0].Scalp : "-");
                rd.SetParameterValue("Skull", objResponse.PostMortemReponseZero[0].Skull != null ? objResponse.PostMortemReponseZero[0].Skull : "-");
                rd.SetParameterValue("Membrance", objResponse.PostMortemReponseZero[0].Membranes != null ? objResponse.PostMortemReponseZero[0].Membranes : "-");
                rd.SetParameterValue("Brain", objResponse.PostMortemReponseZero[0].Brain != null ? objResponse.PostMortemReponseZero[0].Brain : "-");
                rd.SetParameterValue("Vertebrae", objResponse.PostMortemReponseZero[0].Vertebrae != null ? objResponse.PostMortemReponseZero[0].Vertebrae : "-");
                rd.SetParameterValue("SpinalCord", objResponse.PostMortemReponseZero[0].SpinalCord != null ? objResponse.PostMortemReponseZero[0].SpinalCord : "-");

                //III - THORAX
                rd.SetParameterValue("WallsStemum", objResponse.PostMortemReponseZero[0].WallsStemum != null ? objResponse.PostMortemReponseZero[0].WallsStemum : "-");
                rd.SetParameterValue("Pleurae", objResponse.PostMortemReponseZero[0].Pleurae != null ? objResponse.PostMortemReponseZero[0].Pleurae : "-");
                rd.SetParameterValue("LarynxAndTrachea", objResponse.PostMortemReponseZero[0].LaryNx != null ? objResponse.PostMortemReponseZero[0].LaryNx : "-");
                rd.SetParameterValue("RightLung", objResponse.PostMortemReponseZero[0].RightLung != null ? objResponse.PostMortemReponseZero[0].RightLung : "-");
                rd.SetParameterValue("LeftLung", objResponse.PostMortemReponseZero[0].LeftLung != null ? objResponse.PostMortemReponseZero[0].LeftLung : "-");
                rd.SetParameterValue("PericardiumAndHeart", objResponse.PostMortemReponseZero[0].Pencardium != null ? objResponse.PostMortemReponseZero[0].Pencardium : "-");
                rd.SetParameterValue("BloodVessel", objResponse.PostMortemReponseZero[0].BloodVes != null ? objResponse.PostMortemReponseZero[0].BloodVes : "-");

                //IV - ABDOMEN
                rd.SetParameterValue("Walls", objResponse.PostMortemReponseZero[0].Walls != null ? objResponse.PostMortemReponseZero[0].Walls : "-");
                rd.SetParameterValue("Peritoneum", objResponse.PostMortemReponseZero[0].Peritoneum != null ? objResponse.PostMortemReponseZero[0].Peritoneum : "-");
                rd.SetParameterValue("MouthPharynxAndEsophagus", objResponse.PostMortemReponseZero[0].Mouth != null ? objResponse.PostMortemReponseZero[0].Mouth : "-");
                rd.SetParameterValue("Diaphragm", objResponse.PostMortemReponseZero[0].Diaphragm != null ? objResponse.PostMortemReponseZero[0].Diaphragm : "-");
                rd.SetParameterValue("StomatchAndItsContents", objResponse.PostMortemReponseZero[0].StomachContents != null ? objResponse.PostMortemReponseZero[0].StomachContents : "-");
                rd.SetParameterValue("Pancreas", objResponse.PostMortemReponseZero[0].Pancrease != null ? objResponse.PostMortemReponseZero[0].Pancrease : "-");
                rd.SetParameterValue("SmallIntestineAndItsContents", objResponse.PostMortemReponseZero[0].SIntestineContents != null ? objResponse.PostMortemReponseZero[0].SIntestineContents : "-");
                rd.SetParameterValue("LargeIntestineAndItsContents", objResponse.PostMortemReponseZero[0].LIntestineContents != null ? objResponse.PostMortemReponseZero[0].LIntestineContents : "-");
                rd.SetParameterValue("Liver", objResponse.PostMortemReponseZero[0].Liver != null ? objResponse.PostMortemReponseZero[0].Liver : "-");
                rd.SetParameterValue("Spleen", objResponse.PostMortemReponseZero[0].Spleen != null ? objResponse.PostMortemReponseZero[0].Spleen : "-");
                rd.SetParameterValue("RightKidney", objResponse.PostMortemReponseZero[0].RKidneys != null ? objResponse.PostMortemReponseZero[0].RKidneys : "-");
                rd.SetParameterValue("leftKidney", objResponse.PostMortemReponseZero[0].LKidneys != null ? objResponse.PostMortemReponseZero[0].LKidneys : "-");
                rd.SetParameterValue("UrinaryBladder", objResponse.PostMortemReponseZero[0].Unnary != null ? objResponse.PostMortemReponseZero[0].Unnary : "-");
                rd.SetParameterValue("OrgansOfGeneration", objResponse.PostMortemReponseZero[0].Organs != null ? objResponse.PostMortemReponseZero[0].Organs : "-");
                //V - Upper & Lower Limbs
                rd.SetParameterValue("UpperLimbsInjuries", objResponse.PostMortemReponseZero[0].ULInjuries != null ? objResponse.PostMortemReponseZero[0].ULInjuries : "-");
                rd.SetParameterValue("ULDisease", objResponse.PostMortemReponseZero[0].ULDisease != null ? objResponse.PostMortemReponseZero[0].ULDisease : "-");
                rd.SetParameterValue("ULFracutre", objResponse.PostMortemReponseZero[0].ULFracutre != null ? objResponse.PostMortemReponseZero[0].ULFracutre : "-");
                rd.SetParameterValue("ULDislocation", objResponse.PostMortemReponseZero[0].ULDislocation != null ? objResponse.PostMortemReponseZero[0].ULDislocation : "-");
                rd.SetParameterValue("LLInjuries", objResponse.PostMortemReponseZero[0].LLInjuries != null ? objResponse.PostMortemReponseZero[0].LLInjuries : "-");
                rd.SetParameterValue("LLDisease", objResponse.PostMortemReponseZero[0].LLDisease != null ? objResponse.PostMortemReponseZero[0].LLDisease : "-");
                rd.SetParameterValue("LLFracutre", objResponse.PostMortemReponseZero[0].LLFracutre != null ? objResponse.PostMortemReponseZero[0].LLFracutre : "-");
                rd.SetParameterValue("LLDislocation", objResponse.PostMortemReponseZero[0].LLDislocation != null ? objResponse.PostMortemReponseZero[0].LLDislocation : "-");

                rd.SetParameterValue("ChemicalExam", objResponse.PostMortemReponseZero[0].ChemicalExam == true ? "Yes" : "No");
                rd.SetParameterValue("DNALAB", objResponse.PostMortemReponseZero[0].DNALab == true ? "Yes" : "No");
                rd.SetParameterValue("Histopathologist", objResponse.PostMortemReponseZero[0].Histopathologist == true ? "Yes" : "No");
                rd.SetParameterValue("BallisticExpert", objResponse.PostMortemReponseZero[0].BallisticExpert == true ? "Yes" : "No");

                rd.SetParameterValue("ArticleHandOverToPolice", objResponse.PostMortemReponseZero[0].ArticalToPolice != null ? objResponse.PostMortemReponseZero[0].ArticalToPolice : "-");
                rd.SetParameterValue("Opinion", objResponse.PostMortemReponseZero[0].DoctorOpinion != null ? objResponse.PostMortemReponseZero[0].DoctorOpinion : "-");
                rd.SetParameterValue("FinalOpinion", objResponse.PostMortemReponseZero[0].FinalOpinion != null ? objResponse.PostMortemReponseZero[0].FinalOpinion : "-");
                rd.SetParameterValue("TimeBetweenInjuryAndDeath", objResponse.PostMortemReponseZero[0].TimeBetweenInjuryAndDeath != null ? objResponse.PostMortemReponseZero[0].TimeBetweenInjuryAndDeath : "-");
                rd.SetParameterValue("TimeBetweenDeathAndPostmortem", objResponse.PostMortemReponseZero[0].TimeBetweenDeathAndPostmortem != null ? objResponse.PostMortemReponseZero[0].TimeBetweenDeathAndPostmortem : "-");
                rd.SetParameterValue("ElapsedPortableTime", objResponse.PostMortemReponseZero[0].ElapsedPortableTime != null ? objResponse.PostMortemReponseZero[0].ElapsedPortableTime : "-");
                
                rd.SetParameterValue("LabExpertReport", objResponse.PostMortemReponseZero[0].LabExpertReport != null ? objResponse.PostMortemReponseZero[0].LabExpertReport : "-");

                // Images
                // 1
                if (objResponse.PostMortemReponseZero[0].PatientDrawImageUrl != null)
                {
                    //string lastPath = GetLastPath(objResponse.PostMortemReponseZero[0].PatientDrawImageUrl);
                    string tempUrlPath = baseURL + objResponse.PostMortemReponseZero[0].PatientDrawImageUrl;
                    rd.SetParameterValue("PatientDrawImageUrl", tempUrlPath);
                }
                else
                {
                    rd.SetParameterValue("PatientDrawImageUrl", "");
                }
                // 2
                if (objResponse.PostMortemReponseZero[0].PatientManualReportUrl != null)
                {
                    //string lastPath = GetLastPath(objResponse.PostMortemReponseZero[0].PatientManualReportUrl);
                    string tempUrlPath = baseURL + objResponse.PostMortemReponseZero[0].PatientManualReportUrl;
                    rd.SetParameterValue("PatientManualReportUrl", tempUrlPath);
                }
                else
                {
                    rd.SetParameterValue("PatientManualReportUrl", "");
                }
                // 3
                if (objResponse.PostMortemReponseZero[0].PoliceFingerPrintUrl != null)
                {
                    //string lastPath = GetLastPath(objResponse.PostMortemReponseZero[0].PoliceFingerPrintUrl);
                    string tempUrlPath = baseURL + objResponse.PostMortemReponseZero[0].PoliceFingerPrintUrl;
                    rd.SetParameterValue("PoliceFingerPrintUrl", tempUrlPath);
                }
                else
                {
                    rd.SetParameterValue("PoliceFingerPrintUrl", "");
                }
                // 4
                if (objResponse.PostMortemReponseZero[0].PoliceSignatureImageUrl != null)
                {
                    //string lastPath = GetLastPath(objResponse.PostMortemReponseZero[0].PoliceSignatureImageUrl);
                    string tempUrlPath = baseURL + objResponse.PostMortemReponseZero[0].PoliceSignatureImageUrl;
                    rd.SetParameterValue("PoliceSignatureImageUrl", tempUrlPath);
                }
                else
                {
                    rd.SetParameterValue("PoliceSignatureImageUrl", "");
                }

                if (objResponse.PostMortemReponseZero[0].QrCodeImagePath != null)
                {
                    //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                    string tempUrlPath = baseURL + objResponse.PostMortemReponseZero[0].QrCodeImagePath;
                    rd.SetParameterValue("QRCodePath", tempUrlPath);
                }
                else
                {
                    rd.SetParameterValue("QRCodePath", "");
                }

                // Images END






                //rd.SetParameterValue("PoliceSignatureImageUrl", objResponse.PostMortemReponseZero[0].PoliceSignatureImageUrl != null ? objResponse.PostMortemReponseZero[0].PoliceSignatureImageUrl : "-");
                rd.SetParameterValue("PoliceOfficerName", objResponse.PostMortemReponseZero[0].PoliceOfficerName != null ? objResponse.PostMortemReponseZero[0].PoliceOfficerName : "-");
                //rd.SetParameterValue("PoliceFingerPrintUrl", objResponse.PostMortemReponseZero[0].PoliceFingerPrintUrl != null ? objResponse.PostMortemReponseZero[0].PoliceFingerPrintUrl : "");
                rd.SetParameterValue("Date", objResponse.PostMortemReponseZero[0].CreatedOn != null ? objResponse.PostMortemReponseZero[0].CreatedOn.ToString() : "-");
                rd.SetParameterValue("FinalReport", objResponse.PostMortemReponseZero[0].PatientDrawImageUrl != null ? objResponse.PostMortemReponseZero[0].PatientDrawImageUrl : "-");

                //rd.SetDataSource(PatientList);

                var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StreamContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = exportFilename
            };
            return response;
            }else
            {
                throw new Exception("Record Not Found");
            }

        }


        [HttpPost]
        [ActionName("GetBirthCertificate")]
        public HttpResponseMessage GetBirthCertificate(Guid patientVisitId)
        {

            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetSingleBirthCertificateReport";

            string report = "~/Reports";
            string reportFileName = "BirthCertificate.rpt";
            string exportFilename = "BC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = _emcService.GetBirthCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);


            //rd.SetDataSource(PatientList);
            //rd.SetParameterValue("PersonName", "-");
            //rd.SetParameterValue("ToDate", "-");
            rd.SetParameterValue("ReportTitle", "BIRTH CERTIFICATE");
            rd.SetParameterValue("MRNo", PatientList[0].Mrno != null ? PatientList[0].Mrno : "-");
            rd.SetParameterValue("MotherName", PatientList[0].MotherName != null ? "Mr/Mrs " + PatientList[0].MotherName : "-");
            rd.SetParameterValue("MotherCnic", PatientList[0].MotherCnic != null ? PatientList[0].MotherCnic : "-");
            rd.SetParameterValue("ChildName", PatientList[0].ChildName != null ? PatientList[0].ChildName : "-");
            rd.SetParameterValue("FatherName", PatientList[0].FatherName != null ? "Mr/Mrs " + PatientList[0].FatherName : "-");
            rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.ToString() != null ? PatientList[0].IssueDate.Value.ToString("dd/MM/yyyy") : "-");
            rd.SetParameterValue("FatherCnic", PatientList[0].FatherCnic != null ? PatientList[0].FatherCnic : "-");
            rd.SetParameterValue("GrandFatherName", PatientList[0].GrandFatherName != null ? "Mr/Mrs " + PatientList[0].GrandFatherName : "-");
            rd.SetParameterValue("GrandFatherCnic", PatientList[0].GrandFatherCnic != null ? PatientList[0].GrandFatherCnic : "-");
            rd.SetParameterValue("EntryDate", PatientList[0].EntryDate.ToString() != null ? PatientList[0].EntryDate.Value.ToString("dd/MM/yyyy") : "-");
            rd.SetParameterValue("AnnualNumber", PatientList[0].AnnualNumber != null ? PatientList[0].AnnualNumber.ToString() : "-");
            rd.SetParameterValue("DOB", PatientList[0].Dob.ToString() != null ? PatientList[0].Dob.Value.ToString("dd/MM/yyyy") : "-");

            rd.SetParameterValue("PlaceOfBirth", PatientList[0].PlaceOfBirth != null ? PatientList[0].PlaceOfBirth : "-");
            rd.SetParameterValue("GynaeUnit", PatientList[0].GynaeUnit != null ? PatientList[0].GynaeUnit : "-");
            rd.SetParameterValue("MotherNationality", PatientList[0].MotherNationality != null ? PatientList[0].MotherNationality : "-");
            rd.SetParameterValue("FatherNationality", PatientList[0].FatherNationality != null ? PatientList[0].FatherNationality : "-");
            rd.SetParameterValue("ChildBloodGroup", PatientList[0].ChildBloodGroup != null ? PatientList[0].ChildBloodGroup : "-");
            rd.SetParameterValue("MotherBloodGroup", PatientList[0].MotherBloodGroup != null ? PatientList[0].MotherBloodGroup : "-");
            rd.SetParameterValue("Occupation", PatientList[0].Occupation != null ? PatientList[0].Occupation : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList[0].HealthFacilityName != null ? PatientList[0].HealthFacilityName : "-");
            rd.SetParameterValue("Gender", PatientList[0].Gender != null ? PatientList[0].Gender : "-");
            rd.SetParameterValue("Tehsil", PatientList[0].Tehsil != null ? PatientList[0].Tehsil : "-");
            rd.SetParameterValue("District", PatientList[0].District != null ? PatientList[0].District : "-");
            rd.SetParameterValue("ParmanentAddress", PatientList[0].ParmanentAddress != null ? PatientList[0].ParmanentAddress : "-");
            rd.SetParameterValue("Relation", PatientList[0].Relation != null ? PatientList[0].Relation : "-");
            rd.SetParameterValue("ApplicantName", PatientList[0].ApplicantName != null ? PatientList[0].ApplicantName : "-");
            rd.SetParameterValue("ApplicantCnic", PatientList[0].ApplicantCnic != null ? PatientList[0].ApplicantCnic : "-");
            rd.SetParameterValue("EntryStatus", PatientList[0].EntryStatus != null ? PatientList[0].EntryStatus : "-");
            rd.SetParameterValue("TrackingId", PatientList[0].TrackingId != null ? PatientList[0].TrackingId : "-");
            rd.SetParameterValue("DistrictOfBirth", PatientList[0].DistrictOfBirth != null ? PatientList[0].DistrictOfBirth : "-");


            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            //rd.SetParameterValue("CrmsNo", PatientList[0].CrmsNo);

            rd.SetParameterValue("CrmsNo", PatientList[0].CrmsNo != null ? PatientList[0].CrmsNo : "-");

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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

        [HttpGet]
        [ActionName("GetSingleBirthCertificate")]
        public HttpResponseMessage GetSingleBirthCertificate(Guid patientVisitId)
        {

            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetSingleBirthCertificateReport";

            string report = "~/Reports";
            string reportFileName = "BirthCertificate.rpt";
            string exportFilename = "BC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";
            var PatientList = _emcService.GetBirthCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);


            //rd.SetDataSource(PatientList);
            //rd.SetParameterValue("PersonName", "-");
            //rd.SetParameterValue("ToDate", "-");
            rd.SetParameterValue("ReportTitle", "BIRTH CERTIFICATE");
            rd.SetParameterValue("MRNo", PatientList[0].Mrno != null ? PatientList[0].Mrno : "-");
            rd.SetParameterValue("MotherName", PatientList[0].MotherName != null ? "Mr/Mrs " + PatientList[0].MotherName : "-");
            rd.SetParameterValue("MotherCnic", PatientList[0].MotherCnic != null ? PatientList[0].MotherCnic : "-");
            rd.SetParameterValue("ChildName", PatientList[0].ChildName != null ? PatientList[0].ChildName : "-");
            rd.SetParameterValue("FatherName", PatientList[0].FatherName != null ? "Mr/Mrs " + PatientList[0].FatherName : "-");
            rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.ToString() != null ? PatientList[0].IssueDate.Value.ToString("dd/MM/yyyy") : "-");
            rd.SetParameterValue("FatherCnic", PatientList[0].FatherCnic != null ? PatientList[0].FatherCnic : "-");
            rd.SetParameterValue("GrandFatherName", PatientList[0].GrandFatherName != null ? "Mr/Mrs " + PatientList[0].GrandFatherName : "-");
            rd.SetParameterValue("GrandFatherCnic", PatientList[0].GrandFatherCnic != null ? PatientList[0].GrandFatherCnic : "-");
            rd.SetParameterValue("EntryDate", PatientList[0].EntryDate.ToString() != null ? PatientList[0].EntryDate.Value.ToString("dd/MM/yyyy") : "-");
            rd.SetParameterValue("AnnualNumber", PatientList[0].AnnualNumber != null ? PatientList[0].AnnualNumber.ToString() : "-");
            rd.SetParameterValue("DOB", PatientList[0].Dob.ToString() != null ? PatientList[0].Dob.Value.ToString("dd/MM/yyyy") : "-");

            rd.SetParameterValue("PlaceOfBirth", PatientList[0].PlaceOfBirth != null ? PatientList[0].PlaceOfBirth : "-");
            rd.SetParameterValue("GynaeUnit", PatientList[0].GynaeUnit != null ? PatientList[0].GynaeUnit : "-");
            rd.SetParameterValue("MotherNationality", PatientList[0].MotherNationality != null ? PatientList[0].MotherNationality : "-");
            rd.SetParameterValue("FatherNationality", PatientList[0].FatherNationality != null ? PatientList[0].FatherNationality : "-");
            rd.SetParameterValue("ChildBloodGroup", PatientList[0].ChildBloodGroup != null ? PatientList[0].ChildBloodGroup : "-");
            rd.SetParameterValue("MotherBloodGroup", PatientList[0].MotherBloodGroup != null ? PatientList[0].MotherBloodGroup : "-");
            rd.SetParameterValue("Occupation", PatientList[0].Occupation != null ? PatientList[0].Occupation : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList[0].HealthFacilityName != null ? PatientList[0].HealthFacilityName : "-");
            rd.SetParameterValue("Gender", PatientList[0].Gender != null ? PatientList[0].Gender : "-");
            rd.SetParameterValue("Tehsil", PatientList[0].Tehsil != null ? PatientList[0].Tehsil : "-");
            rd.SetParameterValue("District", PatientList[0].District != null ? PatientList[0].District : "-");
            rd.SetParameterValue("ParmanentAddress", PatientList[0].ParmanentAddress != null ? PatientList[0].ParmanentAddress : "-");
            rd.SetParameterValue("Relation", PatientList[0].Relation != null ? PatientList[0].Relation : "-");
            rd.SetParameterValue("ApplicantName", PatientList[0].ApplicantName != null ? PatientList[0].ApplicantName : "-");
            rd.SetParameterValue("ApplicantCnic", PatientList[0].ApplicantCnic != null ? PatientList[0].ApplicantCnic : "-");
            rd.SetParameterValue("EntryStatus", PatientList[0].EntryStatus != null ? PatientList[0].EntryStatus : "-");
            rd.SetParameterValue("TrackingId", PatientList[0].TrackingId != null ? PatientList[0].TrackingId : "-");
            rd.SetParameterValue("DistrictOfBirth", PatientList[0].DistrictOfBirth != null ? PatientList[0].DistrictOfBirth : "-");


            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            //rd.SetParameterValue("CrmsNo", PatientList[0].CrmsNo);

            rd.SetParameterValue("CrmsNo", PatientList[0].CrmsNo != null ? PatientList[0].CrmsNo : "-");

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        [ActionName("GetDeathCertificate")]
        public HttpResponseMessage GetDeathCertificate(Guid patientVisitId)
        {


            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetDeathCertificateReport";

            string report = "~/Reports";
            string reportFileName = "DeathCertificate.rpt";
            string exportFilename = "DC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList = _emcService.GetDeathCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);

            rd.SetParameterValue("ReportTitle", "DEATH CERTIFICATE");
            rd.SetParameterValue("FullName", PatientList[0].FullName != null ? PatientList[0].FullName : "-");
            rd.SetParameterValue("CNIC", PatientList[0].CNIC != null ? PatientList[0].CNIC : "-");
            rd.SetParameterValue("TrackingId", PatientList[0].TrackingId != null ? PatientList[0].TrackingId : "-");
            rd.SetParameterValue("CrmsNo", PatientList[0].CrmsNo != null ? PatientList[0].CrmsNo : "-");
            rd.SetParameterValue("DeceasedPersonName", PatientList[0].DeceasedPersonName != null ? "Mr/Ms " + PatientList[0].DeceasedPersonName : "-");
            rd.SetParameterValue("DeceasedPersonCNIC", PatientList[0].DeceasedPersonCNIC != null ? PatientList[0].DeceasedPersonCNIC : "-");
            rd.SetParameterValue("DeceasedPersonSicknessPeriod", PatientList[0].DeceasedPersonSicknessPeriod.ToString() != null ? PatientList[0].DeceasedPersonSicknessPeriod.ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDob", PatientList[0].DeceasedPersonDob.ToString() != null ? PatientList[0].DeceasedPersonDob.Value.ToShortDateString().ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDateAndTimeOfAdmission", PatientList[0].DeceasedPersonDateAndTimeOfAdmission.ToString() != null ? PatientList[0].DeceasedPersonDateAndTimeOfAdmission.ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDateAndTimeOfDeath", PatientList[0].DeceasedPersonDateAndTimeOfDeath.ToString() != null ? PatientList[0].DeceasedPersonDateAndTimeOfDeath.ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDateOfBurlal", PatientList[0].DeceasedPersonDateOfBurlal.ToString() != null ? PatientList[0].DeceasedPersonDateOfBurlal.Value.ToShortDateString().ToString() : "-");
            rd.SetParameterValue("DeadBodyReceivedBy", PatientList[0].DeadBodyReceivedBy != null ? PatientList[0].DeadBodyReceivedBy : "-");
            rd.SetParameterValue("PlaceOfDeath", PatientList[0].PlaceOfDeath != null ? PatientList[0].PlaceOfDeath : "-");
            rd.SetParameterValue("CauseOfDeath", PatientList[0].CauseOfDeath != null ? PatientList[0].CauseOfDeath : "-");
            rd.SetParameterValue("NatureOfDeath", PatientList[0].NatureOfDeath != null ? PatientList[0].NatureOfDeath : "-");
            rd.SetParameterValue("BuriedAt", PatientList[0].BuriedAt != null ? PatientList[0].BuriedAt : "-");
            rd.SetParameterValue("MotherName", PatientList[0].MotherName != null ? "Mr/Ms " + PatientList[0].MotherName : "-");
            rd.SetParameterValue("MotherCNIC", PatientList[0].MotherCNIC != null ? PatientList[0].MotherCNIC : "-");
            rd.SetParameterValue("FatherName", PatientList[0].FatherName != null ? "Mr/Ms " + PatientList[0].FatherName : "-");
            rd.SetParameterValue("FatherCNIC", PatientList[0].FatherCNIC != null ? PatientList[0].FatherCNIC : "-");
            rd.SetParameterValue("HusbandName", PatientList[0].HusbandName != null ? PatientList[0].HusbandName : "-");
            rd.SetParameterValue("HusbandCNIC", PatientList[0].HusbandCNIC != null ? PatientList[0].HusbandCNIC : "-");
            rd.SetParameterValue("Address", PatientList[0].Address != null ? PatientList[0].Address : "-");
            rd.SetParameterValue("ApplicantName", PatientList[0].ApplicantName != null ? "Mr/Ms " + PatientList[0].ApplicantName : "-");
            rd.SetParameterValue("ApplicantCNIC", PatientList[0].ApplicantCNIC != null ? PatientList[0].ApplicantCNIC : "-");
            rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.ToString() != null ? PatientList[0].IssueDate.Value.ToShortDateString() : "-");
            rd.SetParameterValue("EntryDate", PatientList[0].EntryDate.ToString() != null ? PatientList[0].EntryDate.Value.ToShortDateString() : "-");
            rd.SetParameterValue("DeceasedPersonNationality", PatientList[0].DeceasedPersonNationality.ToString() != null ? PatientList[0].DeceasedPersonNationality.ToString() : "-");
            rd.SetParameterValue("EntryStatus", PatientList[0].EntryStatus != null ? PatientList[0].EntryStatus : "-");
            rd.SetParameterValue("Gender", PatientList[0].Gender != null ? PatientList[0].Gender : "-");
            rd.SetParameterValue("Religion", PatientList[0].Religion != null ? PatientList[0].Religion : "-");
            rd.SetParameterValue("Relation", PatientList[0].Relation != null ? PatientList[0].Relation : "-");
            rd.SetParameterValue("District", PatientList[0].District != null ? PatientList[0].District : "-");
            rd.SetParameterValue("Tehsil", PatientList[0].Tehsil != null ? PatientList[0].Tehsil : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList[0].HealthFacilityName != null ? PatientList[0].HealthFacilityName : "-");

            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }


            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        
        
        [HttpGet]
        [ActionName("GetSingleDeathCertificate")]
        public HttpResponseMessage GetSingleDeathCertificate(Guid patientVisitId)
        {


            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetDeathCertificateReport";

            string report = "~/Reports";
            string reportFileName = "DeathCertificate.rpt";
            string exportFilename = "DC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList = _emcService.GetDeathCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);

            rd.SetParameterValue("ReportTitle", "DEATH CERTIFICATE");
            rd.SetParameterValue("FullName", PatientList[0].FullName != null ? PatientList[0].FullName : "-");
            rd.SetParameterValue("CNIC", PatientList[0].CNIC != null ? PatientList[0].CNIC : "-");
            rd.SetParameterValue("TrackingId", PatientList[0].TrackingId != null ? PatientList[0].TrackingId : "-");
            rd.SetParameterValue("CrmsNo", PatientList[0].CrmsNo != null ? PatientList[0].CrmsNo : "-");
            rd.SetParameterValue("DeceasedPersonName", PatientList[0].DeceasedPersonName != null ? "Mr/Ms " + PatientList[0].DeceasedPersonName : "-");
            rd.SetParameterValue("DeceasedPersonCNIC", PatientList[0].DeceasedPersonCNIC != null ? PatientList[0].DeceasedPersonCNIC : "-");
            rd.SetParameterValue("DeceasedPersonSicknessPeriod", PatientList[0].DeceasedPersonSicknessPeriod.ToString() != null ? PatientList[0].DeceasedPersonSicknessPeriod.ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDob", PatientList[0].DeceasedPersonDob.ToString() != null ? PatientList[0].DeceasedPersonDob.Value.ToShortDateString().ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDateAndTimeOfAdmission", PatientList[0].DeceasedPersonDateAndTimeOfAdmission.ToString() != null ? PatientList[0].DeceasedPersonDateAndTimeOfAdmission.ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDateAndTimeOfDeath", PatientList[0].DeceasedPersonDateAndTimeOfDeath.ToString() != null ? PatientList[0].DeceasedPersonDateAndTimeOfDeath.ToString() : "-");
            rd.SetParameterValue("DeceasedPersonDateOfBurlal", PatientList[0].DeceasedPersonDateOfBurlal.ToString() != null ? PatientList[0].DeceasedPersonDateOfBurlal.Value.ToShortDateString().ToString() : "-");
            rd.SetParameterValue("DeadBodyReceivedBy", PatientList[0].DeadBodyReceivedBy != null ? PatientList[0].DeadBodyReceivedBy : "-");
            rd.SetParameterValue("PlaceOfDeath", PatientList[0].PlaceOfDeath != null ? PatientList[0].PlaceOfDeath : "-");
            rd.SetParameterValue("CauseOfDeath", PatientList[0].CauseOfDeath != null ? PatientList[0].CauseOfDeath : "-");
            rd.SetParameterValue("NatureOfDeath", PatientList[0].NatureOfDeath != null ? PatientList[0].NatureOfDeath : "-");
            rd.SetParameterValue("BuriedAt", PatientList[0].BuriedAt != null ? PatientList[0].BuriedAt : "-");
            rd.SetParameterValue("MotherName", PatientList[0].MotherName != null ? "Mr/Ms " + PatientList[0].MotherName : "-");
            rd.SetParameterValue("MotherCNIC", PatientList[0].MotherCNIC != null ? PatientList[0].MotherCNIC : "-");
            rd.SetParameterValue("FatherName", PatientList[0].FatherName != null ? "Mr/Ms " + PatientList[0].FatherName : "-");
            rd.SetParameterValue("FatherCNIC", PatientList[0].FatherCNIC != null ? PatientList[0].FatherCNIC : "-");
            rd.SetParameterValue("HusbandName", PatientList[0].HusbandName != null ? PatientList[0].HusbandName : "-");
            rd.SetParameterValue("HusbandCNIC", PatientList[0].HusbandCNIC != null ? PatientList[0].HusbandCNIC : "-");
            rd.SetParameterValue("Address", PatientList[0].Address != null ? PatientList[0].Address : "-");
            rd.SetParameterValue("ApplicantName", PatientList[0].ApplicantName != null ? "Mr/Ms " + PatientList[0].ApplicantName : "-");
            rd.SetParameterValue("ApplicantCNIC", PatientList[0].ApplicantCNIC != null ? PatientList[0].ApplicantCNIC : "-");
            rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.ToString() != null ? PatientList[0].IssueDate.Value.ToShortDateString() : "-");
            rd.SetParameterValue("EntryDate", PatientList[0].EntryDate.ToString() != null ? PatientList[0].EntryDate.Value.ToShortDateString() : "-");
            rd.SetParameterValue("DeceasedPersonNationality", PatientList[0].DeceasedPersonNationality.ToString() != null ? PatientList[0].DeceasedPersonNationality.ToString() : "-");
            rd.SetParameterValue("EntryStatus", PatientList[0].EntryStatus != null ? PatientList[0].EntryStatus : "-");
            rd.SetParameterValue("Gender", PatientList[0].Gender != null ? PatientList[0].Gender : "-");
            rd.SetParameterValue("Religion", PatientList[0].Religion != null ? PatientList[0].Religion : "-");
            rd.SetParameterValue("Relation", PatientList[0].Relation != null ? PatientList[0].Relation : "-");
            rd.SetParameterValue("District", PatientList[0].District != null ? PatientList[0].District : "-");
            rd.SetParameterValue("Tehsil", PatientList[0].Tehsil != null ? PatientList[0].Tehsil : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList[0].HealthFacilityName != null ? PatientList[0].HealthFacilityName : "-");

            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }


            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        [ActionName("GetFitnessCertificate")]
        public async Task<HttpResponseMessage> GetFitnessCertificate(Guid patientVisitId)
        {


            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetFitnessCertificateReport";

            string report = "~/Reports";
            string reportFileName = "FitnessCertificate.rpt";
            string exportFilename = "DC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList =await _emcService.GetFitnessCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);


            rd.SetParameterValue("FullName", PatientList.fitnessSingleRecord[0].FullName != null ? PatientList.fitnessSingleRecord[0].FullName : "-");
            rd.SetParameterValue("Profession", PatientList.fitnessSingleRecord[0].Profession != null ? PatientList.fitnessSingleRecord[0].Profession : "-");

            rd.SetParameterValue("Education", PatientList.fitnessSingleRecord[0].Education != null ? PatientList.fitnessSingleRecord[0].Education : "-");

            rd.SetParameterValue("Age", PatientList.fitnessSingleRecord[0].Age.ToString() != null ? PatientList.fitnessSingleRecord[0].Age.ToString() + " years" : "-");
            rd.SetParameterValue("CNIC", PatientList.fitnessSingleRecord[0].CNIC != null ? PatientList.fitnessSingleRecord[0].CNIC : "-");
            rd.SetParameterValue("ParmanentAddress", PatientList.fitnessSingleRecord[0].ParmanentAddress != null ? PatientList.fitnessSingleRecord[0].ParmanentAddress : "-");
            if (PatientList.fitnessSingleRecord[0].DOB.HasValue)
            {
                rd.SetParameterValue("DOB", PatientList.fitnessSingleRecord[0].DOB.ToString() != null ? PatientList.fitnessSingleRecord[0].DOB.Value.ToString("dd/MM/yyyy") : "-");
            }

            rd.SetParameterValue("IssueDate", PatientList.fitnessSingleRecord[0].IssueDate.ToString() != null ? PatientList.fitnessSingleRecord[0].IssueDate.Value.ToString("dd/MM/yyyy") : "-");

            rd.SetParameterValue("CreatedOn", PatientList.fitnessSingleRecord[0].CreatedOn.ToString() != null ? PatientList.fitnessSingleRecord[0].CreatedOn.ToString() : "-");

            rd.SetParameterValue("RelativeName", PatientList.fitnessSingleRecord[0].RelativeName != null ? PatientList.fitnessSingleRecord[0].RelativeName : "-");
            rd.SetParameterValue("EmployeeLetterNo", PatientList.fitnessSingleRecord[0].EmployeeLetterNo != null ? PatientList.fitnessSingleRecord[0].EmployeeLetterNo : "-");
            rd.SetParameterValue("LetterDateTime", PatientList.fitnessSingleRecord[0].LetterDateTime.ToString() != null ? PatientList.fitnessSingleRecord[0].LetterDateTime.Value.ToString("dd/MM/yyyy").ToString() : "-");
            rd.SetParameterValue("DesignationAppliedFor", PatientList.fitnessSingleRecord[0].DesignationAppliedFor != null ? PatientList.fitnessSingleRecord[0].DesignationAppliedFor : "-");
            rd.SetParameterValue("AgeByAppearance", PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() != null ? PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() + " years" : "-");
            rd.SetParameterValue("CheckedBy", PatientList.fitnessSingleRecord[0].CheckedBy != null ? PatientList.fitnessSingleRecord[0].CheckedBy : "-");
            //rd.SetParameterValue("IssueDate", PatientList.fitnessSingleRecord[0].IssueDate.ToString() != null ? PatientList.fitnessSingleRecord[0].IssueDate.ToString() : "-");
            rd.SetParameterValue("BodilyInfirmity", PatientList.fitnessSingleRecord[0].BodilyInfirmity != null ? PatientList.fitnessSingleRecord[0].BodilyInfirmity : "-");
            rd.SetParameterValue("Department", PatientList.fitnessSingleRecord[0].Department != null ? PatientList.fitnessSingleRecord[0].Department : "");

            rd.SetParameterValue("HB", PatientList.fitnessSingleRecord[0].HB != null ? PatientList.fitnessSingleRecord[0].HB : "-");
            rd.SetParameterValue("MCV", PatientList.fitnessSingleRecord[0].MCV != null ? PatientList.fitnessSingleRecord[0].MCV : "-");
            rd.SetParameterValue("HCV", PatientList.fitnessSingleRecord[0].HCV != null ? PatientList.fitnessSingleRecord[0].HCV : "-");
            rd.SetParameterValue("TLC", PatientList.fitnessSingleRecord[0].TLC.ToString() != null ? PatientList.fitnessSingleRecord[0].TLC.ToString() : "-");
            rd.SetParameterValue("Neutorphils", PatientList.fitnessSingleRecord[0].Neutorphils.ToString() != null ? PatientList.fitnessSingleRecord[0].Neutorphils.ToString() : "-");
            rd.SetParameterValue("Lymphocytes", PatientList.fitnessSingleRecord[0].Lymphocytes.ToString() != null ? PatientList.fitnessSingleRecord[0].Lymphocytes.ToString() : "-");
            rd.SetParameterValue("Eosinophils", PatientList.fitnessSingleRecord[0].Eosinophils.ToString() != null ? PatientList.fitnessSingleRecord[0].Eosinophils.ToString() : "-");
            rd.SetParameterValue("Platelets", PatientList.fitnessSingleRecord[0].Platelets.ToString() != null ? PatientList.fitnessSingleRecord[0].Platelets.ToString() : "-");
            rd.SetParameterValue("ESR", PatientList.fitnessSingleRecord[0].ESR.ToString() != null ? PatientList.fitnessSingleRecord[0].ESR.ToString() : "-");
            rd.SetParameterValue("Monocytes", PatientList.fitnessSingleRecord[0].Monocytes.ToString() != null ? PatientList.fitnessSingleRecord[0].Monocytes.ToString() : "-");

            if(PatientList.fitnessSingleRecord[0].IsWidal == true)
            {
                rd.SetParameterValue("WidalOrGeneral", "WIDAL TEST");
                rd.SetParameterValue("VisionOrRemarkHeading", "Remarks");
                rd.SetParameterValue("HeightOrSalmonellaTyphiOHeading", "Salmonella Typhi (O)");
                rd.SetParameterValue("ChestOrSalmonellaTyphiHHeading", "Salmonella Typhi (H)");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAOHeading", "Salmonella Para Typhi A(O)");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAHHeading", "Salmonella Para Typhi A(H)");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBOHeading", "Salmonella Para Typhi B(O)");
                rd.SetParameterValue("BSROrSalmonellaTyphiBHHeading", "Salmonella Para Typhi B(H)");
                rd.SetParameterValue("HepB", "-");
                rd.SetParameterValue("HepC", "-");
                rd.SetParameterValue("PregnancyTestHeading", "-");

                rd.SetParameterValue("VisionOrRemark", PatientList.fitnessSingleRecord[0].Remarks?.ToString() != null ? PatientList.fitnessSingleRecord[0].Remarks : "-");
                rd.SetParameterValue("BpSystolic", "-");
                rd.SetParameterValue("BpDiaSystolic", "-");
                rd.SetParameterValue("BpSystolicHeading", "-");
                rd.SetParameterValue("BpDiaSystolicHeading", "-");
                rd.SetParameterValue("HeightOrSalmonellaTyphiO", PatientList.fitnessSingleRecord[0].SalmonellaTyphiO.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiO.ToString() : "-");
                rd.SetParameterValue("ChestOrSalmonellaTyphiH", PatientList.fitnessSingleRecord[0].SalmonellaTyphiH.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiH.ToString() : "-");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAO", PatientList.fitnessSingleRecord[0].SalmonellaTyphiAO.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiAO.ToString() : "-");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAH", PatientList.fitnessSingleRecord[0].SalmonellaTyphiAH.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiAH.ToString() : "-");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBO", PatientList.fitnessSingleRecord[0].SalmonellaTyphiBO.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiBO.ToString() : "-");
                rd.SetParameterValue("BSROrSalmonellaTyphiBH", PatientList.fitnessSingleRecord[0].SalmonellaTyphiBH.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiBH.ToString() : "-");
            }
            else
            {
                rd.SetParameterValue("VisionOrRemarkHeading", "Vision");
                rd.SetParameterValue("HeightOrSalmonellaTyphiOHeading", "Height");
                rd.SetParameterValue("ChestOrSalmonellaTyphiHHeading", "Chest");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAOHeading", "Weight");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAHHeading", "Mark of identification");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBOHeading", "HIV");
                rd.SetParameterValue("BpSystolicHeading", "Bp Systolic");
                rd.SetParameterValue("BpDiaSystolicHeading", "Bp Diastolic");

                rd.SetParameterValue("BSROrSalmonellaTyphiBHHeading", "BSR");
                rd.SetParameterValue("HepB", "Hep(B)");
                rd.SetParameterValue("HepC", "Hep(C)");

                char lastChar = PatientList.fitnessSingleRecord[0].CNIC[PatientList.fitnessSingleRecord[0].CNIC.Length - 1];

                // Convert the last character to integer
                int lastDigit = int.Parse(lastChar.ToString());

                if(lastDigit %2 == 0)
                {
                    rd.SetParameterValue("PregnancyTestHeading", "Pregnancy test");
                }
                else
                {
                    rd.SetParameterValue("PregnancyTestHeading", "-");
                }

                rd.SetParameterValue("WidalOrGeneral", "General Parameter");
                rd.SetParameterValue("VisionOrRemark", PatientList.fitnessSingleRecord[0].Vision?.ToString() != null ? PatientList.fitnessSingleRecord[0].Vision : "-");
                rd.SetParameterValue("BpSystolic", PatientList.fitnessSingleRecord[0].BpSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("BpDiaSystolic", PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("HeightOrSalmonellaTyphiO", PatientList.fitnessSingleRecord[0].Height.ToString() != null ? PatientList.fitnessSingleRecord[0].Height.ToString() + " cm" : "-");
                rd.SetParameterValue("ChestOrSalmonellaTyphiH", PatientList.fitnessSingleRecord[0].Chest.ToString() != null ? PatientList.fitnessSingleRecord[0].Chest.ToString() : "-");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAO", PatientList.fitnessSingleRecord[0].Weight.ToString() != null ? PatientList.fitnessSingleRecord[0].Weight.ToString() + " kg" : "-");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAH", PatientList.fitnessSingleRecord[0].MarkOfIdentification != null ? PatientList.fitnessSingleRecord[0].MarkOfIdentification : "-");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBO", PatientList.fitnessSingleRecord[0].HivTest != null ? PatientList.fitnessSingleRecord[0].HivTest : "-");
                rd.SetParameterValue("BSROrSalmonellaTyphiBH", PatientList.fitnessSingleRecord[0].BSR != null ? PatientList.fitnessSingleRecord[0].BSR : "-");
            }
           
          
            rd.SetParameterValue("IsWidal", PatientList.fitnessSingleRecord[0].IsWidal.ToString() != null ? PatientList.fitnessSingleRecord[0].IsWidal.ToString() : "-");

            rd.SetParameterValue("SerologyTestOne", PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() : "-");
            rd.SetParameterValue("SerologyTestTwo", PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() : "-");

            rd.SetParameterValue("Color", PatientList.fitnessSingleRecord[0].Color?.ToString() != null ? PatientList.fitnessSingleRecord[0].Color : "-");
            rd.SetParameterValue("SpecificGravity",PatientList.fitnessSingleRecord[0].SpecificGravity.ToString() != null ? PatientList.fitnessSingleRecord[0].SpecificGravity.ToString() : "-");
            rd.SetParameterValue("PH",PatientList.fitnessSingleRecord[0].PH.ToString() != null ? PatientList.fitnessSingleRecord[0].PH.ToString() : "-");
            rd.SetParameterValue("Protien", PatientList.fitnessSingleRecord[0].Protien.ToString() != null ? PatientList.fitnessSingleRecord[0].Protien.ToString() : "-");
            rd.SetParameterValue("Glucose", PatientList.fitnessSingleRecord[0].Glucose.ToString() != null ? PatientList.fitnessSingleRecord[0].Glucose.ToString() : "-");
            rd.SetParameterValue("Ketones", PatientList.fitnessSingleRecord[0].Ketones.ToString() != null ? PatientList.fitnessSingleRecord[0].Ketones.ToString() : "-");
            rd.SetParameterValue("Urobilinogen", PatientList.fitnessSingleRecord[0].Urobilinogen.ToString() != null ? PatientList.fitnessSingleRecord[0].Urobilinogen.ToString() : "-");
            rd.SetParameterValue("PussCells", PatientList.fitnessSingleRecord[0].PussCells.ToString() != null ? PatientList.fitnessSingleRecord[0].PussCells.ToString() : "-");
            rd.SetParameterValue("RBCs", PatientList.fitnessSingleRecord[0].RBCs.ToString() != null ? PatientList.fitnessSingleRecord[0].RBCs.ToString() : "-");
            rd.SetParameterValue("Crystals", PatientList.fitnessSingleRecord[0].Crystals.ToString() != null ? PatientList.fitnessSingleRecord[0].Crystals.ToString() : "-");
            rd.SetParameterValue("EpethlialCells", PatientList.fitnessSingleRecord[0].EpethlialCells.ToString() != null ? PatientList.fitnessSingleRecord[0].EpethlialCells.ToString() : "-");
            rd.SetParameterValue("Bacteria", PatientList.fitnessSingleRecord[0].Bacteria.ToString() != null ? PatientList.fitnessSingleRecord[0].Bacteria.ToString() : "-");
            rd.SetParameterValue("Casts", PatientList.fitnessSingleRecord[0].Casts.ToString() != null ? PatientList.fitnessSingleRecord[0].Casts.ToString() : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() != null ? PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() : "-");
           

            string patientImage = "";
            if (PatientList.fitnessSingleRecord[0].PatientImageUrl != null)
            {
                patientImage = baseURL + PatientList.fitnessSingleRecord[0].PatientImageUrl;

            }

            rd.SetParameterValue("PatientImageUrl", patientImage != null ? patientImage : "-");
            string PatientRightThumb = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl != null)
            {
                PatientRightThumb = baseURL + PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl;

            }

            rd.SetParameterValue("PatientRightThumbImageUrl", PatientRightThumb != null ? PatientRightThumb : "-");
            string PatientRightIndex = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl != null)
            {
                PatientRightIndex = baseURL + PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl;

            }


            rd.SetParameterValue("PatientRightIndexImageUrl", PatientRightIndex != null ? PatientRightIndex : "-" ?? "-");
            string PatientRightMiddle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl != null)
            {
                PatientRightMiddle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl;

            }

            rd.SetParameterValue("PatientRightMiddleImageUrl", PatientRightMiddle != null ? PatientRightMiddle : "-");
            string PatientRightRing = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl != null)
            {
                PatientRightRing = baseURL + PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl;

            }


            rd.SetParameterValue("PatientRightRingImageUrl", PatientRightRing != null ? PatientRightRing : "-");
            string PatientRightLittle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl != null)
            {
                PatientRightLittle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl;

            }

            rd.SetParameterValue("PatientRightLittleImageUrl", PatientRightLittle != null ? PatientRightLittle : "-");

            rd.SetParameterValue("Colour", PatientList.fitnessSingleRecord[0].Colour != null ? PatientList.fitnessSingleRecord[0].Colour : "-");
            rd.SetParameterValue("Consistency", PatientList.fitnessSingleRecord[0].Consistency != null ? PatientList.fitnessSingleRecord[0].Consistency : "-");
            rd.SetParameterValue("Mucus", PatientList.fitnessSingleRecord[0].Mucus.ToString() != null ? PatientList.fitnessSingleRecord[0].Mucus.ToString() : "-");
            rd.SetParameterValue("Blood", PatientList.fitnessSingleRecord[0].Blood.ToString() != null ? PatientList.fitnessSingleRecord[0].Blood.ToString() : "-");
            rd.SetParameterValue("FitnessStoolExaminationPussCells", PatientList.fitnessSingleRecord[0].FitnessStoolExaminationPussCells.ToString() != null ? PatientList.fitnessSingleRecord[0].FitnessStoolExaminationPussCells.ToString() : "-");
            rd.SetParameterValue("OVA", PatientList.fitnessSingleRecord[0].OVA.ToString() != null ? PatientList.fitnessSingleRecord[0].OVA.ToString() : "-");
            rd.SetParameterValue("FitnessStoolExaminationRBCs", PatientList.fitnessSingleRecord[0].FitnessStoolExaminationRBCs.ToString() != null ? PatientList.fitnessSingleRecord[0].FitnessStoolExaminationRBCs.ToString() : "-");
            rd.SetParameterValue("VegetativeForms", PatientList.fitnessSingleRecord[0].VegetativeForms.ToString() != null ? PatientList.fitnessSingleRecord[0].VegetativeForms.ToString() : "-");
            rd.SetParameterValue("OccultBlood", PatientList.fitnessSingleRecord[0].OccultBlood.ToString() != null ? PatientList.fitnessSingleRecord[0].OccultBlood.ToString() : "-");
            rd.SetParameterValue("XRayChestPAView", PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() != null ? PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() : " ");

            rd.SetParameterValue("UserName", PatientList.fitnessSingleRecord[0].UserName?.ToString() != null ? PatientList.fitnessSingleRecord[0].UserName?.ToString() : "-");

            rd.SetParameterValue("SerologyTest1Value", PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() : "-");
            rd.SetParameterValue("SerologyTest2Value", PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() : "-");
            rd.SetParameterValue("PregNancyTest", PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() : "-");
            rd.SetParameterValue("HepBTest", PatientList.fitnessSingleRecord[0].HepBTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepBTest?.ToString() : "-");
            rd.SetParameterValue("HepCTest", PatientList.fitnessSingleRecord[0].HepCTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepCTest?.ToString() : "-");

            if (PatientList.fitnessSingleRecord[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList.fitnessSingleRecord[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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

        [HttpGet]
        [ActionName("GetSingleFitnessCertificate")]
        public async Task<HttpResponseMessage> GetSingleFitnessCertificate(Guid patientVisitId)
        {


            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetFitnessCertificateReport";

            string report = "~/Reports";
            string reportFileName = "FitnessCertificate.rpt";
            string exportFilename = "DC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList =await _emcService.GetFitnessCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);


            rd.SetParameterValue("FullName", PatientList.fitnessSingleRecord[0].FullName != null ? PatientList.fitnessSingleRecord[0].FullName : "-");
            rd.SetParameterValue("Profession", PatientList.fitnessSingleRecord[0].Profession != null ? PatientList.fitnessSingleRecord[0].Profession : "-");

            rd.SetParameterValue("Education", PatientList.fitnessSingleRecord[0].Education != null ? PatientList.fitnessSingleRecord[0].Education : "-");

            rd.SetParameterValue("Age", PatientList.fitnessSingleRecord[0].Age.ToString() != null ? PatientList.fitnessSingleRecord[0].Age.ToString() + " years" : "-");
            rd.SetParameterValue("CNIC", PatientList.fitnessSingleRecord[0].CNIC != null ? PatientList.fitnessSingleRecord[0].CNIC : "-");
            rd.SetParameterValue("ParmanentAddress", PatientList.fitnessSingleRecord[0].ParmanentAddress != null ? PatientList.fitnessSingleRecord[0].ParmanentAddress : "-");
            if (PatientList.fitnessSingleRecord[0].DOB.HasValue)
            {
                rd.SetParameterValue("DOB", PatientList.fitnessSingleRecord[0].DOB.ToString() != null ? PatientList.fitnessSingleRecord[0].DOB.Value.ToString("dd/MM/yyyy") : "-");
            }

            rd.SetParameterValue("IssueDate", PatientList.fitnessSingleRecord[0].IssueDate.ToString() != null ? PatientList.fitnessSingleRecord[0].IssueDate.Value.ToString("dd/MM/yyyy") : "-");

            rd.SetParameterValue("CreatedOn", PatientList.fitnessSingleRecord[0].CreatedOn.ToString() != null ? PatientList.fitnessSingleRecord[0].CreatedOn.ToString() : "-");

            rd.SetParameterValue("RelativeName", PatientList.fitnessSingleRecord[0].RelativeName != null ? PatientList.fitnessSingleRecord[0].RelativeName : "-");
            rd.SetParameterValue("EmployeeLetterNo", PatientList.fitnessSingleRecord[0].EmployeeLetterNo != null ? PatientList.fitnessSingleRecord[0].EmployeeLetterNo : "-");
            rd.SetParameterValue("LetterDateTime", PatientList.fitnessSingleRecord[0].LetterDateTime.ToString() != null ? PatientList.fitnessSingleRecord[0].LetterDateTime.Value.ToString("dd/MM/yyyy").ToString() : "-");
            rd.SetParameterValue("DesignationAppliedFor", PatientList.fitnessSingleRecord[0].DesignationAppliedFor != null ? PatientList.fitnessSingleRecord[0].DesignationAppliedFor : "-");
            rd.SetParameterValue("AgeByAppearance", PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() != null ? PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() + " years" : "-");
            rd.SetParameterValue("CheckedBy", PatientList.fitnessSingleRecord[0].CheckedBy != null ? PatientList.fitnessSingleRecord[0].CheckedBy : "-");
            //rd.SetParameterValue("IssueDate", PatientList.fitnessSingleRecord[0].IssueDate.ToString() != null ? PatientList.fitnessSingleRecord[0].IssueDate.ToString() : "-");
            rd.SetParameterValue("BodilyInfirmity", PatientList.fitnessSingleRecord[0].BodilyInfirmity != null ? PatientList.fitnessSingleRecord[0].BodilyInfirmity : "-");
            rd.SetParameterValue("Department", PatientList.fitnessSingleRecord[0].Department != null ? PatientList.fitnessSingleRecord[0].Department : "");

            rd.SetParameterValue("HB", PatientList.fitnessSingleRecord[0].HB != null ? PatientList.fitnessSingleRecord[0].HB : "-");
            rd.SetParameterValue("MCV", PatientList.fitnessSingleRecord[0].MCV != null ? PatientList.fitnessSingleRecord[0].MCV : "-");
            rd.SetParameterValue("HCV", PatientList.fitnessSingleRecord[0].HCV != null ? PatientList.fitnessSingleRecord[0].HCV : "-");
            rd.SetParameterValue("TLC", PatientList.fitnessSingleRecord[0].TLC.ToString() != null ? PatientList.fitnessSingleRecord[0].TLC.ToString() : "-");
            rd.SetParameterValue("Neutorphils", PatientList.fitnessSingleRecord[0].Neutorphils.ToString() != null ? PatientList.fitnessSingleRecord[0].Neutorphils.ToString() : "-");
            rd.SetParameterValue("Lymphocytes", PatientList.fitnessSingleRecord[0].Lymphocytes.ToString() != null ? PatientList.fitnessSingleRecord[0].Lymphocytes.ToString() : "-");
            rd.SetParameterValue("Eosinophils", PatientList.fitnessSingleRecord[0].Eosinophils.ToString() != null ? PatientList.fitnessSingleRecord[0].Eosinophils.ToString() : "-");
            rd.SetParameterValue("Platelets", PatientList.fitnessSingleRecord[0].Platelets.ToString() != null ? PatientList.fitnessSingleRecord[0].Platelets.ToString() : "-");
            rd.SetParameterValue("ESR", PatientList.fitnessSingleRecord[0].ESR.ToString() != null ? PatientList.fitnessSingleRecord[0].ESR.ToString() : "-");
            rd.SetParameterValue("Monocytes", PatientList.fitnessSingleRecord[0].Monocytes.ToString() != null ? PatientList.fitnessSingleRecord[0].Monocytes.ToString() : "-");

            if(PatientList.fitnessSingleRecord[0].IsWidal == true)
            {
                rd.SetParameterValue("WidalOrGeneral", "WIDAL TEST");
                rd.SetParameterValue("VisionOrRemarkHeading", "Remarks");
                rd.SetParameterValue("HeightOrSalmonellaTyphiOHeading", "Salmonella Typhi (O)");
                rd.SetParameterValue("ChestOrSalmonellaTyphiHHeading", "Salmonella Typhi (H)");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAOHeading", "Salmonella Para Typhi A(O)");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAHHeading", "Salmonella Para Typhi A(H)");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBOHeading", "Salmonella Para Typhi B(O)");
                rd.SetParameterValue("BSROrSalmonellaTyphiBHHeading", "Salmonella Para Typhi B(H)");
                rd.SetParameterValue("HepB", "-");
                rd.SetParameterValue("HepC", "-");
                rd.SetParameterValue("PregnancyTestHeading", "-");

                rd.SetParameterValue("VisionOrRemark", PatientList.fitnessSingleRecord[0].Remarks?.ToString() != null ? PatientList.fitnessSingleRecord[0].Remarks : "-");
                rd.SetParameterValue("BpSystolic", "-");
                rd.SetParameterValue("BpDiaSystolic", "-");
                rd.SetParameterValue("BpSystolicHeading", "-");
                rd.SetParameterValue("BpDiaSystolicHeading", "-");
                rd.SetParameterValue("HeightOrSalmonellaTyphiO", PatientList.fitnessSingleRecord[0].SalmonellaTyphiO.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiO.ToString() : "-");
                rd.SetParameterValue("ChestOrSalmonellaTyphiH", PatientList.fitnessSingleRecord[0].SalmonellaTyphiH.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiH.ToString() : "-");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAO", PatientList.fitnessSingleRecord[0].SalmonellaTyphiAO.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiAO.ToString() : "-");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAH", PatientList.fitnessSingleRecord[0].SalmonellaTyphiAH.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiAH.ToString() : "-");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBO", PatientList.fitnessSingleRecord[0].SalmonellaTyphiBO.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiBO.ToString() : "-");
                rd.SetParameterValue("BSROrSalmonellaTyphiBH", PatientList.fitnessSingleRecord[0].SalmonellaTyphiBH.ToString() != null ? PatientList.fitnessSingleRecord[0].SalmonellaTyphiBH.ToString() : "-");
            }
            else
            {
                rd.SetParameterValue("VisionOrRemarkHeading", "Vision");
                rd.SetParameterValue("HeightOrSalmonellaTyphiOHeading", "Height");
                rd.SetParameterValue("ChestOrSalmonellaTyphiHHeading", "Chest");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAOHeading", "Weight");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAHHeading", "Mark of identification");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBOHeading", "HIV");
                rd.SetParameterValue("BpSystolicHeading", "Bp Systolic");
                rd.SetParameterValue("BpDiaSystolicHeading", "Bp Diastolic");

                rd.SetParameterValue("BSROrSalmonellaTyphiBHHeading", "BSR");
                rd.SetParameterValue("HepB", "Hep(B)");
                rd.SetParameterValue("HepC", "Hep(C)");

                char lastChar = PatientList.fitnessSingleRecord[0].CNIC[PatientList.fitnessSingleRecord[0].CNIC.Length - 1];

                // Convert the last character to integer
                int lastDigit = int.Parse(lastChar.ToString());

                if(lastDigit %2 == 0)
                {
                    rd.SetParameterValue("PregnancyTestHeading", "Pregnancy test");
                }
                else
                {
                    rd.SetParameterValue("PregnancyTestHeading", "-");
                }

                rd.SetParameterValue("WidalOrGeneral", "General Parameter");
                rd.SetParameterValue("VisionOrRemark", PatientList.fitnessSingleRecord[0].Vision?.ToString() != null ? PatientList.fitnessSingleRecord[0].Vision : "-");
                rd.SetParameterValue("BpSystolic", PatientList.fitnessSingleRecord[0].BpSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("BpDiaSystolic", PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("HeightOrSalmonellaTyphiO", PatientList.fitnessSingleRecord[0].Height.ToString() != null ? PatientList.fitnessSingleRecord[0].Height.ToString() + " cm" : "-");
                rd.SetParameterValue("ChestOrSalmonellaTyphiH", PatientList.fitnessSingleRecord[0].Chest.ToString() != null ? PatientList.fitnessSingleRecord[0].Chest.ToString() : "-");
                rd.SetParameterValue("WeightOrSalmonellaTyphiAO", PatientList.fitnessSingleRecord[0].Weight.ToString() != null ? PatientList.fitnessSingleRecord[0].Weight.ToString() + " kg" : "-");
                rd.SetParameterValue("MarkOfIdentificationOrSalmonellaTyphiAH", PatientList.fitnessSingleRecord[0].MarkOfIdentification != null ? PatientList.fitnessSingleRecord[0].MarkOfIdentification : "-");
                rd.SetParameterValue("HivTestOrSalmonellaTyphiBO", PatientList.fitnessSingleRecord[0].HivTest != null ? PatientList.fitnessSingleRecord[0].HivTest : "-");
                rd.SetParameterValue("BSROrSalmonellaTyphiBH", PatientList.fitnessSingleRecord[0].BSR != null ? PatientList.fitnessSingleRecord[0].BSR : "-");
            }
           
          
            rd.SetParameterValue("IsWidal", PatientList.fitnessSingleRecord[0].IsWidal.ToString() != null ? PatientList.fitnessSingleRecord[0].IsWidal.ToString() : "-");

            rd.SetParameterValue("SerologyTestOne", PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() : "-");
            rd.SetParameterValue("SerologyTestTwo", PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() : "-");

            rd.SetParameterValue("Color", PatientList.fitnessSingleRecord[0].Color?.ToString() != null ? PatientList.fitnessSingleRecord[0].Color : "-");
            rd.SetParameterValue("SpecificGravity",PatientList.fitnessSingleRecord[0].SpecificGravity.ToString() != null ? PatientList.fitnessSingleRecord[0].SpecificGravity.ToString() : "-");
            rd.SetParameterValue("PH",PatientList.fitnessSingleRecord[0].PH.ToString() != null ? PatientList.fitnessSingleRecord[0].PH.ToString() : "-");
            rd.SetParameterValue("Protien", PatientList.fitnessSingleRecord[0].Protien.ToString() != null ? PatientList.fitnessSingleRecord[0].Protien.ToString() : "-");
            rd.SetParameterValue("Glucose", PatientList.fitnessSingleRecord[0].Glucose.ToString() != null ? PatientList.fitnessSingleRecord[0].Glucose.ToString() : "-");
            rd.SetParameterValue("Ketones", PatientList.fitnessSingleRecord[0].Ketones.ToString() != null ? PatientList.fitnessSingleRecord[0].Ketones.ToString() : "-");
            rd.SetParameterValue("Urobilinogen", PatientList.fitnessSingleRecord[0].Urobilinogen.ToString() != null ? PatientList.fitnessSingleRecord[0].Urobilinogen.ToString() : "-");
            rd.SetParameterValue("PussCells", PatientList.fitnessSingleRecord[0].PussCells.ToString() != null ? PatientList.fitnessSingleRecord[0].PussCells.ToString() : "-");
            rd.SetParameterValue("RBCs", PatientList.fitnessSingleRecord[0].RBCs.ToString() != null ? PatientList.fitnessSingleRecord[0].RBCs.ToString() : "-");
            rd.SetParameterValue("Crystals", PatientList.fitnessSingleRecord[0].Crystals.ToString() != null ? PatientList.fitnessSingleRecord[0].Crystals.ToString() : "-");
            rd.SetParameterValue("EpethlialCells", PatientList.fitnessSingleRecord[0].EpethlialCells.ToString() != null ? PatientList.fitnessSingleRecord[0].EpethlialCells.ToString() : "-");
            rd.SetParameterValue("Bacteria", PatientList.fitnessSingleRecord[0].Bacteria.ToString() != null ? PatientList.fitnessSingleRecord[0].Bacteria.ToString() : "-");
            rd.SetParameterValue("Casts", PatientList.fitnessSingleRecord[0].Casts.ToString() != null ? PatientList.fitnessSingleRecord[0].Casts.ToString() : "-");
            rd.SetParameterValue("HealthFacilityName", PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() != null ? PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() : "-");
           

            string patientImage = "";
            if (PatientList.fitnessSingleRecord[0].PatientImageUrl != null)
            {
                patientImage = baseURL + PatientList.fitnessSingleRecord[0].PatientImageUrl;

            }

            rd.SetParameterValue("PatientImageUrl", patientImage != null ? patientImage : "-");
            string PatientRightThumb = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl != null)
            {
                PatientRightThumb = baseURL + PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl;

            }

            rd.SetParameterValue("PatientRightThumbImageUrl", PatientRightThumb != null ? PatientRightThumb : "-");
            string PatientRightIndex = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl != null)
            {
                PatientRightIndex = baseURL + PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl;

            }


            rd.SetParameterValue("PatientRightIndexImageUrl", PatientRightIndex != null ? PatientRightIndex : "-" ?? "-");
            string PatientRightMiddle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl != null)
            {
                PatientRightMiddle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl;

            }

            rd.SetParameterValue("PatientRightMiddleImageUrl", PatientRightMiddle != null ? PatientRightMiddle : "-");
            string PatientRightRing = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl != null)
            {
                PatientRightRing = baseURL + PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl;

            }


            rd.SetParameterValue("PatientRightRingImageUrl", PatientRightRing != null ? PatientRightRing : "-");
            string PatientRightLittle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl != null)
            {
                PatientRightLittle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl;

            }

            rd.SetParameterValue("PatientRightLittleImageUrl", PatientRightLittle != null ? PatientRightLittle : "-");

            rd.SetParameterValue("Colour", PatientList.fitnessSingleRecord[0].Colour != null ? PatientList.fitnessSingleRecord[0].Colour : "-");
            rd.SetParameterValue("Consistency", PatientList.fitnessSingleRecord[0].Consistency != null ? PatientList.fitnessSingleRecord[0].Consistency : "-");
            rd.SetParameterValue("Mucus", PatientList.fitnessSingleRecord[0].Mucus.ToString() != null ? PatientList.fitnessSingleRecord[0].Mucus.ToString() : "-");
            rd.SetParameterValue("Blood", PatientList.fitnessSingleRecord[0].Blood.ToString() != null ? PatientList.fitnessSingleRecord[0].Blood.ToString() : "-");
            rd.SetParameterValue("FitnessStoolExaminationPussCells", PatientList.fitnessSingleRecord[0].FitnessStoolExaminationPussCells.ToString() != null ? PatientList.fitnessSingleRecord[0].FitnessStoolExaminationPussCells.ToString() : "-");
            rd.SetParameterValue("OVA", PatientList.fitnessSingleRecord[0].OVA.ToString() != null ? PatientList.fitnessSingleRecord[0].OVA.ToString() : "-");
            rd.SetParameterValue("FitnessStoolExaminationRBCs", PatientList.fitnessSingleRecord[0].FitnessStoolExaminationRBCs.ToString() != null ? PatientList.fitnessSingleRecord[0].FitnessStoolExaminationRBCs.ToString() : "-");
            rd.SetParameterValue("VegetativeForms", PatientList.fitnessSingleRecord[0].VegetativeForms.ToString() != null ? PatientList.fitnessSingleRecord[0].VegetativeForms.ToString() : "-");
            rd.SetParameterValue("OccultBlood", PatientList.fitnessSingleRecord[0].OccultBlood.ToString() != null ? PatientList.fitnessSingleRecord[0].OccultBlood.ToString() : "-");
            rd.SetParameterValue("XRayChestPAView", PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() != null ? PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() : " ");

            rd.SetParameterValue("UserName", PatientList.fitnessSingleRecord[0].UserName?.ToString() != null ? PatientList.fitnessSingleRecord[0].UserName?.ToString() : "-");

            rd.SetParameterValue("SerologyTest1Value", PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() : "-");
            rd.SetParameterValue("SerologyTest2Value", PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() : "-");
            rd.SetParameterValue("PregNancyTest", PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() : "-");
            rd.SetParameterValue("HepBTest", PatientList.fitnessSingleRecord[0].HepBTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepBTest?.ToString() : "-");
            rd.SetParameterValue("HepCTest", PatientList.fitnessSingleRecord[0].HepCTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepCTest?.ToString() : "-");

            if (PatientList.fitnessSingleRecord[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList.fitnessSingleRecord[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        [ActionName("GetFitnessCertificateForAslah")]
        public async Task<HttpResponseMessage> GetFitnessCertificateForAslah(Guid patientVisitId)
        {

            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetFitnessCertificateReport";

            string report = "~/Reports";
            string reportFileName = "FitnessCertificateAslah.rpt";
            string exportFilename = "FCA_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList = await _emcService.GetFitnessCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            //rd.SetDataSource(PatientList.PsychologicalQuestionsAndAnswer);

            rd.SetParameterValue("FullName", PatientList.fitnessSingleRecord[0].FullName != null ? PatientList.fitnessSingleRecord[0].FullName : "-");
            rd.SetParameterValue("IssueDate", PatientList.fitnessSingleRecord[0].IssueDate.Value.ToShortDateString() != null ? PatientList.fitnessSingleRecord[0].IssueDate.Value.ToShortDateString() : "-");

            rd.SetParameterValue("Profession", PatientList.fitnessSingleRecord[0].Profession != null ? PatientList.fitnessSingleRecord[0].Profession : "-");

            rd.SetParameterValue("RecomendedOrNot", PatientList.fitnessSingleRecord[0].RecomendedOrNot != null ? PatientList.fitnessSingleRecord[0].RecomendedOrNot : "-");

            rd.SetParameterValue("Education", PatientList.fitnessSingleRecord[0].Education != null ? PatientList.fitnessSingleRecord[0].Education : "-");

            rd.SetParameterValue("TrackingId", PatientList.fitnessSingleRecord[0].TrackingId != null ? PatientList.fitnessSingleRecord[0].TrackingId : "-");

            rd.SetParameterValue("PresentJob", PatientList.fitnessSingleRecord[0].PresentJob != null ? PatientList.fitnessSingleRecord[0].PresentJob : "-");

            rd.SetParameterValue("Age", PatientList.fitnessSingleRecord[0].Age.ToString() != null ? PatientList.fitnessSingleRecord[0].Age.ToString() + " years" : "-");
            rd.SetParameterValue("CNIC", PatientList.fitnessSingleRecord[0].CNIC != null ? PatientList.fitnessSingleRecord[0].CNIC : "-");
            rd.SetParameterValue("ParmanentAddress", PatientList.fitnessSingleRecord[0].ParmanentAddress != null ? PatientList.fitnessSingleRecord[0].ParmanentAddress : "-");
            if (PatientList.fitnessSingleRecord[0].DOB.HasValue)
            {
                rd.SetParameterValue("DOB", PatientList.fitnessSingleRecord[0].DOB.ToString() != null ? PatientList.fitnessSingleRecord[0].DOB.Value.ToString("dd/MM/yyyy") : "-");
            }
            
            rd.SetParameterValue("RelativeName", PatientList.fitnessSingleRecord[0].RelativeName != null ? PatientList.fitnessSingleRecord[0].RelativeName : "-");
            rd.SetParameterValue("EmployeeLetterNo", PatientList.fitnessSingleRecord[0].EmployeeLetterNo != null ? PatientList.fitnessSingleRecord[0].EmployeeLetterNo : "-");
            rd.SetParameterValue("LetterDateTime", PatientList.fitnessSingleRecord[0].LetterDateTime.ToString() != null ? PatientList.fitnessSingleRecord[0].LetterDateTime.Value.ToString("dd/MM/yyyy").ToString() : "-");
            rd.SetParameterValue("DesignationAppliedFor", PatientList.fitnessSingleRecord[0].DesignationAppliedFor != null ? PatientList.fitnessSingleRecord[0].DesignationAppliedFor : "-");
            rd.SetParameterValue("AgeByAppearance", PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() != null ? PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() + " years" : "-");
            rd.SetParameterValue("CheckedBy", PatientList.fitnessSingleRecord[0].CheckedBy != null ? PatientList.fitnessSingleRecord[0].CheckedBy : "-");
            //rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.ToString() != null ? PatientList[0].IssueDate.ToString() : "-");
            rd.SetParameterValue("BodilyInfirmity", PatientList.fitnessSingleRecord[0].BodilyInfirmity != null ? PatientList.fitnessSingleRecord[0].BodilyInfirmity : "-");
            rd.SetParameterValue("Department", PatientList.fitnessSingleRecord[0].Department != null ? PatientList.fitnessSingleRecord[0].Department : "");

     


                rd.SetParameterValue("Vision", PatientList.fitnessSingleRecord[0].Vision?.ToString() != null ? PatientList.fitnessSingleRecord[0].Vision : "-");
                rd.SetParameterValue("BpSystolic", PatientList.fitnessSingleRecord[0].BpSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("BpDiaSystolic", PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("Height", PatientList.fitnessSingleRecord[0].Height.ToString() != null ? PatientList.fitnessSingleRecord[0].Height.ToString() + " cm" : "-");
                rd.SetParameterValue("Chest", PatientList.fitnessSingleRecord[0].Chest.ToString() != null ? PatientList.fitnessSingleRecord[0].Chest.ToString() : "-");
                rd.SetParameterValue("Weight", PatientList.fitnessSingleRecord[0].Weight.ToString() != null ? PatientList.fitnessSingleRecord[0].Weight.ToString() + " kg" : "-");
                rd.SetParameterValue("MarkOfIdentification", PatientList.fitnessSingleRecord[0].MarkOfIdentification != null ? PatientList.fitnessSingleRecord[0].MarkOfIdentification : "-");
                rd.SetParameterValue("HivTest", PatientList.fitnessSingleRecord[0].HivTest != null ? PatientList.fitnessSingleRecord[0].HivTest : "-");
                rd.SetParameterValue("BSR", PatientList.fitnessSingleRecord[0].BSR != null ? PatientList.fitnessSingleRecord[0].BSR : "-");
                rd.SetParameterValue("HepBTest", PatientList.fitnessSingleRecord[0].HepBTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepBTest?.ToString() : "-");
                rd.SetParameterValue("HepCTest", PatientList.fitnessSingleRecord[0].HepCTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepCTest?.ToString() : "-");
            char lastChar = PatientList.fitnessSingleRecord[0].CNIC[PatientList.fitnessSingleRecord[0].CNIC.Length - 1];

            // Convert the last character to integer
            int lastDigit = int.Parse(lastChar.ToString());

            if (lastDigit % 2 == 0)
            {
                rd.SetParameterValue("PregnancyTestHeading", "Pregnancy test");
            }
            else
            {
                rd.SetParameterValue("PregnancyTestHeading", "-");
            }

            rd.SetParameterValue("PregNancyTest", PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() : "-");
            rd.SetParameterValue("BpSystolic", PatientList.fitnessSingleRecord[0].BpSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpSystolic.ToString() + " mm" : "-");
            rd.SetParameterValue("BpDiaSystolic", PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() + " mm" : "-");

            rd.SetParameterValue("SerologyTestOne", PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() : "-");
            rd.SetParameterValue("SerologyTestTwo", PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() : "-");

            rd.SetParameterValue("HealthFacilityName", PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() != null ? PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() : "-");


            string patientImage = "";
            if (PatientList.fitnessSingleRecord[0].PatientImageUrl != null)
            {
                patientImage = baseURL + PatientList.fitnessSingleRecord[0].PatientImageUrl;

            }

            rd.SetParameterValue("PatientImageUrl", patientImage != null ? patientImage : "-");
            string PatientRightThumb = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl != null)
            {
                PatientRightThumb = baseURL + PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl;

            }

            rd.SetParameterValue("PatientRightThumbImageUrl", PatientRightThumb != null ? PatientRightThumb : "-");
            string PatientRightIndex = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl != null)
            {
                PatientRightIndex = baseURL + PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl;

            }


            rd.SetParameterValue("PatientRightIndexImageUrl", PatientRightIndex != null ? PatientRightIndex : "-" ?? "-");
            string PatientRightMiddle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl != null)
            {
                PatientRightMiddle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl;

            }

            rd.SetParameterValue("PatientRightMiddleImageUrl", PatientRightMiddle != null ? PatientRightMiddle : "-");
            string PatientRightRing = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl != null)
            {
                PatientRightRing = baseURL + PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl;

            }


            rd.SetParameterValue("PatientRightRingImageUrl", PatientRightRing != null ? PatientRightRing : "-");
            string PatientRightLittle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl != null)
            {
                PatientRightLittle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl;
            }

            rd.SetParameterValue("PatientRightLittleImageUrl", PatientRightLittle != null ? PatientRightLittle : "-");

           
            rd.SetParameterValue("XRayChestPAView", PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() != null ? PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() : " ");

            rd.SetParameterValue("UserName", PatientList.fitnessSingleRecord[0].UserName?.ToString() != null ? PatientList.fitnessSingleRecord[0].UserName?.ToString() : "-");

            rd.SetParameterValue("SerologyTest1Value", PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() : "-");
            rd.SetParameterValue("SerologyTest2Value", PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() : "-");
            
            

            if (PatientList.fitnessSingleRecord[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList.fitnessSingleRecord[0].QrCodeImagePath;
                 rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            if (PatientList.PsychologicalQuestionsAndAnswer.Count > 0)
            {
                rd.SetParameterValue("Question1", PatientList.PsychologicalQuestionsAndAnswer[0].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[0].Question?.ToString() : "-");
                rd.SetParameterValue("Answer1", PatientList.PsychologicalQuestionsAndAnswer[0].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[0].Answer?.ToString() : "-");

                rd.SetParameterValue("Question2", PatientList.PsychologicalQuestionsAndAnswer[1].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[1].Question?.ToString() : "-");
                rd.SetParameterValue("Answer2", PatientList.PsychologicalQuestionsAndAnswer[1].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[1].Answer?.ToString() : "-");

                rd.SetParameterValue("Question3", PatientList.PsychologicalQuestionsAndAnswer[2].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[2].Question?.ToString() : "-");
                rd.SetParameterValue("Answer3", PatientList.PsychologicalQuestionsAndAnswer[2].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[2].Answer?.ToString() : "-");

                rd.SetParameterValue("Question4", PatientList.PsychologicalQuestionsAndAnswer[3].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[3].Question?.ToString() : "-");
                rd.SetParameterValue("Answer4", PatientList.PsychologicalQuestionsAndAnswer[3].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[3].Answer?.ToString() : "-");

                rd.SetParameterValue("Question5", PatientList.PsychologicalQuestionsAndAnswer[4].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[4].Question?.ToString() : "-");
                rd.SetParameterValue("Answer5", PatientList.PsychologicalQuestionsAndAnswer[4].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[4].Answer?.ToString() : "-");

                rd.SetParameterValue("Question6", PatientList.PsychologicalQuestionsAndAnswer[5].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[5].Question?.ToString() : "-");
                rd.SetParameterValue("Answer6", PatientList.PsychologicalQuestionsAndAnswer[5].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[5].Answer?.ToString() : "-");

                rd.SetParameterValue("Question7", PatientList.PsychologicalQuestionsAndAnswer[6].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[6].Question?.ToString() : "-");
                rd.SetParameterValue("Answer7", PatientList.PsychologicalQuestionsAndAnswer[6].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[6].Answer?.ToString() : "-");

                rd.SetParameterValue("Question8", PatientList.PsychologicalQuestionsAndAnswer[7].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[7].Question?.ToString() : "-");
                rd.SetParameterValue("Answer8", PatientList.PsychologicalQuestionsAndAnswer[7].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[7].Answer?.ToString() : "-");

                rd.SetParameterValue("Question9", PatientList.PsychologicalQuestionsAndAnswer[8].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[8].Question?.ToString() : "-");
                rd.SetParameterValue("Answer9", PatientList.PsychologicalQuestionsAndAnswer[8].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[8].Answer?.ToString() : "-");

                rd.SetParameterValue("Question10", PatientList.PsychologicalQuestionsAndAnswer[9].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[9].Question?.ToString() : "-");
                rd.SetParameterValue("Answer10", PatientList.PsychologicalQuestionsAndAnswer[9].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[9].Answer?.ToString() : "-");
            }
            

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        
        [HttpGet]
        [ActionName("GetSingleFitnessCertificateForAslah")]
        public async Task<HttpResponseMessage> GetSingleFitnessCertificateForAslah(Guid patientVisitId)
        {

            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetFitnessCertificateReport";

            string report = "~/Reports";
            string reportFileName = "FitnessCertificateAslah.rpt";
            string exportFilename = "FCA_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList = await _emcService.GetFitnessCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);
            //rd.SetDataSource(PatientList.PsychologicalQuestionsAndAnswer);

            rd.SetParameterValue("FullName", PatientList.fitnessSingleRecord[0].FullName != null ? PatientList.fitnessSingleRecord[0].FullName : "-");
            rd.SetParameterValue("IssueDate", PatientList.fitnessSingleRecord[0].IssueDate.Value.ToShortDateString() != null ? PatientList.fitnessSingleRecord[0].IssueDate.Value.ToShortDateString() : "-");

            rd.SetParameterValue("Profession", PatientList.fitnessSingleRecord[0].Profession != null ? PatientList.fitnessSingleRecord[0].Profession : "-");

            rd.SetParameterValue("Education", PatientList.fitnessSingleRecord[0].Education != null ? PatientList.fitnessSingleRecord[0].Education : "-");

            rd.SetParameterValue("TrackingId", PatientList.fitnessSingleRecord[0].TrackingId != null ? PatientList.fitnessSingleRecord[0].TrackingId : "-");

            rd.SetParameterValue("PresentJob", PatientList.fitnessSingleRecord[0].PresentJob != null ? PatientList.fitnessSingleRecord[0].PresentJob : "-");

            rd.SetParameterValue("Age", PatientList.fitnessSingleRecord[0].Age.ToString() != null ? PatientList.fitnessSingleRecord[0].Age.ToString() + " years" : "-");
            rd.SetParameterValue("CNIC", PatientList.fitnessSingleRecord[0].CNIC != null ? PatientList.fitnessSingleRecord[0].CNIC : "-");
            rd.SetParameterValue("ParmanentAddress", PatientList.fitnessSingleRecord[0].ParmanentAddress != null ? PatientList.fitnessSingleRecord[0].ParmanentAddress : "-");
            if (PatientList.fitnessSingleRecord[0].DOB.HasValue)
            {
                rd.SetParameterValue("DOB", PatientList.fitnessSingleRecord[0].DOB.ToString() != null ? PatientList.fitnessSingleRecord[0].DOB.Value.ToString("dd/MM/yyyy") : "-");
            }
            
            rd.SetParameterValue("RelativeName", PatientList.fitnessSingleRecord[0].RelativeName != null ? PatientList.fitnessSingleRecord[0].RelativeName : "-");
            rd.SetParameterValue("EmployeeLetterNo", PatientList.fitnessSingleRecord[0].EmployeeLetterNo != null ? PatientList.fitnessSingleRecord[0].EmployeeLetterNo : "-");
            rd.SetParameterValue("LetterDateTime", PatientList.fitnessSingleRecord[0].LetterDateTime.ToString() != null ? PatientList.fitnessSingleRecord[0].LetterDateTime.Value.ToString("dd/MM/yyyy").ToString() : "-");
            rd.SetParameterValue("DesignationAppliedFor", PatientList.fitnessSingleRecord[0].DesignationAppliedFor != null ? PatientList.fitnessSingleRecord[0].DesignationAppliedFor : "-");
            rd.SetParameterValue("AgeByAppearance", PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() != null ? PatientList.fitnessSingleRecord[0].AgeByAppearance.ToString() + " years" : "-");
            rd.SetParameterValue("CheckedBy", PatientList.fitnessSingleRecord[0].CheckedBy != null ? PatientList.fitnessSingleRecord[0].CheckedBy : "-");
            //rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.ToString() != null ? PatientList[0].IssueDate.ToString() : "-");
            rd.SetParameterValue("BodilyInfirmity", PatientList.fitnessSingleRecord[0].BodilyInfirmity != null ? PatientList.fitnessSingleRecord[0].BodilyInfirmity : "-");
            rd.SetParameterValue("Department", PatientList.fitnessSingleRecord[0].Department != null ? PatientList.fitnessSingleRecord[0].Department : "");

            rd.SetParameterValue("RecomendedOrNot", PatientList.fitnessSingleRecord[0].RecomendedOrNot != null ? PatientList.fitnessSingleRecord[0].RecomendedOrNot : "-");


            rd.SetParameterValue("Vision", PatientList.fitnessSingleRecord[0].Vision?.ToString() != null ? PatientList.fitnessSingleRecord[0].Vision : "-");
                rd.SetParameterValue("BpSystolic", PatientList.fitnessSingleRecord[0].BpSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("BpDiaSystolic", PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() + " mm" : "-");
                rd.SetParameterValue("Height", PatientList.fitnessSingleRecord[0].Height.ToString() != null ? PatientList.fitnessSingleRecord[0].Height.ToString() + " cm" : "-");
                rd.SetParameterValue("Chest", PatientList.fitnessSingleRecord[0].Chest.ToString() != null ? PatientList.fitnessSingleRecord[0].Chest.ToString() : "-");
                rd.SetParameterValue("Weight", PatientList.fitnessSingleRecord[0].Weight.ToString() != null ? PatientList.fitnessSingleRecord[0].Weight.ToString() + " kg" : "-");
                rd.SetParameterValue("MarkOfIdentification", PatientList.fitnessSingleRecord[0].MarkOfIdentification != null ? PatientList.fitnessSingleRecord[0].MarkOfIdentification : "-");
                rd.SetParameterValue("HivTest", PatientList.fitnessSingleRecord[0].HivTest != null ? PatientList.fitnessSingleRecord[0].HivTest : "-");
                rd.SetParameterValue("BSR", PatientList.fitnessSingleRecord[0].BSR != null ? PatientList.fitnessSingleRecord[0].BSR : "-");
                rd.SetParameterValue("HepBTest", PatientList.fitnessSingleRecord[0].HepBTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepBTest?.ToString() : "-");
                rd.SetParameterValue("HepCTest", PatientList.fitnessSingleRecord[0].HepCTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].HepCTest?.ToString() : "-");
            char lastChar = PatientList.fitnessSingleRecord[0].CNIC[PatientList.fitnessSingleRecord[0].CNIC.Length - 1];

            // Convert the last character to integer
            int lastDigit = int.Parse(lastChar.ToString());

            if (lastDigit % 2 == 0)
            {
                rd.SetParameterValue("PregnancyTestHeading", "Pregnancy test");
            }
            else
            {
                rd.SetParameterValue("PregnancyTestHeading", "-");
            }

            rd.SetParameterValue("PregNancyTest", PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() != null ? PatientList.fitnessSingleRecord[0].PregNancyTest?.ToString() : "-");
            rd.SetParameterValue("BpSystolic", PatientList.fitnessSingleRecord[0].BpSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpSystolic.ToString() + " mm" : "-");
            rd.SetParameterValue("BpDiaSystolic", PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() != null ? PatientList.fitnessSingleRecord[0].BpDiaSystolic.ToString() + " mm" : "-");

            rd.SetParameterValue("SerologyTestOne", PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestOne.ToString() : "-");
            rd.SetParameterValue("SerologyTestTwo", PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTestTwo.ToString() : "-");

            rd.SetParameterValue("HealthFacilityName", PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() != null ? PatientList.fitnessSingleRecord[0].HealthFacilityName.ToString() : "-");


            string patientImage = "";
            if (PatientList.fitnessSingleRecord[0].PatientImageUrl != null)
            {
                patientImage = baseURL + PatientList.fitnessSingleRecord[0].PatientImageUrl;

            }

            rd.SetParameterValue("PatientImageUrl", patientImage != null ? patientImage : "-");
            string PatientRightThumb = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl != null)
            {
                PatientRightThumb = baseURL + PatientList.fitnessSingleRecord[0].PatientRightThumbImageUrl;

            }

            rd.SetParameterValue("PatientRightThumbImageUrl", PatientRightThumb != null ? PatientRightThumb : "-");
            string PatientRightIndex = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl != null)
            {
                PatientRightIndex = baseURL + PatientList.fitnessSingleRecord[0].PatientRightIndexImageUrl;

            }


            rd.SetParameterValue("PatientRightIndexImageUrl", PatientRightIndex != null ? PatientRightIndex : "-" ?? "-");
            string PatientRightMiddle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl != null)
            {
                PatientRightMiddle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightMiddleImageUrl;

            }

            rd.SetParameterValue("PatientRightMiddleImageUrl", PatientRightMiddle != null ? PatientRightMiddle : "-");
            string PatientRightRing = "";

            if (PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl != null)
            {
                PatientRightRing = baseURL + PatientList.fitnessSingleRecord[0].PatientRightRingImageUrl;

            }


            rd.SetParameterValue("PatientRightRingImageUrl", PatientRightRing != null ? PatientRightRing : "-");
            string PatientRightLittle = "";


            if (PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl != null)
            {
                PatientRightLittle = baseURL + PatientList.fitnessSingleRecord[0].PatientRightLittleImageUrl;
            }

            rd.SetParameterValue("PatientRightLittleImageUrl", PatientRightLittle != null ? PatientRightLittle : "-");

           
            rd.SetParameterValue("XRayChestPAView", PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() != null ? PatientList.fitnessSingleRecord[0].XRayChestPAView?.ToString() : " ");

            rd.SetParameterValue("UserName", PatientList.fitnessSingleRecord[0].UserName?.ToString() != null ? PatientList.fitnessSingleRecord[0].UserName?.ToString() : "-");

            rd.SetParameterValue("SerologyTest1Value", PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest1Value?.ToString() : "-");
            rd.SetParameterValue("SerologyTest2Value", PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() != null ? PatientList.fitnessSingleRecord[0].SerologyTest2Value?.ToString() : "-");
            
            

            if (PatientList.fitnessSingleRecord[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList.fitnessSingleRecord[0].QrCodeImagePath;
                 rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            if (PatientList.PsychologicalQuestionsAndAnswer.Count > 0)
            {
                rd.SetParameterValue("Question1", PatientList.PsychologicalQuestionsAndAnswer[0].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[0].Question?.ToString() : "-");
                rd.SetParameterValue("Answer1", PatientList.PsychologicalQuestionsAndAnswer[0].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[0].Answer?.ToString() : "-");

                rd.SetParameterValue("Question2", PatientList.PsychologicalQuestionsAndAnswer[1].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[1].Question?.ToString() : "-");
                rd.SetParameterValue("Answer2", PatientList.PsychologicalQuestionsAndAnswer[1].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[1].Answer?.ToString() : "-");

                rd.SetParameterValue("Question3", PatientList.PsychologicalQuestionsAndAnswer[2].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[2].Question?.ToString() : "-");
                rd.SetParameterValue("Answer3", PatientList.PsychologicalQuestionsAndAnswer[2].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[2].Answer?.ToString() : "-");

                rd.SetParameterValue("Question4", PatientList.PsychologicalQuestionsAndAnswer[3].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[3].Question?.ToString() : "-");
                rd.SetParameterValue("Answer4", PatientList.PsychologicalQuestionsAndAnswer[3].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[3].Answer?.ToString() : "-");

                rd.SetParameterValue("Question5", PatientList.PsychologicalQuestionsAndAnswer[4].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[4].Question?.ToString() : "-");
                rd.SetParameterValue("Answer5", PatientList.PsychologicalQuestionsAndAnswer[4].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[4].Answer?.ToString() : "-");

                rd.SetParameterValue("Question6", PatientList.PsychologicalQuestionsAndAnswer[5].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[5].Question?.ToString() : "-");
                rd.SetParameterValue("Answer6", PatientList.PsychologicalQuestionsAndAnswer[5].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[5].Answer?.ToString() : "-");

                rd.SetParameterValue("Question7", PatientList.PsychologicalQuestionsAndAnswer[6].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[6].Question?.ToString() : "-");
                rd.SetParameterValue("Answer7", PatientList.PsychologicalQuestionsAndAnswer[6].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[6].Answer?.ToString() : "-");

                rd.SetParameterValue("Question8", PatientList.PsychologicalQuestionsAndAnswer[7].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[7].Question?.ToString() : "-");
                rd.SetParameterValue("Answer8", PatientList.PsychologicalQuestionsAndAnswer[7].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[7].Answer?.ToString() : "-");

                rd.SetParameterValue("Question9", PatientList.PsychologicalQuestionsAndAnswer[8].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[8].Question?.ToString() : "-");
                rd.SetParameterValue("Answer9", PatientList.PsychologicalQuestionsAndAnswer[8].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[8].Answer?.ToString() : "-");

                rd.SetParameterValue("Question10", PatientList.PsychologicalQuestionsAndAnswer[9].Question?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[9].Question?.ToString() : "-");
                rd.SetParameterValue("Answer10", PatientList.PsychologicalQuestionsAndAnswer[9].Answer?.ToString() != null ? PatientList.PsychologicalQuestionsAndAnswer[9].Answer?.ToString() : "-");
            }
            

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        [ActionName("GetIcvCertificate")]
        public HttpResponseMessage GetIcvCertificate(Guid patientVisitId)
        {


            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetSingleIcvCertificateReport";

            string report = "~/Reports";
            string reportFileName = "IcvCertificateReport.rpt";
            string exportFilename = "DC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList = _emcService.GetIcvCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);

            rd.SetParameterValue("OpdNo", PatientList[0].OpdNo?.ToString() ?? "-");
            rd.SetParameterValue("FullName","Mr/Mrs " + PatientList[0].FullName?.ToString() ?? "-");
            rd.SetParameterValue("RelativeName", "Mr/Mrs " + PatientList[0].RelativeName?.ToString() ?? "-");
            //rd.SetParameterValue("Age", PatientList[0].Age.ToString() + " years" ?? "-");
            rd.SetParameterValue("Nationality", PatientList[0].Nationality?.ToString() ?? "-");
            rd.SetParameterValue("CNIC", PatientList[0].CNIC?.ToString() ?? "-");
            rd.SetParameterValue("PassportNo", PatientList[0].PassportNo?.ToString() ?? "-");
            rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.Value.ToShortDateString() ?? "-");
            rd.SetParameterValue("CreatedOn", PatientList[0].CreatedOn.ToString() ?? "-");
            rd.SetParameterValue("HealthFacility", PatientList[0].HealthFacility.ToString() ?? "-");
            rd.SetParameterValue("VaccinationDate", PatientList[0].VaccinationDate.Value.ToShortDateString() ?? "-");
            dynamic vaccination = JsonConvert.DeserializeObject(PatientList[0].Vaccination);
            //List<IcvVacination> nameObjects = JsonConvert.DeserializeObject<List<IcvVacination>>(x);
            // Extract names
            string vacination = "";
            int index = 0;
            foreach (var nameObject in vaccination)
            {
                vacination += (index + 1).ToString() + " - " + nameObject.name.ToString() + "  ";
                index++;
            }

            rd.SetParameterValue("Vaccination", vacination ?? "-");

            var patient = PatientList[0];

            // Calculate age
            int? age = CalculateAge(patient.DateOfBirth);

            rd.SetParameterValue("Age", age.ToString() ?? "-");

            //rd.SetParameterValue("Condition", PatientList[0].Condition?.ToString() ?? "-");
            //rd.SetParameterValue("DateOfBirth", PatientList[0].DateOfBirth.ToString() ?? "-");
            rd.SetParameterValue("NameOfDisease", PatientList[0].NameOfDisease?.ToString() ?? "-");
            //rd.SetParameterValue("VaccinationDate", PatientList[0].VaccinationDate.ToString() ?? "-");
            //rd.SetParameterValue("Relation", PatientList[0].Relation?.ToString() ?? "-");

            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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
        
        [HttpGet]
        [ActionName("GetSingleIcvCertificate")]
        public HttpResponseMessage GetSingleIcvCertificate(Guid patientVisitId)
        {


            if (patientVisitId == null)
                return null;

            string StoreProcedureName = "emc.SpGetSingleIcvCertificateReport";

            string report = "~/Reports";
            string reportFileName = "IcvCertificateReport.rpt";
            string exportFilename = "DC_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss_tt") + ".pdf";


            var PatientList = _emcService.GetIcvCertificate(patientVisitId, StoreProcedureName);


            string reportPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(report), reportFileName);
            ReportDocument rd = new ReportDocument();
            rd.Load(reportPath);

            rd.SetParameterValue("OpdNo", PatientList[0].OpdNo?.ToString() ?? "-");
            rd.SetParameterValue("FullName","Mr/Mrs " + PatientList[0].FullName?.ToString() ?? "-");
            rd.SetParameterValue("RelativeName", "Mr/Mrs " + PatientList[0].RelativeName?.ToString() ?? "-");
            //rd.SetParameterValue("Age", PatientList[0].Age.ToString() + " years" ?? "-");
            rd.SetParameterValue("Nationality", PatientList[0].Nationality?.ToString() ?? "-");
            rd.SetParameterValue("CNIC", PatientList[0].CNIC?.ToString() ?? "-");
            rd.SetParameterValue("PassportNo", PatientList[0].PassportNo?.ToString() ?? "-");
            rd.SetParameterValue("IssueDate", PatientList[0].IssueDate.Value.ToShortDateString() ?? "-");
            rd.SetParameterValue("CreatedOn", PatientList[0].CreatedOn.ToString() ?? "-");
            rd.SetParameterValue("HealthFacility", PatientList[0].HealthFacility.ToString() ?? "-");
            rd.SetParameterValue("VaccinationDate", PatientList[0].VaccinationDate.Value.ToShortDateString() ?? "-");
            dynamic vaccination = JsonConvert.DeserializeObject(PatientList[0].Vaccination);
            //List<IcvVacination> nameObjects = JsonConvert.DeserializeObject<List<IcvVacination>>(x);
            // Extract names
            string vacination = "";
            int index = 0;
            foreach (var nameObject in vaccination)
            {
                vacination += (index + 1).ToString() + " - " + nameObject.name.ToString() + "  ";
                index++;
            }

            rd.SetParameterValue("Vaccination", vacination ?? "-");

            var patient = PatientList[0];

            // Calculate age
            int? age = CalculateAge(patient.DateOfBirth);

            rd.SetParameterValue("Age", age.ToString() ?? "-");

            //rd.SetParameterValue("Condition", PatientList[0].Condition?.ToString() ?? "-");
            //rd.SetParameterValue("DateOfBirth", PatientList[0].DateOfBirth.ToString() ?? "-");
            rd.SetParameterValue("NameOfDisease", PatientList[0].NameOfDisease?.ToString() ?? "-");
            //rd.SetParameterValue("VaccinationDate", PatientList[0].VaccinationDate.ToString() ?? "-");
            //rd.SetParameterValue("Relation", PatientList[0].Relation?.ToString() ?? "-");

            if (PatientList[0].QrCodeImagePath != null)
            {
                //string lastPath = GetLastPath(PatientList[0].PatientFinalReportUrl);
                string tempUrlPath = baseURL + PatientList[0].QrCodeImagePath;
                rd.SetParameterValue("QRCodePath", tempUrlPath);
            }
            else
            {
                rd.SetParameterValue("QRCodePath", "");
            }

            var pdfBytes = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            rd.Dispose();
            var response = new HttpResponseMessage(HttpStatusCode.Created)
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

        public static string GetLastPath(string input)
        {
            // Split the input string into an array of paths
            string[] paths = input.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            // Check if there are at least two paths
            if (paths.Length >= 2)
            {
                // Select the last path in the array
                return paths[paths.Length - 1];
            }
            else
            {
                // If there are fewer than two paths, return the original input
                return input;
            }
        }

        int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dateOfBirth.Year;

            // Check if the birthday has occurred this year
            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}