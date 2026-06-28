using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO
{
    public class ViewIndentMasterDto
    {
        public Guid IndentMasterId { get; set; }
        public string? IndentNumber { get; set; }
        public Guid FromMimsBranchId { get; set; }
        public Guid ToMimsBranchId { get; set; }
        public bool? IsMimsAcknowledged { get; set; }
        public int? HealthfacilityId { get; set; }
        public Guid MimsIndentStatusProfileId { get; set; }
        public Guid MedicineSourceTypeProfileId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime IssuedOn { get; set; }
        public Guid IssuedBy { get; set; }
    }

    public class ViewIndentMasterListByStatusFromSPDto
    {
        public long SrNo { get; set; }
        public Guid IndentMasterId { get; set; }
        public string? IndentNumber { get; set; }
        public bool? IsMimsAcknowledged { get; set; }
        public Guid? FromBranchId { get; set; }
        public string? FromBranch { get; set; }
        public Guid? ToBranchId { get; set; }
        public string? ToBranch { get; set; }
        public string? CurrentStatus { get; set; }
        public DateTime? ActionOn { get; set; }
        public string? ActionBy { get; set; }
    }

    public partial class ViewIndentMasterDetialListListTotalCount
    {
        public int TotalRecord { get; set; }
    }

    public class SPIndentDetailListDto
    {
        public Guid? IndentDetailId { get; set; }
        public long? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public long MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public DateTime? MedicineMfgDate { get; set; }
        public DateTime? MedicineExpDate { get; set; }
        public decimal? RequestedQty { get; set; }
        public decimal? IssuedQty { get; set; }
        public decimal? ReceivedQty { get; set; }
        public string? Remarks { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsSMLMedicine { get; set; }

    }
    
}
