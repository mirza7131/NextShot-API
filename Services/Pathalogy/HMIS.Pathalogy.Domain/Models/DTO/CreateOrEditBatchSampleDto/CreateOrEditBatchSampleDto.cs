using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.CreateOrEditBatchSampleDto
{
    public class CreateOrEditBatchSampleDto
    {
        public string BatchNumber { get; set; }
        public List<Details> SampleBatchDetails { get; set; }
    }
    public class Details
    {
        public string BarcodeNo { get; set; }
        public string Cnic { get; set; }
        public Guid PatientLabTestId { get; set; }
    }
}
