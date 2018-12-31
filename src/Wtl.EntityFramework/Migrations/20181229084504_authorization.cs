using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wtl.EntityFramework.Migrations
{
    public partial class authorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmdUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    AuthenticationSource = table.Column<string>(maxLength: 64, nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: true),
                    Nickname = table.Column<string>(maxLength: 32, nullable: true),
                    Password = table.Column<string>(maxLength: 128, nullable: false),
                    EmailConfirmationCode = table.Column<string>(maxLength: 328, nullable: true),
                    PasswordResetCode = table.Column<string>(maxLength: 328, nullable: true),
                    LockoutEndDateUtc = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 32, nullable: true),
                    IsPhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(maxLength: 128, nullable: true),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdUsers_SmdUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmdUsers_SmdUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginAttempts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    UserNameOrEmailAddress = table.Column<string>(maxLength: 255, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    BrowserInfo = table.Column<string>(maxLength: 512, nullable: true),
                    Result = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmdRoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdRoles_SmdUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmdRoles_SmdUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmdSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdSettings_SmdUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmdUserClaims",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdUserClaims_SmdUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmdUserLogins",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdUserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdUserLogins_SmdUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmdUserRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdUserRole_SmdUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmdUserTokens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Value = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdUserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdUserTokens_SmdUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmdPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    IsGranted = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    RoleId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdPermissions_SmdRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SmdRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmdPermissions_SmdUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmdUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmdRoleClaims",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    RoleId = table.Column<long>(nullable: false),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmdRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmdRoleClaims_SmdRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SmdRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmdPermissions_Name",
                table: "SmdPermissions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SmdPermissions_RoleId",
                table: "SmdPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdPermissions_UserId",
                table: "SmdPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdRoleClaims_ClaimType",
                table: "SmdRoleClaims",
                column: "ClaimType");

            migrationBuilder.CreateIndex(
                name: "IX_SmdRoleClaims_RoleId",
                table: "SmdRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdRoles_CreatorUserId",
                table: "SmdRoles",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdRoles_LastModifierUserId",
                table: "SmdRoles",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdRoles_Name",
                table: "SmdRoles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SmdSettings_Name",
                table: "SmdSettings",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SmdSettings_UserId",
                table: "SmdSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserClaims_ClaimType",
                table: "SmdUserClaims",
                column: "ClaimType");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserClaims_UserId",
                table: "SmdUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserLogins_UserId",
                table: "SmdUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserLogins_LoginProvider_ProviderKey",
                table: "SmdUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserRole_RoleId",
                table: "SmdUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserRole_UserId",
                table: "SmdUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUsers_CreatorUserId",
                table: "SmdUsers",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUsers_EmailAddress",
                table: "SmdUsers",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUsers_LastModifierUserId",
                table: "SmdUsers",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUsers_UserName",
                table: "SmdUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_SmdUserTokens_UserId",
                table: "SmdUserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAttempts_UserId",
                table: "UserLoginAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAttempts_UserNameOrEmailAddress_Result",
                table: "UserLoginAttempts",
                columns: new[] { "UserNameOrEmailAddress", "Result" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmdPermissions");

            migrationBuilder.DropTable(
                name: "SmdRoleClaims");

            migrationBuilder.DropTable(
                name: "SmdSettings");

            migrationBuilder.DropTable(
                name: "SmdUserClaims");

            migrationBuilder.DropTable(
                name: "SmdUserLogins");

            migrationBuilder.DropTable(
                name: "SmdUserRole");

            migrationBuilder.DropTable(
                name: "SmdUserTokens");

            migrationBuilder.DropTable(
                name: "UserLoginAttempts");

            migrationBuilder.DropTable(
                name: "SmdRoles");

            migrationBuilder.DropTable(
                name: "SmdUsers");
        }
    }
}
