using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientLabTestDto
{
    public class CreateOrEditPatientLabTestDto
    {
        public Guid? PatientLabTestId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }

        public Guid? LabDepartmentProfileId { get; set; }

        public int? LabTestId { get; set; }

        public string? BarcodeNo { get; set; }

        public bool? IsSampleCollected { get; set; }

        public bool? IsReportGenerated { get; set; }
        public bool? IsOnBedSample { get; set; }

        public Guid? TestAdvisedBy { get; set; }

        public bool? IsAdvisedExternally { get; set; }

        public Guid? SourceSystemId { get; set; }
        public string? SourcePkId { get; set; }


        public string? SourceBarcode { get; set; }

        public string? SourceDoctorName { get; set; }
        public string? SourceLabTestId { get; set; }

        public bool? IsFromCallCenter { get; set; }

        public bool? IsPerformedPrivately { get; set; }
        public bool IsActive { get; set; }

    }
}
