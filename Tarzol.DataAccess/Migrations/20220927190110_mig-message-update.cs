using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarzol.DataAccess.Migrations
{
    public partial class migmessageupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverMail",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderMail",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverMail",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderMail",
                table: "Messages");
        }
    }
}
