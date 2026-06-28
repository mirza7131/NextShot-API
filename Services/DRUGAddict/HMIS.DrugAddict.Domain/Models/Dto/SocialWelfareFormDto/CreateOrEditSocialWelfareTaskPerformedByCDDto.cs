using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class CreateOrEditSocialWelfareTaskPerformedByCDDto
    {
        public Guid Id { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? SocialWelfareFormId { get; set; }

        public Guid DoctorId { get; set; }

        public string? PhoneNo { get; set; }

        public string? HomeVisited { get; set; }
        public string? PtTelephoneOrHomeVisited { get; set; }

        public DateTime VisitDate { get; set; }

        public string RelationWithPt { get; set; } = null!;

        public string PtMedicineRoutine { get; set; } = null!;

        public string PtDoctorCheckup { get; set; } = null!;

        public string PtDailyRoutine { get; set; } = null!;

        public string PtFamilyAttitude { get; set; } = null!;

        public string? CounsellingOfPatientOrFamily { get; set; }

        public string? GuidanceProvided { get; set; }

        public string? PtStatus { get; set; }
        public int? SessionNo { get; set; }
        public string? PtRelativeOrOtherDetail { get; set; }
        public string? Latitude { get; set; }

        public string? Longitude { get; set; }
        public string? CounselingofFamily { get; set; }
    }
}
