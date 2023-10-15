using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XICommServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedMailEventLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppMailEventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SmtpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendGridEventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendGridMessageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TLS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingCampainId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingCampainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLogSynced = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMailEventLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMailEventLogs");
        }
    }
}
