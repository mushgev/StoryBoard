using StoryBoard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoryBoard.Controllers
{
    public abstract class BaseController : Controller
    {
        protected StoryBoardIdentity Identity
        {
            get
            {
                return User.Identity as StoryBoardIdentity;
            }
        }
    }
}