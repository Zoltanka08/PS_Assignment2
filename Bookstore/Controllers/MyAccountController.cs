using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using AutoMapper;
using XMLDatabase.Models;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class MyAccountController : Controller
    {
        //
        // GET: /MyAccount/

        private IEmployeeService userService;

        public MyAccountController(IEmployeeService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userService.GetByUsername(model.UserName);
                if (user != null && user.Password.Equals(model.Password))
                {
                    var inUser = User;
                    return RedirectToAction("Index", "Books", null);
                }
                    
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        
        public ActionResult Index()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); });
            IMapper mapper = config.CreateMapper();

            IEnumerable<User> users = userService.GetAll();
            IEnumerable<UserViewModel> userModels = mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
            return View(userModels);
        }

    }
}
