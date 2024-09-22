using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesDer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDescriptionFromCustomTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnDescription",
                table: "CustomTables");

            migrationBuilder.DropColumn(
                name: "KgDescription",
                table: "CustomTables");

            migrationBuilder.DropColumn(
                name: "RuDescription",
                table: "CustomTables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnDescription",
                table: "CustomTables",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KgDescription",
                table: "CustomTables",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RuDescription",
                table: "CustomTables",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
