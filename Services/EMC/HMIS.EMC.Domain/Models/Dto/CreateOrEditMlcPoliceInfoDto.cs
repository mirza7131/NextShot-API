using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMlcPoliceInfoDto
    {
        public Guid? MlcpoliceInfoId { get; set; }

        public Guid? Mlcid { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? PoliceSignatureTypeProfileId { get; set; }

        public DateTime? PoliceSignatureDateTime { get; set; }

        public string? PolicePersonNameDesignation { get; set; }

        public string? PoliceStationName { get; set; }

        public string? PolicePeron2NameDesignation { get; set; }

        public string? PoliceStationAddress { get; set; }

        public string? CommentsByPolice { get; set; }

    }
}
