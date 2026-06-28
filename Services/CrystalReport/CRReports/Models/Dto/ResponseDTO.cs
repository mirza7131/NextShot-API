using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CRReports.Models.Dto
{
    public class ResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = "Data Fetched";
        public Object data { get; set; }
    }
}