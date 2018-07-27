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
    public class WhoWillReadRepository : IRepository<WhoWillRead>
    {
        private dbContext db;

        public WhoWillReadRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<WhoWillRead> GetAll()
        {
            return db.UsersWhoWillRead;
        }

        public WhoWillRead Get(int id)
        {
            return db.UsersWhoWillRead.Find(id);
        }

        public void Create(WhoWillRead user)
        {
            db.UsersWhoWillRead.Add(user);
        }

        public void Update(WhoWillRead user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            WhoWillRead user = db.UsersWhoWillRead.Find(id);
            if (user != null)
                db.UsersWhoWillRead.Remove(user);
        }
    }
}
