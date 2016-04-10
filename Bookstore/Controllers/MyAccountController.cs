using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using AutoMapper;
using XMLDatabase.Models;
using Bookstore.Models;
using ElectroShopMobile.CustomAttributes;
using Bookstore.CustomPrincipalNamespace;
using System.Web.Script.Serialization;
using System.Web.Security;

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
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.Username = user.Username;
                    serializeModel.Role = user.Role;
                    serializeModel.Firstname = user.Firstname;
                    serializeModel.Lastname = user.Lastname;

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string userData = serializer.Serialize(serializeModel);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        user.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false,
                        userData
                        );

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Books", null);
                }
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
