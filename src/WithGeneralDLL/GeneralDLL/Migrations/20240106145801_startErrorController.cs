using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralDLL.Core.Migrations
{
    /// <inheritdoc />
    public partial class startErrorController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorReports",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer16 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer17 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layer20 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Program = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorReports", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorReports");
        }
    }
}
