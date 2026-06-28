using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.AlmonerDental
{
    public class SpecialityFeeDTO
    {
        public int? AmountReceived { get; set; }
        public int? TotalRefund { get; set; }
        public int? Balance { get; set; }
    }


    public class DentalProcedureStatDTO
    {
        public int? AmountToBeCollected { get; set; }
        public int? AmountReceived { get; set; }
    }
    //public class DentalProcedureListDTO
    //{
    //    public string? PatientName { get; set; }
    //    public string? Cnic { get; set; }
    //    public string? ProcedureTitle { get; set; }
    //    public string? ProcedureFee { get; set; }
    //    public string? CreatedOn { get; set; }
    //}
}
