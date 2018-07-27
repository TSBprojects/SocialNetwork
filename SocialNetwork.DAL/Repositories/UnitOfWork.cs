using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.EntityFramework;
using SocialNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private dbContext db;
        private ImageRepository imgRep;
        private RoleRepository roleRep;
        private UserRepository userRep;
        private FriendsRepository FrReqRep;
        private DialogRepository DialogRep;
        private DialogMessageRepository DialogMessRep;
        private UserInDialogRepository UsInDRep;
        private WhoWillReadRepository WWRRep;

        public UnitOfWork(string connectionString)
        {
            db = new dbContext(connectionString);
        }

        public IRepository<Image> Images
        {
            get
            {
                if (imgRep == null)
                    imgRep = new ImageRepository(db);
                return imgRep;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRep == null)
                    roleRep = new RoleRepository(db);
                return roleRep;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRep == null)
                    userRep = new UserRepository(db);
                return userRep;
            }
        }

        public IRepository<Friends> Friends
        {
            get
            {
                if (FrReqRep == null)
                    FrReqRep = new FriendsRepository(db);
                return FrReqRep;
            }
        }

        public IRepository<Dialog> Dialogs
        {
            get
            {
                if (DialogRep == null)
                    DialogRep = new DialogRepository(db);
                return DialogRep;
            }
        }

        public IRepository<DialogMessage> DialogMessages
        {
            get
            {
                if (DialogMessRep == null)
                    DialogMessRep = new DialogMessageRepository(db);
                return DialogMessRep;
            }
        }

        public IRepository<UserInDialog> UsersInDialog
        {
            get
            {
                if (UsInDRep == null)
                    UsInDRep = new UserInDialogRepository(db);
                return UsInDRep;
            }
        }

        public IRepository<WhoWillRead> UsersWhoWillRead
        {
            get
            {
                if (WWRRep == null)
                    WWRRep = new WhoWillReadRepository(db);
                return WWRRep;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
