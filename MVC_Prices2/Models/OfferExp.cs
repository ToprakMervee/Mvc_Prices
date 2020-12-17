using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class OfferExp
    {
        public string Reference { get; set; }
        public string Exp1 { get; set; }
        public string Exp2 { get; set; }
        public int Id { get; set; }
        public bool IsMaster { get; set; }
    }
}