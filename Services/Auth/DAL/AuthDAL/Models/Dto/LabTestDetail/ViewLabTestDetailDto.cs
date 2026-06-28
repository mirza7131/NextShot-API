using AuthDAL.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.LabTestDto
{
    public class ViewLabTestDetailDto
    {
        public int? LabTestId { get; set; }

        public string? TestName { get; set; }

        public string? TestNormalValue { get; set; }

        public string? TestUnit { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public int LabTestDetailId { get; set; }

        public virtual ViewLabTestDto? LabTest { get; set; }
    }
}
