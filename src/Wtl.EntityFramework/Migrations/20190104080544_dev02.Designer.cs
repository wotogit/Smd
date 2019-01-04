﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wtl.EntityFramework;

namespace Wtl.EntityFramework.Migrations
{
    [DbContext(typeof(WtlDbContext))]
    [Migration("20190104080544_dev02")]
    partial class dev02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Smd.Authorization.PermissionSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsGranted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("SmdPermissions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionSetting");
                });

            modelBuilder.Entity("Smd.Authorization.Roles.RoleClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(256);

                    b.Property<string>("ClaimValue");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("ClaimType");

                    b.HasIndex("RoleId");

                    b.ToTable("SmdRoleClaims");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserLinkId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress");

                    b.HasIndex("UserId");

                    b.HasIndex("UserName");

                    b.ToTable("SmdUserAccounts");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(256);

                    b.Property<string>("ClaimValue");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ClaimType");

                    b.HasIndex("UserId");

                    b.ToTable("SmdUserClaims");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey");

                    b.ToTable("SmdUserLogins");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserLoginAttempt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(512);

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64);

                    b.Property<string>("ClientName")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<byte>("Result");

                    b.Property<long?>("UserId");

                    b.Property<string>("UserNameOrEmailAddress")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserNameOrEmailAddress", "Result");

                    b.ToTable("SmdUserLoginAttempts");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long>("RoleId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("SmdUserRoles");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<long>("UserId");

                    b.Property<string>("Value")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SmdUserTokens");
                });

            modelBuilder.Entity("Smd.Configuration.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<long?>("UserId");

                    b.Property<string>("Value")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("SmdSettings");
                });

            modelBuilder.Entity("Wtl.Authorization.Roles.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsStatic");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("Name");

                    b.ToTable("SmdRoles");
                });

            modelBuilder.Entity("Wtl.Authorization.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AuthenticationSource")
                        .HasMaxLength(64);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256);

                    b.Property<string>("EmailConfirmationCode")
                        .HasMaxLength(328);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsEmailConfirmed");

                    b.Property<bool>("IsPhoneNumberConfirmed");

                    b.Property<bool>("IsTwoFactorEnabled");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<DateTime?>("LockoutEndDateUtc");

                    b.Property<string>("Name")
                        .HasMaxLength(32);

                    b.Property<string>("Nickname")
                        .HasMaxLength(32);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("PasswordResetCode")
                        .HasMaxLength(328);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32);

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(128);

                    b.Property<string>("SignInToken");

                    b.Property<DateTime?>("SignInTokenExpireTimeUtc");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("EmailAddress");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("UserName");

                    b.ToTable("SmdUsers");
                });

            modelBuilder.Entity("Wtl.Orders.SaleOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("OrderNum");

                    b.HasKey("Id");

                    b.ToTable("SaleOrders");
                });

            modelBuilder.Entity("Smd.Authorization.Roles.RolePermission", b =>
                {
                    b.HasBaseType("Smd.Authorization.PermissionSetting");

                    b.Property<long>("RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("SmdPermissions");

                    b.HasDiscriminator().HasValue("RolePermission");
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserPermission", b =>
                {
                    b.HasBaseType("Smd.Authorization.PermissionSetting");

                    b.Property<long>("UserId");

                    b.HasIndex("UserId");

                    b.ToTable("SmdPermissions");

                    b.HasDiscriminator().HasValue("UserPermission");
                });

            modelBuilder.Entity("Smd.Authorization.Roles.RoleClaim", b =>
                {
                    b.HasOne("Wtl.Authorization.Roles.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserClaim", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserLogin", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserRole", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserToken", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wtl.Authorization.Roles.Role", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("Wtl.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("Wtl.Authorization.Users.User", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("Wtl.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("Smd.Authorization.Roles.RolePermission", b =>
                {
                    b.HasOne("Wtl.Authorization.Roles.Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Smd.Authorization.Users.UserPermission", b =>
                {
                    b.HasOne("Wtl.Authorization.Users.User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}