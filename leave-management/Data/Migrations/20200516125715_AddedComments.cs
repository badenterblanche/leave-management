using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class AddedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedByEmployeeComment",
                table: "LeaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedEmployeeComment",
                table: "LeaveRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedByEmployeeComment",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "RequestedEmployeeComment",
                table: "LeaveRequests");
        }
    }
}
