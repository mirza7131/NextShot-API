using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class BirthCertificateDto
    {
        public Guid BirthCertificateId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public int HealthFacilityId { get; set; }
        public string? MRNo { get; set; }
        public DateTime IssueDate { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
        //public int MyProperty { get; set; }
    }
}
