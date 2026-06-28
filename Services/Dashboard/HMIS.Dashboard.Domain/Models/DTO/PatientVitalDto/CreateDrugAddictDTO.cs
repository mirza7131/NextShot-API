using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientVitalDto
{
    public class CreateDrugAddictDTO
    {
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? Name { get; set; }
        public Guid ProfileId { get; set; }
        public string? ProfileTypeId { get; set; }
        public string? ProfileTypeName { get; set; }     
        public string? SequenceNo { get; set; }     
        public string? ShortName { get; set; }     

    }
}
