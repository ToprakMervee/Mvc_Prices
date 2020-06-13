using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class LedgesViewModel
    {
        public string system { get; set; }
        public decimal colorlessPriceFactor { get; set; }
        public decimal coloredPriceFactor { get; set; }
    }
}