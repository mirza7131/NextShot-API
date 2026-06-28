using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditPostMortemExternalFormDto
    {
        public Guid? PostmortemExternalExaminationId { get; set; }

        public Guid? MlcpostmortemId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? PhysicalRemarks { get; set; }

        public string? ClothRemarks { get; set; }

        public string? NeckRemarks { get; set; }

        public string? InjuriesRemarks { get; set; }
    }
}
