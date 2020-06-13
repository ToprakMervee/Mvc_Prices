using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Prices2.Controllers
{
    public class UserInfoUpdateController : Controller
    {
        // GET: UserInfoUpdate
        private UserManager<AppUser> userManager;

        public UserInfoUpdateController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
        }
        public ActionResult Index()
        {
            InfoUpdateModel updateModel = new InfoUpdateModel();
            string id = User.Identity.GetUserId();
            var info = userManager.FindById(id);
            updateModel.eMail = info.Email;
            updateModel.fullName = info.FullName;
            updateModel.phoneNumber = info.PhoneNumber;
            updateModel.userName = info.UserName;
            return View(updateModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index (InfoUpdateModel model)
        {
            string id = User.Identity.GetUserId();
            var info = userManager.FindById(id);
            info.FullName = model.fullName;
            info.Email = model.eMail;
            info.PhoneNumber = model.phoneNumber;
            userManager.Update(info);
            if(model.newPassword!=null && model.oldPassword!=null)
            {
                userManager.ChangePassword(id, model.oldPassword, model.newPassword);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}