using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.NursingEventDto
{
    public class SPGetPatientNursingEventsDto
    {
        public Guid? NursingEventsId { get; set; }
        public Guid? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? TokenNo { get; set; }
        public string? Events { get; set; }
        public DateTime? AdvisedOn { get; set; }
        public string? AdvisedBy { get; set; }
        public DateTime? AcknowledgedOn { get; set; }
        public string? AcknowledgedBy { get; set; }


    }

    public class SPGetPatientNursingEventsCountDto
    {
        public int TotalRecord { get; set; }
    }
}
