using StoryBoard.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoryBoard.Controllers
{
    [Authorize]
    public class GroupsController : BaseController
    {
        private readonly GroupLogic _groupLogic;

        public GroupsController(GroupLogic groupLogic)
        {
            _groupLogic = groupLogic;
        }

        public ActionResult Index(int? page)
        {
            var groups = _groupLogic.GetAll(page);
            return View(groups);
        }
    }
}