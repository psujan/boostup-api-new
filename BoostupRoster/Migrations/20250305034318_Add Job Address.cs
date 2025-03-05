using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boostup.API.Migrations
{
    /// <inheritdoc />
    public partial class AddJobAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "Leave",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Leave",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leave");
        }
    }
}
