using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces
{
    public interface IMessageService
    {
        ServiceResult<IEnumerable<DialogMessageDTO>> SearchInDialog(int dialogId, string query);
        ServiceResult<double> GetMessPartsCount(int dialogId, int amount);
        ServiceResult<IEnumerable<DialogMessageDTO>> GetPartOfMessages(int dialogId, int part, int amount);
        ServiceResult<SearchResult<DialogDTO>> SearchInDialogs(int userId, string query);
        ServiceResult<DialogMessageDTO> GetMessage(int messId);
        ServiceResult<DialogDTO> GetDialog(int dialogId, int userId);
        ServiceResult<IEnumerable<DialogDTO>> GetDialogs(int userId);
        ServiceResult<IEnumerable<UserDTO>> GetUsersInDialog(int dialogId);
        ServiceResult<IEnumerable<DialogMessageDTO>> GetDialogMessages(int dialogId);
        ServiceResult<IEnumerable<DialogDTO>> GetUnreadDialogs(int userId);
        ServiceResult<DialogMessageDTO> GetLastMessInDialog(int dialogId);
        ServiceResult<int> NewMessCount(int userId);
        ServiceResult<int> DialogNewMessCount(int dialogId, int userId);
        ServiceResult<DialogMessageDTO> SendMessage(DialogMessageDTO messDTO);
        ServiceResult<int> StartDialog(int FirstMember, int SecondMember);
        ServiceResult<int> StartDialog(string name, int[] membersId);
        ServiceResult<UserDTO> GetCompanion(int dialogId, int userId);
        void AddToDialog(int dialogId, int userId);
        void ReadMessages(int dialogId, int userId);
        ServiceResult<bool> IsReadByUser(int messId, int userId);
        ServiceResult<bool> IsReadBySomebody(int messId);
        ServiceResult<bool> IsUserInDialog(int dialogId, int userId);
        void RemoveMessage(int messId);
        void RemoveDialog(int dialogId);
    }
}
