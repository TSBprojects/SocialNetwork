using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.EntityFramework;
using SocialNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    class UserInDialogRepository : IRepository<UserInDialog>
    {
        private dbContext db;

        public UserInDialogRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<UserInDialog> GetAll()
        {
            return db.UsersInDialog;
        }

        public UserInDialog Get(int id)
        {
            return db.UsersInDialog.Find(id);
        }

        public void Create(UserInDialog user)
        {
            db.UsersInDialog.Add(user);
        }

        public void Update(UserInDialog user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserInDialog user = db.UsersInDialog.Find(id);
            if (user != null)
                db.UsersInDialog.Remove(user);
        }
    }
}
