using AutoMapper;
using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SocialNetwork.BLL.Infrastructure
{
    public class AutoMapperBLLConfig
    {
        public static IMapper Mapper;
        public static void RegisterMappings()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dialog, DialogDTO>();
                cfg.CreateMap<DialogMessage, DialogMessageDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Image, ImageDTO>();
            }).CreateMapper();
        }
    }
}
