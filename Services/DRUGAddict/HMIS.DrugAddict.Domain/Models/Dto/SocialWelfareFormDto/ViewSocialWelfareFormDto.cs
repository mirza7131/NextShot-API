using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class ViewSocialWelfareFormDto
    {
        public Guid SocialWelfareFormId { get; set; }

        public string? FullName { get; set; }

        public string? MobileNo { get; set; }

        public string? Mrno { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public DateTime? VisitDate { get; set; }

        public DateTime? Dob { get; set; }

        public decimal? Age { get; set; }

        public string Gender { get; set; } = null!;

        public string? VisitHf { get; set; }

        public string? PatientHf { get; set; }

        public string Relation { get; set; } = null!;

        public string? PatientDivisionName { get; set; }

        public string? PatientProvinceName { get; set; }

        public string? PatientDistrictName { get; set; }

        public string? PatientTehsilName { get; set; }

        public DateTime? PatientCreatedOn { get; set; }

        public DateTime? PatientVisitCreatedOn { get; set; }

        public int? HealthFacilityProvinceId { get; set; }

        public int? HealthFacilityDivisionId { get; set; }

        public int? HealthFacilityDistrictId { get; set; }

        public int? HealthFacilityTehsilId { get; set; }

        public int HealthFacilityId { get; set; }

        public int? DepartementLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public string? PatientVisitCreatedByName { get; set; }

        public byte ActionTypeId { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
