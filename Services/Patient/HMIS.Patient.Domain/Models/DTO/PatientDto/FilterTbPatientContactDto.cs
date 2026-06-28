using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class FilterTbPatientContactDto: PagerDto
    {
        public string? User { get; set; }
        public Guid? ContactId { get; set; }
        public string? ContactName { get; set; }
        public string? ContactNo { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? Age { get; set; }
        public string? Relation { get; set; }
        public bool? isSputumCollected { get; set; }
        public Guid? CollectedBy { get; set; }


    }
}
