using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string ProductName { get; set; }
        [MaxLength(250)]
        public string ProductDetail { get; set; }
        [Required]
        public int MaxWidth { get; set; }
        [Required]
        public int MinWidth { get; set; }
        public int DefWidth { get; set; }
        [Required]
        public int MaxHeight { get; set; }
        [Required]
        public int MinHeight { get; set; }
        public int DefHeight { get; set; }
        [Required]
        public decimal BasePrice { get; set; }
        [Required]
        [MaxLength(70)]
        public string PicUrl { get; set; }
        public bool IsWingFixed { get; set; }
        public bool IsArmFixed { get; set; }
        public List<Glass> Glass { get; set; }
        public List<Price> Prices { get; set; }

    }
}