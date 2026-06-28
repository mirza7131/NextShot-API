using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.MenuDto
{
    public class ViewModulesDto
    {
        public Guid MenuId { get; set; }

        public Guid? ModuleId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public int? SeqNo { get; set; }

        public Guid? ParentId { get; set; }

        public bool? IsApi { get; set; }

        public bool? IsLabel { get; set; }

        public bool? IsModule { get; set; }

        public bool IsDisplayMenu { get; set; }

        public bool IsActive { get; set; }

        public string? ImageUrl { get; set; }

        public IList<ViewModulesDto> children { get; set; } = new List<ViewModulesDto>();
    }
}
