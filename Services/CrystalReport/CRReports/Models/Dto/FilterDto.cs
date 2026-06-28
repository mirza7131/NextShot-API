using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRReports.Models.Dto
{
    public class FilterDto
    {
        public Guid PatientVisitId { get; set; }
        public string CopyType { get; set; }
        public string PaperType { get; set; }
    }
}