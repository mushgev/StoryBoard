using StoryBoard.Business;
using StoryBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StoryBoard.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly UserLogic _userLogic;

        public SecurityController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _userLogic.Register(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(model);
            }
            catch(Exception)
            {
                throw;
            }

            FormsAuthentication.RedirectFromLoginPage(model.Name, false);

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _userLogic.Login(model);
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

            FormsAuthentication.RedirectFromLoginPage(model.Name, false);

            return View(model);
        }
    }
}