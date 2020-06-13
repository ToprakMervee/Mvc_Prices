using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;

namespace MVC_Prices2.Controllers
{
    public class StoreController : Controller
    {
        private UserManager<AppUser> userManager;

        public StoreController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
        }
        // GET: Store
        public ActionResult Index()
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var stores = db.Stores.ToList();
                return View(stores);
            }
        }

        [HttpPost]
        public ActionResult Add(Store store)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var stores = db.Stores.ToList();
                bool isSaved = false;
                if (!stores.Any(a => a.StoreName == store.StoreName))
                {
                    try
                    {
                        store.isActive = true;
                        db.Stores.Add(store);
                        db.SaveChanges();
                        isSaved = true;
                    }
                    catch (Exception e)
                    {
                        var error = e.Message;
                    }

                }
                return Json(new { success = isSaved });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id=0)
        {
            string result="";
            bool result2 = true;
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                
                var store = db.Stores.FirstOrDefault(a => a.Id == id);
                if (store!=null)
                {
                    var count = userManager.Users.Count(a => a.StoreId == id);
                    if (count>0)
                    {
                        result = "There are users in this store!";
                        result2 = false;
                    }
                    else
                    {
                        db.Stores.Remove(store);
                        db.SaveChanges();
                        result = "Success";
                    }
                }
            }

            return Json(new {success= result2, acresult=result},JsonRequestBehavior.AllowGet);
        }
    }
}