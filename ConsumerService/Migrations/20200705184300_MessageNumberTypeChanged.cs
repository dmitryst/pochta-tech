using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsumerService.Migrations
{
    public partial class MessageNumberTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Number",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
