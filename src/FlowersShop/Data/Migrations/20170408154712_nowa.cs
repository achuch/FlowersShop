using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersShop.Data.Migrations
{
    public partial class nowa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressCity",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressLocalNumber",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressZipCode",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdressHouseNumber",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressCity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressLocalNumber",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressZipCode",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AdressHouseNumber",
                table: "Order");
        }
    }
}
