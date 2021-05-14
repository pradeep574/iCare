﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ICareContext : DbContext
    {
        public ICareContext()
            : base("name=ICareContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Insurer> Insurers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<Get_hospitals_Result> Get_hospitals(int pincode)
        {
            var pincodeParameter = pincode != null ?
                new ObjectParameter("pincode", pincode) :
                new ObjectParameter("pincode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_hospitals_Result>("Get_hospitals", pincodeParameter);
        }
    }
}
