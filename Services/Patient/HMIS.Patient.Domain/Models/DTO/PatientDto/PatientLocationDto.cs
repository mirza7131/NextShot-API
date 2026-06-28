using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class PatientLocationDto
    {
        public int TehsilId { set; get; }
        public int? DistrictId { set; get; }
        public int? DivisionId { set; get; }
        public int? ProvinceId { set; get; }
    }
}
