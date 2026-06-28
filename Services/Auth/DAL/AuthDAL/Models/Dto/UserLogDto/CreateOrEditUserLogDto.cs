using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.UserLogDto
{
    public class CreateOrEditUserLogDto
    {
        public long? UserLogId { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? ExpireOn { get; set; }

        public Guid? UserId { get; set; }

        public string? HealthFacilityCode { get; set; }

        public int? HealthFacilityId { get; set; }

        public bool? IsActive { get; set; }

    }
}
