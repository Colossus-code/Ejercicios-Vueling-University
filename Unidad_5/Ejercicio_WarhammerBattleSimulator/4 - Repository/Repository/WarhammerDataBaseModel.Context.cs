﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WarhammerDataBaseEntities : DbContext
    {
        public WarhammerDataBaseEntities()
            : base("name=WarhammerDataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Armies> Armies { get; set; }
        public virtual DbSet<CommanderProfiles> CommanderProfiles { get; set; }
        public virtual DbSet<UnitProfiles> UnitProfiles { get; set; }
        public virtual DbSet<UnitsForArmies> UnitsForArmies { get; set; }
        public virtual DbSet<Weapons> Weapons { get; set; }
    }
}
