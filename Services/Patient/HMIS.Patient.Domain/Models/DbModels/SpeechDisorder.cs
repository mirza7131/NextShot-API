using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DbModels
{

    public partial class SpeechDisorder
    {
        public Guid? SpeechDisorderId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? SpeechDisorderProfileId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public byte? ActionTypeId { get; set; }
    }
}
