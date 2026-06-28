using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.MenuDto
{
    public class MenuListAccessDto
    {
        public Guid MenuId { get; set; }

        public Guid? ModuleId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Url { get; set; }

        public string? Icon { get; set; }

    }
}
