using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.CreatePatientImagesDTO
{
    public class PatientImagesDTO
    {
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public Guid? ProfileTypeId { get; set; }
        public Guid? ProfileId { get; set; }
        public Guid? UnknownPatientId { get; set; }
        public string?  base64 { get; set; }
    }
}
