using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Domain.Models.DTO.DistrictDto;
using HMIS.Dashboard.Domain.Models.DTO.DivisionDto;
using HMIS.Dashboard.Domain.Models.DTO.HealthFacilityDto;
using HMIS.Dashboard.Domain.Models.DTO.ProfileDto;
using HMIS.Dashboard.Domain.Models.DTO.ProvinceDto;
using HMIS.Dashboard.Domain.Models.DTO.TehsilDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.DentalDto
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
