using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Business
{
    public abstract class BaseLogic
    {
        protected readonly ModelEntityFactory _factory;

        public BaseLogic(ModelEntityFactory factory)
        {
            _factory = factory;
        }
    }
}
