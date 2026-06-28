using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.Enums
{
    public enum ActionTypeEnum
    {
       Create = 1,
       Edit = 2,
       Deleted = 3,
       BeforeDelete = 4,
       AfterUpdate = 5,
    }

    public enum RequestModeEnum
    {
        CreatePatientWithVisit = 1,
        UpdatePatientWithVisit = 2,
        CreateVisit = 3,
        UpdatePatient = 4,
    }

    public enum TbMedicineDeliveryStatusEnum
    {
        Pending = 1,
        Packing = 2,
        Dispatch = 3,
        Delivered = 4,
    }

    public enum AdmissionSourceTypeEnum
    {
        Registration = 1,
        Referred = 2,
    }

    public enum RiderLabTestStatusEnum
    {
        PendingforSampleCollection = 1,
        OutforSampleCollection = 2,
        SampleCollected = 3,
        DeliveredtoLab = 4,
        Cancelled = 5,
    }
    public enum SSCStatus
    {
        All = 0,
        Pending = 1,
        Eligible = 2,
        Not_Eligible = 3,
        Claim_Submitted = 4,
        Claim_Rejected = 5,
        Claim_Approved = 6,
        Claim_ReSubmitted = 7
    }

    public enum SscDocumentStatus
    {
        All = 0,
        Pending = 1,
        Rejected = 2,
        Approved = 3,
    }

    public enum SampleConsignmentStatus
    {
        Pending = 1,
        Rejected = 2,
        Approved = 3,
    }

    public enum SampleConsignmentDetailStatus
    {
        Pending = 1,
        Rejected = 2,
        Accepted = 3,
    }

    public enum DataSyncLogStatus
    {
        Pending = 1,
        Error = 2,
        Completed = 3,
        NotUploaded = 4,
    }

    public enum AlmonerListTypeEnums
    {
        UnPaid_Pathalogy = 1,
        Paid_Pathalogy = 2,
        UnPaid_Radiology = 3,
        Paid_Radiology = 4,
        UnPaid_Private = 5,
        Paid_Private = 6,
    }

    public enum SpecialityRunningMode
    {
        Both_OnlineOffline = 1,
        Online = 2,
        Offline = 3,
    }
}
