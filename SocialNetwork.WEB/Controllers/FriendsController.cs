using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.WEB.Controllers
{
    public class FriendsController : Controller
    {
        IHelpService Helper;
        IFriendService friendService;
        IMessageService messService;
        public FriendsController(IFriendService frS, IMessageService mesServ, IHelpService h)
        {
            messService = mesServ;
            friendService = frS;
            Helper = h;
        }

        [Route("friends")]
        public ActionResult Friends()
        {
            ViewBag.isList = false;
            return View("FriendsContainer", null, "all");
        }

        [Route("friends/section={id}")]
        public ActionResult Friends(string id)
        {
            ViewBag.isList = false;
            return View("FriendsContainer", null, id);
        }

        public void SendFriendRequest(int friendId)
        {
            friendService.SendFriendRequest(Helper.GetUser(User.Identity.Name).Id, friendId);
        }

        public void AcceptFriendRequest(int friendId)
        {
            friendService.AcceptFriendRequest(Helper.GetUser(User.Identity.Name).Id, friendId);
        }

        public ActionResult UserFriends(int id)
        {
            ViewBag.onlineFriendsCount = friendService.GetOnlineFriends(id).Data.Count();
            return PartialView("_UserFriendsMy", friendService.GetFriends(Helper.GetUser(User.Identity.Name).Id).Data);
        }

        public ActionResult Handler(string section, bool isList)
        {
            List<UserDTO> usDTO;
            ViewBag.isList = isList;
            int userId = Helper.GetUser(User.Identity.Name).Id;
            switch (section)
            {
                case ("all"):
                    {
                        usDTO = friendService.GetFriends(userId).Data.ToList();
                        usDTO.ForEach(u => u.TempDialogId = messService.StartDialog(userId, u.Id).Data);
                        ViewBag.FriendsCount = usDTO.Count;
                        ViewBag.OnlineFriendsCount = friendService.GetOnlineFriendsCount(userId).Data;
                        return PartialView("_FriendsList", usDTO);
                    }
                case ("online"):
                    {
                        usDTO = friendService.GetOnlineFriends(userId).Data.ToList();
                        usDTO.ForEach(u => u.TempDialogId = messService.StartDialog(userId, u.Id).Data);
                        ViewBag.FriendsCount = friendService.GetFriendsCount(userId).Data;
                        ViewBag.OnlineFriendsCount = usDTO.Count;
                        return PartialView("_FriendsList", usDTO);
                    }
                case ("in_requests"):
                    {
                        usDTO = friendService.GetIncomingRequests(userId).Data.ToList();
                        usDTO.ForEach(u => u.TempDialogId = messService.StartDialog(userId, u.Id).Data);
                        ViewBag.IncomingRequestsCount = usDTO.Count;
                        ViewBag.OutboundRequestsCount = friendService.GetOutboundRequests(userId).Data.Count();
                        return PartialView("_FriendsRequests", usDTO);
                    }
                case ("out_requests"):
                    {
                        usDTO = friendService.GetOutboundRequests(userId).Data.ToList();
                        usDTO.ForEach(u => u.TempDialogId = messService.StartDialog(userId, u.Id).Data);
                        ViewBag.IncomingRequestsCount = friendService.GetIncomingRequests(userId).Data.Count();
                        ViewBag.OutboundRequestsCount = usDTO.Count;
                        return PartialView("_FriendsRequests", usDTO);
                    }
                case ("find"):
                    {

                        return PartialView("_FindFriends");
                    }
                default:
                    {
                        usDTO = friendService.GetFriends(userId).Data.ToList();
                        usDTO.ForEach(u => u.TempDialogId = messService.StartDialog(userId, u.Id).Data);
                        ViewBag.FriendsCount = usDTO.Count;
                        ViewBag.OnlineFriendsCount = friendService.GetOnlineFriendsCount(userId).Data;
                        return PartialView("_FriendsList", usDTO);
                    };
            }
        }
    }
}