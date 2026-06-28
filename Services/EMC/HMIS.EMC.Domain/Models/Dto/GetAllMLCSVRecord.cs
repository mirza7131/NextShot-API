using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class GetAllMLCSVRecord
    {
        public Guid? Mlcid { get; set; }
        public string? Mlcno { get; set; }
        public Guid? DoctorId { get; set; }
        public long? ReportCounts { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorCNIC { get; set; }
        public string? PatientName { get; set; }
        public string? Relation { get; set; }
        public bool? IsFinalReport { get; set; }

    }
}
