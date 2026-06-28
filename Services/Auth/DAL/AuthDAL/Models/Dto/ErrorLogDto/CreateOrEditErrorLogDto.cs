using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.ErrorLogDto
{
    public class CreateOrEditErrorLogDto
    {
        public long? ErrorLogId { get; set; }

        public string? Message { get; set; }

        public string? StackTrace { get; set; }

        public string? InnerException { get; set; }

        public string? Method { get; set; }

        public string? Route { get; set; }

        public string? RouteBase { get; set; }
    }
}
