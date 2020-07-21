using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Models;

namespace MVC_Prices2.Identity
{
    public class AppUser:IdentityUser
    {
        public int StoreId { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
   
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
       
     

        
       
    }
}