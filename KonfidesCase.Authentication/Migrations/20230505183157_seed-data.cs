using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KonfidesCase.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8f2d6793-5abb-477f-8476-31a44b6fb37d"), new Guid("a27eefbf-f5ef-4a3a-96cf-f410be702dbf") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8f2d6793-5abb-477f-8476-31a44b6fb37d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a27eefbf-f5ef-4a3a-96cf-f410be702dbf"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("05e41633-f3e4-474f-82de-aa5d42c03f0c"), "8ef7e7e7-6033-4e5b-b883-13cbd3bc9786", "user", "USER" },
                    { new Guid("699d3445-702d-431f-ade3-a2b012d0479f"), "5b945b79-9814-4b60-82b0-476aefc871d8", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Mail Adresi", "EmailConfirmed", "Ad", "Soyad", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Şifrelenmiş Şifre", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dc5ee49c-418d-4df9-88a8-9a12b168bc50"), 0, "b9937eb7-25bf-40eb-a584-8811e4c1a4c4", "admin@example.com", true, "Admin", "Manager", true, new DateTimeOffset(new DateTime(2023, 5, 5, 21, 36, 55, 732, DateTimeKind.Unspecified).AddTicks(8932), new TimeSpan(0, 3, 0, 0, 0)), "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHMyx3rr0eEb1DFz2KuQSnb8I9KQ6Y6sxfu5niCBfq+/YnzKeGcgyqmulEQQrUFeIQ==", null, false, "8587072d-1d35-48a2-9981-758d97d2b67e", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("699d3445-702d-431f-ade3-a2b012d0479f"), new Guid("dc5ee49c-418d-4df9-88a8-9a12b168bc50") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("05e41633-f3e4-474f-82de-aa5d42c03f0c"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("699d3445-702d-431f-ade3-a2b012d0479f"), new Guid("dc5ee49c-418d-4df9-88a8-9a12b168bc50") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("699d3445-702d-431f-ade3-a2b012d0479f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dc5ee49c-418d-4df9-88a8-9a12b168bc50"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8f2d6793-5abb-477f-8476-31a44b6fb37d"), "ff0facc4-164f-421e-a53a-0169260334c5", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Mail Adresi", "EmailConfirmed", "Ad", "Soyad", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Şifrelenmiş Şifre", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a27eefbf-f5ef-4a3a-96cf-f410be702dbf"), 0, "af5b5f48-925f-475a-9981-536ff1d1f9d8", "admin@example.com", true, "Admin", "Manager", true, new DateTimeOffset(new DateTime(2023, 5, 5, 17, 38, 37, 423, DateTimeKind.Unspecified).AddTicks(7033), new TimeSpan(0, 3, 0, 0, 0)), "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEBmLgEws4DmF2wjY9hvdKL3kJ/BcuLjxkt9UaA6E+ctW4y9Wp4s1kzIE2+iwn2bgw==", null, false, "511a8b16-271a-4508-b464-132695d149ce", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8f2d6793-5abb-477f-8476-31a44b6fb37d"), new Guid("a27eefbf-f5ef-4a3a-96cf-f410be702dbf") });
        }
    }
}
