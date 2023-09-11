using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsShoppingCart.Migrations
{
    public partial class PSalesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleEndDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleEndDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");
        }
    }
}
