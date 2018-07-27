using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Infrastructure;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Data.Entity.Infrastructure;
using AutoMapper;

namespace SocialNetwork.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork db;
        public AccountService(IUnitOfWork uow)
        {
            db = uow;
        }

        public ServiceResult<LoginDTO> LoginUser(LoginDTO logDTO)
        {
            int pass = logDTO.Password.GetHashCode();
            if (db.Users.GetAll().Any(u => u.Email == logDTO.Login && u.HashPassword == pass))
            {          
                FormsAuthentication.SetAuthCookie(logDTO.Login, createPersistentCookie: false);
                return new ServiceResult<LoginDTO>(null,null);
            }
            else
            {
                return new ServiceResult<LoginDTO>(null, string.Empty, "Неправильный логин или пароль");
            }
        }

        public ServiceResult<RegistrationDTO> RegisterUser(RegistrationDTO regDTO)
        {
            if (!db.Users.GetAll().Any(u => u.Email == regDTO.Email))
            {
                FormsAuthentication.SetAuthCookie(regDTO.Email, createPersistentCookie: false);
                db.Users.Create(new User() { FirstName = regDTO.FirstName, LastName = regDTO.LastName, Email = regDTO.Email, HashPassword = regDTO.Password.GetHashCode(), RoleId = 2, ProfileImageId = 1 });
                db.Save();
                return new ServiceResult<RegistrationDTO>(null, null);
            }
            else
            {
                return new ServiceResult<RegistrationDTO>(null,"Email", "Пользователь с таким адресом электронной почты уже зарегистрирован");
            }
        }

    }
}