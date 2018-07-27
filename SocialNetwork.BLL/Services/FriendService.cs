using AutoMapper;
using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Infrastructure;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class FriendService : IFriendService
    {
        IMapper Mapper;
        IUnitOfWork db;
        IHelpService Helper;
        public FriendService(IUnitOfWork uow, IHelpService h)
        {
            Mapper = AutoMapperBLLConfig.Mapper;
            db = uow;
            Helper = h;
        }

        public ServiceResult<IEnumerable<UserDTO>> FindFriends()
        {
            return new ServiceResult<IEnumerable<UserDTO>>(Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(db.Users.GetAll()), null);
        }

        public ServiceResult<IEnumerable<string>> GetFriendsCities(int userId)
        {
            User user = db.Users.Get(userId);
            IEnumerable<Friends> UsersFrinds;
            List<string> Cities;
            if (user != null)
            {
                Cities = new List<string>();
                UsersFrinds = db.Friends.GetAll().Where(f => (f.FromUserId == userId || f.ToUserId == userId) && f.Status == 1);

                foreach (Friends fr in UsersFrinds)
                {
                    if (fr.FromUserId == userId)
                    {
                        Cities.Add(fr.ToUser.City);
                    }
                    else if (fr.ToUserId == userId)
                    {  
                        Cities.Add(fr.FromUser.City);
                    }
                }
                return new ServiceResult<IEnumerable<string>>(Cities, null);
            }
            else
            {
                return new ServiceResult<IEnumerable<string>>(null, "", "Пользователь не найден");
            }
        }

        public ServiceResult<IEnumerable<UserDTO>> SearchInFriends(int userId, string query, string city, int ageFr, int ageTo, bool? sex)
        {
            User friend;
            User user = db.Users.Get(userId);
            IEnumerable<Friends> UsersFrinds;
            List<UserDTO> Friends;
            if (user != null)
            {
                Friends = new List<UserDTO>();
                UsersFrinds = db.Friends.GetAll().Where(f => (f.FromUserId == userId || f.ToUserId == userId) && f.Status == 1);

                foreach (Friends fr in UsersFrinds)
                {
                    if (fr.FromUserId == userId)
                    {
                        friend = fr.ToUser;
                        if(sex!=null)
                        {
                            if (Regex.IsMatch((friend.FirstName + " " + friend.LastName).ToLower(), query.ToLower()) &&
                                friend.City == city && (friend.Age >= ageFr && friend.Age <= ageTo) && friend.Sex == sex)
                                Friends.Add(Mapper.Map<User, UserDTO>(friend));
                        }
                        else
                        {
                            if (Regex.IsMatch((friend.FirstName + " " + friend.LastName).ToLower(), query.ToLower()) &&
                                friend.City == city && (friend.Age >= ageFr && friend.Age <= ageTo))
                                Friends.Add(Mapper.Map<User, UserDTO>(friend));
                        }
                    }
                    else if (fr.ToUserId == userId)
                    {
                        friend = fr.FromUser;
                        if (sex != null)
                        {
                            if (Regex.IsMatch((friend.FirstName + " " + friend.LastName).ToLower(), query.ToLower()) &&
                                friend.City == city && (friend.Age >= ageFr && friend.Age <= ageTo) && friend.Sex == sex)
                                Friends.Add(Mapper.Map<User, UserDTO>(friend));
                        }
                        else
                        {
                            if (Regex.IsMatch((friend.FirstName + " " + friend.LastName).ToLower(), query.ToLower()) &&
                                friend.City == city && (friend.Age >= ageFr && friend.Age <= ageTo))
                                Friends.Add(Mapper.Map<User, UserDTO>(friend));
                        }
                    }
                }
                return new ServiceResult<IEnumerable<UserDTO>>(Friends, null);
            }
            else
            {
                return new ServiceResult<IEnumerable<UserDTO>>(null, "", "Пользователь не найден");
            }

        }

        public ServiceResult<int> GetFriendsCount(int userId)
        {
            User user = db.Users.Get(userId);
            if (user != null)
            {
                return new ServiceResult<int>(db.Friends.GetAll().Where(f => (f.FromUserId == userId || f.ToUserId == userId) && f.Status == 1).Count(), null);
            }
            else
            {
                return new ServiceResult<int>(-1, "", "Пользователь не найден");
            }
        }

        public ServiceResult<int> GetOnlineFriendsCount(int userId)
        {
            int count = 0;
            User user = db.Users.Get(userId);
            if (user != null)
            {
                foreach (Friends fr in db.Friends.GetAll().Where(f => (f.FromUserId == userId || f.ToUserId == userId) && f.Status == 1))
                {
                    if (fr.FromUserId == userId && fr.ToUser.Online)
                    {
                        count++;
                    }
                    else if (fr.ToUserId == userId && fr.FromUser.Online)
                    {
                        count++;
                    }
                }
                return new ServiceResult<int>(count, null);
            }
            else
            {
                return new ServiceResult<int>(-1, "", "Пользователь не найден");
            }
        }

        public ServiceResult<IEnumerable<UserDTO>> GetFriends(int userId)
        {
            User user = db.Users.Get(userId);
            IEnumerable<Friends> UsersFrinds;
            List<UserDTO> Friends;
            if (user != null)
            {
                Friends = new List<UserDTO>();
                UsersFrinds = db.Friends.GetAll().Where(f => (f.FromUserId == userId || f.ToUserId == userId) && f.Status == 1);

                foreach (Friends fr in UsersFrinds)
                {
                    if (fr.FromUserId == userId)
                    {
                        Friends.Add(Mapper.Map<User, UserDTO>(fr.ToUser));
                    }
                    else if (fr.ToUserId == userId)
                    {
                        Friends.Add(Mapper.Map<User, UserDTO>(fr.FromUser));
                    }
                }
                return new ServiceResult<IEnumerable<UserDTO>>(Friends, null);
            }
            else
            {
                return new ServiceResult<IEnumerable<UserDTO>>(null, "", "Пользователь не найден");
            }
        }

        public ServiceResult<IEnumerable<UserDTO>> GetOnlineFriends(int userId)
        {
            User user = db.Users.Get(userId);
            IEnumerable<Friends> UsersFrinds;
            List<UserDTO> onlineFrinds;
            if (user != null)
            {
                onlineFrinds = new List<UserDTO>();
                UsersFrinds = db.Friends.GetAll().Where(f => (f.FromUserId == userId || f.ToUserId == userId) && f.Status == 1);

                foreach (Friends fr in UsersFrinds)
                {
                    if (fr.FromUserId == userId && fr.ToUser.Online)
                    {
                        onlineFrinds.Add(Mapper.Map<User, UserDTO>(fr.ToUser));
                    }
                    else if (fr.ToUserId == userId && fr.FromUser.Online)
                    {
                        onlineFrinds.Add(Mapper.Map<User, UserDTO>(fr.FromUser));
                    }
                }
                return new ServiceResult<IEnumerable<UserDTO>>(onlineFrinds, null);
            }
            else
            {
                return new ServiceResult<IEnumerable<UserDTO>>(null, "", "Пользователь не найден");
            }
        }

        public ServiceResult<IEnumerable<UserDTO>> GetIncomingRequests(int userId)
        {
            List<UserDTO> users = new List<UserDTO>();
            db.Friends.GetAll().Where(f => f.ToUserId == userId && f.Status == 0).ToList().ForEach(f => users.Add(Mapper.Map<User,UserDTO>(f.FromUser)));
            return new ServiceResult<IEnumerable<UserDTO>>(users, null);
        }

        public ServiceResult<IEnumerable<UserDTO>> GetOutboundRequests(int userId)
        {
            List<UserDTO> users = new List<UserDTO>();
            db.Friends.GetAll().Where(f => f.FromUserId == userId && f.Status == 0).ToList().ForEach(f => users.Add(Mapper.Map<User, UserDTO>(f.ToUser)));
            return new ServiceResult<IEnumerable<UserDTO>>(users, null);
        }

        public void SendFriendRequest(int From, int To)
        {
            Friends Bunch = db.Friends.GetAll().FirstOrDefault(f => f.FromUserId == To && f.ToUserId == From);

            if (Bunch != null)
            {
                Bunch.Status = 1;
                db.Save();
            }
            else
            {
                db.Friends.Create(new Friends() { FromUserId = From, ToUserId = To, Status = 0 });
                db.Save();
            }
        }

        public void AcceptFriendRequest(int To, int From)
        {
            db.Friends.GetAll().FirstOrDefault(f => f.FromUserId == From && f.ToUserId == To).Status = 1;
            db.Save();
        }

        public void RemoveSubscriber(int ownerId, int userId)
        {
            db.Friends.Delete(db.Friends.GetAll().FirstOrDefault(f => f.FromUserId == userId && f.ToUserId == ownerId && f.Status == 0).Id);
            db.Save();
        }

        public void Unsubscribe(int userId, int ownerId)
        {
            db.Friends.Delete(db.Friends.GetAll().FirstOrDefault(f => f.FromUserId == userId && f.ToUserId == ownerId && f.Status == 0).Id);
            db.Save();
        }

        public void RemoveFriend(int OwnerId, int FriendId)
        {
            db.Friends.GetAll().FirstOrDefault(f => ((f.FromUserId == OwnerId && f.ToUserId == FriendId) || (f.FromUserId == FriendId && f.ToUserId == OwnerId)) && f.Status == 1).Status = 0;
            db.Save();
        }

        //  1 - друзья
        //  0 - подписчик
    }
}
