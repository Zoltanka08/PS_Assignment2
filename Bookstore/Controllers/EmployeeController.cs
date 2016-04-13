using AutoMapper;
using Bookstore.Models;
using ElectroShopMobile.CustomAttributes;
using Services.CustomExceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XMLDatabase.Models;

namespace Bookstore.Controllers
{
    [UserAuthorize(Roles = "admin")]
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        private IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public ActionResult Index()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); });
            IMapper mapper = config.CreateMapper();

            IEnumerable<User> users = employeeService.GetAll().Where(u => u.Role.Equals("employee"));
            IEnumerable<UserViewModel> userModels = mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
            return View(userModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); });
            IMapper mapper = config.CreateMapper();

            User employee = mapper.Map<UserViewModel, User>(model);
            employee.Role = "employee";

            try
            {
                employeeService.Insert(employee);
            }
            catch(DatabaseException)
            {
                ModelState.AddModelError("CustomError","User cannot be inserted!");
                return View(model);
            }

            return RedirectToAction("Index", "Employee", null);
        }

        public ActionResult Edit(int id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); });
            IMapper mapper = config.CreateMapper();

            User employee = employeeService.GetById(id);
            UserViewModel employeeModel = mapper.Map<User, UserViewModel>(employee);

            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); });
            IMapper mapper = config.CreateMapper();

            User employee = mapper.Map<UserViewModel, User>(model);
            employee.Role = "employee";

            try
            {
                employeeService.Update(employee);
            }
            catch(DatabaseException)
            {
                ModelState.AddModelError("CustomError", "Edit has been failed!");
                return View(model);
            }

            return RedirectToAction("Index", "Employee", null);
        }


       [UserAuthorize(Roles = "admin")]
       public ActionResult Delete(int id)
       {
           try
           {
               employeeService.Delete(id);
           }
           catch(DatabaseException)
           {
               Response.Write(@"<SCRIPT LANGUAGE=""JavaScript"">alert('" + "Delete fas been failed!" + "')</SCRIPT>");
           }
           return RedirectToAction("Index","Employee",null);
       }

    }
}
