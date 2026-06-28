using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.Dto.DataSyncToOffline
{
    public class ResponseOfflineSyncDto
    {
        public string? Message { get; set; }
        public bool Status { get; set; } = true;
        public ResponseDataOfflineSyncDto Data { get; set; }
    }  

    public class ResponseDataOfflineSyncDto
    {
        public string[] JsonData { get; set; }
    }

}
