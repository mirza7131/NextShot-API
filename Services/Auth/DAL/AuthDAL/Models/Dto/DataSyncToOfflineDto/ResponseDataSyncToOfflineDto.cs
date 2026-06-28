using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.DataSyncToOfflineDto
{
    public class ResponseDataSyncToOfflineDto
    {
        public string[] JsonData { get; set; }
    }

    public class ResponseDataSyncFromSPDto
    {
        public string? JsonData { get; set; }
    }

    public class ResponseSyncDataInsertOrUpdateDto
    {
        public bool Response { get; set; }
    }
}
