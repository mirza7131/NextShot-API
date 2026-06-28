using HMIS.Dashboard.Domain.Models.DTO.PatientLabTestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class CreateOrEditPatientExternallyDto
    {
        public CreateOrEditPatientExternallyDto()
        {
            PatientLabTests = new List<CreateOrEditPatientLabTestExternallyDto>();
        }
        public string? PkId { get; set; }
        public string? SourcePatientId { get; set; }
        public string? SourceMrno { get; set; }
        public string? SourceVisitId { get; set; }
        public string? SystemSourceShortName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? GuardianName { get; set; }

        public string? NameOfCnicHolder { get; set; }

        public bool IsSelf { get; set; } = false;

        //public Guid? RelationProfileId { get; set; }
        public string Relation { get; set; }

        public string Cnic { get; set; } = null!;

        public int? Age { get; set; }

        public DateTime? Dob { get; set; }

        //public Guid? GenderProfileId { get; set; }

        public string Gender { get; set; }

        public string? MobileNo { get; set; }

        //public int? ProvinceId { get; set; }

        //public int? DivisionId { get; set; }

        //public int? DistrictId { get; set; }

        public string TehsilName { get; set; }

        //public int? UnionCouncilId { get; set; }
        public bool IsReffer { get; set; }

        public int? RefferedHealthFacilityHrId { get; set; }
        public string? RefferedHealthFacilityId { get; set; }
        public string? DoctorName { get; set; }

        public int HealthFacilityHrId { get; set; }
        public string? HealthFacilityId { get; set; }

        public string? ParmanentAddress { get; set; }
        public string? TemporaryAddress { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<CreateOrEditPatientLabTestExternallyDto> PatientLabTests { get; set; }
    }
}
