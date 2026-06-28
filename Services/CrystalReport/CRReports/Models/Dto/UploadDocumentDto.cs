using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRReports.Models.Dto
{
    public class UploadDocumentDto
    {
        public string ProjectName { get; set; }
        public string FolderName { get; set; }
        public string Base64String { get; set; }
    }
}