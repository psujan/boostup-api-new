using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boostup.API.Migrations
{
    /// <inheritdoc />
    public partial class OneManyRelnBetwenEmployeeandLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeDetailId",
                table: "Leave",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leave_EmployeeDetailId",
                table: "Leave",
                column: "EmployeeDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_EmployeeDetail_EmployeeDetailId",
                table: "Leave",
                column: "EmployeeDetailId",
                principalTable: "EmployeeDetail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_EmployeeDetail_EmployeeDetailId",
                table: "Leave");

            migrationBuilder.DropIndex(
                name: "IX_Leave_EmployeeDetailId",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "EmployeeDetailId",
                table: "Leave");
        }
    }
}
