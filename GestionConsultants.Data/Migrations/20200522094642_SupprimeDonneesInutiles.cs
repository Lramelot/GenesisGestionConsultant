using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionConsultants.Data.Migrations
{
    public partial class SupprimeDonneesInutiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "MissionsConsultants");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Consultants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "MissionsConsultants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Consultants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 1,
                column: "Rate",
                value: 500.0);

            migrationBuilder.UpdateData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 2,
                column: "Rate",
                value: 600.0);

            migrationBuilder.UpdateData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 3,
                column: "Rate",
                value: 450.0);

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 1,
                column: "Experience",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 3,
                column: "Experience",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 4,
                column: "Experience",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 5,
                column: "Experience",
                value: 2);
        }
    }
}
