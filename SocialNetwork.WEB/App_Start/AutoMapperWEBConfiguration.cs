using AutoMapper;
using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.App_Start
{
    public class AutoMapperWEBConfiguration
    {
        public static IMapper Mapper;
        public static void RegisterMappings()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginModel, LoginDTO>();
                cfg.CreateMap<RegistrationModel, RegistrationDTO>();
                cfg.CreateMap<DialogMessageModel, DialogMessageDTO>();
            }).CreateMapper();
        }
    }
}