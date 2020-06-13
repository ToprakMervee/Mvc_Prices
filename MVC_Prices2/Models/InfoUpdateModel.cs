using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Prices2.Models
{
    public class InfoUpdateModel
    {
        public string fullName { get; set; }
        public string userName { get; set; }
        public string eMail { get; set; }
        public string phoneNumber { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

    }
}