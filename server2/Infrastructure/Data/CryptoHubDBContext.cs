using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class CryptoHubDBContext : DbContext
    {
        public CryptoHubDBContext()
        {
        }

        public CryptoHubDBContext(DbContextOptions<CryptoHubDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-MSIFIJAA;Database=CryptoHubDB;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Post1).HasColumnName("Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasMany(d => d.Follows)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserFollower",
                        l => l.HasOne<User>().WithMany().HasForeignKey("FollowId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserFollower_FollowerId"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserFollower_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "FollowId");

                            j.ToTable("UserFollower");
                        });

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Follows)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserFollower",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserFollower_UserId"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("FollowId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserFollower_FollowerId"),
                        j =>
                        {
                            j.HasKey("UserId", "FollowId");

                            j.ToTable("UserFollower");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
