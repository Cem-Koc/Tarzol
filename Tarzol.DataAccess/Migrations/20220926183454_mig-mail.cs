using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarzol.DataAccess.Migrations
{
    public partial class migmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailStatus",
                table: "Messages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailStatus",
                table: "Messages");
        }
    }
}
