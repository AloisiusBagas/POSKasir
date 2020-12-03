using Microsoft.EntityFrameworkCore.Migrations;

namespace PosKasir.Entities.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "semen",
                columns: table => new
                {
                    SemenID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SemenName = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    stock = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true, defaultValue: "Date('Now')"),
                    CreatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_semen", x => x.SemenID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "semen");
        }
    }
}
