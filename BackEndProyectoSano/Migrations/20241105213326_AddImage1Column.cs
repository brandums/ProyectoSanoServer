using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndProyectoSano.Migrations
{
    /// <inheritdoc />
    public partial class AddImage1Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Rutinas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Rutinas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Rutinas");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Rutinas");
        }
    }
}
