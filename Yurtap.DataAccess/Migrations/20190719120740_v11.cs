using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yurtap.DataAccess.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kisiler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkleyenId = table.Column<int>(nullable: true),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    TcKimlikNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kisiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    EkleyenId = table.Column<int>(nullable: true),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    KisiId = table.Column<int>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    Sifre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KisiId);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    EkleyenId = table.Column<int>(nullable: true),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    KisiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.KisiId);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    EkleyenId = table.Column<int>(nullable: true),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    KisiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.KisiId);
                });

            migrationBuilder.CreateTable(
                name: "YoklamaBasliklari",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkleyenId = table.Column<int>(nullable: true),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    Baslik = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoklamaBasliklari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yoklamalar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkleyenId = table.Column<int>(nullable: true),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    YoklamaBaslikId = table.Column<byte>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false),
                    Liste = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yoklamalar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kisiler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "YoklamaBasliklari");

            migrationBuilder.DropTable(
                name: "Yoklamalar");
        }
    }
}
