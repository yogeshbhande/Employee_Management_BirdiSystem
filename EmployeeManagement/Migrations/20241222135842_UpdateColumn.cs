using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EmployeeFileMappings",
                newName: "UploadDate");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "EmployeeFileMappings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "EmployeeFileMappings");

            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "EmployeeFileMappings",
                newName: "CreatedAt");
        }
    }
}
