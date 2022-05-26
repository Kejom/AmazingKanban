using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazingKanban.Server.Data.Migrations
{
    public partial class tableNamesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUserAccesses");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "TaskComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BoardAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardAccesses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAccesses_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardAccesses_BoardId",
                table: "BoardAccesses",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAccesses_UserId",
                table: "BoardAccesses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardAccesses");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TaskComments");

            migrationBuilder.CreateTable(
                name: "BoardUserAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUserAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardUserAccesses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardUserAccesses_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardUserAccesses_BoardId",
                table: "BoardUserAccesses",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUserAccesses_UserId",
                table: "BoardUserAccesses",
                column: "UserId");
        }
    }
}
