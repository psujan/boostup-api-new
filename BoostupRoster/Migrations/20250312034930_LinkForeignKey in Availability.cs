using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boostup.API.Migrations
{
    /// <inheritdoc />
    public partial class LinkForeignKeyinAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAvailability_EmployeeDetail_EmployeeDetailId",
                table: "EmployeeAvailability");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAvailability_EmployeeDetailId",
                table: "EmployeeAvailability");

            migrationBuilder.DropColumn(
                name: "EmployeeDetailId",
                table: "EmployeeAvailability");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAvailability_EmployeeId",
                table: "EmployeeAvailability",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAvailability_EmployeeDetail_EmployeeId",
                table: "EmployeeAvailability",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAvailability_EmployeeDetail_EmployeeId",
                table: "EmployeeAvailability");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAvailability_EmployeeId",
                table: "EmployeeAvailability");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDetailId",
                table: "EmployeeAvailability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAvailability_EmployeeDetailId",
                table: "EmployeeAvailability",
                column: "EmployeeDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAvailability_EmployeeDetail_EmployeeDetailId",
                table: "EmployeeAvailability",
                column: "EmployeeDetailId",
                principalTable: "EmployeeDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
