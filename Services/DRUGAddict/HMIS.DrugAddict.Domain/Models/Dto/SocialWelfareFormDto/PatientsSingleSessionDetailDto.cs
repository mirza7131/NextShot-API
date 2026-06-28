using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class PatientsSingleSessionDetailDto
    {
        public string? FullName { get; set; }
        public decimal? Age { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? Mrno { get; set; }
        public Guid? PatientVistId { get; set; }
    }
}
