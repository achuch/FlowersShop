using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersShop.Data.Migrations
{
    public partial class nowa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfFinished",
                table: "Order",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Order",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfFinished",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Order",
                nullable: false);
        }
    }
}
