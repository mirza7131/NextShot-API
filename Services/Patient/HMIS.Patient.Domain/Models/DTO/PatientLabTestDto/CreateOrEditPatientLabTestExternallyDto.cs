using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientLabTestDto
{
    public class CreateOrEditPatientLabTestExternallyDto
    {
        //public Guid? PatientLabTestId { get; set; }

        //public Guid? PatientId { get; set; }

        //public Guid? PatientVisitId { get; set; }

        public Guid LabDepartmentProfileId { get; set; }

        public int LabTestId { get; set; }
        public string? SourceLabTestId { get; set; }

        //public bool? IsSampleCollected { get; set; }

        //public DateTime? SampleCollectedOn { get; set; }

        //public string? TestAdvisedBy { get; set; }
        public string? SourceBarcodeNo { get; set; }
        public string? SourceBarcodeBase64 { get; set; }

        public bool? IsActive { get; set; }
    }
       
}
