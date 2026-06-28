using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.MedicineDispatchDto
{
    public class ViewPharmacySlipDto
    {
        public ViewPharmacySlipDto()
        {
            MedicineDispatchLists = new List<MedicineDispatchListDto>();
        }
        public string HealthFacilityName { get; set; } = null!;
        public string TokenNo { get; set; } = null!;
        public string? MrNo { get; set; } = null!;
        public int? VisitNo { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string PatientName { get; set; } = null!;
        public string PrescribedBy { get; set; } = null!;
        public string Pharmacist { get; set; } = null!;

        public virtual List<MedicineDispatchListDto> MedicineDispatchLists { get; set; }
    }
    public class MedicineDispatchListDto
    {
        public int? MedicineId { get; set; }
        public string? Name { get; set; }
        public int? QuantityPrescribed { get; set; }
        public int? QuantityDispatch { get; set; }
    }
}
