using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleBatchListDto
{
    public class SampleBatchListDto
    {
        public string? BatchNumber { get; set; }
        public DateTime? BatchCreatedOn { get; set; }
        public string? BatchCreatedBy { get; set; }
    }
}
