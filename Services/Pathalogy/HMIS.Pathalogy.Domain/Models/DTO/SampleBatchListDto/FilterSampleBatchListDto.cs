using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleBatchListDto
{
    public class FilterSampleBatchListDto : PagerDto
    {
        public string? BatchNumber { get; set; }
        public DateTime? BatchCreatedOn { get; set; }
        public DateTime? BatchResultUploadedOn { get; set; }
        public string? BatchCreatedBy { get; set; }
        public int? FilterBy { get; set; }
        //public int? ListType { get; set; }
        public string? FilterString { get; set; }
    }
}
