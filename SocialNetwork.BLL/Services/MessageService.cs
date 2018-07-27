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
    public class MessageService : IMessageService
    {
        IMapper Mapper;
        IUnitOfWork db;
        IHelpService Helper;
        public MessageService(IUnitOfWork uow, IHelpService h)
        {
            Mapper = AutoMapperBLLConfig.Mapper;
            db = uow;
            Helper = h;
        }

        public ServiceResult<IEnumerable<DialogMessageDTO>> SearchInDialog(int dialogId, string query)
        {
            List<DialogMessageDTO> dms = new List<DialogMessageDTO>();
            foreach(DialogMessageDTO dm in Mapper.Map<IEnumerable<DialogMessage>, IEnumerable<DialogMessageDTO>>(db.DialogMessages.GetAll().Where(dm => dm.DialogId == dialogId)))
            {
                if (Regex.IsMatch(dm.Text.ToLower(), query.ToLower())) dms.Add(dm);
            }
            return new ServiceResult<IEnumerable<DialogMessageDTO>>(dms, null);
        }

        public ServiceResult<SearchResult<DialogDTO>> SearchInDialogs(int userId, string query)
        {
            UserDTO userDTO;
            DialogDTO dialogDTO;
            DialogMessageDTO dmDTO;
            List<DialogDTO> ByDialogName = new List<DialogDTO>();
            List<DialogDTO> ByMessage = new List<DialogDTO>();

            foreach (UserInDialog uid in db.UsersInDialog.GetAll().Where(u => u.MemberId == userId))
            {
                dialogDTO = Mapper.Map<Dialog, DialogDTO>(db.Dialogs.Get(uid.DialogId));
                if (dialogDTO.Name == "")
                {
                    userDTO = Mapper.Map<User, UserDTO>(db.Users.Get(GetCompanion(dialogDTO.Id, userId).Data.Id));
                    dialogDTO.Name = userDTO.FirstName + " " + userDTO.LastName;
                    dialogDTO.ProfileImage = userDTO.ProfileImage;
                }

                if (Regex.IsMatch(dialogDTO.Name.ToLower(), query.ToLower()))
                {
                    if (GetLastMessInDialog(dialogDTO.Id).Data != null)
                    {
                        dialogDTO.LastMessInDialog = GetLastMessInDialog(dialogDTO.Id).Data;
                        ByDialogName.Add(dialogDTO);
                    }
                }
                foreach (DialogMessage dm in db.DialogMessages.GetAll().Where(dm => dm.DialogId == dialogDTO.Id))
                {
                    dmDTO = Mapper.Map<DialogMessage, DialogMessageDTO>(dm);
                    if (Regex.IsMatch(dm.Text.ToLower(), query.ToLower()))
                    {
                        dialogDTO.LastMessInDialog = dmDTO;
                        if (dialogDTO.Name == "")
                        {
                            userDTO = Mapper.Map<User, UserDTO>(db.Users.Get(GetCompanion(dialogDTO.Id, userId).Data.Id));
                            dialogDTO.Name = userDTO.FirstName + " " + userDTO.LastName;
                            dialogDTO.ProfileImage = userDTO.ProfileImage;
                        }
                        ByMessage.Add(dialogDTO);
                        break;
                    }
                }
            }
            if(ByDialogName.Count == 0 && ByMessage.Count == 0) return new ServiceResult<SearchResult<DialogDTO>>(null, "", "Не найдено диалогов по такому запросу.");
            else if (ByDialogName.Count==0) return new ServiceResult<SearchResult<DialogDTO>>(new SearchResult<DialogDTO>(null,ByMessage), null);
            else if (ByMessage.Count == 0) return new ServiceResult<SearchResult<DialogDTO>>(new SearchResult<DialogDTO>(ByDialogName, null), null);
            return new ServiceResult<SearchResult<DialogDTO>>(new SearchResult<DialogDTO>(ByDialogName, ByMessage), null);

        }

        public ServiceResult<DialogMessageDTO> GetMessage(int messId)
        {
            DialogMessage dm = db.DialogMessages.Get(messId);
            if (dm != null)
            {
                return new ServiceResult<DialogMessageDTO>(Mapper.Map<DialogMessage, DialogMessageDTO>(dm), null);
            }
            else return new ServiceResult<DialogMessageDTO>(null, "", "Такого сообщения не существует");       
        }

        public ServiceResult<DialogDTO> GetDialog(int dialogId, int userId)
        {
            UserDTO uDTO;
            DialogDTO dialogDTO = Mapper.Map<Dialog,DialogDTO>(db.Dialogs.Get(dialogId));

            if (GetLastMessInDialog(dialogDTO.Id).Data != null)
                dialogDTO.LastMessInDialog = GetLastMessInDialog(dialogDTO.Id).Data;

            if (dialogDTO.Name == "")
            {
                uDTO = Mapper.Map<User,UserDTO>(db.Users.Get(GetCompanion(dialogId, userId).Data.Id));
                dialogDTO.ProfileImage = uDTO.ProfileImage;
                dialogDTO.Name = uDTO.FirstName + " " + uDTO.LastName;
                return new ServiceResult<DialogDTO>(dialogDTO, null);
            }
            else
            {
                return new ServiceResult<DialogDTO>(dialogDTO, null);
            }
        }

        public ServiceResult<IEnumerable<DialogDTO>> GetDialogs(int userId)
        {
            UserDTO userDTO;
            DialogDTO dialogDTO;
            List<DialogDTO> dlDTO = new List<DialogDTO>();

            foreach (UserInDialog uid in db.UsersInDialog.GetAll().Where(u => u.MemberId == userId))
            {
                dialogDTO = Mapper.Map<Dialog, DialogDTO>(db.Dialogs.Get(uid.DialogId));

                if (GetLastMessInDialog(dialogDTO.Id).Data == null) continue;

                dialogDTO.LastMessInDialog = GetLastMessInDialog(dialogDTO.Id).Data;
                if (dialogDTO.Name == "")
                {
                    userDTO = Mapper.Map<User, UserDTO>(db.Users.Get(GetCompanion(dialogDTO.Id, userId).Data.Id));
                    dialogDTO.Name = userDTO.FirstName + " " + userDTO.LastName;
                    dialogDTO.ProfileImage = userDTO.ProfileImage;
                }
                dlDTO.Add(dialogDTO);
            }
            return new ServiceResult<IEnumerable<DialogDTO>>(dlDTO.OrderByDescending(d=>d.LastMessInDialog.Date), null);
        }

        public ServiceResult<IEnumerable<UserDTO>> GetUsersInDialog(int dialogId)
        {
            List<User> users = new List<User>();
            db.UsersInDialog.GetAll().Where(uid => uid.DialogId == dialogId).ToList().ForEach(u => users.Add(u.Member));
            return new ServiceResult<IEnumerable<UserDTO>>(Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users), null);
        }

        public ServiceResult<IEnumerable<DialogMessageDTO>> GetDialogMessages(int dialogId)
        {
            return new ServiceResult<IEnumerable<DialogMessageDTO>>(Mapper.Map <IEnumerable<DialogMessage>, IEnumerable < DialogMessageDTO >> (db.DialogMessages.GetAll().Where(dm => dm.DialogId == dialogId)), null);
        }

        public ServiceResult<IEnumerable<DialogMessageDTO>> GetPartOfMessages(int dialogId, int part, int amount)
        {
            List<DialogMessageDTO> dmsL = new List<DialogMessageDTO>();
            IEnumerable<DialogMessageDTO> dms = Mapper.Map<IEnumerable<DialogMessage>, IEnumerable<DialogMessageDTO>>(db.DialogMessages.GetAll().Where(d => d.DialogId == dialogId));
            int to = dms.Count() - ((part - 1) * (amount - 1));
            for (int i = to-amount; i < to; i++)
            {
                if (i >= 0) dmsL.Add(dms.ElementAt(i));
            }
            return new ServiceResult<IEnumerable<DialogMessageDTO>>(dmsL, null);
        }

        public ServiceResult<double> GetMessPartsCount(int dialogId, int amount)
        {
            IEnumerable<DialogMessage> dms = db.DialogMessages.GetAll().Where(d => d.DialogId == dialogId);
            double tmp = Math.Ceiling((double)dms.Count()/amount);
            return new ServiceResult<double>(Math.Ceiling(tmp), null);
        }

        public ServiceResult<IEnumerable<DialogDTO>> GetUnreadDialogs(int userId)
        {
            UserDTO userDTO;
            DialogDTO dialogDTO;
            List<DialogDTO> dlDTO = new List<DialogDTO>();
            IEnumerable<WhoWillRead> whwr = db.UsersWhoWillRead.GetAll().Where(uWWR => uWWR.UserId == userId);
            foreach (WhoWillRead u in whwr)
            {
                dialogDTO = Mapper.Map<Dialog, DialogDTO>(db.DialogMessages.Get(u.MessageId).Dialog);

                if (GetLastMessInDialog(dialogDTO.Id).Data == null) continue;

                dialogDTO.LastMessInDialog = GetLastMessInDialog(dialogDTO.Id).Data;
                if (dialogDTO.Name == "")
                {
                    userDTO = Mapper.Map<User, UserDTO>(db.Users.Get(GetCompanion(dialogDTO.Id, userId).Data.Id));
                    dialogDTO.Name = userDTO.FirstName + " " + userDTO.LastName;
                    dialogDTO.ProfileImage = userDTO.ProfileImage;
                }
                if(!dlDTO.Any(d=>d.Id == dialogDTO.Id)) dlDTO.Add(dialogDTO);
            }
            return new ServiceResult<IEnumerable<DialogDTO>>(dlDTO, null);
        }

        public ServiceResult<int> StartDialog(int FirstMember, int SecondMember)
        {
            int id;
            IEnumerable<UserInDialog> d = db.UsersInDialog.GetAll().Where(u => (u.MemberId == FirstMember || u.MemberId == SecondMember) && u.Dialog.Name == "");
            foreach (UserInDialog uid in d)
            {
                id = GetCompanion(uid.DialogId, uid.MemberId).Data.Id;
                if (id == SecondMember || id == FirstMember)
                    return new ServiceResult<int>(uid.DialogId, null);
            }

            Dialog newD = new Dialog() { Name = "", ProfileImageId=1 };
            db.Dialogs.Create(newD);
            db.UsersInDialog.Create(new UserInDialog() { DialogId = newD.Id, MemberId = FirstMember});
            db.UsersInDialog.Create(new UserInDialog() { DialogId = newD.Id, MemberId = SecondMember });
            db.Save();
            return new ServiceResult<int>(newD.Id, null);
        }

        public ServiceResult<int> StartDialog(string name, int[] membersId)
        {
            Dialog newD = new Dialog() { Name = name };
            db.Dialogs.Create(newD);
            foreach(int uId in membersId)
            {
                db.UsersInDialog.Create(new UserInDialog() { DialogId = newD.Id, MemberId = uId });
            }
            db.Save();
            return new ServiceResult<int>(newD.Id, null);
        }

        public void AddToDialog(int dialogId, int userId)
        {
            db.UsersInDialog.Create(new UserInDialog() { DialogId = dialogId, MemberId = userId });
            db.Save();
        }

        public ServiceResult<DialogMessageDTO> SendMessage(DialogMessageDTO messDTO)
        {
            DialogMessage dm = new DialogMessage() { DialogId = messDTO.DialogId, FromUserId = messDTO.FromUserId, Text = messDTO.Text, Date = messDTO.Date, Status = false };
            db.DialogMessages.Create(dm);
            foreach (UserInDialog uid in db.UsersInDialog.GetAll().Where(u => u.Member.Id != messDTO.FromUserId && u.DialogId == messDTO.DialogId))
            {
                db.UsersWhoWillRead.Create(new WhoWillRead() { MessageId = dm.Id, UserId = uid.MemberId });
            }
            db.Save();
            return new ServiceResult<DialogMessageDTO>(Mapper.Map<DialogMessage,DialogMessageDTO>(db.DialogMessages.GetAll().Last()),null);
        }

        public ServiceResult<int> NewMessCount(int userId)
        {
            return new ServiceResult<int>(db.UsersWhoWillRead.GetAll().Where(uWWR => uWWR.UserId == userId).Count(),null);
        }

        public ServiceResult<int> DialogNewMessCount(int dialogId, int userId)
        {
            int k = 0;
            foreach(DialogMessage dm in db.DialogMessages.GetAll().Where(dm => dm.DialogId == dialogId))
            {
                if (db.UsersWhoWillRead.GetAll().Any(uWWR => uWWR.UserId == userId && uWWR.MessageId == dm.Id)) k++;
            }
            return new ServiceResult<int>(k, null);
        }

        public ServiceResult<DialogMessageDTO> GetLastMessInDialog(int dialogId)
        {
            IEnumerable<DialogMessage> dms = db.DialogMessages.GetAll().Where(dm => dm.DialogId == dialogId);
            if (dms.Count() != 0)
            {
                return new ServiceResult<DialogMessageDTO>(Mapper.Map<DialogMessage, DialogMessageDTO>(dms.OrderByDescending(d => d.Date).First()), null);
            }
            else return new ServiceResult<DialogMessageDTO>(null, "", "Сообщений не найдено");
            //DialogMessage lastDM = new DialogMessage() { Date = DateTime.MinValue };
            //IEnumerable<DialogMessage> dms = db.DialogMessages.Find(dm => dm.DialogId == dialogId);
            //if (dms.Count()!=0)
            //{
            //    foreach (DialogMessage dm in dms)
            //        if (dm.Date > lastDM.Date)
            //            lastDM = dm;
            //    return new ServiceResult<DialogMessageDTO>(Mapper.Map<DialogMessage, DialogMessageDTO>(lastDM), null);
            //}
            //else return new ServiceResult<DialogMessageDTO>(null,"", "Сообщений не найдено");     
        }

        public void ReadMessages(int dialogId, int userId)
        {
            WhoWillRead wwr;
            IEnumerable<DialogMessage> dms = db.DialogMessages.GetAll().Where(dm => dm.DialogId == dialogId && dm.FromUserId != userId && dm.UsersWhoWillRead.Count!=0);
            foreach (DialogMessage dm in dms)
            {
                wwr = db.UsersWhoWillRead.GetAll().FirstOrDefault(uWWR => uWWR.UserId == userId && uWWR.MessageId == dm.Id);
                if(wwr!=null)
                {
                    db.UsersWhoWillRead.Delete(wwr.Id);
                    dm.Status = true;
                }
            }
            db.Save();
        }

        public ServiceResult<bool> IsReadByUser(int messId, int userId)
        {
            return new ServiceResult<bool>(!db.UsersWhoWillRead.GetAll().Any(uWWR => uWWR.MessageId == messId && uWWR.UserId == userId), null);
        }

        public ServiceResult<bool> IsReadBySomebody(int messId)
        {
            return new ServiceResult<bool>(db.DialogMessages.Get(messId).Status, null);
        }

        public ServiceResult<bool> IsUserInDialog(int dialogId, int userId)
        {
            return new ServiceResult<bool>(db.UsersInDialog.GetAll().Any(uid => uid.DialogId == dialogId && uid.MemberId == userId),null);
        }

        public void RemoveMessage(int messId)
        {
            db.DialogMessages.Delete(messId);
            db.Save();
        }

        public void RemoveDialog(int dialogId)
        {
            db.Dialogs.Delete(dialogId);
            db.Save();
        }

        public ServiceResult<UserDTO> GetCompanion(int dialogId, int userId)
        {
            UserInDialog UserInD;
            IEnumerable<UserInDialog> UsersInD;
            UsersInD = db.UsersInDialog.GetAll().Where(u => u.DialogId == dialogId);
            UserInD = UsersInD.FirstOrDefault(u => u.MemberId != userId);
            return new ServiceResult<UserDTO>(Mapper.Map<User,UserDTO>(UserInD.Member), null);
        }
    }
}
