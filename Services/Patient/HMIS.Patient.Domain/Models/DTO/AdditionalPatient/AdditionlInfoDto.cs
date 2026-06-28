using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.AdditionalPatient
{
    public class AdditionlInfoDto
    {
        public Guid PatientAdditionalInfoId { get; set; }

        public Guid? PatientId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? GuardianName { get; set; }

        public string? GuardianAddress { get; set; }

        public string? GuardianCnic { get; set; }

        public string? GuardianMobileNo { get; set; }

        public string Caste { get; set; } = null!;

        public string Occupation { get; set; } = null!;

        public string? IncidentPlace { get; set; }

        public Guid? CaseTypeProfileId { get; set; }

        public Guid? DoctorId { get; set; }

        public string? Mlcno { get; set; }

        public Guid? MlctypeProfileId { get; set; }

        public string? PoliceDistrict { get; set; }
        public string? CaseAgainst { get; set; }
        public string? EmcMleFormOpenType { get; set; }

        public string? PoliceDocketOne { get; set; }

        public string? PoliceDocketTwo { get; set; }

        public string? PoliceDocketThree { get; set; }
    }
}
