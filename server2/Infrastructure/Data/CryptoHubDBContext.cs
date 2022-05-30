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
        public virtual DbSet<UserFollower> UserFollowers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=CryptoHubDB;User Id = SA;Password = yourStrong(!)Password");
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

                entity.HasData(
                    new Post
                    {
                        PostId = 1,
                        UserId = 1,
                        Post1 = "Crypto awesome"
                    },
                    new Post
                    {
                        PostId = 2,
                        UserId = 1,
                        Post1 = "Crypto Amazing"
                    }, 
                    new Post
                    {
                        PostId = 3,
                        UserId = 2,
                        Post1 = "cool site"
                    }, 
                    new Post
                    {
                        PostId = 4,
                        UserId = 3,
                        Post1 = "send money"
                    }, 
                    new Post
                    {
                        PostId = 5,
                        UserId = 3,
                        Post1 = "please"
                    }
                );
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasData(
                    new User
                    {
                        UserId = 1,
                        Email = "johndoe@gmail.com",
                        Firstname = "john",
                        Lastname = "doe",
                        Username = "john",
                        Password = "1234"
                    },
                    new User
                    {
                        UserId = 2,
                        Email = "elonmusk@gmail.com",
                        Firstname = "elon",
                        Lastname = "musk",
                        Username = "elon",
                        Password = "1234"
                    },
                    new User
                    {
                        UserId = 3,
                        Email = "billgates@gmail.com",
                        Firstname = "bill",
                        Lastname = "gates",
                        Username = "bill",
                        Password = "windows"
                    }

                );
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.ToTable("UserFollower");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FollowDate).HasColumnType("datetime");

                entity.HasOne(d => d.Follow)
                    .WithMany(p => p.UserFollowerFollows)
                    .HasForeignKey(d => d.FollowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFollower_FollowerId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFollowerUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFollower_UserId");

                entity.HasData(
                    new UserFollower
                    {
                        Id=1,
                        UserId=1,
                        FollowId=2,
                        FollowDate=DateTime.Now
                    },
                    new UserFollower
                    {
                        Id = 2,
                        UserId = 1,
                        FollowId = 3,
                        FollowDate = DateTime.Now
                    },
                    new UserFollower
                    {
                        Id = 3,
                        UserId = 3,
                        FollowId = 2,
                        FollowDate = DateTime.Now
                    },
                    new UserFollower
                    {
                        Id = 4,
                        UserId = 2,
                        FollowId = 1,
                        FollowDate = DateTime.Now
                    }

                    );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
