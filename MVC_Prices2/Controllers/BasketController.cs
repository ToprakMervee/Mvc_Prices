
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Identity;
using MVC_Prices2.Models;
using MVC_Prices2.ViewModels;
using Newtonsoft.Json;

namespace MVC_Prices2.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private UserManager<AppUser> userManager;

        public BasketController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            userManager = new UserManager<AppUser>(userStore);
        }
        // GET: Basket
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                List<BasketView> list = db.OfferDet
                    .Where(a => a.OfferMas.User == user && a.OfferMas.Status == 0).Select(x => new BasketView()
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
                        ArmType = x.ArmType,
                        MasId = x.BasketMas_ID,
                        DoorHandle = x.DoorHandle,
                        LatchArm = x.LatchArm,
                        UpOpenning = x.UpOpenning,
                        GlassType = x.GlassQnt == "0" ? "doppio Ug 1.1" : "triplo Ug 0.7",
                        GlassQnt = x.GlassQnt,
                        Note = x.Note,
                        LatoD = x.LatoD,
                        Extra = x.Extra

                    }).OrderBy(a => a.ID).ToList();
                var json = JsonConvert.SerializeObject(list);
                ViewBag.json = json;
                return View(list);
            }
        }

        public ActionResult CountBasket()
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                int list = db.OfferDet.Where(a => a.OfferMas.User == user && a.OfferMas.Status == 0).ToList().Count();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetProduct(int id = 0)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var proddets = db.OfferDet
                    .Where(a => a.ID == id).Select(x => new BasketView()
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
                        Width = x.Width,
                        ColorUrl = x.Colors.ColorUrl,
                        Reference = x.OfferMas.ReferenceNo,
                        Date = x.OfferMas.Date,
                        IsFixed = x.Product.IsWingFixed,

                    }).ToList();
                var proddet = proddets.FirstOrDefault();

                return Json(proddet);
            }
        }

        [HttpPost]
        public ActionResult SaveNote(OfferExp expData)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var theNote = db.OfferDet.FirstOrDefault(a => a.ID == expData.Id);
                theNote.Note = expData.Exp1;
                db.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuantity(OfferDet oDet)
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                try
                {
                    var offerDet = db.OfferDet.FirstOrDefault(a => a.ID == oDet.ID && a.OfferMas.User == user);
                    offerDet.Quantity = oDet.Quantity;
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ToOffer(OfferExp expData)
        {
            string userId = User.Identity.GetUserId();
            bool result = true;
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                OfferMas basket =
                    db.OfferMas.FirstOrDefault(a => a.ReferenceNo == expData.Reference && a.Status == 0 && a.User == userId);

                if (basket == null)
                {
                    result = false;
                }
                else
                {
                    basket.Exp1 = expData.Exp1;
                    basket.Exp2 = expData.Exp2;
                    basket.Status = 1;
                    db.SaveChanges();
                }
            }

            return Json(new { success = result });
        }
        [HttpPost]
        public ActionResult DelProduct(int id = 0)
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {

                try
                {
                    if (id == -1)
                    {
                        var offerMas = db.OfferMas.FirstOrDefault(b => b.User == user && (b.Status == 0));
                        if (offerMas != null)
                        {
                            db.OfferMas.Remove(offerMas);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var offerDet = db.OfferDet.FirstOrDefault(a => a.ID == id && a.OfferMas.User == user);
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
        public ActionResult UpdateBasket(OfferDet offer)
        {
            if (offer == null) return Json(new { success = false });

            using (PriceDataModel2 db = new PriceDataModel2())
            {
                var offerdet = db.OfferDet.FirstOrDefault(x => x.ID == offer.ID);
                if (offerdet == null) return Json(new { success = false });
                offerdet.ColorName = offer.ColorName;
                offerdet.Height = offer.Height;
                offerdet.Width = offer.Width;
                offerdet.System = offer.System;
                offerdet.Quantity = offer.Quantity;
                offerdet.ProductId = db.OfferDet.FirstOrDefault(a => a.ID == offer.ID).ProductId;
                int systemId = Convert.ToInt32(offerdet.System == "Pro 7006 ®" ? "0" : offer.System == "Pro 7006 ® 2" ? "2" : "1");
                bool colorName = offerdet.ColorName != "bianco";
                var priceMatrix = db.Prices.Where(p => p.ProductId == offerdet.ProductId && p.Activity && p.Profil == systemId && p.Color == colorName).ToList();

                if (!priceMatrix.Any())
                {
                    offerdet.Price = 0;
                }
                foreach (var item in priceMatrix)
                {
                    if (offerdet.Width > item.Width - 100 && offerdet.Width <= item.Width && offerdet.Height > item.Height - 100 && offerdet.Height <= item.Height && item.Color == colorName)
                    {
                        offerdet.Price = item.Prices;
                        offerdet.Price = Math.Ceiling(offerdet.Price);
                        break;
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        public ActionResult PdfPage(int id)
        {
            var user = User.Identity.GetUserId();
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                List<BasketView> list = db.OfferDet
                    .Where(a => a.OfferMas.User == user && a.OfferMas.ID == id).Select(x => new BasketView()
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
                        ArmType = x.ArmType,
                        MasId = x.BasketMas_ID,
                        DoorHandle = x.DoorHandle,
                        LatchArm = x.LatchArm,
                        UpOpenning = x.UpOpenning,
                        GlassType = x.GlassQnt == "0" ? "doppio Ug 1.1" : "triplo Ug 0.7",
                        GlassQnt = x.GlassQnt,
                        Note = x.Note,
                        LatoD = x.LatoD,
                        Extra = x.Extra

                    }).OrderBy(a => a.ID).ToList();
                var json = JsonConvert.SerializeObject(list);
                ViewBag.json = json;
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult AddToBasket(OfferDet offer)
        {
            if (offer == null) return Json(new { success = false });

            using (PriceDataModel2 db = new PriceDataModel2())
            {
                OfferDet offerdet = db.OfferDet.FirstOrDefault(x => x.ID == offer.ID);

                if (offerdet == null) return Json(new { success = false });
                offerdet.ColorName = offer.ColorName;
                offerdet.Height = offer.Height;
                offerdet.Width = offer.Width;
                offerdet.System = offer.System;
                offerdet.Quantity = offer.Quantity;
                offerdet.ProductId = db.OfferDet.FirstOrDefault(a => a.ID == offer.ID).ProductId;
                int systemId = Convert.ToInt32(offerdet.System == "Pro 7006 ®" ? "0" : offer.System == "Pro 7006 ® 2" ? "2" : "1");
                bool colorName = offerdet.ColorName != "bianco";
                var priceMatrix = db.Prices.Where(p => p.ProductId == offerdet.ProductId && p.Activity && p.Profil == systemId && p.Color == colorName).ToList();

                foreach (var item in priceMatrix)
                {
                    if (offerdet.Width > item.Width - 100 && offerdet.Width <= item.Width && offerdet.Height > item.Height - 100 && offerdet.Height <= item.Height && item.Color == colorName)
                    {
                        offerdet.Price = item.Prices;
                        offerdet.Price = Math.Ceiling(offerdet.Price);
                    }
                } //////////////////3. profil çeşidi fiyat hesap alanı



                db.OfferDet.Add(offerdet);

                db.SaveChanges();
            }
            return Json(new { success = true });
        }


    }
}