using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class WTF1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateLeaveTypesVMClass");

            migrationBuilder.DropTable(
                name: "LeaveHistories");

            migrationBuilder.DropTable(
                name: "LeaveTypeVMClass");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
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
                    table.PrimaryKey("PK_LeaveRequests", x => x.LeaveRequestID);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_ApprovedByEmployeeID",
                        column: x => x.ApprovedByEmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeID",
                        column: x => x.LeaveTypeID,
                        principalTable: "LeaveTypes",
                        principalColumn: "LeaveTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_RequestedEmployeeID",
                        column: x => x.RequestedEmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovedByEmployeeID",
                table: "LeaveRequests",
                column: "ApprovedByEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeID",
                table: "LeaveRequests",
                column: "LeaveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestedEmployeeID",
                table: "LeaveRequests",
                column: "RequestedEmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.CreateTable(
                name: "CreateLeaveTypesVMClass",
                columns: table => new
                {
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateLeaveTypesVMClass", x => x.LeaveTypeID);
                });

            migrationBuilder.CreateTable(
                name: "LeaveHistories",
                columns: table => new
                {
                    LeaveRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    ApprovedByEmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateActioned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false),
                    RequestedEmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LeaveTypeVMClass",
                columns: table => new
                {
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeVMClass", x => x.LeaveTypeID);
                });

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
    }
}
