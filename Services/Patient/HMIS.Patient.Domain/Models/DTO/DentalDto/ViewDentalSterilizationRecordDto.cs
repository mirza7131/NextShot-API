using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.DistrictDto;
using HMIS.Patient.Domain.Models.DTO.DivisionDto;
using HMIS.Patient.Domain.Models.DTO.HealthFacilityDto;
using HMIS.Patient.Domain.Models.DTO.ProfileDto;
using HMIS.Patient.Domain.Models.DTO.ProvinceDto;
using HMIS.Patient.Domain.Models.DTO.TehsilDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DentalDto
{
    public class ViewDentalSterilizationRecordDto
    {
        public Guid? DentalSterilizationRecordId { get; set; }
        public Guid? EquipmentProfileId { get; set; }
        public string? EquipmentName { get; set; }
        public string? CreatedBy { get; set; }
        public int? NoOfPouches { get; set; }
        public bool IsActive { get; set; }
        public bool? CloseToExpiryDentalMaterial { get; set; }
        public string? Remarks { get; set; }

    }
}
