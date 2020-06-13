using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class Ledge
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Colorless { get; set; }
        public string Colored { get; set; }
        public decimal ColorlessPriceFactor { get; set; }
        public decimal ColoredPriceFactor { get; set; }
        public string PicValue { get; set; }
        public bool Activity { get; set; }
    }
}