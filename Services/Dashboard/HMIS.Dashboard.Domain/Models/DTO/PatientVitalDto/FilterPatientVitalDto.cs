using HMIS.Dashboard.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientVitalDto
{
    public class FilterPatientVitalDto : PagerDto
    {
        public string? User { get; set; }
        public string? FullName { get; set; }
        public string? MobileNo { get; set; }
        public string? Cnic { get; set; }
        public string? Mrno { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? PatientProvinceId { get; set; }
        public int? MinWeight { get; set; }
        public int? MaxWeight { get; set; }
        public string? CurrentStation { get; set; }
        public Guid? GenderId { get; set; }
        public bool NormalRespitary { get; set; } = false;
        public bool HighRespitary { get; set; } = false;
        public bool NormalTemperature { get; set; } = false;
        public bool HighTemperature { get; set; } = false;

        public bool NormalPulse { get; set; } = false;
        public bool HighPulse { get; set; } = false;
        public bool LowPulse { get; set; } = false;

        public bool NormalBloodPressure { get; set; } = false;
        public bool LowBloodPressure { get; set; } = false;
        public bool HighBloodPressure { get; set; } = false;


    }
}
