﻿using StoryBoard.Business;
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

        public StoriesController(StoryLogic storyLogic)
        {
            _storyLogic = storyLogic;
        }

        public ActionResult Index(int? page)
        {
            var userId = Identity.UserId;
            var stories = _storyLogic.GetUserStories(userId, page);
            return View(stories);
        }

        public ActionResult Create()
        {
            var story = new StoryModel();
            return View("Edit", story);
        }

        public ActionResult Edit(int id)
        {
            var story = _storyLogic.Get(id);
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