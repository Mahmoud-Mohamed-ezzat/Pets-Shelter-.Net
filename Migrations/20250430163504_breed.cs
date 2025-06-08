using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animal2.Migrations
{
    /// <inheritdoc />
    public partial class breed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
