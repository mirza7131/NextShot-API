using HMIS.Pathalogy.Domain.Models.DTO.ProfileTypeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDetailDto
{
    public class ViewSampleConsignmentDetailDto
    {
        public Guid? SampleConsignmentDetailId { get; set; }
        public Guid? SampleConsignmentId { get; set; }
        public Guid? PatientLabTestId { get; set; }
        public int? LabTestId { get; set; }
        public string? LabTestName { get; set; }
        public string? StatusReason { get; set; }
        public byte? Status { get; set; }
    }
}
