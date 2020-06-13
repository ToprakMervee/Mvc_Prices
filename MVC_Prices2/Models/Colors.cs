using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class Colors
    {
        [Key]
        [MaxLength(55)]
        public string ColorName { get; set; }
        [MaxLength(75)]
        public string ColorUrl { get; set; }
    }
}