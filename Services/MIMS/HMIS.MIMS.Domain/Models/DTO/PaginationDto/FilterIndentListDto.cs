using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.PaginationDto
{
    public class FilterIndentListDto : PagerDto
    {
        //public Guid? MimsIndentStatusProfileId { get; set; }
        public Guid? ToMimsBranchId { get; set; }
        public string? ListType { get; set; }
        public int? IndentStatus { get; set; }
        public Guid? FilterMimsBranchId { get; set; }
        public Guid? FilterMimsBranchStatusProfileId { get; set; }
        public bool? ShowIndentsRequestedToMyBranch { get; set; }
    }

}
