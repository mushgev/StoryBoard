using StoryBoard.Data;
using StoryBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Business
{
    public abstract class BaseLogic
    {
        protected const int ListPageSize = 10;
        protected const int ListPagesCount = 10;

        protected readonly ModelEntityFactory _factory;
        //Will be Garbage Collected
        protected readonly StoryBoardContext _context;

        public BaseLogic(ModelEntityFactory factory, StoryBoardContext context)
        {
            _factory = factory;
            _context = context;
        }

        protected PagedModel<T> ToPagedModel<T>(int total, int page, List<T> items) where T : class
        {
            var totalPagesCount = (int)Math.Ceiling((double)total / ListPageSize);

            var pageStart = (int)(Math.Ceiling((double)page / ListPagesCount) - 1) * ListPagesCount + 1;
            var pageEnd = pageStart + ListPagesCount - 1;
            if (pageEnd > totalPagesCount)
            {
                pageEnd = totalPagesCount;
            }

            return new PagedModel<T>
            {
                List = items,
                Page = page,
                PageSize = ListPageSize,
                PageCount = totalPagesCount,
                PageStart = pageStart,
                PageLength = ListPagesCount,
                PageEnd = pageEnd
            };
        }
    }
}
