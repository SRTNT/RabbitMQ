using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralDLL.Migrations
{
    /// <inheritdoc />
    public partial class FirstStructureCashDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashBackup",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateExpire = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashBackup", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashBackup");
        }
    }
}
