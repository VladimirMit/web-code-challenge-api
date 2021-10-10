using Microsoft.EntityFrameworkCore.Migrations;

namespace Cognizant.CodeChallenge.Api.Migrations
{
    public partial class InputType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "CodeTask",
                type: "text",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputType",
                table: "CodeTask");
        }
    }
}
