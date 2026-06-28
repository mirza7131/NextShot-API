using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DentalDto
{
    public class CreateOrEditDentalSterilizationRecordDto
    {
        public Guid? DentalSterilizationRecordId { get; set; }
        public Guid? EquipmentProfileId { get; set; }
        public int? NoOfPouches { get; set; }
        public bool IsActive { get; set; }
        public bool? CloseToExpiryDentalMaterial { get; set; }
        public string? Remarks { get; set; }

    }
}
