using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarzol.DataAccess.Migrations
{
    public partial class migmessageupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverEmailStatus",
                table: "Messages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverEmailStatus",
                table: "Messages");
        }
    }
}
