using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;
using MVC_Prices2.ViewModels;

namespace MVC_Prices2.Controllers
{
    [Authorize]

    public class OrderController : Controller
    {
        private UserManager<AppUser> userManager;

        public OrderController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Offers()
        {

            return View();
        }


        public ActionResult GetOffers()
        {
            string user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                try
                {
                    var offers = db.OfferMas.Where(a => a.User == user && !a.IsDeleted && a.Status == 1 && a.IsActive)
                        .ToList<OfferMas>();

                    return Json(offers, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return View(e.Message);
                }
            }
        }

        public ActionResult OfferDetail(int id = 0)
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                List<BasketView> list = db.OfferDet
                    .Where(a => a.OfferMas.User == user && a.OfferMas.Status == 1 && a.OfferMas.ID == id && a.OfferMas.IsActive && !a.OfferMas.IsDeleted).Select(x => new BasketView()
                    {
                        ID = x.ID,
                        ProductName = x.Product.ProductName,
                        PicUrl = x.Product.PicUrl,
                        ProductDetail = x.Product.ProductDetail,
                        System = x.System,
                        ProductId = x.ProductId,
                        Color = x.ColorName,
                        Direction = x.Direction,
                        Height = x.Height,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        ColorUrl = x.Colors.ColorUrl,
                        Width = x.Width,
                        Reference = x.OfferMas.ReferenceNo,
                        Date = x.OfferMas.Date,
                        Glass = db.Glass.Where(a => a.GlassType == x.GlassQnt).ToList(),
                        MasId = x.BasketMas_ID,
                        DoorHandle = x.DoorHandle,
                        UpOpenning = x.Description,
                        GlassType = x.GlassQnt == "0" ? "doppio Ug 1.1" : "triplo Ug 1.5",
                        GlassQnt = x.GlassQnt,
                        Note = x.Note,
                        LatoD = x.LatoD,
                        Extra = x.Extra,
                        Status = x.OfferMas.Status,
                        Exp1 = x.OfferMas.Exp1,
                        Exp2 = x.OfferMas.Exp2

                    }).OrderBy(a=> a.ID).ToList();
                foreach (var el in list)
                {
                    int[] ids = new[] { 1, 2, 3, 10, 11 };
                    string prof = el.System == "2" ? "81" : "70";
                    string wing = ids.Contains(el.ProductId) ? "1w" : "2w";
                    string glass = el.GlassQnt == "0" ? "2g" : "3g";
                    string prodName = prof + wing + glass;

                    var ugValue = db.Prices.FirstOrDefault(a => a.Product.PType == "UG" && a.Product.ProductName == prodName && el.Width > a.Width - 100 && el.Width <= a.Width && el.Height > a.Height - 100 && el.Height <= a.Height && a.Activity);
                    el.RemainSeconds = Convert.ToDouble(ugValue.Prices);
                }
                return View(list);
            }
        }

        //[HttpPost]
        //public JsonResult GetTempOffer(int id = 0)
        //{
        //    var user = User.Identity.GetUserId();
        //    using (PriceDataModel2 db = new PriceDataModel2())
        //    {
        //        List<BasketView> list = db.OfferDet
        //            .Where(a => a.OfferMas.User == user && a.OfferMas.Status == 1).Select(x => new BasketView()
        //            {
        //                ID = x.ID,
        //                ProductName = x.Product.ProductName,
        //                PicUrl = x.Product.PicUrl,
        //                ProductDetail = x.Product.ProductDetail,
        //                System = x.System,
        //                ProductId = x.ProductId,
        //                Color = x.ColorName,
        //                Direction = x.Direction,
        //                Height = x.Height,
        //                Price = x.Price,
        //                Quantity = x.Quantity,
        //                ColorUrl = x.Colors.ColorUrl,
        //                Width = x.Width,
        //                Reference = x.OfferMas.ReferenceNo,
        //                Date = x.OfferMas.Date,
        //                Glass = db.Glass.Where(a => a.GlassType == x.GlassQnt).ToList(),
        //                ArmType = x.ArmType,
        //                MasId = x.BasketMas_ID,
        //                DoorHandle = x.DoorHandle,
        //                LatchArm = x.LatchArm,
        //                Description = x.Description,
        //                GlassType = x.GlassQnt == "0" ? "doppio, trasparente" : x.GlassQnt == "1" ? "doppio, satinato" : "triplo, trasparente",
        //                GlassQnt = x.GlassQnt,
        //                Note = x.Note,
        //                LatoD = x.LatoD,
        //                Extra = x.Extra

        //            }).OrderBy(a => a.ID).ToList();

        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public JsonResult ToOrder(int id = 0)
        {
            var user = User.Identity.GetUserId();
            var userCard = userManager.Users.FirstOrDefault(a => a.Id == user);
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var offerMas = db.OfferMas.FirstOrDefault(a => a.ID == id && a.Store.Id == userCard.StoreId);
                if (offerMas != null)
                {
                    var lastindex = db.Reference.FirstOrDefault(c => c.StoreId == userCard.StoreId && c.Type=="Order")  ;
                    if (lastindex == null)
                    {

                        Reference lastindex1 = new Reference();
                        lastindex1.StoreId = userCard.StoreId;
                        lastindex1.LastReference = 1;
                        lastindex1.Type = "Order";
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
                    var store = db.Stores.FirstOrDefault(a => a.Id == userCard.StoreId);
                    string lastindex2 = "000000" + lastindex.LastReference;
                    offerMas.ReferenceNo = "OLM" + store.StoreCode + lastindex2.Substring(lastindex2.Length - 5, 5);
                    offerMas.Status = 2;
                    offerMas.OrderDate=DateTime.Now;
                    db.SaveChanges();
                }
            }

            return Json(new { sucess = true });
        }

        [HttpPost]
        public JsonResult SaveTempOffer(List<BasketView> tempData)
        {
            if (tempData == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            int id = 0;
            var first = tempData.First();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var offerMas = db.OfferMas.FirstOrDefault(a => a.ID == first.MasId);
                if (offerMas != null)
                {
                    offerMas.IsActive = false;
                    db.SaveChanges();
                    offerMas.IsActive = true;
                    offerMas.RevisionId += 1;
                    db.OfferMas.Add(offerMas);
                    db.SaveChanges();
                    id = offerMas.ID;
                }

                List<OfferDet> offerDetList = new List<OfferDet>();
                foreach (var el in tempData)
                {
                    OfferDet offerDet = new OfferDet()
                    {
                        
                        ColorName = el.Color,
                        Direction = el.Direction,
                        Height = el.Height,
                        OfferMas = offerMas,
                        Price = el.Price,
                        ProductId = el.ProductId,
                        Quantity = el.Quantity,
                        System = el.System,
                        Width = el.Width
                    };
                    offerDetList.Add(offerDet);
                }

                db.OfferDet.AddRange(offerDetList);
                db.SaveChanges();
            }

            return Json(new { success = true, masId = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DelOffer(string Id)
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {

                try
                {
                    if (Id.Contains("LM"))
                    {
                        var offerMas = db.OfferMas.FirstOrDefault(b =>
                            b.User == user && b.ReferenceNo == Id && !b.IsDeleted);
                        if (offerMas != null)
                        {
                            offerMas.IsDeleted = true;
                            offerMas.IsActive = false;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        int id = Convert.ToInt32(Id);
                        var offerDet = db.OfferDet.FirstOrDefault(a => a.ID == id);
                        int masId = offerDet.BasketMas_ID;
                        db.OfferDet.Remove(offerDet);
                        db.SaveChanges();
                        if (!db.OfferDet.Any(c => c.BasketMas_ID == masId))
                        {
                            var offerMas = db.OfferMas.FirstOrDefault(b => b.ID == masId);
                            db.OfferMas.Remove(offerMas);
                            db.SaveChanges();
                        }
                    }

                }
                catch (Exception e)
                {
                    return Json(new { success = e.Message });
                }
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult GetRevisions(string refno)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var offerMasList = db.OfferMas.Where(a => a.ReferenceNo == refno && !a.IsActive).ToList();
                return Json(offerMasList, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetRevisionDets(int refno)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                string user = User.Identity.GetUserId();
                List<BasketView> offerDetList = db.OfferDet
                    .Where(a => a.OfferMas.ID == refno).Select(x => new BasketView()
                    {
                        ID = x.ID,
                        ProductName = x.Product.ProductName,
                        PicUrl = x.Product.PicUrl,
                        ProductDetail = x.Product.ProductDetail,
                        System = x.System,
                        ProductId = x.ProductId,
                        Color = x.ColorName,
                        Direction = x.Direction,
                        Height = x.Height,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        ColorUrl = x.Colors.ColorUrl,
                        Width = x.Width,
                        Reference = x.OfferMas.ReferenceNo,
                        Date = x.OfferMas.Date,
                        Glass = db.Glass.Where(a => a.GlassType == x.GlassQnt).ToList(),
                        MasId = x.BasketMas_ID,
                        DoorHandle = x.DoorHandle,
                        UpOpenning = x.Description,
                        GlassType = x.GlassQnt == "0" ? "doppio, trasparente" : x.GlassQnt == "1" ? "doppio, satinato" : "triplo, trasparente",
                        GlassQnt = x.GlassQnt,
                        Note = x.Note,
                        LatoD = x.LatoD,
                        Extra = x.Extra

                    }).OrderBy(a => a.ID).ToList();
                return Json(offerDetList, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult ReturnToRevision(int refno)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var turnedOne = db.OfferMas.FirstOrDefault(a => a.ID == refno);
                if (turnedOne != null)
                {
                    var activeOne = db.OfferMas.FirstOrDefault(a => a.ReferenceNo == turnedOne.ReferenceNo && a.IsActive);
                    if (activeOne != null)
                    {
                        activeOne.IsActive = false;
                        turnedOne.IsActive = true;
                        db.SaveChanges();
                    }
                }
            }
            return Json(new { success = true, id = refno });
        }

        public ActionResult Orders()
        {
         
            return View();
        }
        [NonAction]
        public double GetDayDiff(DateTime dt)
        { 
            
            var seconds = Math.Abs((DateTime.Now-dt).Days);
            return seconds;
        }
        public JsonResult GetOrders()
        {
            List<OrderViewModel> orders=new List<OrderViewModel>();
            using (PriceDataModel2 db=new PriceDataModel2())
            {
                

                string user = User.Identity.GetUserId();
                var userCard = userManager.Users.FirstOrDefault(a => a.Id == user);
                orders = db.OfferMas
                    .Where(a => a.Status==2 && a.Store.Id==userCard.StoreId &&a.IsActive && !a.IsDeleted).Select(x => new OrderViewModel()
                    {
                        ID = x.ID,
                        ReferenceNo = x.ReferenceNo,
                        Date = x.Date,
                        Exp1 = x.Exp1,
                        Exp2 = x.Exp2,
                        OrderDate = x.OrderDate.Value,
                        OrderDet = x.OfferDet.Select(y=> new OrderDetViewModel()
                        {
                            Color = y.ColorName,
                            Direction = y.Direction,
                            Height = y.Height,
                            Quantity = y.Quantity,
                            ProductName = y.Product.ProductName,
                            Price = y.Price,
                            ColorUrl = y.Colors.ColorUrl,
                            PicUrl = y.Product.PicUrl,
                            ProductDetail = y.Product.ProductDetail,
                            ProductId = y.ProductId,
                            MasId = y.BasketMas_ID,
                            System = y.System,
                            Width = y.Width

                        }).ToList()
                      

                    }).OrderBy(a => a.ID).ToList();
            }

            foreach (var el in orders)
            {
                el.RemainSeconds = GetDayDiff(el.OrderDate.Value);
                el.DateDiff = el.RemainSeconds < 1;
            }
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReturnToOffer(int id = 0)
        {
            using (PriceDataModel2 db=new PriceDataModel2())
            {
                var order = db.OfferMas.FirstOrDefault(a => a.ID == id);
                if (order!=null)
                {
                    order.Status = 1;
                    db.SaveChanges();
                }
            }
            return Json(new {sucess = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteOrder(int id = 0)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var order = db.OfferMas.FirstOrDefault(a => a.ID == id);
                if (order != null)
                {
                    order.IsDeleted = true;
                    
                    db.SaveChanges();
                }
            }
            return Json(new { sucess = true }, JsonRequestBehavior.AllowGet);
        }

    }
}