using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientLabTestDto
{
    public class ViewPatientLabTestDto
    {
        public Guid PatientLabTestId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? LabDepartmentProfileId { get; set; }

        public int? LabTestId { get; set; }

        public bool? IsSampleCollected { get; set; }

        public bool? IsReportGenerated { get; set; }

        public bool? IsOnBedSample { get; set; }

        public Guid? TestAdvisedBy { get; set; }

        public bool IsActive { get; set; }

    }
}
