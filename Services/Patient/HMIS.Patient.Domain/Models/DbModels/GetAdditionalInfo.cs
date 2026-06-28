using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DbModels
{
    public class GetAdditionalInfo
    {
        public Guid PatientAdditionalInfoId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid PatientOpenVisitId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? GuardianName { get; set; }

        public string? GuardianAddress { get; set; }

        public string? GuardianCnic { get; set; }

        public string? GuardianMobileNo { get; set; }

        public string? Caste { get; set; }

        public string? Occupation { get; set; }

        public string? MleIncidentPlace { get; set; }

        public Guid? CaseTypeProfileId { get; set; }

        public Guid? MlcSvCaseTypeProfileId { get; set; }

        public Guid? DoctorId { get; set; }

        public string? Mlcno { get; set; }

        public Guid? MlctypeProfileId { get; set; }

        public string? PmeIncidentPlace { get; set; }

        public string? MlcSvIncidentPlace { get; set; }

        public string? PoliceDistrict { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? PoliceDocketOne { get; set; }

        public string? PoliceDocketTwo { get; set; }

        public string? PoliceDocketThree { get; set; }

        public string? CaseAgainst { get; set; }

        public string? MlcType { get; set; }
    }
}
