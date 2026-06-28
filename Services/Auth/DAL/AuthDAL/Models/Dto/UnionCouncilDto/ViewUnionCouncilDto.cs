using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.UnionCouncilDto
{
    public class ViewUnionCouncilDto
    {
        public int UnionCouncilId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public int TehsilId { get; set; }

        public bool IsActive { get; set; }
    }

    public class ViewUcDto
    {
        public int? UcId { get; set; }

        public string? Name { get; set; }

        public string? TehsilCode { get; set; }

    }
}
