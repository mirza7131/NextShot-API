using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.LabTestDetailDto;
using AuthDAL.Models.Dto.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.LabTestDto
{
    public class ViewLabTestDto
    {
        public int LabTestId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? DepartmentProfileId { get; set; }

        public Guid? LabTestCategoryProfileId { get; set; }
        public Guid? LabTestTypeProfileId { get; set; }
        public bool? IsSampleRequired { get; set; }

        public string? SampleType { get; set; }

        public decimal? TestPrice { get; set; }

        public decimal? DoctorShare { get; set; }

        public decimal? StaffShare { get; set; }

        public decimal? GovtShare { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<CreateOrEditLabTestDetailDto> LabTestDetails { get; set; } = new List<CreateOrEditLabTestDetailDto>();
        public virtual Profile? LabTestTypeProfile { get; set; }
        public byte ActionTypeId { get; set; }
    }
}
