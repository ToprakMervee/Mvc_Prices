using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.ViewModels
{
    public class OrderDetViewModel
    {
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
        public string ArmType { get; set; }
    }
}