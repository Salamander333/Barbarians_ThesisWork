using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbarians.Data.Migrations
{
    public partial class AddedCraftablesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CraftableArmors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    MaterialRequired = table.Column<int>(nullable: false),
                    MaterialCount = table.Column<int>(nullable: false),
                    Defence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraftableArmors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CraftableWeapons",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    MaterialRequired = table.Column<int>(nullable: false),
                    MaterialCount = table.Column<int>(nullable: false),
                    Damage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraftableWeapons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CraftableArmors");

            migrationBuilder.DropTable(
                name: "CraftableWeapons");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TasksGather",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
