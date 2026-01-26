using Thebook.Models;

namespace Thebook.DTOs
{
    public class LibroGetDto
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Categoria { get; set; }
        public int CantidadDisponible { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; }
    }
}
