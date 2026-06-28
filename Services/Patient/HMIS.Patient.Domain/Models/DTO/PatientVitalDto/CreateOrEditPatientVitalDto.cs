using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientVitalDto
{
    public class CreateOrEditPatientVitalDto
    {
        public CreateOrEditPatientVitalDto()
        {

            DrugAddiction = new List<CreateDrugAddictDTO>();
            CoMorbid = new List<CreateCoMorbidDTO>();
        }
        public Guid? PatientVitalId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public Guid? PatientLastVisitId { get; set; } // in case of ncd
        public bool IsFollowup { get; set; } = false;// in case of ncd

        public string? Bpsystolic { get; set; }
        public string? FormType { get; set; }

        public string? BpdiaSystolic { get; set; }

        public string? Pulse { get; set; }

        public string? Temprature { get; set; }

        public string? Weight { get; set; }

        public string? Height { get; set; }

        public string? ResperatoryRate { get; set; }
        public string? BMI { get; set; }

        public int? DepartmentLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public Guid? VitalsCollectedBy { get; set; }
        public string? VitalsCollectedByDesignation { get; set; }

        public Guid? PatientConditionProfileId { get; set; }

        public bool IsVisitClose { get; set; } = false;
        public bool IsActive { get; set; }
        public decimal? BloodSugar { get; set; }
        public decimal? Waist { get; set; } = 0;
        public decimal? Hip { get; set; } = 0;
        public decimal? RatioHipToWaist { get; set; } = 0;
        public bool? IsNcdPositive { get; set; }
        public bool? IsMuawinPositive { get; set; }
        public virtual ICollection<CreateDrugAddictDTO>? DrugAddiction { get; set; }
        public virtual ICollection<CreateCoMorbidDTO>? CoMorbid { get; set; }

    }
}
