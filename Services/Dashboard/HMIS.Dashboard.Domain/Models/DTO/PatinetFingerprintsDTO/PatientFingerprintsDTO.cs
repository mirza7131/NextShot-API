using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.CreatePatinetFingerprintsDTO
{
    public class PatientFingerprintsDTO
    {
        public Guid? fingerPrintId { get; set; }
        public Guid? fingerPrintProfileTypeId { get; set; }
        public Guid? UnknownPatientId { get; set; }
        public string? base64 { get; set; }
    }
}
