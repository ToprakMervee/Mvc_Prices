using MVC_Prices2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Prices2.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Charts
        public ActionResult Index()
        {
            
            return View();
        }
        public JsonResult Infos()
        {
            PriceDataModel2 db = new PriceDataModel2();
            List<ChartModel> chartModels = new List<ChartModel>();
            var stores = db.Stores.Where(p=>p.isActive==true).ToList();
            var money = db.OfferDet.ToList();
            var orders = db.OfferMas.ToList();
            
            
            foreach(var item in stores)
            {
                ChartModel chart = new ChartModel();
                chart.storeName = item.StoreName;
                
                foreach(var ord in orders)
                {
                    if (ord.Store.Id == item.Id && ord.IsActive == true && ord.Status == 2) 
                    {
                        chart.storeOrder += 1;
                        foreach (var mon in money)
                        {
                            if (ord.ID == mon.BasketMas_ID)
                                chart.storeMoney = chart.storeMoney + (mon.Price * mon.Quantity);
                        }
                    }
                        
                }
                chartModels.Add(chart);
                
            }
            
            return Json(chartModels, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StoreChart()
        {
            PriceDataModel2 db = new PriceDataModel2();
            StoreModel storeModel = new StoreModel(); 
            var stores = db.Stores.Where(p=>p.isActive==true).ToList();
            List<SelectListItem> values = (from i in stores.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.StoreName,
                                               Value = i.StoreName
                                           }
                                           ).ToList();
            ViewBag.vls = values;
            return View(storeModel);
        }

        public JsonResult ChartDatas(string sName)
        {

            PriceDataModel2 db = new PriceDataModel2();
            List<ChartDataModel> chartModelList = new List<ChartDataModel>();
            var store = db.Stores.FirstOrDefault(p => p.StoreName == sName && p.isActive==true);
            var orders = db.OfferMas.Where(p => p.Store.Id == store.Id );
            var offers = db.OfferDet.ToList();
            for(int i = 1;i<=12;i++)
            {
                ChartDataModel chartData = new ChartDataModel();
                foreach(var ord in orders)
                {
                    if(ord.Status == 2&&ord.OrderDate.Value.Month==i&&ord.OrderDate.Value.Year==2020 )
                    {
                        chartData.totalOrder += 1;
                        foreach(var of in offers)
                        {
                            if(of.BasketMas_ID==ord.ID)
                            {
                                chartData.orderMoney = chartData.orderMoney + (of.Quantity * of.Price);
                            }
                        }
                    }
                    if ( ord.Status !=0 && ord.Date.Month == i &&ord.Date.Year==2020)
                    {
                        chartData.totalOffer += 1;
                        foreach (var of in offers)
                        {
                            if (of.BasketMas_ID == ord.ID)
                            {
                                chartData.offerMoney = chartData.offerMoney + (of.Quantity * of.Price);
                            }
                        }
                    }
                    
                }
                chartModelList.Add(chartData);
            }

            return Json(chartModelList, JsonRequestBehavior.AllowGet);


        }
    }
}