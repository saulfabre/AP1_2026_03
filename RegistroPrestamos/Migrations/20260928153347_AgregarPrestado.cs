using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroPrestamos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrestado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Prestado",
                table: "Libros",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prestado",
                table: "Libros");
        }
    }
}
