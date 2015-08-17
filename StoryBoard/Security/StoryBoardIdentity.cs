using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Security
{
    public class StoryBoardIdentity : IIdentity
    {
        private int _userId;
        private string _name;

        public string AuthenticationType
        {
            get { return "FormsAuthentication"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrWhiteSpace(_name); }
        }

        public int UserId
        {
            get { return _userId; }
        }

        public string Name
        {
            get { return _name; }
        }

        public StoryBoardIdentity(int userId, string name)
        {
            _userId = userId;
            _name = name;
        }
    }
}
