using HMIS.Dashboard.Domain.Models.DTO.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HMIS.Dashboard.Domain.Models.DTO.PaginationDto
{
    public class PagedListDto<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedListDto(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static async Task<PagedListDto<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();
            var items =  await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedListDto<T>(items, count, pageNumber, pageSize);
        }

        public static PagedListDto<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedListDto<T>(items, count, pageNumber, pageSize);
        }

        public static Task ToPagedListAsync(IQueryable<ViewPatientQueDto> responseObj, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
