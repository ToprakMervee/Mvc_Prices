﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class ChartDataModel
    {
        public int totalOffer { get; set; }
        public int totalOrder { get; set; }
        public decimal offerMoney { get; set; }
        public decimal orderMoney { get; set; }
    }
}