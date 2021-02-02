using System.Web.Mvc;

using Unity.Mvc5;
using cleanArchitecture.Application.Interfaces;
using cleanArchitecture.Application.Services;
using cleanArchitecture.Infra.Data.Interfaces;
using cleanArchitecture.Infra.Data.Repositories;
using Unity;

namespace CRUD_cleanArchitecture_MVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRepository, UserRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}