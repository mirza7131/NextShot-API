using HMIS.DrugAddict.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.ExternalApisDto
{
    public class ViewDrugAddictPatientVisitsDto
    {
        public Guid PatientVisitId { get; set; }
        public Guid? PatientId { get; set; }
        public string? FullName { get; set; }

        public string Cnic { get; set; } = null!;

        public string Addicted { get; set; } = null!;

        public string? GuardianName { get; set; }

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }

        public string Gender { get; set; } = null!;

        public string? ParmanentAddress { get; set; }
        public string? District { get; set; }

        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        public string TreatmentStatus { get; set; } = null!;

        public string Rehabilitation { get; set; } = null!;

        public string? HealthFacility { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityTypeName { get; set; }

    }
}
