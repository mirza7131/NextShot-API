using HMIS.Dashboard.Domain.Models.DTO.FilterDto;
using HMIS.Dashboard.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.DashboardDto
{

    public class DashboardFilter : UserLevelFilterDto
    {
        public string? User { get; set; }
        public string? TbPatientTypeContstant { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? listType { get; set; }
        public string? DiseaseProfileId { get; set; }
        public bool SwitchConnection { get; set; } = false;
        public string? conditionType { get; set; }
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int TotalRecords { get; set; } = 0;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

    public class SyncUtilityDashboardFilter: PagerDto { 
        
        public bool? isAllList { get; set; } 
        public string? Status { get; set; }


    }
    public class DashboardBASFilter : UserLevelFilterDto
    {
        public string? User { get; set; }
        public string? TbPatientTypeContstant { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? listType { get; set; }
        public bool IsFilter { get; set; } = false;

    }
    public class MedicineIssuedFilter : UserLevelFilterDto
    {
        public string? User { get; set; }
        public string? TbPatientTypeContstant { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? listType { get; set; }
        public int? MedicineId { get; set; }

    }




    public class TbDashboardFilter : PagerDto
    {
        public string? User { get; set; }
        public string? TbPatientTypeContstant { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public bool SwitchConnection { get; set; } = false;

    }

    public class TbDashboardMedicineDeliveryFilter : PagerDto
    {
        public string? User { get; set; }
        //public string? TbPatientTypeContstant { get; set; }
        //public int? DepartmentId { get; set; }
        //public int? SectionId { get; set; }

        public int? ListType { get; set; }
        public string? CNIC { get; set; }
        public string? MobileNo { get; set; }
        public string? MrNo { get; set; }

        public bool SwitchConnection { get; set; } = false;

    }
}