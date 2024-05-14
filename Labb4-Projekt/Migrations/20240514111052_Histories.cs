using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb4_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class Histories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeHistorys",
                columns: table => new
                {
                    ChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldAppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewAppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeHistorys", x => x.ChangeID);
                });

            migrationBuilder.InsertData(
                table: "ChangeHistorys",
                columns: new[] { "ChangeID", "ChangeType", "NewAppointmentTime", "OldAppointmentTime", "WhenChanged" },
                values: new object[] { 1, "Ombokning", new DateTime(2024, 5, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 14, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 14, 10, 30, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeHistorys");
        }
    }
}
