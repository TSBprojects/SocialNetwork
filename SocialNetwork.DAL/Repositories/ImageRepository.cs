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
    public class ImageRepository : IRepository<Image>
    {
        private dbContext db;

        public ImageRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return db.Images;
        }

        public Image Get(int id)
        {
            return db.Images.Find(id);
        }

        public void Create(Image img)
        {
            db.Images.Add(img);
        }

        public void Update(Image img)
        {
            db.Entry(img).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Image img = db.Images.Find(id);
            if (img != null)
                db.Images.Remove(img);
        }
    }
}
