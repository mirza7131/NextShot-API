using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientLocationPrefixDto
{
    public class CreateOrEditPatientLocationPrefixDto
    {
        public Guid Id { get; set; }

        public string? MrnprefixCode { get; set; }

        public string? LocationCode { get; set; }

    }

    public class ViewPatientLocationPrefixDto
    {
        public Guid Id { get; set; }

        public string? MrnprefixCode { get; set; }

        public string? LocationCode { get; set; }

    }


    
}
