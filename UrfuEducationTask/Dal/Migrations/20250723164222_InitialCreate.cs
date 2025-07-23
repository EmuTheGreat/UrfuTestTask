using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heads",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heads", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Standard = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cypher = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Standard = table.Column<int>(type: "integer", nullable: false),
                    InstituteId = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccreditationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_Programs_Heads_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Heads",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programs_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institutes",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramModules",
                columns: table => new
                {
                    ModulesUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramsUuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramModules", x => new { x.ModulesUuid, x.ProgramsUuid });
                    table.ForeignKey(
                        name: "FK_ProgramModules_Modules_ModulesUuid",
                        column: x => x.ModulesUuid,
                        principalTable: "Modules",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramModules_Programs_ProgramsUuid",
                        column: x => x.ProgramsUuid,
                        principalTable: "Programs",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramModules_ProgramsUuid",
                table: "ProgramModules",
                column: "ProgramsUuid");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_HeadId",
                table: "Programs",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_InstituteId",
                table: "Programs",
                column: "InstituteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramModules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Heads");

            migrationBuilder.DropTable(
                name: "Institutes");
        }
    }
}
