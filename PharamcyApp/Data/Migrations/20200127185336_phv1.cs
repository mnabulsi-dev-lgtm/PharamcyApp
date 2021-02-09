using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharamcyApp.Data.Migrations
{
    public partial class phv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(type: "nvarchar(20)", maxLength: 5, nullable: false),
                    DeseaseType = table.Column<int>(nullable: false),
                    productionCon = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    EXPDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineID);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    PharmacyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyName = table.Column<string>(maxLength: 20, nullable: false),
                    PharmacyLocation = table.Column<int>(nullable: false),
                    phonenumber = table.Column<string>(maxLength: 10, nullable: true),
                    workhour = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    emailConf = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.PharmacyID);
                });

            migrationBuilder.CreateTable(
                name: "Sell",
                columns: table => new
                {
                    SellID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineID = table.Column<int>(nullable: false),
                    PharmacyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sell", x => x.SellID);
                    table.ForeignKey(
                        name: "FK_Sell_Medicine_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicine",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sell_Pharmacy_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sell_MedicineID",
                table: "Sell",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_PharmacyID",
                table: "Sell",
                column: "PharmacyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sell");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Pharmacy");
        }
    }
}
