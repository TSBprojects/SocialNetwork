using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces
{
    public interface IFriendService
    {
        ServiceResult<IEnumerable<UserDTO>> FindFriends();

        ServiceResult<IEnumerable<string>> GetFriendsCities(int userId);

        ServiceResult<IEnumerable<UserDTO>> SearchInFriends(int userId, string query, string city, int ageFr, int ageTo, bool? sex);

        ServiceResult<int> GetFriendsCount(int userId);

        ServiceResult<int> GetOnlineFriendsCount(int userId);

        ServiceResult<IEnumerable<UserDTO>> GetFriends(int userId);

        ServiceResult<IEnumerable<UserDTO>> GetOnlineFriends(int userId);

        ServiceResult<IEnumerable<UserDTO>> GetIncomingRequests(int userId);

        ServiceResult<IEnumerable<UserDTO>> GetOutboundRequests(int userId);

        void SendFriendRequest(int From, int To);

        void AcceptFriendRequest(int To, int From);

        void RemoveSubscriber(int ownerId, int userId);

        void Unsubscribe(int userId, int ownerId);

        void RemoveFriend(int OwnerId, int FriendId);
    }
}
