using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.History;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Prices2.Controllers;
using MVC_Prices2.Identity;

namespace MVC_Prices2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System.Web.Security;

    public partial class PriceDataModel2 : DbContext
    {
        public PriceDataModel2()
            : base("name=PriceDataModel2")
        {
          
        }
        
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OfferDet> OfferDet { get; set; }
        public virtual DbSet<OfferMas> OfferMas { get; set; }
        public virtual DbSet<OrderMas> OrderMas { get; set; }
        public virtual DbSet<OrderDet> OrderDet { get; set; }
        public virtual DbSet<Reference> Reference { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Glass> Glass { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Ledge> Ledges { get; set; }


        //public object Role { get; internal set; }
        //public RoleAdminController Roles { get; internal set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }
}
