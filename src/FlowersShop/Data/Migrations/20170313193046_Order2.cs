using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersShop.Data.Migrations
{
    public partial class Order2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ApplicationUserId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ApplicationUserId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Order",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Order",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ApplicationUserId1",
                table: "Order",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId1",
                table: "Order",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
