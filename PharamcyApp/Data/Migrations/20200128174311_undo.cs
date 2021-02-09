using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharamcyApp.Data.Migrations
{
    public partial class undo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackCoverImage",
                table: "Pharmacy",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImage",
                table: "Pharmacy",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackCoverImage",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Pharmacy");
        }
    }
}
