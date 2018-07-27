using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.WEB.Models;
using SocialNetwork.WEB.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        IHelpService Helper;
        IMessageService messService; // для тестов
        IFriendService friendService;
        public HomeController(IHelpService h, IMessageService mesServ, IFriendService fs)
        {
            friendService = fs;
            messService = mesServ;
            Helper = h;
        }

        public ActionResult Index()
        {
            //messService.GetAllDialogs(1);
            //messService.StartDialog(3,4);
            //messService.SendMessage();
           // if (User.Identity.IsAuthenticated) Online.UserOnline(Helper.GetUser(User.Identity.Name).Data.Id);
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            //Online.UserOnline(Helper.GetUser(User.Identity.Name).Data.Id);
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Contact()
        {
            //Online.UserOnline(Helper.GetUser(User.Identity.Name).Data.Id);
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}