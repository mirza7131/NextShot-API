using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDto
{
    public class FilterSampleConsignmentDto : PagerDto
    {
        public Guid? SampleConsignmentId { get; set; }
        public int? ToHealthFacilityId { get; set; }
        public int? FromHealthFacilityId { get; set; }
        public string? User { get; set; }
        public int? LabTestStage { get; set; } = 1;
        public bool? IsArchive { get; set; } = false;
        public string? BatchNumber { get; set; }

        public int? FilterBy { get; set; }
        public int? ConsignmentStatus { get; set; }
    }
}

