using SocialNetwork.BLL.Interfaces;
using SocialNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class OnlineService : IOnlineService
    {
        IUnitOfWork db;
        IHelpService Helper;
        public OnlineService(IUnitOfWork uow, IHelpService h)
        {
            db = uow;
            Helper = h;
        }

        public void UserOnline(int userId)
        {
            db.Users.Get(userId).Online = true;
            db.Save();
        }

        public void UserOffline(int userId)
        {
            db.Users.Get(userId).Online = false;
            db.Save();
        }






        //private static Timer Timer = new Timer(new TimerCallback(Refresh));
        //private static Dictionary<int, DateTime> UsersOnline = new Dictionary<int, DateTime>();

        //public static void Refresh(object obj)
        //{
        //    for (int i = 0; i < UsersOnline.Count; i++)
        //    {
        //        if (DateTime.Now - UsersOnline.ElementAt(i).Value > new TimeSpan(0, 3, 0))
        //        {
        //            UsersOnline.Remove(UsersOnline.ElementAt(i).Key);
        //            GlobalHost.ConnectionManager.GetHubContext<WorkHub>().Clients.All.refresh(GetAllOnlineUsers());
        //        }
        //    }
        //}
        //public static int[] GetAllOnlineUsers()
        //{
        //    int[] usersId = new int[UsersOnline.Count];
        //    for (int i = 0; i < UsersOnline.Count; i++) usersId[i] = UsersOnline.ElementAt(i).Key;
        //    return usersId;
        //}
        //public static bool IsUserOnline(int id)
        //{
        //    return UsersOnline.ContainsKey(id);
        //}
        //public static void UserOnline(int id)
        //{
        //    UsersOnline.Remove(id);
        //    UsersOnline.Add(id, DateTime.Now);
        //    GlobalHost.ConnectionManager.GetHubContext<WorkHub>().Clients.All.refresh(GetAllOnlineUsers());
        //}
        //public static void UserOffline(int id)
        //{
        //    UsersOnline.Remove(id);
        //    GlobalHost.ConnectionManager.GetHubContext<WorkHub>().Clients.All.refresh(GetAllOnlineUsers());
        //}
        //public static void StartTimer()
        //{
        //    Timer.Change(0, 60000);
        //}
    }
}
