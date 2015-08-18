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
    public class StoriesController : BaseController
    {
        private readonly StoryLogic _storyLogic;
        private readonly GroupLogic _groupLogic;

        public StoriesController(StoryLogic storyLogic, GroupLogic groupLogic)
        {
            _storyLogic = storyLogic;
            _groupLogic = groupLogic;
        }

        public ActionResult Index(int? page)
        {
            var userId = Identity.UserId;
            var stories = _storyLogic.GetUserStories(userId, page);
            return View(stories);
        }

        public ActionResult Create()
        {
            var userId = Identity.UserId;
            ViewBag.GroupOptions = new MultiSelectList(_groupLogic.GetUserGroups(userId), "GroupId", "Name");

            var story = new StoryModel();
            return View("Edit", story);
        }

        public ActionResult Edit(int id)
        {
            var userId = Identity.UserId;
            var story = _storyLogic.Get(id);

            ViewBag.GroupOptions = new MultiSelectList(_groupLogic.GetUserGroups(userId), "GroupId", "Name", story.Groups);

            return View(story);
        }

        public ActionResult Details(int id)
        {
            var story = _storyLogic.Get(id);
            return View(story);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var story = _storyLogic.Get(id);
            return View(story);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StoryModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.UserId = Identity.UserId;
                await _storyLogic.Edit(model);
            }
            catch (ArgumentException ex)
            {
                ViewBag.GroupOptions = new MultiSelectList(_groupLogic.GetUserGroups(Identity.UserId), "GroupId", "Name", model.Groups);
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
        public async Task<ActionResult> Delete(int storyId)
        {
            await _storyLogic.Delete(storyId);
            return RedirectToAction("Index");
        }
    }
}