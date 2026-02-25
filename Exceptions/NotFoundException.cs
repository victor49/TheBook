namespace Thebook.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string nombreModelo, object id)
            : base($"{nombreModelo} con id '{id}' no existe")
        {

        }

        public NotFoundException(string titulo) 
            : base($"El libro {titulo} no existe")
        { 
        }
    }
}
