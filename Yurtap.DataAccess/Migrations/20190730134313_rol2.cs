using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yurtap.DataAccess.Migrations
{
    public partial class rol2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkleyenId = table.Column<int>(nullable: false),
                    EklemeTarihi = table.Column<DateTime>(nullable: false),
                    SonGuncelleyenId = table.Column<int>(nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(nullable: true),
                    Durum = table.Column<byte>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    Ekleme = table.Column<bool>(nullable: false),
                    Silme = table.Column<bool>(nullable: false),
                    Guncelleme = table.Column<bool>(nullable: false),
                    Listeleme = table.Column<bool>(nullable: false),
                    Arama = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roller");
        }
    }
}
