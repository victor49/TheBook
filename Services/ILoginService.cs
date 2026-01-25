using Thebook.DTOs;

namespace Thebook.Services
{
    public interface ILoginService
    {
        Task<string> GetEmpleado(string email, string password);
    }
}
