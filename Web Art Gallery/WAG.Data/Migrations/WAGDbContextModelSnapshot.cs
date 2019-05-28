﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WAG.Data;

namespace WAG.Data.Migrations
{
    [DbContext(typeof(WAGDbContext))]
    partial class WAGDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WAG.Data.Models.ArtEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EditedOn");

                    b.Property<DateTime>("EventDate");

                    b.Property<string>("MainPictureFileName");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Title");

                    b.Property<string>("WAGUserId");

                    b.HasKey("Id");

                    b.HasIndex("WAGUserId");

                    b.ToTable("ArtEvents");
                });

            modelBuilder.Entity("WAG.Data.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleContentFileName");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("EditedOn");

                    b.Property<string>("MainPictureFileName");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Title");

                    b.Property<string>("WAGUserId");

                    b.HasKey("Id");

                    b.HasIndex("WAGUserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("WAG.Data.Models.ArtisticWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtisticWorkCategoryId");

                    b.Property<bool>("Availability");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("EditedOn");

                    b.Property<bool>("HasFrame");

                    b.Property<double>("Height");

                    b.Property<string>("PictureFileName");

                    b.Property<decimal>("Price");

                    b.Property<string>("Technique");

                    b.Property<double>("Width");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ArtisticWorkCategoryId");

                    b.ToTable("ArtisticWorks");
                });

            modelBuilder.Entity("WAG.Data.Models.ArtisticWorkCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MainPictureFileName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ArtisticWorkCategories");
                });

            modelBuilder.Entity("WAG.Data.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("TextBody");

                    b.Property<string>("WAGUserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("WAGUserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WAG.Data.Models.ContactMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Read");

                    b.Property<string>("TextBody");

                    b.Property<string>("Title");

                    b.Property<string>("WAGUserId");

                    b.HasKey("Id");

                    b.HasIndex("WAGUserId");

                    b.ToTable("ContactMessages");
                });

            modelBuilder.Entity("WAG.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArtisticWorkId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeliveryAddress");

                    b.Property<string>("OrderInfo");

                    b.Property<string>("TelephoneNumberForContact");

                    b.Property<string>("WAGUserId");

                    b.HasKey("Id");

                    b.HasIndex("ArtisticWorkId");

                    b.HasIndex("WAGUserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WAG.Data.Models.WAGUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WAG.Data.Models.WAGUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WAG.Data.Models.WAGUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WAG.Data.Models.WAGUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WAG.Data.Models.WAGUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WAG.Data.Models.ArtEvent", b =>
                {
                    b.HasOne("WAG.Data.Models.WAGUser", "WAGUser")
                        .WithMany()
                        .HasForeignKey("WAGUserId");
                });

            modelBuilder.Entity("WAG.Data.Models.Article", b =>
                {
                    b.HasOne("WAG.Data.Models.WAGUser", "WAGUser")
                        .WithMany("Articles")
                        .HasForeignKey("WAGUserId");
                });

            modelBuilder.Entity("WAG.Data.Models.ArtisticWork", b =>
                {
                    b.HasOne("WAG.Data.Models.ArtisticWorkCategory", "ArtisticWorkCategory")
                        .WithMany("ArtisticWorks")
                        .HasForeignKey("ArtisticWorkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WAG.Data.Models.Comment", b =>
                {
                    b.HasOne("WAG.Data.Models.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WAG.Data.Models.WAGUser", "WAGUser")
                        .WithMany("Comments")
                        .HasForeignKey("WAGUserId");
                });

            modelBuilder.Entity("WAG.Data.Models.ContactMessage", b =>
                {
                    b.HasOne("WAG.Data.Models.WAGUser", "WAGUser")
                        .WithMany("ContactMessages")
                        .HasForeignKey("WAGUserId");
                });

            modelBuilder.Entity("WAG.Data.Models.Order", b =>
                {
                    b.HasOne("WAG.Data.Models.ArtisticWork", "ArtisticWork")
                        .WithMany()
                        .HasForeignKey("ArtisticWorkId");

                    b.HasOne("WAG.Data.Models.WAGUser", "WAGUser")
                        .WithMany("Orders")
                        .HasForeignKey("WAGUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
