using MVC_Prices2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Prices2.Controllers
{
    [Authorize]
    [Authorize(Roles = "ProlineAdmin")]
    public class PriceController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PriceMatrix(int? id = 0)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                
                PriceMatrixModel matrix = new PriceMatrixModel();
                var product = db.Products.FirstOrDefault(a => a.Id == id);              
                matrix.MaxHeight = product.MaxHeight;
                matrix.MaxWidth = product.MaxWidth;
                matrix.MinHeight = product.MinHeight;
                matrix.MinWidth = product.MinWidth;
                matrix.productID = product.Id;
                var activityControl = db.Prices.FirstOrDefault(p => p.Activity && p.Product.Id == id);
                if(activityControl==null)
                {
                    return View(matrix);
                }
                else
                {
                    matrix.priceListWhite = db.Prices.Where(p => p.Activity == true && p.Product.Id == id && p.Color==false).ToList();
                    matrix.priceListColored = db.Prices.Where(p => p.Activity == true && p.Product.Id == id && p.Color == true).ToList();
                }
                
                return View(matrix);
            }
        }
        [HttpPost]
        
        public ActionResult PriceMatrix(List<ListPricesModel> listPrices)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {

                var a = listPrices.First(p=>p.productID!=0);
                List<Price> price = db.Prices.Where(p => p.Product.Id == a.productID && p.Color == a.renk&&p.Activity==true).ToList();
                foreach (var item in price)
                {
                    item.Activity = false;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                foreach(var item in listPrices)
                {
                    if(item.productID!=0)
                    {
                        Price addPrice = new Price() /*{ Activity=true,Color=item.renk,}*/;
                        addPrice.Activity = true;
                        addPrice.Color = item.renk;
                        addPrice.Height = item.boy;
                        addPrice.Width = item.en;
                        addPrice.ProductId = item.productID;
                        addPrice.Prices = item.fiyat;
                        db.Prices.Add(addPrice);
                        db.SaveChanges();
                    }
                    
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult GetLedges ()
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                LedgesViewModel ledgesModel = new LedgesViewModel();
                var ledge = db.Ledges.Where(p => p.Activity == true).ToList();

                List<SelectListItem> values = (from i in ledge.ToList()

                                               select new SelectListItem
                                               {

                                                   Text = i.Model + "/" + i.Colorless + "-" + i.ColorlessPriceFactor + "€/" + i.Colored + "-" + i.ColoredPriceFactor + "€",
                                                   Value = i.PicValue

                                               }
                                               ).ToList();
                ViewBag.vls = values;
                return View(ledgesModel);
            }
        }
        [HttpPost]
        [ActionName("GetLedges")]
        public ActionResult GetLedgesPost(LedgesViewModel viewModel)
        {
            using (PriceDataModel2 db = new PriceDataModel2())
            {
                if (viewModel.coloredPriceFactor !=0 || viewModel.colorlessPriceFactor!=0)
                {
                    Ledge ledge = db.Ledges.Where(p => p.Activity == true && p.PicValue == viewModel.system).FirstOrDefault();
                    Ledge newLedgePrice = new Ledge();
                    if (viewModel.colorlessPriceFactor != 0 &&viewModel.coloredPriceFactor==0)
                    {
                        newLedgePrice.Activity = ledge.Activity;
                        newLedgePrice.Colored = ledge.Colored;
                        newLedgePrice.ColoredPriceFactor = ledge.ColoredPriceFactor;
                        newLedgePrice.Colorless = ledge.Colorless;
                        newLedgePrice.ColorlessPriceFactor = viewModel.colorlessPriceFactor;
                        newLedgePrice.Model = ledge.Model;
                        newLedgePrice.PicValue = ledge.PicValue;
                        db.Ledges.Add(newLedgePrice);
                        db.SaveChanges();
                    }
                    if (viewModel.coloredPriceFactor != 0 && viewModel.colorlessPriceFactor == 0)
                    {
                        newLedgePrice.Activity = ledge.Activity;
                        newLedgePrice.Colored = ledge.Colored;
                        newLedgePrice.ColoredPriceFactor = viewModel.coloredPriceFactor;
                        newLedgePrice.Colorless = ledge.Colorless;
                        newLedgePrice.ColorlessPriceFactor = ledge.ColorlessPriceFactor;
                        newLedgePrice.Model = ledge.Model;
                        newLedgePrice.PicValue = ledge.PicValue;
                        db.Ledges.Add(newLedgePrice);
                        db.SaveChanges();
                    }
                    if(viewModel.coloredPriceFactor != 0 && viewModel.colorlessPriceFactor != 0)
                    {
                        newLedgePrice.Activity = ledge.Activity;
                        newLedgePrice.Colored = ledge.Colored;
                        newLedgePrice.ColoredPriceFactor = viewModel.coloredPriceFactor;
                        newLedgePrice.Colorless = ledge.Colorless;
                        newLedgePrice.ColorlessPriceFactor = viewModel.colorlessPriceFactor;
                        newLedgePrice.Model = ledge.Model;
                        newLedgePrice.PicValue = ledge.PicValue;
                        db.Ledges.Add(newLedgePrice);
                        db.SaveChanges();
                    }

                    ledge.Activity = false;
                    db.Entry(ledge).State = EntityState.Modified;
                    db.SaveChanges();

                    
                }
                return RedirectToAction("GetLedges", "Price");
            }
        }
    }
}