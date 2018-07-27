using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.EntityFramework
{
    public class dbContext : DbContext
    {
        static dbContext()
        {
            Database.SetInitializer(new MyDbInitializer());
        }
        public dbContext(string connectionString) : base(connectionString) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<DialogMessage> DialogMessages { get; set; }
        public DbSet<UserInDialog> UsersInDialog { get; set; }
        public DbSet<WhoWillRead> UsersWhoWillRead { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder
            //    .Entity<Dialog>()
            //    .HasOptional(u => u.ProfileImage)
            //    .WithOptionalPrincipal();

            modelBuilder.Entity<User>()
                .HasRequired(c => c.ProfileImage)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friends>()
                .HasRequired(c => c.ToUser)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friends>()
                .HasRequired(c => c.FromUser)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }

    public class MyDbInitializer : DropCreateDatabaseAlways<dbContext>
    {
        protected override void Seed(dbContext db)
        {
            db.Roles.Add(new Role() { Id = 1, Name = "Admin" });
            db.Roles.Add(new Role() { Id = 2, Name = "User" });
            db.Images.Add(new Image() { FilePath = "/Content/dbImages/default_1.jpg" });
            db.SaveChanges();
            db.Users.Add(new User() { FirstName = "Vasya", LastName = "Petrov", Email = "email@mail.ru", HashPassword = "123456".GetHashCode(), RoleId = 1, ProfileImageId = 1, City="Саратов", Age=21, Sex=true });
            db.Users.Add(new User() { FirstName = "Kolya", LastName = "Ageev", Email = "hello@mail.ru", HashPassword = "123456".GetHashCode(), RoleId = 2, ProfileImageId = 1, City = "Москва", Age = 21, Sex = true });
            db.Users.Add(new User() { FirstName = "Петя", LastName = "Жучкин", Email = "dom@mail.ru", HashPassword = "123456".GetHashCode(), RoleId = 2, ProfileImageId = 1, City = "Энгельс", Age = 21, Sex = true });
            db.Users.Add(new User() { FirstName = "50cent", LastName = "Дмитриевич", Email = "pop@mail.ru", HashPassword = "123456".GetHashCode(), RoleId = 2, ProfileImageId = 1, City = "Петербург", Age = 21, Sex = true });
            db.SaveChanges();
            db.Friends.Add(new Friends() { FromUserId = 2, ToUserId = 1, Status = 1 });
            db.Friends.Add(new Friends() { FromUserId = 3, ToUserId = 1, Status = 0 });
            db.Friends.Add(new Friends() { FromUserId = 4, ToUserId = 1, Status = 1 });
            db.Friends.Add(new Friends() { FromUserId = 3, ToUserId = 4, Status = 1 });
            db.Friends.Add(new Friends() { FromUserId = 2, ToUserId = 3, Status = 0 });
            db.Friends.Add(new Friends() { FromUserId = 4, ToUserId = 2, Status = 0 });
            db.SaveChanges();   
            db.Dialogs.Add(new Dialog() { Name = "Беседа1", ProfileImageId = 1 });
            db.Dialogs.Add(new Dialog() { Name = "", ProfileImageId = 1 });
            db.Dialogs.Add(new Dialog() { Name = "Беседа2", ProfileImageId = 1 });
            db.Dialogs.Add(new Dialog() { Name = "", ProfileImageId = 1 });
            db.SaveChanges();
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 1, MemberId = 1 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 1, MemberId = 3 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 1, MemberId = 4 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 2, MemberId = 4 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 2, MemberId = 1 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 3, MemberId = 3 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 3, MemberId = 2 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 3, MemberId = 4 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 4, MemberId = 4 });
            db.UsersInDialog.Add(new UserInDialog() { DialogId = 4, MemberId = 3 });
            db.SaveChanges();
            //for(int i=1;i<=1000;i++) db.DialogMessages.Add(new DialogMessage() { DialogId = 1, FromUserId = 1, Text = ""+i, Status = false, Date = new DateTime(2015, 7, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 1, Text = "Message1", Status = false, Date = new DateTime(2016, 6, 20, 18, 30, 26) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 4, Text = "Message2", Status = false, Date = new DateTime(2016, 7, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 4, FromUserId = 3, Text = "Message", Status = false, Date = new DateTime(2015, 7, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 1, Text = "Message3", Status = false, Date = new DateTime(2017, 4, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 3, FromUserId = 2, Text = "Message", Status = false, Date = new DateTime(2015, 7, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 4, FromUserId = 4, Text = "Message", Status = false, Date = new DateTime(2015, 7, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 1, Text = "Message4", Status = false, Date = new DateTime(2017, 4, 20, 18, 30, 26) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 1, Text = "Message5", Status = false, Date = new DateTime(2017, 5, 20, 18, 30, 26) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 1, Text = "Message6", Status = false, Date = new DateTime(2017, 6, 20, 18, 30, 26) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 2, FromUserId = 1, Text = "Message7", Status = false, Date = DateTime.Now });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 3, FromUserId = 3, Text = "Message", Status = false, Date = new DateTime(2015, 7, 20, 18, 30, 25) });
            //db.DialogMessages.Add(new DialogMessage() { DialogId = 4, FromUserId = 3, Text = "Message", Status = false, Date = new DateTime(2015, 7, 20, 18, 30, 25) });
            //db.SaveChanges();
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 1, UserId = 3 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 1, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 2, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 3, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 3, UserId = 3 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 4, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 5, UserId = 3 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 5, UserId = 2 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 5, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 6, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 6, UserId = 3 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 7, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 8, UserId = 3 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 8, UserId = 2 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 8, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 9, UserId = 4 });
            //db.UsersWhoWillRead.Add(new WhoWillRead() { MessageId = 9, UserId = 3 });
            db.SaveChanges();
        }
    }
}
