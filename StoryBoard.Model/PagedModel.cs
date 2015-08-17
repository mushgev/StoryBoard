using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Model
{
    public class PagedModel<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageStart { get; set; }
        public int PageEnd { get; set; }
        public int PageLength { get; set; }
        public int PageCount { get; set; }

        public List<T> List { get; set; }
    }
}
