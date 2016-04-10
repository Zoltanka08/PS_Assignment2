using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookstore.DependencyInjection;
using Bookstore.Controllers;
using Services.Interfaces;
using Services.Services;
using ElectroShopMobile.CustomAttributes;

namespace Bookstore.App_Start
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            ContainerBootstrap.RegisterTypes(container);
            container.
                RegisterType<BooksController>().
                RegisterType<IBookService, BookService>();
            container.
                RegisterType<MyAccountController>().
                RegisterType<IEmployeeService, EmployeeService>();
            container.
                RegisterType<UserAuthorizeAttribute>().
                RegisterType<IEmployeeService,EmployeeService>();
        }
         
    }
}