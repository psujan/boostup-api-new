using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boostup.API.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeProfileImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfileImage_EmployeeDetail_EmployeeId",
                table: "EmployeeProfileImage");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfileImage_EmployeeDetail_EmployeeId",
                table: "EmployeeProfileImage",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfileImage_EmployeeDetail_EmployeeId",
                table: "EmployeeProfileImage");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfileImage_EmployeeDetail_EmployeeId",
                table: "EmployeeProfileImage",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
