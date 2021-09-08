using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class fixnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Persons",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Persons",
                newName: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Persons",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Persons",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Persons",
                newName: "Birthdate");
        }
    }
}
