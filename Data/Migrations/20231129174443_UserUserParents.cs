using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JwtTest.Data.Migrations
{
    public partial class UserUserParents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserParentsId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserParents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PinMother = table.Column<string>(type: "text", nullable: false),
                    PinFather = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserParents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserParentsId",
                table: "Users",
                column: "UserParentsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserParents_UserParentsId",
                table: "Users",
                column: "UserParentsId",
                principalTable: "UserParents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
