using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace MVC_Prices2.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Prices { get; set; }
        public bool Color { get; set; }
        public bool Activity { get; set; }
        public int Profil { get; set; }
        public Product Product { get; set; }

    }
}