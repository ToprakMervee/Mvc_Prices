using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;

namespace MVC_Prices.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private UserManager<AppUser> userManager;
        private PriceDataModel2 context =new PriceDataModel2();

        public ProductController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
        }
        public ActionResult Index(int? id = 0)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                
                var product = db.Products.FirstOrDefault(a => a.Id == id);
                ViewData["Glass"] = db.Glass.Where(a => a.Product.Id == id).ToList();
                
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Index(OfferDet basket)
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var basketMas = db.OfferMas.SingleOrDefault(a => a.User == user && a.IsActive && a.Status==0);
                if (basketMas == null)
                {
                    var userCard = userManager.Users.FirstOrDefault(a => a.Id == user);
                    var lastindex = db.Reference.FirstOrDefault(c => c.StoreId == userCard.StoreId);
                    if (lastindex==null)
                    {
                        
                        Reference lastindex1 = new Reference();
                        lastindex1.StoreId = userCard.StoreId;
                        lastindex1.LastReference = 1;
                        lastindex = lastindex1;
                        lastindex.LastReference += 1;
                        db.Reference.Add(lastindex1);
                        db.SaveChanges();
                    }
                    else
                    {
                        lastindex.LastReference += 1;
                        db.SaveChanges();
                    }

                    var store =db.Stores.FirstOrDefault(a => a.Id == userCard.StoreId);
                    OfferMas bmas = new OfferMas();
                    bmas.User = user;
                    bmas.Store = store;
                    bmas.IsActive = true;
                    bmas.IsDeleted = false;
                    bmas.RevisionId = 0;
                    bmas.Date = DateTime.Now;
                    
                    string lastindex2 = "000000" + lastindex.LastReference;
                    bmas.ReferenceNo = "LM" + store.StoreCode + lastindex2.Substring(lastindex2.Length - 6, 6);
                    
                    basket.OfferMas = bmas;
                    db.OfferDet.Add(basket);
                    db.SaveChanges();
                }
                else
                {
                    basket.BasketMas_ID = basketMas.ID;
                    basketMas.Date = DateTime.Now;
                    db.OfferDet.Add(basket);
                    db.SaveChanges();
                }

            }
            return Json(new { success = true });
        }

        public ActionResult Product(int? id = 0)
        {


            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var product = db.Products.SingleOrDefault(a => a.Id == id);

                return View(product);
            }
        }

        public ActionResult Products()
        {




            //if (user.IsAdmin != 1)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var product = db.Products.ToList();

                return View(product);
            }
        }

        public ActionResult GetProducts()
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var product = db.Products.ToList<Product>();

                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProduct()
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var prices = db.Prices.ToList<Price>();

                return View(prices);
            }
        }
        public JsonResult Datas(int? id = 0,int system=0)
        {

            PriceDataModel2 db = new PriceDataModel2();

            var prices = db.Prices.Where(p => p.ProductId == id && p.Activity == true && p.Profil==system).ToList();
            
            return Json(prices, JsonRequestBehavior.AllowGet);


        }
        public JsonResult Ledges()
        {
            PriceDataModel2 db = new PriceDataModel2();
            var ledges = db.Ledges.Where(p => p.Activity == true).ToList();
            return Json(ledges, JsonRequestBehavior.AllowGet);
        }
    }
}