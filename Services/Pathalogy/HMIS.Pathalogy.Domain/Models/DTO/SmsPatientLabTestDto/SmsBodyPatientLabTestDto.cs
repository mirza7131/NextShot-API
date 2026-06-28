using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SmsPatientLabTestDto
{
    public class SmsBodyPatientLabTestDto
    {
        public string? PatientName { get; set; }
        public string? PatientPhoneNo { get; set; }
        public string? TestName { get; set; }
        public string? Link { get; set; }
    }
}
