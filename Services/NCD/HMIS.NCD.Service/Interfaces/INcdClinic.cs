using HMIS.NCD.Domain.Models.DbModels;
using HMIS.NCD.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Service.Interfaces
{
    public interface INcdClinic
    {
        Task<PatientFollowUp> CreateOrEdit(CreateOrEditPatientFollowUpNcdClinicDto Input);
        public Task SavePatientPrescription(PatientPrescriptionDTO input);
        public Task SaveFollowup(PatientPrescriptionDTO input);
        public Task<DateTime?> GetPatientLastIssueBookLetDate(Guid PatientId);
    }
}
