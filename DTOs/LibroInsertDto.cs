namespace Thebook.DTOs
{
    public class LibroInsertDto
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Categoria { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
