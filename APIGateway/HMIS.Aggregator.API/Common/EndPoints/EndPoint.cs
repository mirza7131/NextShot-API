using static System.Net.WebRequestMethods;

namespace HMIS.Aggregator.API.Common.EndPoints
{
    public static class MIMSEndPoint
    {
        //for staging Only
        //public const string MIMSDevBaseUrl = "http://172.16.15.5:1100/";
        //public const string MIMSProBaseUrl = "http://172.16.15.5:1100/";
        //for produciton Only
        public const string MIMSDevBaseUrl = "http://172.16.15.5:1100/";
        public const string MIMSProBaseUrl = "https://mims.pshealthpunjab.gov.pk/";
    }   
    

    public static class PatientEndPoint
    {
        public const string PatientDevBaseUrl = "http://172.16.15.5:1121/";
        public const string PatientProBaseUrl = "http://172.16.15.5:1121/";
    }

    public static class HealthWatchEndPoint
    {
        public const string HealthWatchDevBaseUrl = "http://172.16.15.5:1128/";
        public const string HealthWatchProBaseUrl = "http://172.16.15.5:1128/";
    }

    public static class HealthCertificateEndPoint
    {
        public const string HealthCertificateDevBaseUrl = "http://172.16.15.5:1128/";
        public const string HealthCertificateProBaseUrl = "http://172.16.15.5:1128/";
    }

    public static class HealthCouncilEndPoint
    {
        public const string HealthCouncilDevBaseUrl = "http://172.16.15.5:1128/";
        public const string HealthCouncilProBaseUrl = "http://172.16.15.5:1128/";
    }

    public static class MedicoLegalEndPoint
    {
        public const string MedicoLegalDevBaseUrl = "http://172.16.15.5:1128/";
        public const string MedicoLegalProBaseUrl = "http://172.16.15.5:1128/";
    }

    public static class ProfileTypeEndPoint
    {
        public const string ProfileTypeDevBaseUrl = "http://172.16.15.5:1119/";
        public const string ProfileTypeProBaseUrl = "http://172.16.15.5:1119/";

       
    }

    public static class AuthEndPoints
    {
        public const string DevBaseUrl = "http://172.16.15.5:1119/";
        //public const string DevBaseUrl = "https://localhost:7082/";
        public const string ProBaseUrl = "https://authapi-phis.pshealthpunjab.gov.pk/";
    }

    public static class ProfileEndPoint
    {
        public const string ProfileDevBaseUrl = "http://172.16.15.5:1119/";
        public const string ProfileProBaseUrl = "http://172.16.15.5:1119/";
    }
    public static class TBScreeningEndPoint
    {
        public const string TBScreeningDevBaseUrl = "http://172.16.15.5:1128/";
        public const string TBScreeningProBaseUrl = "http://172.16.15.5:1128/";
    }

    public static class HREndPoint
    {
        public const string HRBaseUrl = "https://hrmis.pshealthpunjab.gov.pk/";
        public const string HrPhisBaseUrl = "https://hr-phis.pshealthpunjab.gov.pk/";
    }
    public static class AidsEndPoint
    {
        public const string AidsDevBaseUrl = "http://116.58.20.67:1128/";
        public const string AidsProBaseUrl = "http://116.58.20.67:1128/";
    }

    public static class EMREndPoint
    {
        //public const string EMRDevBaseUrl = "http://116.58.20.67:1105/";

        public const string EMRDevBaseUrl = "http://172.16.119.83:56872/";

       
        public const string EMRProBaseUrl = "https://phmis.pshealthpunjab.gov.pk/";
    }


    public static class NadraVerificationEndPoint
    {
        public const string NadraVerificationDevBaseUrl = "http://nadraapi.pshealthpunjab.gov.pk/";
        public const string NadraVerificationProBaseUrl = "http://nadraapi.pshealthpunjab.gov.pk/";
    }

    public static class VerifiedPatientDataFromNADRA
    {
        public const string VerifiedPatientDataFromNADRADevBaseUrl = "http://nadraapi.pshealthpunjab.gov.pk/";
        public const string VerifiedPatientDataFromNADRAProBaseUrl = "http://nadraapi.pshealthpunjab.gov.pk/";
    }


    public static class PatientDiagnoseEndPoint
    {
        public const string ProfileDevBaseUrl = "https://localhost:7151/";
        public const string ProfileProBaseUrl = "http://172.16.15.5:1121/";
    }
    public static class NCDEndPoint
    {
        public const string ProfileDevBaseUrl = "http://116.58.20.67:1185/";
        public const string ProfileProBaseUrl = "https://ncdapi-phis.pshealthpunjab.gov.pk/";
    }

}
