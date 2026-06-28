using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class UltraSoundAndDiagnosisDTO
    {
        public UltraSoundAndDiagnosisDTO()
        {
            //UltraSoundResults = new List<PatientUltraSoundResultsViewModel>();
        }
        //public List<PatientUltraSoundResultsViewModel> UltraSoundResults { get; set; }
        public BreastUltraSoundResultsDTO? BreastUltraSounds { get; set; }
        public virtual ICollection<CreateOrEditPatientLabTestDto>? PatientUltrasound { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? FormType { get; set; }
        public int? DocDepartmentLookupId { get; set; }
        public int? DocSectionLookupId { get; set; }
        public string? ProvisionalDiagnosis { get; set; }
        public string? ReferSurgeryDepartment { get; set; }
        public string? ReferHealthFacilityID { get; set; }
        public bool? IsReferToTeritaryCareHospital { get; set; }
        public string? UltraSoundComments { get; set; }
        public string? UltraSoundFinding { get; set; }
        public string? FNAC { get; set; }
    }


    public class BreastUltraSoundResultsDTO
    {
        public string? LesionSize { get; set; }
        public string? NumberofLesions { get; set; }
        public string? LeftLesion { get; set; }
        public string? RightLesion { get; set; }
        public string? Texture { get; set; }
        public string? Margins { get; set; }
        public string? Orientation { get; set; }
        public string? Shape { get; set; }
        public string? ThinEcogenicCapsule { get; set; }
        public string? PosteriorAcoustic { get; set; }
        public string? GentleLobulation { get; set; }
        public string? MicroCalcification { get; set; }
        public string? ArchitecturalDistortion { get; set; }
        public string? DilatedDucts { get; set; }
        public string? Skinthickening { get; set; }
        public string? LymphNodesEnlarged { get; set; }
        public string? LymphLocation { get; set; }
        public string? LymphNodesSize { get; set; }
        public string? FattyHilum { get; set; }
        public string? CorticalThickness { get; set; }
        public string? RadiologistImpression { get; set; }
        public string? FNAC { get; set; }
    }
}
