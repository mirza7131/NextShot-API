using HMIS.Patient.Domain.Models.DTO.FilterDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.NursingEventDto
{
    public class FilterNursingEventDto : UserLevelFilterDto
    {
       
            public Guid? PatientId { get; set; }
            public string? TokenNo { get; set; }

            public int? DepartmentId { get; set; }
            public int? SectionId { get; set; }
            public int? ListType { get; set; }


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

    }
