using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class AddedLeaveDataTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    LeaveTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.LeaveTypeID);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAllocations",
                columns: table => new
                {
                    LeaveAllocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    EmployeeID = table.Column<string>(nullable: true),
                    LeaveTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocations", x => x.LeaveAllocationID);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeID",
                        column: x => x.LeaveTypeID,
                        principalTable: "LeaveTypes",
                        principalColumn: "LeaveTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveHistories",
                columns: table => new
                {
                    LeaveRequestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedEmployeeID = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LeaveTypeID = table.Column<int>(nullable: false),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    DateActioned = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    ApprovedByEmployeeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveHistories", x => x.LeaveRequestID);
                    table.ForeignKey(
                        name: "FK_LeaveHistories_AspNetUsers_ApprovedByEmployeeID",
                        column: x => x.ApprovedByEmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveHistories_LeaveTypes_LeaveTypeID",
                        column: x => x.LeaveTypeID,
                        principalTable: "LeaveTypes",
                        principalColumn: "LeaveTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveHistories_AspNetUsers_RequestedEmployeeID",
                        column: x => x.RequestedEmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeID",
                table: "LeaveAllocations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_LeaveTypeID",
                table: "LeaveAllocations",
                column: "LeaveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_ApprovedByEmployeeID",
                table: "LeaveHistories",
                column: "ApprovedByEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_LeaveTypeID",
                table: "LeaveHistories",
                column: "LeaveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_RequestedEmployeeID",
                table: "LeaveHistories",
                column: "RequestedEmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "LeaveHistories");

            migrationBuilder.DropTable(
                name: "LeaveTypes");
        }
    }
}
