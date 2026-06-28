using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class BreastCBCDTO
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? FormType { get; set; }
        public int? DocDepartmentLookupId { get; set; }
        public int? DocSectionLookupId { get; set; }
        public string? CBCStatus { get; set; }
        public string? Pain { get; set; }
        public string? Lump { get; set; }
        public string? NippleDischarge { get; set; }
        public string? SkinChanges { get; set; }
        public string? AxillaryLump { get; set; }
        public string? FNAC { get; set; }
        public string? ReferSurgeryDepartment { get; set; }
        public string? ReferHealthFacilityID { get; set; }
        public string? ReferForUltraSound { get; set; }
    }
}
