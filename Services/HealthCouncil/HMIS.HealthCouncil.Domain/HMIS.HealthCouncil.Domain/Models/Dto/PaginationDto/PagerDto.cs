using HMIS.HealthCouncil.Domain.Models.DTO.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.DTO.PaginationDto
{
    public class PagerDto
    {
        const int maxPageSize = 100000;
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
        public string SearchString { get; set; } = "";
    }

}
