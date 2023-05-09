using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWashSystem.Migrations
{
    /// <inheritdoc />
    public partial class madechangesinaddon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddOns_WashPackages_WashPackageId",
                table: "AddOns");

            migrationBuilder.AlterColumn<int>(
                name: "WashPackageId",
                table: "AddOns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_WashPackages_WashPackageId",
                table: "AddOns",
                column: "WashPackageId",
                principalTable: "WashPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddOns_WashPackages_WashPackageId",
                table: "AddOns");

            migrationBuilder.AlterColumn<int>(
                name: "WashPackageId",
                table: "AddOns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_WashPackages_WashPackageId",
                table: "AddOns",
                column: "WashPackageId",
                principalTable: "WashPackages",
                principalColumn: "Id");
        }
    }
}
