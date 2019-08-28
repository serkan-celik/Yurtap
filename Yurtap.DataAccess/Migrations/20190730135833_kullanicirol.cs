using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yurtap.DataAccess.Migrations
{
    public partial class kullanicirol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arama",
                table: "Roller");

            migrationBuilder.DropColumn(
                name: "Ekleme",
                table: "Roller");

            migrationBuilder.DropColumn(
                name: "Guncelleme",
                table: "Roller");

            migrationBuilder.DropColumn(
                name: "Listeleme",
                table: "Roller");

            migrationBuilder.DropColumn(
                name: "Silme",
                table: "Roller");

            migrationBuilder.CreateTable(
                name: "KullaniciRolleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkleyenId = table.Column<int>(nullable: false),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    KisiId = table.Column<int>(nullable: false),
                    RolId = table.Column<int>(nullable: false),
                    Ekleme = table.Column<bool>(nullable: false),
                    Silme = table.Column<bool>(nullable: false),
                    Guncelleme = table.Column<bool>(nullable: false),
                    Listeleme = table.Column<bool>(nullable: false),
                    Arama = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciRolleri", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciRolleri");

            migrationBuilder.AddColumn<bool>(
                name: "Arama",
                table: "Roller",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ekleme",
                table: "Roller",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Guncelleme",
                table: "Roller",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Listeleme",
                table: "Roller",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Silme",
                table: "Roller",
                nullable: false,
                defaultValue: false);
        }
    }
}
