using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thebook.Migrations
{
    /// <inheritdoc />
    public partial class FixPrestamoForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_LibrosIdLibro",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_UsuariosIdUsuario",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_LibrosIdLibro",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_UsuariosIdUsuario",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "LibrosIdLibro",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "UsuariosIdUsuario",
                table: "Prestamos");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdLibro",
                table: "Prestamos",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdUsuario",
                table: "Prestamos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_IdLibro",
                table: "Prestamos",
                column: "IdLibro",
                principalTable: "Libros",
                principalColumn: "IdLibro",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_IdUsuario",
                table: "Prestamos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_IdLibro",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_IdUsuario",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_IdLibro",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_IdUsuario",
                table: "Prestamos");

            migrationBuilder.AddColumn<int>(
                name: "LibrosIdLibro",
                table: "Prestamos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuariosIdUsuario",
                table: "Prestamos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_LibrosIdLibro",
                table: "Prestamos",
                column: "LibrosIdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_UsuariosIdUsuario",
                table: "Prestamos",
                column: "UsuariosIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_LibrosIdLibro",
                table: "Prestamos",
                column: "LibrosIdLibro",
                principalTable: "Libros",
                principalColumn: "IdLibro",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_UsuariosIdUsuario",
                table: "Prestamos",
                column: "UsuariosIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
