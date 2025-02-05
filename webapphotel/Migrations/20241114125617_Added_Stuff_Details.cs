using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapphotel.Migrations
{
    /// <inheritdoc />
    public partial class Added_Stuff_Details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfHirement",
                table: "Staff",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlySalary",
                table: "Staff",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "DateOfHirement",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "MonthlySalary",
                table: "Staff");
        }
    }
}
