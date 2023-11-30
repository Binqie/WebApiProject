using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JwtTest.Data.Migrations
{
    public partial class UserUserChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "UserChildren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pin = table.Column<string>(type: "varchar(14)", nullable: false),
                    Fio = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChildren", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserUserChildren",
                columns: table => new
                {
                    UserChildrenId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserChildren", x => new { x.UserChildrenId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUserChildren_UserChildren_UserChildrenId",
                        column: x => x.UserChildrenId,
                        principalTable: "UserChildren",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUserChildren_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUserChildren_UsersId",
                table: "UserUserChildren",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
