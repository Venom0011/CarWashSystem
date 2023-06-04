using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWashSystem.Migrations
{
    /// <inheritdoc />
    public partial class addedcarimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarImg",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                table: "Cars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "CarImg",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
