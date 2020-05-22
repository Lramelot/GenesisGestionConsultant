using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionConsultants.Data.Migrations
{
    public partial class ImproveConsultantMissionDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstActif",
                table: "MissionsConsultants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "MissionsConsultants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 1,
                column: "Experience",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 2,
                column: "EstActif",
                value: true);

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
                columns: new[] { "EstActif", "Experience" },
                values: new object[] { true, 2 });

            migrationBuilder.UpdateData(
                table: "MissionsConsultants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EstActif", "Experience" },
                values: new object[] { true, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstActif",
                table: "MissionsConsultants");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "MissionsConsultants");
        }
    }
}
