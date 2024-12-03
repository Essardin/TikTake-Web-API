﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RobocopsWebAPI.Data;

#nullable disable

namespace RobocopsWebAPI.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RobocopsWebAPI.Models.Comment", b =>
                {
                    b.Property<string>("CommentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.FriendList", b =>
                {
                    b.Property<string>("FriendshipId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("FriendSince")
                        .HasColumnType("datetime2");

                    b.Property<string>("FriendsUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FriendshipId");

                    b.HasIndex("UserId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.FriendRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<bool>("FriendRequestReceived")
                        .HasColumnType("bit");

                    b.Property<string>("ReceiverUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RequestApproval")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RequestId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("UserId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.Post", b =>
                {
                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<string>("PostCaption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.UserProfile", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserCreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.Comment", b =>
                {
                    b.HasOne("RobocopsWebAPI.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("RobocopsWebAPI.Models.UserProfile", "UserProfile")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.FriendList", b =>
                {
                    b.HasOne("RobocopsWebAPI.Models.UserProfile", "UserProfile")
                        .WithMany("FriendList")
                        .HasForeignKey("UserId");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.FriendRequest", b =>
                {
                    b.HasOne("RobocopsWebAPI.Models.UserProfile", "ReceiverUserProfile")
                        .WithMany("ReceivedFriendRequests")
                        .HasForeignKey("ReceiverUserId");

                    b.HasOne("RobocopsWebAPI.Models.UserProfile", "SenderUserProfile")
                        .WithMany("SentFriendRequests")
                        .HasForeignKey("UserId");

                    b.Navigation("ReceiverUserProfile");

                    b.Navigation("SenderUserProfile");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.Like", b =>
                {
                    b.HasOne("RobocopsWebAPI.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId");

                    b.HasOne("RobocopsWebAPI.Models.UserProfile", "UserProfile")
                        .WithMany("Likes")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.Post", b =>
                {
                    b.HasOne("RobocopsWebAPI.Models.UserProfile", "UserProfile")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("RobocopsWebAPI.Models.UserProfile", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("FriendList");

                    b.Navigation("Likes");

                    b.Navigation("Posts");

                    b.Navigation("ReceivedFriendRequests");

                    b.Navigation("SentFriendRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
