using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.BLL.Services;
using System.Web.Security;
using AutoMapper;

namespace SocialNetwork.BLL.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        private string connectionString;

        public NinjectDependencyResolver(string connectionString)
        {
            this.connectionString = connectionString;
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //.InSingletonScope()
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IFriendService>().To<FriendService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IOnlineService>().To<OnlineService>();
            kernel.Bind<IHelpService>().To<HelpService>();
        }
    }
}

/*
    public class NinjectServiceModule : NinjectModule
    {
        private string connectionString;
        public NinjectServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope().WithConstructorArgument(connectionString);
        }
    }
*/


