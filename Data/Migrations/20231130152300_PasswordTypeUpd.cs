using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtTest.Data.Migrations
{
    public partial class PasswordTypeUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
