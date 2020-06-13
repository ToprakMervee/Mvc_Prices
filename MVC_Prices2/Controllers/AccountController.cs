using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;

namespace MVC_Prices2.Controllers
{
    public class AccountController : Controller
    {
        public PriceDataModel2 db = new PriceDataModel2();

        private UserManager<AppUser> userManager;

        public AccountController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Login(LoginModel users)
        {   
            var user = userManager.Find(users.UserName, users.Password);
            if (user != null)
            {
                try
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");

                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };
                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    
                  
                    return RedirectToAction("Index", "Home");
                 

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);

                    throw;
                }


            }

            ModelState.AddModelError("", " Invalid username or password");
            return View();


        }

        public ActionResult Logout(User users)
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ForgotPass()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPass(ForgotModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var provider = new DpapiDataProtectionProvider("MVC_Prices2");
                userManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(
                    provider.Create("MVC_Prices2"));
                string code = await userManager.GeneratePasswordResetTokenAsync(user.Id);

                string message;
                var callbackUrl = Url.Action("ResetPass", "Account",
                    new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                message = "To reset your password please click <a href=\"" + callbackUrl + "\">here</a>";
                ModelState.AddModelError("", "Please check your email.");
                await SendEmail(user.Email, "Reset Your Password", message);
            }
            else
            {
                ModelState.AddModelError("", "There is no user signed with this e-mail. ");
            }
            return View();
        }

        [NonAction]
        public async Task SendEmail(string toEmailAddress, string emailSubject, string emailMessage)
        {
            var message = new MailMessage();
            message.From = new MailAddress("postmaster@prolinepvc.com", "Password Reset");
            message.To.Add(toEmailAddress);

            message.Subject = emailSubject;
            message.Body = emailMessage;
            message.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.yandex.com.tr"; //for yandex host
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("postmaster@prolinepvc.com", "fW345IONasdf234");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(message);
            }
        }
        [AllowAnonymous]

        public ActionResult ResetPass(string userId, string code)
        {
            if (String.IsNullOrEmpty(code) || String.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }


        }

        [HttpPost]
        public ActionResult ResetPass(ResetModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match!");
                return View();
            }
            else
            {
                var provider = new DpapiDataProtectionProvider("MVC_Prices2");
                userManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(
                    provider.Create("MVC_Prices2"));
                var result = userManager.ResetPasswordAsync(model.UserId, model.Code, model.Password);


                if (result.Result.Succeeded)
                {

                    return RedirectToAction("Login");
                }
                else
                {
                    string valMessage = "";

                    foreach (var el in result.Result.Errors)
                    {
                        valMessage += el + " ";
                    }
                    ModelState.AddModelError("", valMessage);
                    return View();
                }


            }

        }


    }
}