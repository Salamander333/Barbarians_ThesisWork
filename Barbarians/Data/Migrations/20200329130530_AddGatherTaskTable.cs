using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbarians.Data.Migrations
{
    public partial class AddGatherTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TasksGather",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    Rescource = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    GoldIncome = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksGather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksGather_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksGather_UserId",
                table: "TasksGather",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksGather");
        }
    }
}
