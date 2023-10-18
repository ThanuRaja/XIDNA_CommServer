using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XICommServer.Migrations
{
    /// <inheritdoc />
    public partial class addedtested : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XSense_Cpp",
                table: "ApiKeyAuthorizationApiKeys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "XSense_Cpp",
                table: "ApiKeyAuthorizationApiKeys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
