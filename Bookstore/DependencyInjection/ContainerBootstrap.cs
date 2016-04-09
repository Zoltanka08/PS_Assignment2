using Microsoft.Practices.Unity;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XMLDatabase.DataAccessors;
using XMLDatabase.Interfaces;

namespace Bookstore.DependencyInjection
{
    public class ContainerBootstrap
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IBookService,BookService>();
            container.RegisterType<IBookDataAccessor, BookDataAccessor>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IUserDataAccessor, UserDataAccessor>();
        }
    }
}