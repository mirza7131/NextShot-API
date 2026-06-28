using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMlcSvReportDto
    {
        public Guid? MlcsvreportId { get; set; }

        public Guid? MlcsvinitialInfoId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? NatureOfInjuries { get; set; }

        public string? DurationOfInjuries { get; set; }

        public string? KindOfWeaponUse { get; set; }

        public string? Treatment { get; set; }

        public string? Notes { get; set; }

        public string? KuoinjuryNote { get; set; }

        public string? FinalOpinion { get; set; }

        public Guid? ManualReportImageId { get; set; }

        public Guid? FinalReportImageId { get; set; }

        public bool IsActive { get; set; }

        public CreateOrEditPatientImageDto? ManualReport { get; set; }
        public CreateOrEditPatientImageDto? FinalReport { get; set; }
    }
}
