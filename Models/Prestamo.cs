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
        public DateOnly? FechaDevolucionReal { get; set; }
     
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuarios { get; set; }

        public int IdLibro { get; set; }
        [ForeignKey(nameof(IdLibro))]
        public Libro Libros { get; set; }
    }
}
