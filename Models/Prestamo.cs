using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thebook.Models
{
    public class Prestamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrestamo { get; set; }
        public DateOnly FechaPrestamo { get; set; }
        public DateOnly FechaDevolucionEstimada { get; set; }
        public DateOnly FechaDevolucionReal { get; set; }
     
        public int IdUsuario { get; set; }
        public Usuario Usuarios { get; set; }

        public int IdLibro { get; set; }
        public Libro Libros { get; set; }
    }
}
