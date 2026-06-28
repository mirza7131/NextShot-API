using HMIS.Patient.Domain.Models.DTO.FilterDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;

namespace HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto
{
    public class FilterPatientVisitDto : PagerDto
    {
        public string? User { get; set; }
        public string? FullName { get; set; }
        public string? TokenNo { get; set; }
        public string? MobileNo { get; set; }
        public string? Cnic { get; set; }
        public string? Mrno { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? PatientProvinceId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public Guid? GenderId { get; set; }
        public int? VisitNo { get; set; }
        public string? Relation { get; set; }
        public int? SscStatus { get; set; }
        public bool IsConsultant { get; set; }

    }


    public class FilterSpecialityFeePaymentDto : PagerDto
    {
        public int? HealthfacilityId { get; set; }
    }

}

