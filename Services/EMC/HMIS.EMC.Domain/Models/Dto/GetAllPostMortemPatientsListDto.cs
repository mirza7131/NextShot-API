using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class GetAllPostMortemPatientsListDto
    {
        public Guid MLCPostmortemId { get; set; }
        public string? PMRNo { get; set; }
        public long? ReportCounts { get; set; }
        public string? MobileNo { get; set; }
        public string? FullName { get; set; }
        public string? DoctorName { get; set; }
        public string? Relation { get; set; }
        public string? MRNo { get; set; }
        public int? Age { get; set; }
        public string? CNIC { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        //public string? BookNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid PostmortemExternalExaminationId { get; set; }
        public Guid PostmortemReportId { get; set; }
        public bool? IsFinalReport { get; set; }
    }
}
