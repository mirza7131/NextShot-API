using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PaginationDto
{
    public class ViewPagerDto<T>  where T : class
    {
        //public ViewPagerDto()
        //{
        //    this.data = new List<TEntity>();
        //}
        //public int TotalCount = 0;
        //public int PageSize = 0;
        //public int CurrentPage = 0;
        //public int TotalPages = 0;
        //public bool HasNext = false;
        //public bool HasPrevious = false;
        //public List<TEntity> data;

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
