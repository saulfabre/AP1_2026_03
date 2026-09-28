using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroPrestamos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDevuelto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Devuelto",
                table: "Prestamos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Devuelto",
                table: "Prestamos");
        }
    }
}
