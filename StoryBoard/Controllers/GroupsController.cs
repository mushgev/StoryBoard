using StoryBoard.Business;
using StoryBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult Create()
        {
            var group = new GroupModel();
            return View("Edit", group);
        }

        public ActionResult Edit(int id)
        {
            var group = _groupLogic.Get(id);
            return View(group);
        }

        public ActionResult Details(int id)
        {
            var userId = Identity.UserId;
            var group = _groupLogic.Get(id, userId);
            return View(group);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var group = _groupLogic.Get(id);
            return View(group);
        }

        public async Task<ActionResult> Join(int id)
        {
            var userId = Identity.UserId;
            await _groupLogic.Join(id, userId);
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<ActionResult> Leave(int id)
        {
            var userId = Identity.UserId;
            await _groupLogic.Leave(id, userId);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GroupModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.OwnerId = Identity.UserId;
                await _groupLogic.Edit(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int groupId)
        {
            await _groupLogic.Delete(groupId);
            return RedirectToAction("Index");
        }
    }
}