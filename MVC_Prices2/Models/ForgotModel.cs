using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class ForgotModel
    {
        [Required]
        public string Email { get; set; }
    }
}