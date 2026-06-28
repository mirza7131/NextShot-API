using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRReports.Models.Dto
{
    public class DashboardFilter
    {
        public string User { get; set; }
        public string TbPatientTypeContstant { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public string HealthFacilityCode { get; set; }
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public string listType { get; set; }
        public string DiseaseProfileId { get; set; }
        public bool SwitchConnection { get; set; } = false;
        public string conditionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}