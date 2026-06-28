using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.EMR
{
    public class UpdateLabTestSampleCollectedDto
    {
        public long PkId { get; set; }
        public long? PatientId { get; set; }
        public int LabTestId { get; set; }
        public string TestName { get; set; }
        public bool IsSampleReceived { get; set; }
        public string SampleReceivedBy { get; set; }
        public bool IsSampleRejected { get; set; }
        public string SampleRejectedReason { get; set; }
        public string SampleRejectedBy { get; set; }
        public string SystemSourceId { get; set; }
        public string SourcePatientId { get; set; }
        public string SourcePatientMrNo { get; set; }
        public Nullable<System.DateTime> SampleRejectedDatetime { get; set; }
        public Nullable<System.DateTime> SampleReceivedDatetime { get; set; }
        public bool IsResultUploaded { get; set; }
        public Nullable<System.DateTime> ResultUploadDatetime { get; set; }
        public string ResultUploadedBy { get; set; }
        public string ResultJson { get; set; }
        public string ReportURL { get; set; }
    }
}
