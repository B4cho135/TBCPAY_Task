using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class anotherPersonEnitity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPersons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_RelatedPersonId",
                table: "RelatedPersons",
                column: "RelatedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedPersonId",
                table: "RelatedPersons",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedPersonId",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPersons_RelatedPersonId",
                table: "RelatedPersons");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_PersonId",
                table: "RelatedPersons",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
