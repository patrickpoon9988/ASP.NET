//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Patrick_Identity_TryEntities : DbContext
    {
        public Patrick_Identity_TryEntities()
            : base("name=Patrick_Identity_TryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblAspNetUserClaim> tblAspNetUserClaims { get; set; }
        public virtual DbSet<tblAspNetUserLogin> tblAspNetUserLogins { get; set; }
        public virtual DbSet<tblAspNetUser> tblAspNetUsers { get; set; }
        public virtual DbSet<tblAspNetUserRole> tblAspNetUserRoles { get; set; }
    }
}
