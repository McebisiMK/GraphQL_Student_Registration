using Microsoft.EntityFrameworkCore.Migrations;

namespace Registration.Entities.Migrations
{
    public partial class ChangedSemesterType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Semster",
                table: "Subject",
                type: "varchar(6)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldUnicode: false,
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Semster",
                table: "Subject",
                type: "nvarchar(6)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldUnicode: false,
                oldMaxLength: 50);
        }
    }
}
