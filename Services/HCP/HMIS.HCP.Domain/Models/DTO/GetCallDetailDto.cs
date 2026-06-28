using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Domain.Models.DTO
{
    public class GetCallDetailDto
    {
        public DateTime LastVisitDate { get; set;}
        public DateTime PatientRegistrationDate { get; set;}
        public string? PatientName { get; set;}
        public DateTime DOB { get; set;}
        public int? Age { get; set;}
        public string? CNIC { get; set;}
        public string? MRNo { get; set;}
        public string? GuardianName { get; set;}
        public string? Gendar { get; set;}
        public string? MobileNo { get; set;}
        public string? ParmanentAddress { get; set;}
        public string? District { get; set;}
        public string? Division { get; set;}
        public string? Province { get; set;}
        public DateTime? ScreeningDate { get; set;}
        public string? HealthFacility { get; set;}
    }
}
