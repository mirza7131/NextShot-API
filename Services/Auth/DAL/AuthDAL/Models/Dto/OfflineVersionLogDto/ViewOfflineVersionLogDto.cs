using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.OfflineVersionLogDto
{
    public class ViewOfflineVersionLogDto
    {
        public Guid OfflineVersionLogId { get; set; }

        public string Name { get; set; } = null!;

        public int HealthFacilityId { get; set; }

        public Guid ProjectProfileId { get; set; }

        public string VersionNumber { get; set; } = null!;

        public string? IP_Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
