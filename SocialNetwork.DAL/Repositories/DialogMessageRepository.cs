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
    class DialogMessageRepository : IRepository<DialogMessage>
    {
        private dbContext db;

        public DialogMessageRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<DialogMessage> GetAll()
        {
            return db.DialogMessages;
        }

        public DialogMessage Get(int id)
        {
            return db.DialogMessages.Find(id);
        }

        public void Create(DialogMessage user)
        {
            db.DialogMessages.Add(user);
        }

        public void Update(DialogMessage user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DialogMessage user = db.DialogMessages.Find(id);
            if (user != null)
                db.DialogMessages.Remove(user);
        }
    }
}
