using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.EMRRequest
{
    public class EMRRequestForSuspectedPatient
    {
        public long PatientId { get; set; }
        public long HealthFacility_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string RelativePhoneNumber { get; set; }
        public string RelativeName { get; set; }
        public string RelativeRelation { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressProvince { get; set; }
        public string AddressDivision { get; set; }
        public string AddressDistrict { get; set; }
        public string UC { get; set; }
        public string AddressTehsil { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CnicNumber { get; set; }
        public string CnicFamilyNumber { get; set; }
        public string Gender { get; set; }
        public string UserIdCreatedBy { get; set; }
        public DateTime DateTimeCreatedAt { get; set; }
        public string BloodGroup { get; set; }
        public string DeathReason { get; set; }
        public long? AreaId { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string Mother_name { get; set; }
        public Nullable<System.Guid> PatientRegId { get; set; }
        public List<PatientVisitDiseaseViewModel> Diseases { get; set; }

    }
    public class PatientVisitDiseaseViewModel
    {
        public long Id { get; set; }
        public string DiseaseName { get; set; }

    }
}
