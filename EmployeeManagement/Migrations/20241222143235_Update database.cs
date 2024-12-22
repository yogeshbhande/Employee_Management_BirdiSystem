using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFileMappings_Employees_EmployeesEmployeeId",
                table: "EmployeeFileMappings");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeFileMappings_EmployeesEmployeeId",
                table: "EmployeeFileMappings");

            migrationBuilder.DropColumn(
                name: "EmployeesEmployeeId",
                table: "EmployeeFileMappings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeesEmployeeId",
                table: "EmployeeFileMappings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFileMappings_EmployeesEmployeeId",
                table: "EmployeeFileMappings",
                column: "EmployeesEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFileMappings_Employees_EmployeesEmployeeId",
                table: "EmployeeFileMappings",
                column: "EmployeesEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
