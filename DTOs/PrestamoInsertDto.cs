namespace Thebook.DTOs
{
    public class PrestamoInsertDto
    {       
        public DateOnly FechaDevolucionEstimada { get; set; }

        public int IdUsuario { get; set; }        

        public int IdLibro { get; set; }
    }
}
