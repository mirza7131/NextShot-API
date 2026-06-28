using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMLCDto
    {
        public Guid? Mlcid { get; set; }

        public Guid? MlctypeProfileId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? DoctorId { get; set; }

        public Guid? McdtypeProfileId { get; set; }

        public Guid? PcdtypeProfileId { get; set; }
        public string? PoliceDistrict { get; set; }
        public string? CaseAgainst { get; set; }

        public string? PoliceDocketOne { get; set; }

        public string? PoliceDocketTwo { get; set; }

        public string? PoliceDocketThree { get; set; }

        public string? BookNo { get; set; }
        public string? Mlcno { get; set; }
        public string? ConsentFile { get; set; }

        public Guid? ImageTypeProfileId { get; set; }

        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }

        public bool IsActive { get; set; }
    }

    public class UpdateAssignDoctorDto
    {
       public Guid? Mlcid { get; set; }
       public Guid? DoctorId { get; set; }
       public string? ReasonForChangeDoctor { get; set; }
    }
}
