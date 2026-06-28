using HMIS.Patient.Domain.Models.DTO.PaginationDto;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class FilterPatientDto : PagerDto
    {
        public string? User { get;set; }
        public string? FullName { get;set; }
        public string? MobileNo { get; set; }
        public string? Cnic { get; set; }
        public string? Mrno { get; set; }
        public bool IsOverAllDashboard { get; set; } = false;

    }
    public class FilterMeaslesPatientDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }


    public class FilterDentalListPatientDto : PagerDto
    {
        public int? ListType { get; set; }
        public bool IsOverAllDashboard { get; set; } = false;
        public int? ListTypeForDashboard { get; set; }
    }
}

