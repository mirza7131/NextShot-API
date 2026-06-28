using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto
{
    public class CreateorEditPatientEligibleForSSCDto
    {
        public Guid PatientOpenVisitId { get; set; }
        public bool? IsEligibleForSsc { get; set; }
        public string? SscNumber { get; set; }
        //public Guid? ReasonIfNotEligibleForSsc { get; set; }
        public string? SscStatusReason { get; set; }


    }
}
