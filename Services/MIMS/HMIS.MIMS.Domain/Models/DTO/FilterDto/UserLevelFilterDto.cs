using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.FilterDto
{
    public class UserLevelFilterDto : DateFilterDto
    {
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? HealthFacilityId { get; set; }
        public Guid? MimsBranchId { get; set; }
        public string? IndentNumber { get; set; }


    }
}
