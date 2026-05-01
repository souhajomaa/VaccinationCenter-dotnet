using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VaccinationCenter.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    AdresseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rue = table.Column<int>(type: "INTEGER", nullable: false),
                    CodePostal = table.Column<int>(type: "INTEGER", nullable: false),
                    Ville = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.AdresseId);
                });

            migrationBuilder.CreateTable(
                name: "CentresVaccination",
                columns: table => new
                {
                    CentreVaccinationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capacite = table.Column<int>(type: "INTEGER", nullable: false),
                    NbChaises = table.Column<int>(type: "INTEGER", nullable: false),
                    NumTelephone = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponsableCentre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentresVaccination", x => x.CentreVaccinationId);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citoyens",
                columns: table => new
                {
                    CiToyenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CIN = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Telephone = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroEvax = table.Column<int>(type: "INTEGER", nullable: false),
                    AdresseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citoyens", x => x.CiToyenId);
                    table.ForeignKey(
                        name: "FK_Citoyens_Adresses_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresses",
                        principalColumn: "AdresseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaccins",
                columns: table => new
                {
                    VaccinId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateValidite = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fournisseur = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeVaccin = table.Column<string>(type: "TEXT", nullable: false),
                    CentreVaccinationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccins", x => x.VaccinId);
                    table.ForeignKey(
                        name: "FK_Vaccins_CentresVaccination_CentreVaccinationId",
                        column: x => x.CentreVaccinationId,
                        principalTable: "CentresVaccination",
                        principalColumn: "CentreVaccinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RendezVous",
                columns: table => new
                {
                    RendezVousId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeInfirmiere = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DateVaccination = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NbrDoses = table.Column<int>(type: "INTEGER", nullable: false),
                    CiToyenId = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccinId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendezVous", x => x.RendezVousId);
                    table.ForeignKey(
                        name: "FK_RendezVous_Citoyens_CiToyenId",
                        column: x => x.CiToyenId,
                        principalTable: "Citoyens",
                        principalColumn: "CiToyenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendezVous_Vaccins_VaccinId",
                        column: x => x.VaccinId,
                        principalTable: "Vaccins",
                        principalColumn: "VaccinId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CentresVaccination",
                columns: new[] { "CentreVaccinationId", "Capacite", "NbChaises", "NumTelephone", "ResponsableCentre" },
                values: new object[,]
                {
                    { 1, 500, 50, 74123456, "Dr. Ahmed Ben Ali" },
                    { 2, 300, 30, 74654321, "Dr. Fatma Triki" }
                });

            migrationBuilder.InsertData(
                table: "Comptes",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[] { 1, "admin", "$2a$11$pXUAnqgR07cDjU9TuYbAWekKzptmBYTSfPNWRocg1S2EomwuqS3R.", "Admin" });

            migrationBuilder.InsertData(
                table: "Vaccins",
                columns: new[] { "VaccinId", "CentreVaccinationId", "DateValidite", "Fournisseur", "Quantite", "TypeVaccin" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pfizer Inc.", 1000, "PFizer" },
                    { 2, 1, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moderna Inc.", 500, "Moderna" },
                    { 3, 2, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Johnson & Johnson", 300, "Jhonson" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citoyens_AdresseId",
                table: "Citoyens",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Citoyens_CIN",
                table: "Citoyens",
                column: "CIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citoyens_NumeroEvax",
                table: "Citoyens",
                column: "NumeroEvax",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_Login",
                table: "Comptes",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_CiToyenId",
                table: "RendezVous",
                column: "CiToyenId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_VaccinId",
                table: "RendezVous",
                column: "VaccinId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccins_CentreVaccinationId",
                table: "Vaccins",
                column: "CentreVaccinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "RendezVous");

            migrationBuilder.DropTable(
                name: "Citoyens");

            migrationBuilder.DropTable(
                name: "Vaccins");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "CentresVaccination");
        }
    }
}
