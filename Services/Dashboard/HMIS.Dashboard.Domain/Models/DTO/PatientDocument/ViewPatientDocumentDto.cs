using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDocument
{
    public class ViewPatientDocumentDto
    {
        public Guid PatientDocumentId { get; set; }

        public Guid PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDocumentTypeProfileId { get; set; }

        public Guid? DocumentProfileId { get; set; }

        public string? DocumentName { get; set; }

        public string? Url { get; set; }

        public string? Base64 { get; set; }

        public byte? Status { get; set; }

        public string? StatusReason { get; set; }

        public bool? IsActive { get; set; }
    }
}
