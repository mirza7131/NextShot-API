using HMIS.EMC.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class MlcrecordDto
    {
        public Guid Mlcid { get; set; }

        public Guid? MlctypeProfileId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? DoctorId { get; set; }

        public Guid? McdtypeProfileId { get; set; }

        public Guid? PcdtypeProfileId { get; set; }

        public string? Mlcno { get; set; }

        public string? BookNo { get; set; }

        public string? ConsentFile { get; set; }

        public Guid? ImageTypeProfileId { get; set; }

        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<MlcbodyIdentifierInfo> MlcbodyIdentifierInfos { get; } = new List<MlcbodyIdentifierInfo>();

        public virtual ICollection<MlcpoliceInfo> MlcpoliceInfos { get; } = new List<MlcpoliceInfo>();

        public virtual ICollection<Mlcpostmortem> Mlcpostmortems { get; } = new List<Mlcpostmortem>();

        public virtual ICollection<MlebasicInfo> MlebasicInfos { get; } = new List<MlebasicInfo>();

        public virtual Patient Patient { get; set; } = null!;
    }
}
