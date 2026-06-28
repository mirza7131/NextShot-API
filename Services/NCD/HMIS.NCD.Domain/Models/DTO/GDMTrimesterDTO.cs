using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class GDMTrimesterDTO
    {
        public DateTime? LMPDate { get; set; }
        public float? BSR { get; set; }
        public float? BSF { get; set; }
        public float? HbA1C { get; set; }
        public float? OGTTOneHour { get; set; }
        public float? OGTTTwoHour { get; set; }
        public string? PreGestationalDiabetes { get; set; }
        public string? previouspregnancy { get; set; }
        public string? ConfirmedonTwodifferentdates { get; set; }
        public string? GestationalOGTTat16Weeks { get; set; }
        public string? GestationalOGTTat24Weeks { get; set; }
        public string? Gestationalagegreaterthan34weeks { get; set; }
        public float Onehourpostmeal { get; set; }
        public string? Trimester { get; set; } // 1 = ( ≤ 12 weeks ) ,2=  ( >12 or <24 weeks ),3 = ( ≥ 24 weeks )
        public string? ReferForOGTT { get; set; }
    }
}
