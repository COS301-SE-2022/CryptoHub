using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
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

        public virtual DbSet<Coin> Coins { get; set; } = null!;
        public virtual DbSet<CoinHistory> CoinHistories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Reply> Replies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCoin> UserCoins { get; set; } = null!;
        public virtual DbSet<UserFollower> UserFollowers { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=CryptoHubDB;User Id = SA;Password = CodeForce12345$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>(entity =>
            {
                entity.ToTable("Coin");

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.MarketCapUsd).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaxSupply).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PercentageChange).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Supply).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TradingPriceUsd).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Coins)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Coin_Image");
            });

            modelBuilder.Entity<CoinHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.ToTable("CoinHistory");

                entity.Property(e => e.MarketCapUsd).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaxSupply).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PercentageChange).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Supply).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.TradingPriceUsd).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.CoinHistories)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoinHistory_Coin1");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Comment1)
                    .HasMaxLength(4000)
                    .HasColumnName("Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User1");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("Like");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_Like_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Like_Post1");

                entity.HasOne(d => d.Reply)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReplyId)
                    .HasConstraintName("FK_Like_Reply");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Like_User1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Post1).HasColumnName("Post");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Post_Image");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("Reply");

                entity.Property(e => e.Comment).HasMaxLength(4000);

                entity.HasOne(d => d.CommentNavigation)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reply_Comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reply_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Role1)
                    .HasMaxLength(10)
                    .HasColumnName("Role")
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_User_Image");
            });

            modelBuilder.Entity<UserCoin>(entity =>
            {
                entity.ToTable("UserCoin");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.UserCoins)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCoin_Coin");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCoins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCoin_User");
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.ToTable("UserFollower");

                entity.Property(e => e.Id).HasColumnName("id");

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
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
