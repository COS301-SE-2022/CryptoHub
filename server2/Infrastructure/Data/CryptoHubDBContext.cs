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
        public virtual DbSet<CoinRating> Coinratings { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostReport> Postreports { get; set; } = null!;
        public virtual DbSet<PostTag> Posttags { get; set; } = null!;
        public virtual DbSet<Reply> Replies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCoin> Usercoins { get; set; } = null!;
        public virtual DbSet<UserFollower> Userfollowers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server = 127.0.0.1;Port = 5432; Database = cryptohub; User Id =postgres; Password = P@55w0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>(entity =>
            {
                entity.ToTable("coin");

                entity.Property(e => e.CoinId).HasColumnName("coinid");

                entity.Property(e => e.CoinName)
                    .HasMaxLength(50)
                    .HasColumnName("coinname");

                entity.Property(e => e.ImageId).HasColumnName("imageid");

                entity.Property(e => e.ImageUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("imageurl");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Coins)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("fk_coin_imageid");
            });

            modelBuilder.Entity<CoinRating>(entity =>
            {
                entity.ToTable("coinrating");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoinId).HasColumnName("coinid");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.CoinRatings)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coinrating_coinid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoinRatings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coinrating_userid");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.CommentId).HasColumnName("commentid");

                entity.Property(e => e.Content)
                    .HasMaxLength(4000)
                    .HasColumnName("content");

                entity.Property(e => e.PostId).HasColumnName("postid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment_postid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment_userid");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.ImageId).HasColumnName("imageid");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("likes");

                entity.Property(e => e.LikeId).HasColumnName("likeid");

                entity.Property(e => e.CommentId).HasColumnName("commentid");

                entity.Property(e => e.PostId).HasColumnName("postid");

                entity.Property(e => e.ReplyId).HasColumnName("replyid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("fk_like_commentid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("fk_like_postid");

                entity.HasOne(d => d.Reply)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReplyId)
                    .HasConstraintName("fk_like_replyid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_like_userid");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(250)
                    .HasColumnName("content");

                entity.Property(e => e.Read).HasColumnName("read");

                entity.Property(e => e.RecieverId).HasColumnName("recieverid");

                entity.Property(e => e.TimeDelivered)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("timedelivered")
                    .HasDefaultValueSql("timezone('UTC'::text, now())");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_message_userid");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("boolean")
                    .HasColumnName("isdeleted");

                entity.Property(e => e.LastModified)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("lastmodified");

                entity.Property(e => e.SenderId).HasColumnName("senderid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notification_userid");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.PostId).HasColumnName("postid");

                entity.Property(e => e.Content)
                    .HasColumnType("character varying")
                    .HasColumnName("content");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("datecreated")
                    .HasDefaultValueSql("timezone('UTC'::text, now())");

                entity.Property(e => e.ImageId).HasColumnName("imageid");

                entity.Property(e => e.ImageUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("imageurl");

                entity.Property(e => e.SentimentScore)
                    .HasPrecision(4, 4)
                    .HasColumnName("sentimentscore");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("fk_post_imageid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post_userid");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.ToTable("postreport");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PostId).HasColumnName("postid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_postreportt_postid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_postreport_userid");
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.ToTable("posttag");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PostId).HasColumnName("postid");

                entity.Property(e => e.TagId).HasColumnName("tagid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_posttag_postid");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_posttag_tagid");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("reply");

                entity.Property(e => e.ReplyId).HasColumnName("replyid");

                entity.Property(e => e.CommentId).HasColumnName("commentid");

                entity.Property(e => e.Content)
                    .HasMaxLength(4000)
                    .HasColumnName("content");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reply_commentid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reply_userid");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("roleid");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.TagId).HasColumnName("tagid");

                entity.Property(e => e.Content)
                    .HasMaxLength(50)
                    .HasColumnName("content");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.HasForgottenPassword)
                    .HasColumnType("boolean")
                    .HasColumnName("hasforgottenpassword");

                entity.Property(e => e.ImageId).HasColumnName("imageid");

                entity.Property(e => e.ImageUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("imageurl");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.OTP).HasColumnName("otp");

                entity.Property(e => e.OTPExpirationTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("otpexpirationtime");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("fk_user_imageid");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_roleid");
            });

            modelBuilder.Entity<UserCoin>(entity =>
            {
                entity.ToTable("usercoin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoinId).HasColumnName("coinid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.UserCoins)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usercoin_coinid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCoins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usercoin_userid");
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.ToTable("userfollower");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FollowDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("followdate");

                entity.Property(e => e.FollowId).HasColumnName("followid");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.Follow)
                    .WithMany(p => p.UserFollowerFollows)
                    .HasForeignKey(d => d.FollowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userfollower_followid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFollowerUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userfollower_userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
