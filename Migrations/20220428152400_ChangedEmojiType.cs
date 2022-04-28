using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dieter.Migrations
{
    public partial class ChangedEmojiType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Emoji",
                table: "Products",
                type: "nvarchar(118)",
                maxLength: 118,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(118)",
                oldMaxLength: 118);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Emoji",
                table: "Products",
                type: "nvarchar(118)",
                maxLength: 118,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(118)",
                oldMaxLength: 118,
                oldNullable: true);
        }
    }
}
