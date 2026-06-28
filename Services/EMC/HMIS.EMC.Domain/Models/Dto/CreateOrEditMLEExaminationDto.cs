using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMLEExaminationDto
    {
        public Guid? MleexaminationId { get; set; }
        public Guid MlebasicInfoId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? History { get; set; }

        public string? ClothExamination { get; set; }

        public string? GeneralPhysicalExamination { get; set; }

        public string? InjuriesDescription { get; set; }

        public string? AdvisedInvestigate { get; set; }

        public string? LaboratoryInvestigation { get; set; }

        public string? OpinionSpecialistOrXrayReport { get; set; }

        public string? NatureOfInjuries { get; set; }

        public string? Fabrication { get; set; }

        public string? DurationOfInjuries { get; set; }

        public string? WeaponPoison { get; set; }

        public string? KuoInjuries { get; set; }

        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }

    }
}
