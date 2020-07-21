using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;

namespace MVC_Prices2.Controllers
{
    [Authorize]
    [Authorize(Roles = "ProlineAdmin")]

    public class UsersController : Controller
    {
        private UserManager<AppUser> userManager;
        private object _isAuthenticated;
        private object _userRoleNames;
        private readonly RoleManager<IdentityRole> roleManager;
          
public object WebSecurity { get; private set; }

        public UsersController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));
        }
      

        public ActionResult Index()
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                ViewData["Stores"] = db.Stores.ToList();
                ViewData["Roles"] = roleManager.Roles.ToList();


                return View(userManager.Users);
            }
        }
      

        [HttpPost]
        public ActionResult Add(User user_1)
        {
            var user = new AppUser();
            user.StoreId = user_1.StoreId;
            user.Email = user_1.Email;
            user.UserName = user_1.UserName;
            user.IsActive = true;
            user.FullName = user_1.FullName;
            
          

            try
            {
                var result = userManager.Create(user, user_1.Password);
                result = userManager.AddToRole(user.Id, user_1.Role);
               

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

          
           
            return Json(new { success = true });
        }



        [HttpPost]
        public ActionResult Delete(AppUser _user)
        {
            bool sucess = false;
            var user = userManager.FindById(_user.Id);
            if (user != null && _user.Id != User.Identity.GetUserId())
            {
                user.IsActive = false;
                user.IsDeleted = true;
                sucess = true;
                userManager.Update(user);
                
            }
            return Json(new { success = sucess });
        }

        [HttpPost]
        public ActionResult Edit(User user_1)
        {
            var user = userManager.FindById(user_1.UserId);
            //var user = userManager.Users.FirstOrDefault(a => a.Id == user_1.UserId);
            if (user != null)
            {
                user.StoreId = user_1.StoreId;
                user.Email = user_1.Email;
                //if (user_1.UserName != user.UserName)
                //{
                //    user.UserName = user_1.UserName;
                //}
                user.FullName = user_1.FullName;
              
            }
            try
            {
                var result = userManager.Update(user);
                var role = roleManager.FindById(user.Id);
                if (role!=null)
                {
                    var roles =  userManager.GetRoles(user.Id);
                    userManager.RemoveFromRoles(user.Id, roles.ToArray());
                }
                var result2 = userManager.AddToRole(user.Id, user_1.Role);
            }
            catch (Exception e)
            {
               
            }

            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult ResetPass(User user)
        {

            return Json(new { success = true });
        }
    }
}