using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazingKanban.Server.Data.Migrations
{
    public partial class TasksAndComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KanbanTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowOnBoard = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanTasks_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanbanTasks_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanbanTasks_AspNetUsers_ValidatorId",
                        column: x => x.ValidatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowOnBoard = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KanbanTasks_AssignedToId",
                table: "KanbanTasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanTasks_CreatedById",
                table: "KanbanTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanTasks_ValidatorId",
                table: "KanbanTasks",
                column: "ValidatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_CreatedById",
                table: "TaskComments",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KanbanTasks");

            migrationBuilder.DropTable(
                name: "TaskComments");
        }
    }
}
