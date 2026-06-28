using HMIS.Patient.Domain.Models.DTO.PatientDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto
{
    public class UpdateSscStatusAndDocumentDto
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? SscStatus { get; set; }
        public string? SscStatusReason { get; set; }
        public virtual ICollection<CreateOrEditPatientDocumentDto> PatientDocuments { get; set; } = new List<CreateOrEditPatientDocumentDto>();
    }
}
