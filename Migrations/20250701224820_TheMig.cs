using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogsApi.Migrations
{
    /// <inheritdoc />
    public partial class TheMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reports",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reports",
                table: "Comments");
        }
    }
}
