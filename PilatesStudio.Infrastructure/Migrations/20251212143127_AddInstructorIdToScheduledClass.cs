using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PilatesStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorIdToScheduledClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "ScheduledClasses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledClasses_InstructorId",
                table: "ScheduledClasses",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledClasses_Users_InstructorId",
                table: "ScheduledClasses",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledClasses_Users_InstructorId",
                table: "ScheduledClasses");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledClasses_InstructorId",
                table: "ScheduledClasses");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "ScheduledClasses");
        }
    }
}
