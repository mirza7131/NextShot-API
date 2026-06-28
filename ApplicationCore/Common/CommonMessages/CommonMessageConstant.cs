using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMessages
{
    public static class CommonMessageConstant
    {
        #region CRUD Messages

        public const string Save = "Record Saved Successfully";
        public const string Update = "Record Updated Successfully";
        public const string Delete = "Record  Deleted!";
        public const string Read = "Data Fetched";

        #endregion

        #region Patient

        public const string PatientExistWithCnic = "Patient Exists with this Cnic!";
        public const string PatientExistWithPassport = "Patient Exists with this Passport!";
        public const string PatientExistWithRelationCnic = "Patient Name Exists with this Cnic Relation!";
        public const string PatientExistWithRelationPassport = "Patient Name Exists with this Cnic Passport!";
        public const string VisitIsAlreadyGenerated = "Visit against this Patient is already Exists!";
        public const string IPDVisitIsAlreadyGenerated = "Patient already Admitted in IPD!";
        public const string ERVisitIsAlreadyGenerated = "Patient already Admitted in Emergency Department!";
        public const string OPDVisitIsAlreadyGenerated = "OPD Visit already exist on same Date!";
        public const string HFStationsNotDefined = "Contact Your Health Facility Admin to define Health Facility Stations!";
        public const string StationAlreadyAvailable = "Station Is already available in this HealthFacility!";
        public const string StationNotAvailable = "Station Is not available in Lookup!";

        public const string HealthFacilityNotHaveThisStation = "Health Facility Currently don't have this station!";
        public const string HFMustHaveRegistrationCounter = "Health Facility Must Have Registration Station, Contact your Admin to define Registration Counter!";
        public const string StationNotDefined = "Next Station is not defined!";
        public const string VisitNotExists = "Visit is not exists!";
        public const string VisitClosed = "Visit is Already Closed!";
        public const string PatientCalledAnotherDoctor = "Patient is called by another Doctor!";
        public const string PatientCalledAnotherVital = "Patient is already called by another Vitals Collecter!";
        
        public const string PatientIsAtDoctorCannotAccess = "Patient is Attended By Doctor, Now You cannot Access it.";

        public const string HealthFacilityIdNotAvailable = "Health Facility Id is not available!";
        public const string PatientContactNoAlreadyExists = "Patient Contact No. Already Exists";
        public const string NoLabTestFoundAgainstThisPatient = "No Lab Test Found Against This Patient, First Kindly Recommnend Tests";
        public const string SampleNotCollected = "Sample Not Collected";
        public const string KnownPatient = "Known";
        public const string UnknownPatient = "Unknown";

        public const string PatientReleased = "Patient Released!";
        public const string PatientAlreadyRegistered = "Patient is Already Admitted, agianst this Visit!";
        public const string PatientAdmittedAgainstThisRefer = "Patient is Admitted against your Referred Request, so you cannot Ammend Referred Section!";
        public const string RegisterRoomNoAndFloorNo = "براہے مہربانی اپنے ہسپتال کے اندر سپیشلٹی کے کمرہ نمبر اور منزل نمبر آئی ٹی ایڈمن سے رجسٹر کروائیں پھر آپ مریض کو رجسٹر کر سکتے ہیں";
        public const string PatientNotPaidDentalFee = "The patient has not yet paid the dental fee.";
        public const string FeeRefundAgainPayFee = "The fee has been refunded kindly pay the fee for check up.";
        public const string FeePaidForSomeLabTests = "Once payment is received for gynecological lab tests, the doctor cannot change the status.";
        #endregion


        #region NCD
        public const string MissingScreeningValueOrPreviousPregency = "Missing Screening Value OR Previous Pregency";
        #endregion


        #region TB
        public static string PatientVisitClosedAlready = "Patient visit closed already";
        #endregion

        #region DRTB
        public static string PatientIsNotReferedToDRTB = "Patient is not refered to DRTB, select patient status 'WalkIn'";
        public static string PatientIsReferedToDRTB = "Patient is refered to DRTB, select patient status 'Refered'";
        #endregion

        #region Images
        public const string OnlyJPGImageAllowed = "Please Choose jpg format images.";

        #endregion

        #region Patient Lab Test Externally

        public const string HealthFacilityNotFound = "Health Facilty Not Found!";
        public const string ReferredHealthFacilityNotFound = "Referred Health Facilty Not Found!";
        public const string TehsilNotFound = "Teshil Not Found!";
        public const string SourceSystemNotFound = "Source System Not Found!";

        #endregion

        #region Authentication Messages

        public const string Authenticated = "Authenticated";
        public const string UnAuthenticated = "Not Authenticated";
        public const string Authorized = "Authorized";
        public const string UnAuthorized = "Not Authorized";
        public const string UsernameNotFound = "Username Not Found";
        public const string CnicNotFound = "CNIC Not Found";
        public const string UserNotFound = "User Not Found";
        public const string EmailNotFound = "Email Not Found";
        public const string CNICNotFound = "CNIC # Not Found!";
        public const string IncorrectPassword = "Incorrect Password!";
        public const string UsernameOrEmailIncorrect = "Username Or Email Incorrect";
        public const string UsernameOrPasswordIncorrect = "Username Or Password Incorrect";
        public const string NoAccessTokenPresent = "Access token not provided!";
        public const string UserNotFoundInHR = "User Not Found in HRMIS!";
        public const string EmailAlreadyExists = "Email Already Exists!";
        public const string UsernameAlreadyExists = "UserName Already Exists!";
        public const string CNICAlreadyExists = "CNIC Already Exists!";
        public const string ContactNoAlreadyExists = "Contact No Already Exists!";
        public const string UserAlreadyExists = "User Already Exists!";
        public const string UserAlreadyExistWithDifferentHrId = "User Already Exists With Different HrId!";
        public const string DepartmentOrSectionNotDefined = "Department Or Section Not Defined";
        public const string OTPIsNotValid = "OTP is not valid";
        public const string OTPIsExpired = "OTP is expired";


        #endregion

        #region HR Messages
        public const string UserNotExistInCurrentDivision = "CNIC does not exist in current division";
        #endregion

        #region Http Messages

        public const string HttpContextCannotNull = "HttpContext cannot be null";

        #endregion

        #region Server Error

        public const string InternalServerError = "Internal server error!";
        public const string RecordNotFound = "Record Not Found!";
        public const string OldPasswordNotMatch = "Old Password Not Match";
        public const string CannotEditRecord = "User cannot edit record!";
        public const string CNICINVALID = "CNIC Invalid!";
        public const string WaitOperationTimeOut = "wait operation time out";

        #endregion

        #region Upload File Error

        public const string FileUploadError = "Error in uploading file!";

        #endregion

        #region HFDepartments

        public const string HFDepartmentAlreadyExists = "Department already exists in that health facility";

        #endregion

        # region HCP
        public const string TestingKitsNotFound = "Rapid Kits not Found in this Health Facility";
        //public const string HBVTestingKitsNotFound = "HBV Rapid Kit not Found in this Health Facility";
        public const string HBVKitStock = "HBV RDT / Prickers / Swabs not Found in this Health Facility";
        //public const string HCVTestingKitsNotFound = "HCV Rapid Kit not Found in this Health Facility";
        public const string HCVKitStock = "HCV RDT / Prickers / Swabs not Found in this Health Facility";
        public const string Result = "Result";
        public const string ViralLoad = "Viral Load";
        public const string SampleNotFoundAgainstThisPatient = "Sample Not Found Against This Patient";
        public const string PatientNotFoundinOldEMR = "Patient Not Found in Old EMR";
        public const string VaccinationIsNotFoundInHf = "Vaccination Stock Is Not Found In Health Facility";
        //public const string VaccinationStageStock = "Vaccination / Swabs / Prickers Stock Is Not Found In Health Facility";
        public const string ScreeningStageStock = "Vaccination / Swabs / 2cc Syringe Stock Is Not Found In Health Facility";
        public const string ErrorGettingDiagnoseResult = "Error Getting Diagnose Result";

        #endregion

        #region HttpCodes
        public static string Verified = "1";
        public static string IdentificationIsInProgress = "203";
        #endregion

        #region API Response
        public const string APIResponseError = "Response Error From NADRA API";
        public const string VerificationRequestNotGeneratedOrRecordNotFound = "Verification Request To NADRA Not Generated Or Record Not Found";
        #endregion

        #region Locations
        public const string DistrictNotFound = "District Not Found";
        public const string DivisionNotFound = "Division Not Found";
        public const string ProvinceNotFound = "Province Not Found";
        #endregion

        #region MedicinDispatch

        public const string MedicineDispenseViaMimsNotActive = "Medicine Dispense via MIMS is not active";
        public const string MimsMedicineIndentSyncStatusFailure = "Medicine Indent Synced Failed";
        public const string DispenseQuantityWarning = "You Cannot Dispense more then Prescribed Quantity!";
        public const string DispenseAlert = "Medicine is Dispensed, you can now Delete it!";

        #endregion

        #region Mims

        public const string OpeningStockSynced = "Opening Stock is already Synced!";

        #endregion



        #region EMC
        public const string DuplicateBookNumberFound = "Book No Should be unique try an other Book No...!";
        public const string DuplicateEmployeeLetterNumberFound = "Employee Letter No Should be unique try other one...!";
        public const string DuplicateOpviNumberFound = "Opvi No Should be unique try other one...!";

        #endregion

        #region HealthCouncil
        public static string UserAlreadyExistsWithCnic = "User Already Exists With Cnic";
        public static string UserAlreadyExistsWithPhoneNo = "User Already Exists With Phone No.";
        public static string UserAlreadyExistsWithAccountNo = "User Already Exists With Account No.";
        public static string CategoryExpensedAgainstMeeting = "Category expensed against meeting";
        public static string BankAccountAlreadyCreated = "Bank account already created for this healthfacility";
        public static string DairayNoAlreadyExits = "Dairay No Already Exits";
        public static string ChequeNoAlreadyExits = "Cheque No Already Exits";
        public static string NotificationNoAlreadyExists = "Notification No. already exists";
        #endregion

        #region HealthCheck
        public static string Working = "API is Up!";
        #endregion

        #region Event
        public static string CannotDeleteEvent = "You cannot delete this event, because patient visit exist againt this event!";
        public static string CannotCreateEvent = "Event is already exist against this Unit, you cannot create another one!";
        #endregion


        #region almonar
        public static string FeedPaid = "Patient Fee Already Paid";
        public static string FeeRefundDateHasPassed = "The date for fee refunds has already expired";
        public static string FeeCannotRefund = "The doctor has already checked this patient therefore, the fee cannot be refunded.";
      #endregion



      #region FamilyPlanning
      public static string NullProfileId = "Previous birth profileid is null";
      public static string NullProfileIdExamination = "Examination profileid is null";
      public static string ProfileIdNotEntered = "Previous birth profileid is not added";
      public static string NoOfCesareanNotGiven = "No. of Cesarean not given";
      public static string DTOIsNUll = "DTO is null";
      public static string PatientIdIsNUll = "PatientId is null";
      public static string UserRegistrationNotDone = "User registration details for family planning is not entered";
      public static string PatientExaminationNotDone = "Patient Examination is not done";
		#endregion
	
        

        #region Opening Indent Not Synced
        public static string OpeningIndentNotSynced = "Opening Indent Not Synced!";
        public static string IndentNotSynced = "Indent Not Synced!";
        #endregion
    }
}
