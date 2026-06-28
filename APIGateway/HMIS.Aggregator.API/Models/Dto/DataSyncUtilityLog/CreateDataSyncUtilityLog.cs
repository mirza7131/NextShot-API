using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.Dto.DataSyncUtilityLog
{
    public class CreateDataSyncUtilityLog
    {
        public Guid? DataSyncUtilityLogId { get; set; }

        public int HealthFacilityId { get; set; }

        public string FileName { get; set; } = null!;
        public string? ServerType { get; set; }
        public string? FileSize { get; set; }

        public int Status { get; set; }

        public DateTime StatusUpdatedOn { get; set; }

        public DateTime? UploadedOn { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public DateTime? CompletedOn { get; set; }

        public string? Message { get; set; }
    }
}
