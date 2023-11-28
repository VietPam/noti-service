using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noti_service.Migrations
{
    public partial class _102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdHub",
                table: "tb_user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdHub",
                table: "tb_user");
        }
    }
}
