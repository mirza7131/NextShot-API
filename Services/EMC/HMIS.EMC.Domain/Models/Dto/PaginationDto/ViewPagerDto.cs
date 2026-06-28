using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto.PaginationDto
{
    public class ViewPagerDto<T> where T : class
    {
        public ViewPagerDto()
        {
            List = new List<T>();
        }
        public int TotalCount { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int CurrentPage { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public bool HasNext { get; set; } = false;
        public bool HasPrevious { get; set; } = false;
        public List<T> List { get; set; }
    }
}
