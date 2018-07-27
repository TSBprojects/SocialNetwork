using AutoMapper;
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

namespace SocialNetwork.BLL.Services
{
    public class HelpService : IHelpService
    {
        IMapper Mapper;
        IUnitOfWork db;
        public HelpService(IUnitOfWork uow)
        {
            db = uow;
            Mapper = AutoMapperBLLConfig.Mapper;
        }

        public UserDTO GetUser(string Email)
        {
            return Mapper.Map<User, UserDTO>(db.Users.GetAll().FirstOrDefault(u => u.Email == Email));
        }
        public UserDTO GetUser(int id)
        {
            return Mapper.Map<User, UserDTO>(db.Users.Get(id));
        }
    }
}
