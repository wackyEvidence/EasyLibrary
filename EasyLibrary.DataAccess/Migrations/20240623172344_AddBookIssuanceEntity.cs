using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLibrary.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBookIssuanceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookIssuances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookCopyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IssuanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookIssuances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookIssuances_BookCopies_BookCopyId",
                        column: x => x.BookCopyId,
                        principalTable: "BookCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookIssuances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookIssuances_BookCopyId",
                table: "BookIssuances",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssuances_UserId",
                table: "BookIssuances",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookIssuances");
        }
    }
}
