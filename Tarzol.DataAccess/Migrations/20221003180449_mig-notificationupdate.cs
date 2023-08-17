using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarzol.DataAccess.Migrations
{
    public partial class mignotificationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationDescription",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationTypeSymbol",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SentNotificationGroup",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "CreativeID",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreativeInteractionID",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationStatus",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreativeID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreativeInteractionID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationStatus",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "NotificationDescription",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationType",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationTypeSymbol",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentNotificationGroup",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
