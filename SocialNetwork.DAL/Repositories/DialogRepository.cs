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
    class DialogRepository : IRepository<Dialog>
    {
        private dbContext db;

        public DialogRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Dialog> GetAll()
        {
            return db.Dialogs;
        }

        public Dialog Get(int id)
        {
            return db.Dialogs.Find(id);
        }

        public void Create(Dialog user)
        {
            db.Dialogs.Add(user);
        }

        public void Update(Dialog user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Dialog user = db.Dialogs.Find(id);
            if (user != null)
                db.Dialogs.Remove(user);
        }
    }
}
