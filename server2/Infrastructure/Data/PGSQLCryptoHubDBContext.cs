using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public partial class PGSQLCryptoHubDBContext : DbContext
    {
        public PGSQLCryptoHubDBContext()
        {
        }

        public PGSQLCryptoHubDBContext(DbContextOptions<PGSQLCryptoHubDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Roles> Roles { get; set; } = null!;
     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId)
                .HasColumnName("roleid");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(10)
                    .HasColumnName("rolename")
                    .IsFixedLength();

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
