using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DbModels
{
    public partial class ViewAnmonalList
    {
        public Guid? PatientVisitId { get; set; }

        //public Guid? PatientId { get; set; }

        public string? PatientName { get; set; }
        //public string? LabNo { get; set; }
        public string Cnic { get; set; } = null!;

        public string? MobileNo { get; set; }
        public int? DepartementLookupId { get; set; }
        public int? SectionLookupId { get; set; }

        //public string? LabDepartmentShortName { get; set; }

        //public string LabDepartmentName { get; set; } = null!;

        //public bool? IsPaid { get; set; }

        //public Guid? PaymentReceivedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }


    public partial class ViewAnmonalProcedureList
    {
        public Guid? PatientVisitId { get; set; }
        public string? PatientName { get; set; }
        public string Cnic { get; set; } = null!;
        public string? MobileNo { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public partial class ViewAnmonalProcedurePaymentList
    {
        public Guid? PatientDiagnoseProcedureId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? ProcedureTitle { get; set; }
        public bool? IsPaidProcedureFee { get; set; }
        public int? ProcedureFee { get; set; }
    }


    public partial class ViewAnmonalDashboardList
    {
        public Guid? PaymentReceivedBy { get; set; }
        public string? PatientName { get; set; }
        public string Cnic { get; set; } = null!;
        public string? LabTest { get; set; }
        public decimal TestPrice { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal? DiscountInPercentage { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public partial class ViewAnmonalListTotalCount
    {
        public int TotalRecord { get; set; }
    }
}
