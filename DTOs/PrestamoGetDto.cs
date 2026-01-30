using Thebook.Models;

namespace Thebook.DTOs
{
    public class PrestamoGetDto
    {
        public int IdPrestamo { get; set; }
        public DateOnly FechaPrestamo { get; set; }
        public DateOnly FechaDevolucionEstimada { get; set; }
        public DateOnly FechaDevolucionReal { get; set; }
       
        public UsuarioGetDto Usuario { get; set; }
        public LibroGetDto Libro { get; set; }
    }
}
