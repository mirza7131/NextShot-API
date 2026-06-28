using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto
{
    public class SocaialWelfareAssignDoctorDto
    {
        public Guid Swdoctorassignid { get; set; }

        public Guid DoctorId { get; set; }

        public Guid PatientId { get; set; }

        public Guid VisitId { get; set; }

        public Guid SocialwellfareformId { get; set; }

        public int ProvinceId { get; set; }

        public int DivisionId { get; set; }

        public int DistrictId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
