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
    class FriendsRepository : IRepository<Friends>
    {
        private dbContext db;

        public FriendsRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Friends> GetAll()
        {
            return db.Friends;
        }

        public Friends Get(int id)
        {
            return db.Friends.Find(id);
        }

        public void Create(Friends FrReq)
        {
            db.Friends.Add(FrReq);
        }

        public void Update(Friends FrReq)
        {
            db.Entry(FrReq).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Friends FrReq = db.Friends.Find(id);
            if (FrReq != null)
                db.Friends.Remove(FrReq);
        }
    }
}
