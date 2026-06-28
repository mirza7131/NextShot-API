using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.UnionCouncilDto
{
    public class CreateOrEditUnionCouncilDto
    {
        public int? UnionCouncilId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public int TehsilId { get; set; }

        public bool IsActive { get; set; }
    }
}
