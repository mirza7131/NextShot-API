using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class DrugAddictPatientsByDistrictDto
    {
        public string? PatientDistrictName { get; set; }
        public string? PatientDivisionName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? TotalPatients { get; set; }
        public Guid? PatientVistId { get; set; }
    }
}
