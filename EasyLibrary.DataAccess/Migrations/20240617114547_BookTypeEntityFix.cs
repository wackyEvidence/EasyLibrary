using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLibrary.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BookTypeEntityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Binding",
                table: "BookTypes",
                newName: "Cover");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "BookTypes",
                type: "varchar(13)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(17)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cover",
                table: "BookTypes",
                newName: "Binding");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "BookTypes",
                type: "varchar(17)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)");
        }
    }
}
