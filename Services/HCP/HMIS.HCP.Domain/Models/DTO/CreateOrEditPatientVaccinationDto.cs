using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Domain.Models.DTO
{
    public class CreateOrEditPatientVaccinationDto
    {
        public Guid? PatientVaccinationId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? VaccinationTypeProfileId { get; set; }

        public Guid? VaccinationProfileId { get; set; }

        public int? VaccinationDoseCount { get; set; }

        public DateTime? VaccinationDose1Date { get; set; }

        public DateTime? VaccinationDose2Date { get; set; }

        public DateTime? VaccinationDose3Date { get; set; }
        public DateTime? VaccinationDose4Date { get; set; }
    }
}
