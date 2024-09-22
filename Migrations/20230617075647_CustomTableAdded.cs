using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesDer.Migrations
{
    /// <inheritdoc />
    public partial class CustomTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RuName = table.Column<string>(type: "text", nullable: false),
                    KgName = table.Column<string>(type: "text", nullable: false),
                    EnName = table.Column<string>(type: "text", nullable: false),
                    RuDescription = table.Column<string>(type: "text", nullable: false),
                    EnDescription = table.Column<string>(type: "text", nullable: false),
                    KgDescription = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RuName = table.Column<string>(type: "text", nullable: false),
                    KgName = table.Column<string>(type: "text", nullable: false),
                    EnName = table.Column<string>(type: "text", nullable: false),
                    CustomTableId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomFields_CustomTables_CustomTableId",
                        column: x => x.CustomTableId,
                        principalTable: "CustomTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomTableRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomTableId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTableRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomTableRows_CustomTables_CustomTableId",
                        column: x => x.CustomTableId,
                        principalTable: "CustomTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CustomFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomTableRowId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomValues_CustomFields_CustomFieldId",
                        column: x => x.CustomFieldId,
                        principalTable: "CustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomValues_CustomTableRows_CustomTableRowId",
                        column: x => x.CustomTableRowId,
                        principalTable: "CustomTableRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_CustomTableId",
                table: "CustomFields",
                column: "CustomTableId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTableRows_CustomTableId",
                table: "CustomTableRows",
                column: "CustomTableId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomValues_CustomFieldId",
                table: "CustomValues",
                column: "CustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomValues_CustomTableRowId",
                table: "CustomValues",
                column: "CustomTableRowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomValues");

            migrationBuilder.DropTable(
                name: "CustomFields");

            migrationBuilder.DropTable(
                name: "CustomTableRows");

            migrationBuilder.DropTable(
                name: "CustomTables");
        }
    }
}
