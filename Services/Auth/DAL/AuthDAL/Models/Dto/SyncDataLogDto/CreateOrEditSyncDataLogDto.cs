using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.SyncDataLogDto
{
    public class CreateOrEditSyncDataLogDto
    {
        public long SyncDataLogId { get; set; }

        public string? TableName { get; set; }

        public string? ResourceSystem { get; set; }

        public DateTime? LastSync { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public long? UserLogId { get; set; }

        public byte ActionTypeId { get; set; }

        public bool IsActive { get; set; }
    }
}
