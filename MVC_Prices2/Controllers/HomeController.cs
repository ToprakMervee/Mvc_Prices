using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Prices2.Models;

namespace MVC_Prices.Controllers
{
   [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Product> products=new List<Product>();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                products = db.Products.OrderBy(a=> a.RowNumber).ToList();
            }
            return View(products);
        }

    }
}