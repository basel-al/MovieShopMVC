using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PagedResultSet<TEntity> where TEntity : class
    {
        // 350 records that have ave 
        // 350/30 => 12 last page will have 20
        public PagedResultSet(IEnumerable<TEntity> data, int page, int pageSize, long count)
        {
            Page = page;
            PageSize = pageSize;
            Count = count;
            Data = data;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        }

        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; set; }
        public long Count { get; set; }

        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public IEnumerable<TEntity> Data { get; set; }
    }
}