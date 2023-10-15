using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XICommServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedWebHookConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppWebHookConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiSignatureVerificationKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientWebhookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListenProcessed = table.Column<bool>(type: "bit", nullable: false),
                    ListenDeferred = table.Column<bool>(type: "bit", nullable: false),
                    ListenDelivered = table.Column<bool>(type: "bit", nullable: false),
                    ListenOpen = table.Column<bool>(type: "bit", nullable: false),
                    ListenClick = table.Column<bool>(type: "bit", nullable: false),
                    ListenBounce = table.Column<bool>(type: "bit", nullable: false),
                    ListenDropped = table.Column<bool>(type: "bit", nullable: false),
                    ListenSpamReport = table.Column<bool>(type: "bit", nullable: false),
                    ListenUnsubscribe = table.Column<bool>(type: "bit", nullable: false),
                    ListenGroupUnsubscribe = table.Column<bool>(type: "bit", nullable: false),
                    ListenGroupResubscribe = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWebHookConfigurations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppWebHookConfigurations");
        }
    }
}
