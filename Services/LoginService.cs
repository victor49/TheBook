using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        //inico de sesion
        public async Task<string> GetEmpleado(string email, string password)
        {
            var empleado = await _loginRepository.GetEmpleado(email);

            if (empleado == null || !BCrypt.Net.BCrypt.Verify(password, empleado.Password))
            {
                return null;
            }
            return GenerarTokenJWT(empleado);
        }

        //JWT
        private string GenerarTokenJWT(Empleado empleado)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, empleado.Email),
                new Claim(ClaimTypes.Role, empleado.Rol)                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
