using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.MenuDto
{
    public class ViewMenuAccessDto
    {
        public Guid MenuId { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public bool? IsDisplayMenu { get; set; }

    }
}
