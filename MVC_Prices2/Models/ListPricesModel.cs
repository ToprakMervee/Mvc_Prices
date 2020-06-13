using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class ListPricesModel
    {
        public int en { get; set; }
        public int boy { get; set; }
        public decimal fiyat { get; set; }
        public int productID { get; set; }
        public bool renk { get; set; }
    }
}