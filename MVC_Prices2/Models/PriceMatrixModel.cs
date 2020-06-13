using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class PriceMatrixModel
    {
        public PriceMatrixModel()
        {
            priceListWhite = new List<Price>();
            priceListColored = new List<Price>();
        }
        public int MaxWidth { get; set; }
        public int MinWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MinHeight { get; set; }
        public int productID { get; set; }
        public double Price { get; set; }
        
        public List<Price> priceListWhite { get; set; }
        public List<Price> priceListColored { get; set; }

    }
}