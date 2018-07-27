using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.WEB.Models;
using System.Web.Security;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.WEB.SignalR.Hubs
{
    public class WorkHub : Hub
    {
        private const int _SEC = 20;
        IHelpService Helper = DependencyResolver.Current.GetService<IHelpService>();
        IMessageService messService = DependencyResolver.Current.GetService<IMessageService>();
        IOnlineService onlineService = DependencyResolver.Current.GetService<IOnlineService>();
        public WorkHub()
        {
            if(Timer==null) Timer = new Timer(new TimerCallback(RefreshOnline),null, _SEC * 500, _SEC* 500);
        }

        private static Timer Timer;
        private static List<ConnectedUser> users = new List<ConnectedUser>();

        //===================Messages===================//
        public void SendMessage(int dialogId, string Text)
        {
            bool isToday = true;
            UserDTO fromUser = Helper.GetUser(Context.User.Identity.Name);
            DialogDTO dDTO = messService.GetDialog(dialogId, fromUser.Id).Data;
            DialogMessageDTO prevdm = dDTO.LastMessInDialog;
            IEnumerable<UserDTO> uid = messService.GetUsersInDialog(dDTO.Id).Data;
            DialogMessageDTO dm = messService.SendMessage(new DialogMessageDTO() { DialogId = dialogId, FromUserId = fromUser.Id, Text = Text, Date = DateTime.Now, Status = false }).Data;
            if (prevdm != null)
                if (prevdm.Date.Year == DateTime.Now.Year && prevdm.Date.Month == DateTime.Now.Month && prevdm.Date.Day == prevdm.Date.Day)
                    isToday = false;
            foreach (UserDTO uDTO in uid)
            {
                ConnectedUser cu = users.FirstOrDefault(us => us.userId == uDTO.Id);
                if (cu != null)
                {
                    if(uDTO.Id == fromUser.Id)
                    {
                        if (uid.Count() == 2)
                            Clients.Client(cu.ConnectionId).showMessageForCaller(
                                new
                                {
                                    messId = dm.Id,
                                    userId = fromUser.Id,
                                    UserProfImage = fromUser.ProfileImage.FilePath,
                                    FromUserName = fromUser.FirstName + " " + fromUser.LastName,
                                    dialogId = dDTO.Id,
                                    DialProfImage = dDTO.ProfileImage.FilePath,
                                    DialogName = fromUser.FirstName + " " + fromUser.LastName,
                                    Date = dm.Date.ToString("HH:mm"),
                                    DateSec = TimeSpan.FromTicks(dm.Date.Ticks).TotalSeconds,
                                    Text = Text,
                                    isToday = isToday
                                });
                        else
                            Clients.Client(cu.ConnectionId).showMessageForCaller(
                                new
                                {
                                    messId = dm.Id,
                                    userId = fromUser.Id,
                                    UserProfImage = fromUser.ProfileImage.FilePath,
                                    FromUserName = fromUser.FirstName + " " + fromUser.LastName,
                                    dialogId = dDTO.Id,
                                    DialProfImage = dDTO.ProfileImage.FilePath,
                                    DialogName = dDTO.Name,
                                    Date = dm.Date.ToString("HH:mm"),
                                    DateSec = TimeSpan.FromTicks(dm.Date.Ticks).TotalSeconds,
                                    Text = Text,
                                    isToday = isToday
                                });
                    }
                    else
                    {
                        if (uid.Count() == 2)
                            Clients.Client(cu.ConnectionId).showMessage(
                                new
                                {
                                    messId = dm.Id,
                                    userId = fromUser.Id,
                                    UserProfImage = fromUser.ProfileImage.FilePath,
                                    FromUserName = fromUser.FirstName + " " + fromUser.LastName,
                                    dialogId = dDTO.Id,
                                    DialProfImage = dDTO.ProfileImage.FilePath,
                                    DialogName = fromUser.FirstName + " " + fromUser.LastName,
                                    Date = dm.Date.ToString("HH:mm"),
                                    DateSec = TimeSpan.FromTicks(dm.Date.Ticks).TotalSeconds,
                                    Text = Text,
                                    isToday = isToday
                                });
                        else
                            Clients.Client(cu.ConnectionId).showMessage(
                                new
                                {
                                    messId = dm.Id,
                                    userId = fromUser.Id,
                                    UserProfImage = fromUser.ProfileImage.FilePath,
                                    FromUserName = fromUser.FirstName + " " + fromUser.LastName,
                                    dialogId = dDTO.Id,
                                    DialProfImage = dDTO.ProfileImage.FilePath,
                                    DialogName = dDTO.Name,
                                    Date = dm.Date.ToString("HH:mm"),
                                    DateSec = TimeSpan.FromTicks(dm.Date.Ticks).TotalSeconds,
                                    Text = Text,
                                    isToday = isToday
                                });
                    }
                }
            }
        }
        public void ReadMessages(int dialogId)
        {
            UserDTO owner;
            UserDTO reader = Helper.GetUser(Context.User.Identity.Name);
            DialogDTO dDTO = messService.GetDialog(dialogId, reader.Id).Data;
            DialogMessageDTO lastM;
            if (dDTO.LastMessInDialog!=null)
            {
                lastM = dDTO.LastMessInDialog;
                owner = lastM.FromUser;

                ConnectedUser cuR = users.FirstOrDefault(us => us.userId == reader.Id);
                ConnectedUser cuO = users.FirstOrDefault(us => us.userId == owner.Id);
                if (cuR != null && cuO != null)
                {
                    if (messService.DialogNewMessCount(dialogId, reader.Id).Data != 0)
                    {
                        messService.ReadMessages(dialogId, reader.Id);
                        Clients.Client(cuR.ConnectionId).showReadMessages(cuR.ConnectionId, cuO.ConnectionId);
                        Clients.Client(cuO.ConnectionId).showReadMessages(cuR.ConnectionId, cuO.ConnectionId);
                    }
                }
            }


            //foreach (UserDTO uDTO in messService.GetUsersInDialog(dDTO.Id).Data.Where(u => u.Id == reader.Id || u.Id == owner.Id))
            //{
            //    ConnectedUser cu = users.FirstOrDefault(us => us.userId == uDTO.Id);
            //    if (cu != null)
            //    {
            //        if(messService.DialogNewMessCount(dialogId,reader.Id).Data != 0)
            //        {
            //            messService.ReadMessages(dialogId, uDTO.Id);
            //            Clients.Client(cu.ConnectionId).showReadMessages();
            //        }
            //    }
            //}




            //int id = Helper.GetUser(Context.User.Identity.Name).Data.Id;
            //if (messService.DialogNewMessCount(dialogId, id).Data!=0)
            //{
            //    messService.ReadMessages(dialogId, id);
            //    Clients.All.showReadMessages();
            //}
        }
        //public void UserInDialog(string readerConnId, string ownerConnId)
        //{
        //    Clients.Client(cuR.ConnectionId).readCallBack(cuR.ConnectionId, cuO.ConnectionId);
        //}
        //==============================================//

        //=============Online=============//
        private void RefreshOnline(object obj)
        {
            ConnectedUser cu;
            for (int i = 0; i < users.Count; i++)
            {
                cu = users.ElementAt(i);
                if (DateTime.Now - cu.LastActivity > new TimeSpan(0, 0, _SEC))
                {
                    if(cu.userId!=-1) onlineService.UserOffline(cu.userId);
                    Clients.All.usersOffline(cu.userId);
                }
            }
        }
        //================================//

        public override Task OnConnected()
        {
            int userId;
            if(Context.User.Identity.IsAuthenticated)
            {
                userId = Helper.GetUser(Context.User.Identity.Name).Id;
                if (!users.Any(x => x.ConnectionId == Context.ConnectionId))
                    users.Add(new ConnectedUser() { ConnectionId = Context.ConnectionId, userId = userId, LastActivity = DateTime.Now });

                //====================Online====================//
                onlineService.UserOnline(userId);
                Clients.All.usersOnline(userId);
                //==============================================//
            }
            else
            {
                if (!users.Any(x => x.ConnectionId == Context.ConnectionId))
                    users.Add(new ConnectedUser() { ConnectionId = Context.ConnectionId, userId = -1, LastActivity = DateTime.Now });
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                //_lastDiscUser = item.userId;
                users.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }

    }

    //=====Online=====//
    class ConnectedUser
    {
        public string ConnectionId { get; set; }
        public int userId { get; set; }
        public DateTime LastActivity { get; set; }
    }
}