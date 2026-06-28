using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMessages
{
    public static class CommonStringConstant
    {

        public static string MRN = "MRN";

        #region DrugPatientRole
        public static string DrugAddict = "Drug Addict";
        public static string Mortuary = "Mortaury";
        #endregion

        #region DrugPatientCategory
        public static string Known = "Known";
        public static string Unknown = "Unknown";
        #endregion

        #region TbLabTests
        public static string SSM = "SSM";
        public static string XPert = "XPert";
        public static string CXR = "CXR";
        public static string HIVScreening = "HIV Screening";
        public static string SuggestedForTB = "Suggestive For TB";
        public static string Yes = "Yes";
        public static string No = "No";
        #endregion

        #region TbLabTestResults
        public static string Result = "Result";
        public static string PositiveForAFB = "Positive For AFB";
        public static string NegativeForAFB = "Negative For AFB";
        public static string MTBDetected = "Detected";
        public static string MTBNotDetected = "Not Detected";
        public static string MTBPositive = "MTB Detected";
        public static string MTBNegative = "MTB Not Detected";
        public static string IsHIVScreened = "Is HIV Screened";
        public static string RifampicinResistant= "Rifampicin Resistance";
        #endregion

        #region TB Dashboard
        public static string SSMTestAdvised = "SSM Test Advised";
        public static string CXRTestAdvised = "CXR Test Advise";
        public static string GeneXpertTestAdvised = "Gene-Xpert Test Advise";
        public static string HIVTestAdvised = "HIV Test Advise";
        public static string SSMPositive = "SSM (Positive)";
        public static string SSMNegative = "SSM (Negative)";
        public static string SSMPending = "SSM Result Awaited";
        public static string CXRPositive = "CXR (Positive)";
        public static string CXRNegative = "CXR (Negative)";
        public static string CXRPending = "CXR Result Awaited";
        public static string XpertPositive = "Xpert (Positive)";
        public static string XpertNegative = "Xpert (Negative)";
        public static string XpertPending = "Gene-Xpert Result Awaited";
        public static string ResistanceDetected = "Total Rifampicin Resistance";
        public static string RifampicinResistanceDetected = "Rifampicin Resistance Detected";
        public static string RifampicinResistanceNotDetected = "Rifampicin Resistance Not Detected";
        public static string Error = "Indeterminated";
        public static string HIVResult = "HIV Result";
        public static string HIVReactive = "HIV Reactive";
        public static string Reactive = "Reactive";
        public static string NonReactive = "Non Reactive";
        public static string HIVNonReactive = "HIV Non Reactive";
        public static string HIVPending = "HIV Result Awaited";

        #endregion

        #region DRTB
        public static string WalkIn = "WalkIn";
        public static string Refered = "Refered";
        #endregion

        #region ProfileTypeShortName
        public static string LabTypeXray = "LTXRAY";
        public static string _DrugAddict = "DRGADT";
        public static string TbPatientTypes = "TPT";
        public static string TbTreatmentLength = "TTL";
        public static string TbTreatmentLengthOfInterruption = "TTLI";

        #endregion

        #region ContentType

        public const string contentTypeApplicationJson = "application/json";

        #endregion

        #region Projects
        public const string Hmis = "HMIS";
        public const string HumanResource = "HR";
        #endregion

        #region DataBank
        public const string RelationSelf = "Self";
        #endregion

        #region Common Strings 

        public const string InvalidToken = "Invalid Token";

        #endregion

        #region Health Facility Stations

        public const string RegistrationStation = "PTSTSN";
        public const string VitalStation = "VSTSN";
        public const string DoctorStation = "DCSTSN";
        public const string PharmacyStation = "PSTSN";

        #endregion

        #region ProfileType Const

        public const string CounterStations = "CSTSNL";
        public const string LabSampleRejectedReasons = "LTSRR";


        #endregion

        #region DepartmentName Const

        public const string InPatientDepartment = "InPatient Department";
        public const string IPD = "IPD";
        public const string OPD = "OPD";

        #endregion

        #region User Roles

        public const string Radiologist = "RADLGT";
        public const string RegistrationRole = "RGSTR";
        public const string VitalRole = "VITAL";
        public const string DoctorRole = "DOCTOR";
        public const string BAS = "BAS";
        public const string PharmacyRole = "PHRMCY";

        #endregion

        #region Lab Tests

        public const string InternalLabTest = "LABINT";
        public const string ExternalLabTest = "LABEXT";

        public const int AllCollection = 0;
        public const int PendingCollection = 1;
        public const int SampleCollected = 2;
        public const int ReportGenerated = 3;
        public const int SampleRejected = 4;
        public const int SampleCollectedInDashboard = 11;
        public const int ReportInDashboard = 12;
        #endregion

        #region Diagnose Form Type

        public const string GeneralForm = "GeneralForm";
        public const string PhysiotherapyFormOPD = "PhysiotherapyFormOPD";
        public const string PhysiotherapyFormIPD = "PhysiotherapyFormIPD";
        public const string TbForm = "TbForm";
        public const string HCPForm = "HCPForm";

        public const string SurgeryForm = "SurgeryForm";
        public const string PsychiatryForm = "PsychiatryForm";
        public const string NutritionForm = "NutritionForm";
        public const string SpeechTherapyForm = "SpeechTherapyForm";
        public const string PsychologyForm = "PsychologyForm";
        public const string OccupationalTherapyForm = "OccupationalTherapyForm";
        public const string TechnologyForm = "TechnologyForm";


        #endregion

        #region Images Folder Name
        public const string Menu = "Menu";
        public const string Pathalogy = "Pathalogy";
        public const string pateintFingerprint = "Patient/Fingerprint";
        public const string pateintImages = "Patient/Images";
        public const string PatientDocuments = "Patient/PatientDocuments";
        public const string FeatureAnnouncement = "FeatureAnnouncement";
        #endregion

        #region Search Keys
        public const string MrNo = "MR No";
        public const string MobileNo = "Mobile No";
        public const string CNIC = "CNIC";
        #endregion

        #region Drug Addict
        public static string SWF = "SWF";
        #endregion

        #region SectionName Const

        public const string DentalSurgeonOPD = "Dental Surgeon OPD";
        public const string DentalOPD = "Dental OPD";
        public const string OneWindowTb = "One Window Clinic of T.B";



        #endregion

        #region SourceSystem
         public const string PITBDrugAddict = "PITBDA";
        #endregion


        #region HCP
        public const string HBVPCRTest = "PCR for HBV DNA";
        public const string HCVPCRTest = "PCR for HCV RNA";
        public const string PCR = "PCR";
        #endregion
    }

    public static class CommonConstant
    {

        #region MIMSwardID
        public static int IPDWardID = 342;
        public static int OPDWardID = 263;
        #endregion

    }
}
