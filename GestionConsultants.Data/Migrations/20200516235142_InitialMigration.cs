using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionConsultants.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    Rate = table.Column<double>(nullable: false),
                    Experience = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomEntreprise = table.Column<string>(nullable: true),
                    RateMaximum = table.Column<double>(nullable: false),
                    ExperienceMinimumRequise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionsConsultants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosteInterne = table.Column<string>(nullable: true),
                    Rate = table.Column<double>(nullable: false),
                    CommissionEntreprise = table.Column<double>(nullable: false),
                    ConsultantId = table.Column<int>(nullable: false),
                    MissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionsConsultants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionsConsultants_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionsConsultants_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Consultants",
                columns: new[] { "Id", "Experience", "Nom", "Prenom", "Rate" },
                values: new object[,]
                {
                    { 1, 1, "Ramelot", "Loïc", 500.0 },
                    { 2, 2, "Nguyen", "Duy", 600.0 },
                    { 3, 0, "Gaa", "Corentin", 450.0 }
                });

            migrationBuilder.InsertData(
                table: "Missions",
                columns: new[] { "Id", "ExperienceMinimumRequise", "NomEntreprise", "RateMaximum" },
                values: new object[,]
                {
                    { 1, 1, "Forem", 700.0 },
                    { 2, 0, "SPW", 650.0 }
                });

            migrationBuilder.InsertData(
                table: "MissionsConsultants",
                columns: new[] { "Id", "CommissionEntreprise", "ConsultantId", "MissionId", "PosteInterne", "Rate" },
                values: new object[,]
                {
                    { 1, 10.0, 1, 1, "Lead Developer", 500.0 },
                    { 2, 5.0, 2, 1, "Architecte", 600.0 },
                    { 3, 15.0, 3, 1, "Développzye", 450.0 },
                    { 4, 15.0, 1, 2, "Lead Developer", 475.0 },
                    { 5, 15.0, 3, 2, "Développeur", 450.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionsConsultants_ConsultantId",
                table: "MissionsConsultants",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionsConsultants_MissionId",
                table: "MissionsConsultants",
                column: "MissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionsConsultants");

            migrationBuilder.DropTable(
                name: "Consultants");

            migrationBuilder.DropTable(
                name: "Missions");
        }
    }
}
