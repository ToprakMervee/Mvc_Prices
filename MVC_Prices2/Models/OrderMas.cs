using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MVC_Prices2.Identity;

namespace MVC_Prices2.Models
{
    public class OrderMas
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ReferenceNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderDate { get; set; }
        [MaxLength(100)]
        public string User { get; set; }
        [MaxLength(100)]
        public string Exp1 { get; set; }
        [MaxLength(100)]
        public string Exp2 { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public byte Status { get; set; }
        public Store Store { get; set; }
        public OfferMas OfferMas { get; set; }
        public int RevisionId { get; set; }
        public List<OrderDet> OrderDet { get; set; }
    }
}