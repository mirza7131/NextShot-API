namespace AuthDAL.Models.Dto.MimsMedicineIndentLog

{
    public class CreateOrEditMimsMedicineIndentLogDto
    {
        public Guid MimsMedicineIndentLogId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? ResponseData { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public long? MimsIndentId { get; set; }

        public int? WardId { get; set; }
        public string? WardName { get; set; }
        
        public bool? SyncStatus { get; set; }
        public bool? IsActive { get; set; }
        public byte ActionTypeId { get; set; }

    }

}
