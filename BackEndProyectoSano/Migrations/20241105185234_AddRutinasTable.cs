using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndProyectoSano.Migrations
{
    /// <inheritdoc />
    public partial class AddRutinasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rutinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosicionInicial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Movimientos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respiraciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repeticiones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutinas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rutinas");
        }
    }
}
