using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class SocialWelfareTaskPerformedByCdDto
    {
        public Guid Id { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? SocialWelfareFormId { get; set; }

        public DateTime VisitDate { get; set; }

        public string RelationWithPt { get; set; } = null!;

        public string PtMedicineRoutine { get; set; } = null!;

        public string PtDoctorCheckup { get; set; } = null!;

        public string PtDailyRoutine { get; set; } = null!;

        public string PtAttitudeWithFamily { get; set; } = null!;

        public int? SessionNo { get; set; }

        public bool? IsVisitClosed { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
