using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Security
{
    public class StoryBoardPrincipal : IPrincipal
    {
        private StoryBoardIdentity _identity;

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public StoryBoardPrincipal(StoryBoardIdentity identity)
        {
            _identity = identity;
        }
    }
}
