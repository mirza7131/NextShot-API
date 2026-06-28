using HMIS.Patient.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class ViewPatientWithVisitDto
    {
        public Guid PatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? GuardianName { get; set; }
        public string? NameOfCnicHolder { get; set; }

        public bool? IsSelf { get; set; }

        public Guid? RelationProfileId { get; set; }

        public string Cnic { get; set; } = null!;

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }

        public Guid? NationalityProfileId { get; set; }

        public string? PassportNo { get; set; }

        public Guid? MotherLandProfileId { get; set; }

        public Guid? CasteProfileId { get; set; }

        public Guid? GenderProfileId { get; set; }

        public Guid? BloodGroupProfileId { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? Ntn { get; set; }

        public int? ProvinceId { get; set; }

        public int? DivisionId { get; set; }

        public int? DistrictId { get; set; }

        public int TehsilId { get; set; }

        public int? UnionCouncilId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? Hfmiscode { get; set; }

        public string? ParmanentAddress { get; set; }

        public string? TemporaryAddress { get; set; }

        public Guid? ReligionProfileId { get; set; }

        public Guid? MaritialStatusProfileId { get; set; }

        public string? Domicile { get; set; }

        public DateTime? FollowupDate { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<PatientOpenVisit> PatientOpenVisits { get; } = new List<PatientOpenVisit>();

    }
}
