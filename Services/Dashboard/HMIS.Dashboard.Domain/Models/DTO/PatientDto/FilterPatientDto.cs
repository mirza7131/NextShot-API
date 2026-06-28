using HMIS.Dashboard.Domain.Models.DTO.PaginationDto;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class FilterPatientDto : PagerDto
    {
        public string? User { get;set; }
        public string? FullName { get;set; }
        public string? MobileNo { get; set; }
        public string? Cnic { get; set; }
        public string? Mrno { get; set; }

    }
}

