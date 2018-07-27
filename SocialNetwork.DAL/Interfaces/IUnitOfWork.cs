using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Image> Images { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        IRepository<Friends> Friends { get; }
        IRepository<Dialog> Dialogs { get; }
        IRepository<DialogMessage> DialogMessages { get; }
        IRepository<UserInDialog> UsersInDialog { get; }
        IRepository<WhoWillRead> UsersWhoWillRead { get; }
        void Save();
    }
}
