using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class ChartModel 
    {
        
        public string storeName { get; set; }
        public decimal storeMoney { get; set; }
        public int storeOrder { get; set; }
    }
}