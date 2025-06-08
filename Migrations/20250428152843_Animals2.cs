using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animal2.Migrations
{
    /// <inheritdoc />
    public partial class Animals2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed");

            //migrationBuilder.AddColumn<DateOnly>(
            //    name: "InterviewDate",
            //    table: "Request",
            //    type: "date",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Customers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Customers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "Request");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Animal",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalCategory_AnimalCategoryId",
                table: "Breed",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "Id");
        }
    }
}
