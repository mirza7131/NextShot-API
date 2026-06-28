using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.LabTest
{
    public class SPViewLabTest
    {
        public int LabTestId { get; set; }

        public string? Name { get; set; }

        public string? ShortName { get; set; }

        public string? DepartmentShortName { get; set; }

        public string? DepartmentName { get; set; }

        public Guid? DepartmentProfileId { get; set; }

        public Guid? LabTestCategoryProfileId { get; set; }

        public Guid? LabTestTypeProfileId { get; set; }

        public string? SampleType { get; set; }

        public decimal? TestPrice { get; set; }

        public decimal? DoctorShare { get; set; }

        public decimal? GovtShare { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? IsPerformedPrivately { get; set; }

        public bool IsActive { get; set; }

        public byte ActionTypeId { get; set; }
    }
}
