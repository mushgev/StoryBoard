using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoryBoard.Controllers
{
    [Authorize]
    public class StoriesController : Controller
    {
        // GET: Stories
        public ActionResult Index()
        {
            return View();
        }
    }
}