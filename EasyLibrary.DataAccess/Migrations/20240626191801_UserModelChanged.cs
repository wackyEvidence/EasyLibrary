using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLibrary.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "varchar(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)");

            migrationBuilder.AlterColumn<string>(
                name: "PassportSeries",
                table: "Users",
                type: "varchar(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)");

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Users",
                type: "varchar(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Users",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(60)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "varchar(16)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassportSeries",
                table: "Users",
                type: "varchar(4)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Users",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
