using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class CVCExamDTO
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? FormType { get; set; }
        public int? DocDepartmentLookupId { get; set; }
        public int? DocSectionLookupId { get; set; }
        public string? SpeculumExamination { get; set; }
        public string? VisualInspectionbyAceticAcid { get; set; }
        public int? ReferHealthFacilityID { get; set; }
        public Guid? ReferTypeId { get; set; }
        public string? ConsunForFPScreen { get; set; }
        public bool? DoEdit { get; set; }
        public bool? IsPapSmearPerformed { get; set; }
        public DateTime? PapSmearPerformedDate { get; set; }
        public string? PapSmearTestResult { get; set; }
        public bool? ReferToTurtiaryHospital { get; set; }
        public string? LocationOfAcctowhite { get; set; }
        public string? CryotherapyApplied { get; set; }
        public string? SampleBarcode { get; set; }
    }
}
