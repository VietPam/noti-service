using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noti_service.Migrations
{
    public partial class _101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "tb_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "tb_user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
