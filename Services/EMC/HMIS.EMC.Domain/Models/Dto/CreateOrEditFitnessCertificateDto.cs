using HMIS.EMC.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditFitnessCertificateDto
    {
        // Fitness Table
        public Guid? FitnessCertificateId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public int HealthFacilityId { get; set; }
        public Guid? PatientImageId { get; set; }
        public Guid? RightThumbImageId { get; set; }
        public Guid? RightIndexImageId { get; set; }
        public Guid? RightMiddleImageId { get; set; }
        public Guid? RightRingImageId { get; set; }
        public Guid? RightLittleImageId { get; set; }
        public Guid? PreparedByUserId { get; set; }
        public Guid? ProfessionTypeId { get; set; }
        public string? RelativeName { get; set; }
        public string? EmployeeLetterNo { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? FullName { get; set; }
        public string? PresentJob { get; set; }
        public DateTime? LetterDateTime { get; set; }
        public string? DesignationAppliedFor { get; set; }
        public Guid? DesignationAppliedForTypeProfileId { get; set; }
        public Guid? RecomendedOrnotTypeProfileId { get; set; }
        public int? AgeByAppearance { get; set; }
        public string? CheckedBy { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? BodilyInfirmity { get; set; }
        public string? Department { get; set; }
        public Guid? EducationTypeProfileId { get; set; }
        public string? FormType { get; set; }
        public int? DocDepartmentLookupId { get; set; }
        public int? DocSectionLookupId { get; set; }

        public CreateOrEditPatientImageDto? PatientImage { get; set; }
        public CreateOrEditPatientImageDto? RightIndexImage { get; set; }
        public CreateOrEditPatientImageDto? RightThumb { get; set; }
        public CreateOrEditPatientImageDto? RightMiddle { get; set; }
        public CreateOrEditPatientImageDto? RightRing { get; set; }
        public CreateOrEditPatientImageDto? RightLittle { get; set; }

        //FitnessSerology

        public CreateOrEditFitnessSerologyDto? Serology { get; set; }

        // Fitness CBC

        public CreateOrEditFitnessCBCDto? Cbc { get; set; }

        //FitnessUrineCe

        public CreateOrEditFitnessUrineCEDto? UrineCe { get; set; }
        //FitnessGeneralParameter

        public CreateOrEditFitnessGeneralOrWidalParameterDto? GeneralOrWidalParameter { get; set; }

        //FitnessStoolExamination
        public CreateOrEditFitnessStoolExaminationDto? StoolExamination { get; set; }

        public CreateOrEditEmcDto? emc { get; set; }
        public List<MentalAssessmentDto>? PsyChologicalAssessment { get; set; }
    }
}