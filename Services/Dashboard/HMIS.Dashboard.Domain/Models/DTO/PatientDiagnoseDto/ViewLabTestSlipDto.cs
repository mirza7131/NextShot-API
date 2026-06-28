using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto
{
    public class ViewLabTestSlipDto
    {
        public ViewLabTestSlipDto()
        {
            LabTests = new List<LabTestDto>();
        }
        public string MrNo { get; set; } = null!;
        public string HealthFacilityName { get; set; } = null!;
        public string TokenNo { get; set; } = null!;
        public int? VisitNo { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string PatientName { get; set; } = null!;
        public string AdvisedBy { get; set; } = null!;
        public string AdvisedByDesignation { get; set; } = null!;

        public virtual List<LabTestDto> LabTests { get; set; }
    }

    public class LabTestDto
    {
        public string? Department { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
}
