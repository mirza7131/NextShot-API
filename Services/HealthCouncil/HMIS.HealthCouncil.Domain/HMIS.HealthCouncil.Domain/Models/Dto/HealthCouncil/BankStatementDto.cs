using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{

    public partial class BankStatementDto
    {
        public int? HealthFacilityId { get; set; }

        public string? Month { get; set; }

        public string? File { get; set; }

        public string? Discription { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

}
