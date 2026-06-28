using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRReports.Models.Dto
{


    public class OpdStockViewModel
    {
        public OpdStockViewModel()
        {
            MedicineList = new List<OpdStockDto>();
        }

        public List<OpdStockDto> MedicineList { get; set; }
        public int OverAllQuantitySum { get; set; }
        public int OverAllPriceSum { get; set; }

        public DashboardFilter DashboardFilter { get; set; }
    }
        public class OpdStockDto
    {
        public int Sr { get; set; }
        public int AvailableQuantity { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineTypeName { get; set; }
        public int PricePerItem { get; set; }
        public int TotalPrice { get; set; }
    }


    public class MedicineDispenseViewModel
    {
        public MedicineDispenseViewModel()
        {
            MedicineList = new List<MedicineDispenseListDto>();
        }

        public List<MedicineDispenseListDto> MedicineList { get; set; }
        public int OverAllQuantitySum { get; set; }
        public int OverAllPriceSum { get; set; }

        public DashboardFilter DashboardFilter { get; set; }
    }
    public class MedicineDispenseListDto
    {
        public int InternalMedicineQuantity { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int VisitCount { get; set; }
        public string MedicineTypeName { get; set; }
        public int PricePerItem { get; set; }
        public int TotalPrices { get; set; }
    }
}