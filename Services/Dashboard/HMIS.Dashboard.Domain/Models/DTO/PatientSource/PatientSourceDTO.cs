using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.CreatePatientSource
{
    public class PatientSourceDTO
    {
        public Guid? source { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? UnknownPatientId { get; set; }
        public string? designation { get; set; }
        public string? vehicleNo { get; set; }
        public string? Name { get; set; }
        public string? ContactNo { get; set; }
    }
}
