using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.Pathalogy.Domain.Models.DTO.FilterDto;

namespace HMIS.Pathalogy.Domain.Models.DTO.ViewAnmonalListDto
{
    public class FilterAnmonarDto : PagerDto
    {

        public string? User { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? ListType { get; set; }
        public string? LabTestType { get; set; }

        public int? FilterBy { get; set; }
        public bool IsOverAllDashboard { get; set; } = false;

    }
    public class FilterProcedureDto : PagerDto
    {

        public string? User { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? ListType { get; set; }
        public string? LabTestType { get; set; }
        public string? Cnic { get; set; }

        public int? FilterBy { get; set; }

    }
}
