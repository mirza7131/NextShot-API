using HMIS.DrugAddict.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class ViewSocialWelfareDeputyDirectorDto
    {
        public Guid SocialwellfareFormId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVistId { get; set; }
        public string Gender { get; set; } = null!;
        public string? VisitHf { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FullName { get; set; }
        public string? MobileNo { get; set; }
        public string? Mrno { get; set; }

        public string Cnic { get; set; } = null!;

        public string? LastName { get; set; }

        public int? DistrictId { get; set; }

        public int? DivisionId { get; set; }

        public int? TehsilId { get; set; }
        public bool? isAssignDoctor { get; set; }
        public Guid? AssignedDoctorId { get; set; }
        public string? AssignedDoctor { get; set; }
        public Guid? DoctorId { get; set; }
    }
}
