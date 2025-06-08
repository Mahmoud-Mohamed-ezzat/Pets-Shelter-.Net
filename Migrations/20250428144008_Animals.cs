using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Animal2.Migrations
{
    /// <inheritdoc />
    public partial class Animals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalCategory_AnimalCategoryId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Customers_ShelterStaffId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Animal_AnimalId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Customers_AdopterId",
                table: "Request");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.AlterColumn<int>(
                name: "BreedId",
                table: "Animal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AnimalCategory_AnimalCategoryId",
                table: "Animal",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal",
                column: "BreedId",
                principalTable: "Breed",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Customers_ShelterStaffId",
                table: "Animal",
                column: "ShelterStaffId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Animal_AnimalId",
                table: "Request",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Customers_AdopterId",
                table: "Request",
                column: "AdopterId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalCategory_AnimalCategoryId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Customers_ShelterStaffId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Animal_AnimalId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Customers_AdopterId",
                table: "Request");

            migrationBuilder.AlterColumn<int>(
                name: "BreedId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdopterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShelterStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterviewDate = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interview_Customers_AdopterId",
                        column: x => x.AdopterId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interview_Customers_ShelterStaffId",
                        column: x => x.ShelterStaffId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Adopter", "ADOPTER" },
                    { "3", null, "Shelterstaff", "SHELTERSTAFF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_AdopterId",
                table: "Interview",
                column: "AdopterId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_ShelterStaffId",
                table: "Interview",
                column: "ShelterStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AnimalCategory_AnimalCategoryId",
                table: "Animal",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal",
                column: "BreedId",
                principalTable: "Breed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Customers_ShelterStaffId",
                table: "Animal",
                column: "ShelterStaffId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Animal_AnimalId",
                table: "Request",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Customers_AdopterId",
                table: "Request",
                column: "AdopterId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
