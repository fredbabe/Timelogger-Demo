using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace timelogger_web_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Projects_ProjectId",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Projects_ProjectId",
                table: "Registrations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Projects_ProjectId",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Projects_ProjectId",
                table: "Registrations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
