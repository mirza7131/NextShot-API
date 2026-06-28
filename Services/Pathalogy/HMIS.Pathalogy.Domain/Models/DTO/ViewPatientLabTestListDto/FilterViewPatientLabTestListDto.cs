using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ViewPatientLabTestListDto
{
    public class FilterViewPatientLabTestListDto : PagerDto
    {
        public string? User { get; set; }
        public string? BatchNumber { get; set; }
        public bool? Rider { get; set; }
        public int? Stage { get; set; } = 1;

        public bool? IsExternalSource { get; set; } = false;
        public bool? IsArchive { get; set; } = false;
        public bool? IsRadiologyReportTab { get; set; } = false;

        public bool? IsConsignmentCreated { get; set; } = false;
        public int? OutSourceConsignmentHealthFacilityId { get; set; }
        public int? FilterBy { get; set; }
    }
}
