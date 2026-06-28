using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class ViewTbPatientsListDTO
    {
        public Guid? PatientId { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? Cnic { get; set; }

        public string? MobileNo { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? HealthFacilityName { get; set; }

        public bool IsActive { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }
        public int? PatientProvinceId { get; set; }
    }
}
