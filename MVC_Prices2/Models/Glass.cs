using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace MVC_Prices2.Models
{
    public class Glass
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}