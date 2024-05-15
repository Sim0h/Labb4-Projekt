using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb4_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class historyapplicationID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentID",
                table: "ChangeHistorys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ChangeHistorys",
                keyColumn: "ChangeID",
                keyValue: 1,
                column: "AppointmentID",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentID",
                table: "ChangeHistorys");
        }
    }
}
