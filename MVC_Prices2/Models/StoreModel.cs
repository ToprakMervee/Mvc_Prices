using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class StoreModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }

        public bool isActive { get; set; }
    }
}