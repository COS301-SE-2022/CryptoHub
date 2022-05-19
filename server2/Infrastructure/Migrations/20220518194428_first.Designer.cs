﻿// <auto-generated />
using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(CryptoHubDBContext))]
    [Migration("20220518194428_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<string>("Post1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Post");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Post", (string)null);

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Post1 = "Crypto awesome",
                            UserId = 1
                        },
                        new
                        {
                            PostId = 2,
                            Post1 = "Crypto Amazing",
                            UserId = 1
                        },
                        new
                        {
                            PostId = 3,
                            Post1 = "cool site",
                            UserId = 2
                        },
                        new
                        {
                            PostId = 4,
                            Post1 = "send money",
                            UserId = 3
                        },
                        new
                        {
                            PostId = 5,
                            Post1 = "please",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "johndoe@gmail.com",
                            Firstname = "john",
                            Lastname = "doe",
                            Password = "1234",
                            Username = "john"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "elonmusk@gmail.com",
                            Firstname = "elon",
                            Lastname = "musk",
                            Password = "1234",
                            Username = "elon"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "billgates@gmail.com",
                            Firstname = "bill",
                            Lastname = "gates",
                            Password = "windows",
                            Username = "bill"
                        });
                });

            modelBuilder.Entity("Domain.Models.UserFollower", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("FollowDate")
                        .HasColumnType("datetime");

                    b.Property<int>("FollowId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FollowId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFollower", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FollowDate = new DateTime(2022, 5, 18, 21, 44, 28, 716, DateTimeKind.Local).AddTicks(6284),
                            FollowId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            FollowDate = new DateTime(2022, 5, 18, 21, 44, 28, 716, DateTimeKind.Local).AddTicks(6293),
                            FollowId = 3,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            FollowDate = new DateTime(2022, 5, 18, 21, 44, 28, 716, DateTimeKind.Local).AddTicks(6294),
                            FollowId = 2,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            FollowDate = new DateTime(2022, 5, 18, 21, 44, 28, 716, DateTimeKind.Local).AddTicks(6295),
                            FollowId = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Domain.Models.Post", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Post_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.UserFollower", b =>
                {
                    b.HasOne("Domain.Models.User", "Follow")
                        .WithMany("UserFollowerFollows")
                        .HasForeignKey("FollowId")
                        .IsRequired()
                        .HasConstraintName("FK_UserFollower_FollowerId");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("UserFollowerUsers")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserFollower_UserId");

                    b.Navigation("Follow");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("UserFollowerFollows");

                    b.Navigation("UserFollowerUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
