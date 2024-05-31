using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLibrary.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BookTypeColumnTypeAppearanceDateDefaultValueChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "AppearanceDate",
                table: "BookTypes",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 5, 12));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "AppearanceDate",
                table: "BookTypes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 5, 12),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");
        }
    }
}
