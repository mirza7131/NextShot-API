
using HMIS.Patient.Domain.Models.DTO.PaginationDto;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto
{
    public class ViewPatienListOldTbDto
    {
        public int? PatientId { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? DivisionCode { get; set; }
        public string? TehsilCode { get; set; }
        public string? Cnic { get; set; }
        public string? ContactNo { get; set; }
        public string? HealthFacilityDivision { get; set; }
        public string? HealthFacilityDistrict { get; set; }
        public string? HealthFacilityTehsil { get; set; }
        public string? Report { get; set; }
        public string? Result { get; set; }
        public string? HIVResult { get; set; }
        public int? TestId { get; set; }
        public string? MrNo { get; set; }
        public string? Name { get; set; }

    }
}
