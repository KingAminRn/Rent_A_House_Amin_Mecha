using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_A_House_Amin_Mecha.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalDaysToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "TotalDays");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderFromDate",
                table: "OrderItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderToDate",
                table: "OrderItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderFromDate",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderToDate",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TotalDays",
                table: "OrderItems",
                newName: "Quantity");
        }
    }
}
