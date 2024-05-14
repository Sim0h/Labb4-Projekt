using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb4_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class initialcreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentLength = table.Column<double>(type: "float", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK_Appointments_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyID", "CompanyName" },
                values: new object[] { 1, "SUT23 Kliniken" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "CustomerAddress", "CustomerEmail", "CustomerName", "CustomerPhone" },
                values: new object[,]
                {
                    { 1, "SolGatan 12B", "Sam@testing.se", "Sam Testing", "0712365987" },
                    { 2, "Varbergsgatan 31", "Simon@ståhl.se", "Simon Ståhl", "0744556698" },
                    { 3, "Storgatan 6", "Henrik@johansson.se", "Henrik Johansson", "0723647895" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentID", "AppointmentEnd", "AppointmentLength", "AppointmentName", "AppointmentStart", "CompanyID", "CustomerID" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 8, 11, 30, 0, 0, DateTimeKind.Unspecified), 1.0, "Standard Läkarbesök", new DateTime(2024, 5, 8, 10, 30, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2024, 5, 7, 16, 30, 0, 0, DateTimeKind.Unspecified), 3.0, "Ögonkontroll", new DateTime(2024, 5, 7, 13, 30, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, new DateTime(2024, 5, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), 0.5, "Tandläkarbesök", new DateTime(2024, 5, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CompanyID",
                table: "Appointments",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerID",
                table: "Appointments",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
