using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditMleReportDto
    {
        public Guid? MlereportId { get; set; }
        public Guid MlebasicInfoId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }

        public bool? IsActive { get; set; }

        public CreateOrEditPatientImageDto? MlcManualReport { get; set; }
        public CreateOrEditPatientImageDto? MlcDrawImage { get; set; }
        public Guid? PatientManualReportId { get; set; }
        public Guid? PatientDrawImgId { get; set; }

    }
}
