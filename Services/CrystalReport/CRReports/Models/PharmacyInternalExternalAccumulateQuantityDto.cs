using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRReports.Models
{
    public class PharmacyInternalExternalAccumulateQuantityDto
    {
        public int VisitCount { get; set; }
        public string MedicineName { get; set; }
        public int MedicineId { get; set; }
        public int ExternalMedicineQuantity { get; set; }
        public int InternalMedicineQuantity { get; set; }
    }
}