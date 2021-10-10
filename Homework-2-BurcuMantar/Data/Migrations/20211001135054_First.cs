using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Homework_2_BurcuMantar.Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Clinics = table.Column<string[]>(type: "text[]", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    FileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Clinic = table.Column<string>(type: "text", nullable: true),
                    HospitalId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorPatients",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatients", x => new { x.DoctorId, x.PatientId });
                    table.ForeignKey(
                        name: "FK_DoctorPatients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "Clinics", "Name" },
                values: new object[,]
                {
                    { 1, "Ankara", new[] { "Genel Cerrahi", "Dahiliye", "Göz Hastaliklari", "Pediatri", "Cildiye", "Üroloji", "Endokrinoloji", "Acil" }, "A" },
                    { 2, "İstanbul", new[] { "Genel Cerrahi", "Dahiliye", "Göz Hastaliklari", "Pediatri", "Cildiye", "Üroloji", "Endokrinoloji", "Acil" }, "B" },
                    { 3, "İzmir", new[] { "Genel Cerrahi", "Dahiliye", "Göz Hastaliklari", "Pediatri", "Cildiye", "Üroloji", "Endokrinoloji", "Acil" }, "C" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "FileNumber", "Gender", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, 1122334455L, "M", "Ak", "Veli" },
                    { 2, 1528745889L, "F", "Aka", "Ece" },
                    { 3, 9988776655L, "M", "Akar", "Ege" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Clinic", "Gender", "HospitalId", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, "Genel Cerrahi", "M", 1, "Ak", "Ali" },
                    { 4, "Pediatri", "M", 1, "Gök", "Han" },
                    { 3, "Dahiliye", "M", 2, "Akar", "Cem" },
                    { 6, "Cildiye", "F", 2, "Sucu", "Gaye" },
                    { 2, "Göz Hastaliklari", "M", 3, "Aka", "Can" },
                    { 5, "Göz Hastaliklari", "F", 3, "Karlı", "Naz" }
                });

            migrationBuilder.InsertData(
                table: "DoctorPatients",
                columns: new[] { "DoctorId", "PatientId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 1 },
                    { 4, 2 },
                    { 3, 2 },
                    { 3, 1 },
                    { 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatients_PatientId",
                table: "DoctorPatients",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HospitalId",
                table: "Doctors",
                column: "HospitalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorPatients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Hospitals");
        }
    }
}
