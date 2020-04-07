using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbarians.Data.Migrations
{
    public partial class AddUserHPAndArmorAndWeaponIsBroken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBroken",
                table: "Weapons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBroken",
                table: "Armors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBroken",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Health",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsBroken",
                table: "Armors");
        }
    }
}
