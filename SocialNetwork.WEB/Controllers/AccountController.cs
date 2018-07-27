using AutoMapper;
using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Infrastructure;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.WEB.App_Start;
using SocialNetwork.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        IMapper Mapper;
        IHelpService Helper;
        IFriendService friendService;
        IAccountService userService;
        IMessageService messService;
        public AccountController(IFriendService frS, IAccountService serv, IMessageService mesSer, IHelpService h)
        {
            Helper = h;
            messService = mesSer;
            friendService = frS;
            userService = serv;
            Mapper = AutoMapperWEBConfig.Mapper;
        }

        public ActionResult HeaderLogin()
        {
            if(User.Identity.IsAuthenticated)
            {
                UserDTO u = Helper.GetUser(User.Identity.Name);
                ViewBag.UserName = u.FirstName;
            }
            return PartialView("_HeaderLogin");
        }

        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ServiceResult<LoginDTO> r;
            if (ModelState.IsValid)
            {
                r = userService.LoginUser(Mapper.Map<LoginModel, LoginDTO>(model));
                if (r.Exception == null)
                {
                    if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("dialogs","Messages");
                    else return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(r.Exception.Property, r.Exception.Message);
                    return View(model);
                }
            }
            else return View(model);
        }

        [Route("registration")]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("registration")]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<RegistrationDTO> r = userService.RegisterUser(Mapper.Map<RegistrationModel, RegistrationDTO>(model));
                if (r.Exception == null)
                {
                    if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("friends");
                    else return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(r.Exception.Property, r.Exception.Message);
                    return View(model);
                }
            }
            else return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Online.UserOffline(Helper.GetUser(User.Identity.Name).Data.Id);
            return RedirectToAction("Index", "Home");
        }

        [Route("id={id}")]
        public ActionResult UserPage(int id)
        {
            ViewBag.userId = id;
            ViewBag.Friends = friendService.GetFriends(id).Data;
            return View(Helper.GetUser(id));
        }

        public ActionResult Menu()
        {
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.userId = Helper.GetUser(User.Identity.Name).Id;
                ViewBag.NewMessCount = messService.NewMessCount(ViewBag.userId).Data;
                ViewBag.NewFriendsReqCount = friendService.GetIncomingRequests(ViewBag.userId).Data.Count;
            }
            return View("_Menu");
        }
    }
}