using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_NAME = table.Column<string>(type: "varchar(20)", nullable: false),
                    USER_PASSWORD = table.Column<string>(type: "varchar(50)", nullable: false),
                    LOG_DATE_CREATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LOG_DATE_UPDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LOG_USER_CREATE = table.Column<string>(type: "varchar(50)", nullable: false),
                    LOG_USER_UPDATE = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
