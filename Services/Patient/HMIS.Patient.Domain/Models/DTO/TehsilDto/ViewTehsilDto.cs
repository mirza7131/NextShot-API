using HMIS.Patient.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.TehsilDto
{
    public class ViewTehsilDto
    {
        public int TehsilId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int? DistrictId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public long? UserLogId { get; set; }

        public byte ActionTypeId { get; set; }
    }
}
