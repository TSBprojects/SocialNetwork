using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Linq.Expressions;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.WEB.Models;
using SocialNetwork.BLL.Infrastructure;
using SocialNetwork.BLL.DataTransferObjects;
using AutoMapper;

namespace SocialNetwork.Controllers
{
    public class MessagesController : Controller
    {
        IMapper Mapper;
        IHelpService Helper;
        IMessageService messService;
        public MessagesController(IMessageService mesServ, IHelpService helpServ)
        {
            Mapper = AutoMapperBLLConfig.Mapper;
            messService = mesServ;
            Helper = helpServ;
        }


        [Route("dialogs")]
        public ActionResult Dialogs()
        {
            return View("DialogsContainer", 0);
        }

        [Route("dialogs/id={id}")]
        public ActionResult Dialogs(int id)
        {
            if (messService.IsUserInDialog(id, Helper.GetUser(User.Identity.Name).Id).Data)
            {
                ViewBag.data = id;
                return View("DialogsContainer", 1);
            }
            else return RedirectToAction("Dialogs");
        }

        [Route("dialogs/q={id}")]
        public ActionResult Dialogs(string id)
        {
            ViewBag.data = id;
            return View("DialogsContainer", 2);
        }

        [Route("dialogs/section=unread")]
        public ActionResult Dialogs(int? temp)
        {
            return View("DialogsContainer", 3);
        }

        public ActionResult StartDialog(int userId)
        {
            return RedirectToAction("Dialogs", messService.StartDialog(Helper.GetUser(User.Identity.Name).Id, userId).Data);
        }

        public ActionResult DefaultDialogs(bool isUnread)
        {
            int userId = Helper.GetUser(User.Identity.Name).Id;
            ViewBag.messService = messService;
            ViewBag.userId = userId;
            if (isUnread) return PartialView("_Dialogs", messService.GetUnreadDialogs(userId).Data);
            else return PartialView("_Dialogs", messService.GetDialogs(userId).Data);
        }

        public ActionResult QueryDialogs(string id, bool isAjax)
        {
            int userId = Helper.GetUser(User.Identity.Name).Id;
            ViewBag.query = id;
            ViewBag.isAjax = isAjax;
            ViewBag.userId = userId;
            ViewBag.messService = messService;
            SearchResult<DialogDTO> sr = messService.SearchInDialogs(userId, id).Data;
            return PartialView("_QueryResultDialogs", sr);
        }

        public ActionResult CurrentDialog(int id)
        {
            DialogDTO d = messService.GetDialog(id, Helper.GetUser(User.Identity.Name).Id).Data;
            ViewBag.DialogId = id;
            ViewBag.DialogName = d.Name;
            ViewBag.DialogProfileImage = d.ProfileImage.FilePath;
            ViewBag.PartCount = messService.GetMessPartsCount(id,80).Data;
            return PartialView("_Dialog");
        }

        public ActionResult LoadMessages(int dialogId, int part)
        {
            ViewBag.userId = Helper.GetUser(User.Identity.Name).Id;
            return PartialView("_DisplayingMessages", messService.GetPartOfMessages(dialogId,part,80).Data);
        }

        public ActionResult QueryMessages(int dialogId, string query)
        {
            ViewBag.isSearch = true;
            return PartialView("_QueryResultMessages", messService.SearchInDialog(dialogId, query).Data);
        }

    }
}