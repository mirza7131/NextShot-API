using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class UpdateConsignmentTestReportStatus
    {
        public Guid PatientLabTestId { get; set; }
        public bool IsConsignementLabTestReportApproved { get; set; }
        public string? ConsignmentLabTestReportRejectedReason { get; set; }

    }
}




