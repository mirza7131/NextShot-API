using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;
using AuthDAL.Models.Dto.LabTestDetailDto;
using AuthDAL.Models.Dto.SectionLookupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfDepartmentDto
{
    public class ViewHfDepartmentDto
    {
        public int HfDepartmentId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? DepartmentLookupId { get; set; }
        public List<int?> SectionIds { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionNames { get; set; }
        public string? HealthFacilityName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedByName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ViewHfDepartmentSectionDto> HfDepartmentSections { get; set; } = new List<ViewHfDepartmentSectionDto>();
        public virtual ViewDepartmentLookupDto? DepartmentLookup { get; set; }
    }
}
