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
        public static string LHW = "LHW";

        #region DataSyncToOffline

        public static string SyncToOfflineType = "Offline";
        public static string SyncToOnlineType = "Online";

        public static string SyncDataFethed_Pending = "Pending";
        public static string SyncDataFethed_Error = "Error";
        public static string SyncDataFethed_Completed = "Completed";

        public static string SyncDataInserted_Pending = "Pending";
        public static string SyncDataInserted_Error = "Error";
        public static string SyncDataInserted_Completed = "Completed";

        public static string SyncDataFethed_WorkFlow = "Data Fetched Successfully";

        #endregion

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
        public static string RifampicinResistant = "Rifampicin Resistance";
        #endregion

        #region TB Dashboard
        public static string SSMTestAdvised = "SSM Test Advised";
        public static string CXRTestAdvised = "CXR Test Advised";
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
        public static string Error = "Indeterminate";
        public static string HIVResult = "HIV Result";
        public static string HIVReactive = "HIV Reactive";
        public static string Reactive = "Reactive";
        public static string NonReactive = "Non Reactive";
        public static string HIVNonReactive = "HIV Non Reactive";
        public static string HIVPending = "HIV Result Awaited";
        public static string TotalDiagnose = "Total Diagnose";
        public static string TotalConfirmedPatients = "Total Confirmed Patients";
        public static string TotalNotConfirmed = "Total Not Confirmed";
        public static string MedicineIssued = "Medicine Issued";

        #endregion

        #region DRTB
        public static string WalkIn = "WalkIn";
        public static string Refered = "Refered";
        #endregion

        #region ProfileTypeShortName
        public static string LabTypeXray = "LTXRAY";
        public static string LabTypeUltrasound = "LTUTSD";
        public static string _DrugAddict = "DRGADT";
        public static string TbPatientTypes = "TPT";
        public static string TbTreatmentLength = "TTL";
        public static string TbTreatmentLengthOfInterruption = "TTLI";
        public const string CounterStations = "CSTSNL";
        public const string LabSampleRejectedReasons = "LTSRR";
        public const string DiseaseListShortName = "DSESLU";

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

        #region ProfileShortName Const

        public const string Years = "YEARS";
        public const string Months = "MONTHS";
        public const string Days = "DAYS";
        public const string KnownCaseOfHypertension = "KCOHPTSN";
        public const string KnownCaseOfDiabetes = "KCODBTES";
        public const string KnownCaseOfAsthama = "KCOASTMA";
        public const string KnownCaseOfCOPD = "KCOCOPD";
        public const string CSMTTENYEARS = "CSMTTENYEARS";
        public const string STPNKIMTAD = "STPNKIMTAD";
        public const string MentalHealthQuestion1 = "DOUFLLTLINT";
        public const string MentalHealthQuestion2 = "DOUFELWKNS";
        public const string MentalHealthQuestion3 = "DOUFLDPRS";
        public const string MentalHealthQuestion4 = "DUFELDIFINSTPFUR";


        #endregion

        #region DepartmentId Const
        public static readonly int TbDepartmentId = 2;
        #endregion

        #region DepartmentName Const

        public const string InPatientDepartment = "InPatient Department";
        public const string ERDepartment = "Emergency Department";
        public const string OutPatientDepartment = "Out-Patient Department";
        public const string IPD = "IPD";
        public const string OPD = "OPD";
        public const string ER = "ER";

        #endregion

        #region User Roles

        public const string Radiologist = "RADLGT";
        public const string PrivateAlmoner = "PRIALM";
        public const string PrivatePathology = "PRIPAT";
        public const string RegistrationRole = "RGSTR";
        public const string VitalRole = "VITAL";
        public const string DoctorRole = "DOCTOR";
        public const string BAS = "BAS";
        public const string PharmacyRole = "PHRMCY";
        public const string PMIS = "PMIS";

        #endregion

        #region Lab Tests

        public const string InternalLabTest = "LABINT";
        public const string ExternalLabTest = "LABEXT";

        public const int AllCollection = 0;
        public const int PendingCollection = 1;
        public const int SampleCollected = 2;
        public const int ReportGenerated = 3;
        public const int SampleRejected = 4;
        public const int RadiologyReport = 5;
        public const int SampleCollectedInDashboard = 11;
        public const int ReportInDashboard = 12;
        #endregion

        #region Consignment Rejection Status
        public const int ConsigmnetRejected = 3;
        #endregion

        #region Diagnose Form Type

        public const string GeneralForm = "GeneralForm";
        public const string PhysiotherapyFormOPD = "PhysiotherapyFormOPD";
        public const string PhysiotherapyFormIPD = "PhysiotherapyFormIPD";
        public const string TbForm = "TbForm";
        public const string HCPForm = "HCPForm";
        public const string NCDClinicForm = "NCDClinicForm";
        public const string MuawinClinicsForm = "MuawinClinicsForm";
        public const string NcdAndMuawinClinicForm = "NcdAndMuawinClinicForm";

        public const string SurgeryForm = "SurgeryForm";
        public const string PsychiatryForm = "PsychiatryForm";
        public const string NutritionForm = "NutritionForm";
        public const string NutritionFormIPD = "NutritionFormIPD";
        public const string SpeechTherapyForm = "SpeechTherapyForm";
        public const string PsychologyForm = "PsychologyForm";
        public const string RespiratoryForm = "RespiratoryForm";
        public const string RespiratoryFormIPD = "RespiratoryFormIPD";
        public const string OccupationalTherapyForm = "OccupationalTherapyForm";
        public const string TechnologyForm = "TechnologyForm";


        #endregion

        #region Images Folder Name
        public const string Menu = "Menu";
        public const string Pathalogy = "Pathalogy";
        public const string pateintFingerprint = "Patient/Fingerprint";
        public const string pateintImages = "Patient/Images";
        public const string HealthCouncil = "HealthCouncil/ChequeImage";
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

        #region SectionId Const
        public static readonly int TbSectionId = 30;
        #endregion

        #region SectionName Const

        public const string MedicalOPD = "Medical OPD";
        public const string DentalSurgeonOPD = "Dental Surgeon OPD";
        public const string DentalOPD = "Dental OPD";
        public const string OneWindowTb = "One Window Clinic of T.B";
        public const string GynaeOPD = "Gynaecologist OPD";
        public const string NCDClinicOPD = "NCD Clinic OPD";



        #endregion

        #region SourceSystem
        public const string PITBDrugAddict = "PITBDA";
        public const string SourceSystemEMR = "EMR";
        #endregion

        // Donot change these string, Whole system is based on these.
        #region HCP 
        public const string HBVPCRTest = "PCR for HBV DNA";
        public const string HCVPCRTest = "PCR for HCV RNA";
        public const string PCR = "PCR";
        public const string SVR = "SVR";
        public const string InappropriateSample = "INPSMP";
        public const string InappropriateSampleName = "Inappropriate Sample";

        // HCP Recommended Tests Short Names
        public const string RFT = "RFT";
        public const string HBVPCR = "HBVPCR";
        public const string HCVPCR = "HCVPCR";
        public const string CBC = "CBC";
        public const string HCVASVR = "HCVASVR";
        public const string LFT = "LFT";
        public const string HBVAPCR = "HBVAPCR";
        public const string resample = "resample";

        // PCR Profiles
        public const string HBVResultUploaded = "RESHBV";
        //public const string HBVConsignmentCreatedResultAwaited = "CONHBV"; / Uncomment this when these features will integrate in system accordingly
        //public const string HBVSampleCollectedPCRSVR = "SAMHBV";
        public const string HBVEligibleForAnnualPCRHBVPendingCollection = "ELGPCR";

        // SVR Profiles
        public const string HCVResultUploaded = "RESHCV";
        //public const string HCVConsignmentCreatedResultAwaited = "CONHBV"; / Uncomment this when these features will integrate in system accordingly
        //public const string HCVSampleCollectedPCRSVR = "SAMHBV";
        public const string HCVEligibleForSVRHCVPendingCollection = "ELGSVR";

        public const string PendingBatchList = "PendingBatchList";
        public const string CompletedBatchList = "CompletedBatchList";
        #endregion

        #region EMC
        public const string EMCMLE = "EMCMLEFORM";
        public const string PoliceKhidmatForm = "PoliceKhidmatForm";
        #endregion
        #region Filter BY

        public const int FilterByCNIC = 1;
        public const int FilterByMobileNo = 2;
        public const int FilterByMrNo = 3;
        public const int FilterByBarcode = 4;
        public const int FilterByBatchNumber = 5; // Using these filters in HCP as well.
        public const int FilterByTitle = 6;
        public const int FilterByFromHealthFacility = 7; 

        #endregion

        #region HealthCouncil
        public const string Received = "Received";
        #endregion

        #region Environments
        public const string DevelopmentEnvironment = "Development";
        public const string StaggingEnvironment = "Stagging";
        public const string ProductionEnvironment = "Production";
        #endregion



        #region IPD&Emergency
        public static string Pending = "Pending";
        public static string Dispensed = "Dispenced";
        public static string _Received = "Received";
        public static string Consumed = "Consumed";
        public static string Returned = "Returned";
        public static string Cancelled = "Cancelled";
        #endregion

        #region MLC
        public static string PSTMRTM = "PSTMRTM";
        public static string MLESV = "MLESV";
        public static string MLE = "MLE";
        public static string EMCMLEFORM = "EMCMLEFORM";
        #endregion


        #region NCD
        public const string NewPregnancy = "NEW PREGNANCY";
      #endregion

        #region
        public const string NoEventFoundAgainstThisFacility = "No Event is Assigned to this User, Kindly Contact your Focal Person!";
        #endregion

      #region FamilyPlanning
      public static string Cesarean = "Cesarean";
		#endregion

	
        #region MIMS
            public static string MIMSBranchIdNotAssignedtoUser = "User is not Linked with MIMS! Kindly contact your administrator";
        #endregion

    }

	public static class CommonPrases
    {
        #region Nurse Event Phrases

        public static string MedicineAdvised = "Medicine Advised";
        public static string LabTestAdvised = "Lab Test Advised";
        public static string RadiologyAdvised = "Radiology Test Advised";
        public static string ProcedureAdvised = "Procedures Advised";
        public static string DoctorNotes = "Doctor Write Notes";
        //public static string DoctorNotes = "Doctor Write Notes";

        #endregion

    }

    public static class CommonConstant
    {

        #region MIMSwardID
        public static int IPDWardID = 342;
        public static int OPDWardID = 263;
        #endregion

        #region Departments
        public static int IPD = 1;
        public static int OPD = 2;
        public static int Emergency = 3;
        #endregion

        #region MedicineRequisitionConstant
        public static int pending = 1;
        public static int dispensed = 2;
        public static int received = 3;
        public static int consumed = 4;
        public static int returned = 5;
        public static int cancelled = 6;
        #endregion

        #region Health Facility Type Code
        public static string Private = "060";
        public static string MobileUnits = "073";
        #endregion

    }
}
