using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarzol.DataAccess.Migrations
{
    public partial class mignotificationandmessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SenderID = table.Column<int>(nullable: false),
                    ReceiverID = table.Column<int>(nullable: false),
                    MessageTitle = table.Column<string>(nullable: true),
                    MessageContent = table.Column<string>(nullable: true),
                    Read = table.Column<bool>(nullable: false),
                    MessageTrackingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    NotificationType = table.Column<string>(nullable: true),
                    NotificationTypeSymbol = table.Column<string>(nullable: true),
                    NotificationDescription = table.Column<string>(nullable: true),
                    SentNotificationGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
