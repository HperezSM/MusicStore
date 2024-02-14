using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class customersale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concerts_Genres_GenreId",
                schema: "Musicales",
                table: "Concerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                schema: "Musicales",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Concerts",
                schema: "Musicales",
                table: "Concerts");

            migrationBuilder.RenameTable(
                name: "Genres",
                schema: "Musicales",
                newName: "Genre",
                newSchema: "Musicales");

            migrationBuilder.RenameTable(
                name: "Concerts",
                schema: "Musicales",
                newName: "Concert",
                newSchema: "Musicales");

            migrationBuilder.RenameIndex(
                name: "IX_Concerts_Title",
                schema: "Musicales",
                table: "Concert",
                newName: "IX_Concert_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Concerts_GenreId",
                schema: "Musicales",
                table: "Concert",
                newName: "IX_Concert_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                schema: "Musicales",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Concert",
                schema: "Musicales",
                table: "Concert",
                column: "Id");



            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Musicales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                schema: "Musicales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    OperationNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Concert_ConcertId",
                        column: x => x.ConcertId,
                        principalSchema: "Musicales",
                        principalTable: "Concert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Musicales",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ConcertId",
                schema: "Musicales",
                table: "Sale",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                schema: "Musicales",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Concert_Genre_GenreId",
                schema: "Musicales",
                table: "Concert",
                column: "GenreId",
                principalSchema: "Musicales",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concert_Genre_GenreId",
                schema: "Musicales",
                table: "Concert");


            migrationBuilder.DropTable(
                name: "Sale",
                schema: "Musicales");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Musicales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                schema: "Musicales",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Concert",
                schema: "Musicales",
                table: "Concert");

            migrationBuilder.RenameTable(
                name: "Genre",
                schema: "Musicales",
                newName: "Genres",
                newSchema: "Musicales");

            migrationBuilder.RenameTable(
                name: "Concert",
                schema: "Musicales",
                newName: "Concerts",
                newSchema: "Musicales");

            migrationBuilder.RenameIndex(
                name: "IX_Concert_Title",
                schema: "Musicales",
                table: "Concerts",
                newName: "IX_Concerts_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Concert_GenreId",
                schema: "Musicales",
                table: "Concerts",
                newName: "IX_Concerts_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                schema: "Musicales",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Concerts",
                schema: "Musicales",
                table: "Concerts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Concerts_Genres_GenreId",
                schema: "Musicales",
                table: "Concerts",
                column: "GenreId",
                principalSchema: "Musicales",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
