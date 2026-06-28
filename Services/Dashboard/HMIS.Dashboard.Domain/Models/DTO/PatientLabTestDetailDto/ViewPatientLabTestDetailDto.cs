using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientLabTestDetailDto
{
    public class ViewPatientLabTestDetailDto
    {
        public Guid PatientLabTestDetailId { get; set; }

        public Guid PatientLabTestId { get; set; }

        public int? LabTestId { get; set; }

        public string? TestName { get; set; }

        public string? TestNormalValue { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public string? TestUnit { get; set; }

        public string? Result { get; set; }

        public bool IsActive { get; set; }
    }
}
