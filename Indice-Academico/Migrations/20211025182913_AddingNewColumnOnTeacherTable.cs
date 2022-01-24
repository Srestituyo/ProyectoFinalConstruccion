using Microsoft.EntityFrameworkCore.Migrations;

namespace Indice_Academico.Migrations
{
    public partial class AddingNewColumnOnTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherCode",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherCode",
                table: "Teacher");
        }
    }
}
