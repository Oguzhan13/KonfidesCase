﻿// <auto-generated />
using System;
using KonfidesCase.Authentication.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KonfidesCase.Authentication.Migrations
{
    [DbContext(typeof(KonfidesCaseAuthDbContext))]
    partial class KonfidesCaseAuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KonfidesCase.Authentication.Entities.AuthRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnOrder(2);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a6ef0654-a9c5-4085-8581-673e702c0ad4"),
                            ConcurrencyStamp = "d2ad484a-7587-4530-ab0f-69c1b8c021ed",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("ffbaa166-158e-4254-83df-ee7d13db3749"),
                            ConcurrencyStamp = "d57ae8d7-40af-471a-b402-55bc57d6e83b",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("KonfidesCase.Authentication.Entities.AuthUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int")
                        .HasColumnOrder(17);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(14);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Mail Adresi")
                        .HasColumnOrder(4);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("Onaylanmış Email")
                        .HasColumnOrder(6);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Ad")
                        .HasColumnOrder(7);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Soyad")
                        .HasColumnOrder(8);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit")
                        .HasColumnOrder(12);

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset")
                        .HasColumnOrder(13);

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Standart Mail Adresi")
                        .HasColumnOrder(5);

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Stadart Kullanıcı Adı")
                        .HasColumnOrder(3);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Kriptolanmış Şifre")
                        .HasColumnOrder(9);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Telefon Numarası")
                        .HasColumnOrder(10);

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("Onaylanmış Telefon Numarası")
                        .HasColumnOrder(11);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(15);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit")
                        .HasColumnOrder(16);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Kullanıcı Adı")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .IsUnique()
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("6e7fe5c6-1444-474b-9b43-d078cd892237"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "aec35f00-7ca6-4447-8ac9-a9ac44de1b8b",
                            Email = "admin@example.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "Manager",
                            LockoutEnabled = true,
                            LockoutEnd = new DateTimeOffset(new DateTime(2023, 5, 6, 22, 21, 9, 923, DateTimeKind.Unspecified).AddTicks(9775), new TimeSpan(0, 3, 0, 0, 0)),
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHPMoeCsm2skWv6Oh8H/uS6pO9BpXDTunAmbbSVwCOuBYkSsfRJjFbXK0frfTvmusw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "435084b1-04d3-4fa6-87f6-d434cab7b02c",
                            TwoFactorEnabled = false,
                            UserName = "admin@example.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Kullanıcı Id")
                        .HasColumnOrder(1);

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Rol Id")
                        .HasColumnOrder(2);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("6e7fe5c6-1444-474b-9b43-d078cd892237"),
                            RoleId = new Guid("a6ef0654-a9c5-4085-8581-673e702c0ad4")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("KonfidesCase.Authentication.Entities.AuthRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("KonfidesCase.Authentication.Entities.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("KonfidesCase.Authentication.Entities.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("KonfidesCase.Authentication.Entities.AuthRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KonfidesCase.Authentication.Entities.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("KonfidesCase.Authentication.Entities.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
