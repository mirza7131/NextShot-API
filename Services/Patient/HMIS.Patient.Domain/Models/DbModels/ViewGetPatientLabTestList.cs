using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DbModels
{
    public partial class ViewGetPatientLabTestList
    {
        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }
        public Guid? PatientLabTestId { get; set; }

        public int? LabTestId { get; set; }

        public string? Name { get; set; }

        public Guid? DepartmentProfileId { get; set; }

        public string? DepartmentName { get; set; }

        public string? DepartmentShortName { get; set; }

        public Guid? PrescribedBy { get; set; }

        public string? PrescribedByName { get; set; }
        public string? FormType { get; set; }
        public string? Status { get; set; }
        public bool? IsReportGenerated { get; set; }
        public bool? IsRefunded { get; set; }

        public string? PrescribedByDesignation { get; set; }

        public DateTime? PrescribedOn { get; set; }

        public int? TestPrice { get; set; }
        
    }
}
