using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Heads_HeadId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ModuleIds",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Programs",
                table: "Institutes");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                table: "Modules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Programs_InstituteId",
                table: "Programs",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ProgramId",
                table: "Modules",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Programs_ProgramId",
                table: "Modules",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Heads_HeadId",
                table: "Programs",
                column: "HeadId",
                principalTable: "Heads",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Institutes_InstituteId",
                table: "Programs",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Programs_ProgramId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Heads_HeadId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Institutes_InstituteId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_InstituteId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Modules_ProgramId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Modules");

            migrationBuilder.AddColumn<string>(
                name: "ModuleIds",
                table: "Programs",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Programs",
                table: "Institutes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Heads_HeadId",
                table: "Programs",
                column: "HeadId",
                principalTable: "Heads",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
