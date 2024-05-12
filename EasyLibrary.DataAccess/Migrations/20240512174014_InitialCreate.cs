using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLibrary.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookAuthorEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthorEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookSeriesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeriesEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishingHouseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouseEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Patronymic = table.Column<string>(type: "varchar(50)", nullable: true),
                    PassportNumber = table.Column<string>(type: "varchar(6)", nullable: false),
                    PassportSeries = table.Column<string>(type: "varchar(4)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", nullable: false),
                    PublishingHouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    Binding = table.Column<int>(type: "integer", nullable: false),
                    PublishingYear = table.Column<int>(type: "integer", nullable: false),
                    ISBN = table.Column<string>(type: "varchar(17)", nullable: false),
                    PagesCount = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    AvailableForIssuance = table.Column<bool>(type: "boolean", nullable: false),
                    TimesIssued = table.Column<int>(type: "integer", nullable: false),
                    AppearanceDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2024, 5, 12)),
                    MinAge = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTypes_BookSeriesEntity_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "BookSeriesEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTypes_PublishingHouseEntity_PublishingHouseId",
                        column: x => x.PublishingHouseId,
                        principalTable: "PublishingHouseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthorEntityBookTypeEntity",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookTypesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthorEntityBookTypeEntity", x => new { x.AuthorsId, x.BookTypesId });
                    table.ForeignKey(
                        name: "FK_BookAuthorEntityBookTypeEntity_BookAuthorEntity_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "BookAuthorEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthorEntityBookTypeEntity_BookTypes_BookTypesId",
                        column: x => x.BookTypesId,
                        principalTable: "BookTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopies_BookTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BookTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthorEntity_Name",
                table: "BookAuthorEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthorEntityBookTypeEntity_BookTypesId",
                table: "BookAuthorEntityBookTypeEntity",
                column: "BookTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_InventoryNumber",
                table: "BookCopies",
                column: "InventoryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_TypeId",
                table: "BookCopies",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookSeriesEntity_Name",
                table: "BookSeriesEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_ISBN",
                table: "BookTypes",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_PublishingHouseId",
                table: "BookTypes",
                column: "PublishingHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_SeriesId",
                table: "BookTypes",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_Title",
                table: "BookTypes",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_PublishingHouseEntity_Name",
                table: "PublishingHouseEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthorEntityBookTypeEntity");

            migrationBuilder.DropTable(
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BookAuthorEntity");

            migrationBuilder.DropTable(
                name: "BookTypes");

            migrationBuilder.DropTable(
                name: "BookSeriesEntity");

            migrationBuilder.DropTable(
                name: "PublishingHouseEntity");
        }
    }
}
