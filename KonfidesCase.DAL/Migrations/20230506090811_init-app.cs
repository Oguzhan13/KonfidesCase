using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonfidesCase.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initapp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanıcılar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailAdresi = table.Column<string>(name: "Mail Adresi", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanıcılar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Şehirler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Şehirler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etkinlikler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Organizatör = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Açıklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EtkinlikTarihi = table.Column<DateTime>(name: "Etkinlik Tarihi", type: "datetime2", nullable: false),
                    Kontenjan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Onay = table.Column<bool>(type: "bit", nullable: true),
                    KategoriId = table.Column<int>(name: "Kategori Id", type: "int", nullable: true),
                    ŞehirId = table.Column<int>(name: "Şehir Id", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlikler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Kategoriler_Kategori Id",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Şehirler_Şehir Id",
                        column: x => x.ŞehirId,
                        principalTable: "Şehirler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Biletler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BiletNumarası = table.Column<string>(name: "Bilet Numarası", type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biletler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biletler_Etkinlikler_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Etkinlikler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Biletler_Kullanıcılar_UserId",
                        column: x => x.UserId,
                        principalTable: "Kullanıcılar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kullanıcı-Etkinlik",
                columns: table => new
                {
                    KullanıcıId = table.Column<Guid>(name: "Kullanıcı Id", type: "uniqueidentifier", nullable: true),
                    EtkinlikId = table.Column<Guid>(name: "Etkinlik Id", type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanıcı-Etkinlik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanıcı-Etkinlik_Etkinlikler_Etkinlik Id",
                        column: x => x.EtkinlikId,
                        principalTable: "Etkinlikler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kullanıcı-Etkinlik_Kullanıcılar_Kullanıcı Id",
                        column: x => x.KullanıcıId,
                        principalTable: "Kullanıcılar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Kullanıcılar",
                columns: new[] { "Id", "Mail Adresi", "Ad", "Soyad", "Rol" },
                values: new object[] { new Guid("084d5fda-b7c7-40a2-bade-7db631a70009"), "admin@example.com", "Admin", "Manager", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Biletler_ActivityId",
                table: "Biletler",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Biletler_UserId",
                table: "Biletler",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_Kategori Id",
                table: "Etkinlikler",
                column: "Kategori Id");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_Şehir Id",
                table: "Etkinlikler",
                column: "Şehir Id");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanıcı-Etkinlik_Etkinlik Id",
                table: "Kullanıcı-Etkinlik",
                column: "Etkinlik Id");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanıcı-Etkinlik_Kullanıcı Id",
                table: "Kullanıcı-Etkinlik",
                column: "Kullanıcı Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biletler");

            migrationBuilder.DropTable(
                name: "Kullanıcı-Etkinlik");

            migrationBuilder.DropTable(
                name: "Etkinlikler");

            migrationBuilder.DropTable(
                name: "Kullanıcılar");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Şehirler");
        }
    }
}
