using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using MVC_Prices2.Identity;
using Newtonsoft.Json;

namespace MVC_Prices2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int StoreId { get; set; }

        public bool isActive { get; set; }
        public string UserId { get; set; }

        public static explicit operator User(AppUser v)
        {
            throw new NotImplementedException();
        }
    }
}



