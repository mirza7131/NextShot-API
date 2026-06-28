using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DbModels
{
    public partial class ViewSampleBatchList
    {
        public string? BatchNumber { get; set; }

        public DateTime? BatchCreatedOn { get; set; }
        public DateTime? BatchResultUploadedOn { get; set; }
        public int? SamplesCount { get; set; }

        public string? BatchCreatedBy { get; set; }
    }
}
