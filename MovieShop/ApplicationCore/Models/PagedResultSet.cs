using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PagedResultSet<TEntity> where TEntity : class
    {
        public PagedResultSet(IEnumerable<TEntity> data, int page, int pageSize, long count)
        {

        }


    }
}