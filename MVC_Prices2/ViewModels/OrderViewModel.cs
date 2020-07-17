using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Prices2.Models;

namespace MVC_Prices2.ViewModels
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public string ReferenceNo { get; set; }
        public string Exp1 { get; set; }
        public string Exp2 { get; set; }
        public double RemainSeconds { get; set; }
        public bool DateDiff { get; set; }
        public DateTime Date { get; set; }
        public DateTime? OrderDate { get; set; }
        public List<OrderDetViewModel> OrderDet { get; set; }
    }
}