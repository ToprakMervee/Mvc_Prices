using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVC_Prices2.Models;

namespace MVC_Prices2.ViewModels
{
    public class BasketView
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int MasId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string System { get; set; }
        public string Direction { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public string PicUrl { get; set; }
        public string ColorUrl { get; set; }
        public string ProductDetail { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public int Store { get; set; }
        public int RevisionId { get; set; }
        public double RemainSeconds { get; set; }
        public string ArmType { get; set; }
        public string Note { get; set; }
        public string Exp2 { get; set; }
        public string DoorHandle { get; set; }
        public string LatchArm { get; set; }
        public DateTime Date { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool IsFixed { get; set; }
        public List<Glass> Glass { get; set; }
        public List<OfferDet> OfferDet { get; set; }
        public string UpOpenning { get; set; }
        public string GlassType { get; set; }
        public string GlassQnt { get; set; }
        public string LatoD { get; set; }
        public string Extra { get; set; }
    }
}