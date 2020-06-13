using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class Store
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string StoreName { get; set; }
        [Required]
        [MaxLength(100)]
        public string StoreCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string Adress { get; set; }

        public bool isActive { get; set; }
    }
}