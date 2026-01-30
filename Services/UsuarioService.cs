using Thebook.DTOs;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioGetDto>> Get()
        {
            var usuarios = await _repository.Get();

            return usuarios.Select(u => new UsuarioGetDto
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Identificacion = u.Identificacion,
                Correo = u.Correo
            });
        }

        public async Task<UsuarioGetDto> GetById(int id)
        {
            var usuarios = await _repository.GetById(id);

            if (usuarios != null)
            {
                var usuariDto = new UsuarioGetDto
                {
                    IdUsuario = usuarios.IdUsuario,
                    Nombre = usuarios.Nombre,
                    Identificacion = usuarios.Identificacion,
                    Correo = usuarios.Correo
                };
                return usuariDto;
            }
            return null;
        }

        public async Task<UsuarioGetDto> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioInsertDto.Nombre,
                Apellido = usuarioInsertDto.Apellido,
                Identificacion = usuarioInsertDto.Identificacion,
                Correo = usuarioInsertDto.Correo,
                Celular = usuarioInsertDto.Celular
            };
            await _repository.Add(usuario);
            await _repository.Save();

            var usuarioDto = new UsuarioGetDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Identificacion = usuario.Identificacion,
                Correo = usuario.Correo
            };
            return usuarioDto;

        }       

        public async Task<UsuarioGetDto> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _repository.GetById(id);

            if (usuario != null)
            {
                usuario.Nombre = usuarioUpdateDto.Nombre;
                usuario.Apellido = usuarioUpdateDto.Apellido;
                usuario.Identificacion = usuarioUpdateDto.Identificacion;
                usuario.Correo = usuarioUpdateDto.Correo;
                usuario.Celular = usuarioUpdateDto.Celular;

                _repository.Update(usuario);
                await _repository.Save();

                var usuarioDto = new UsuarioGetDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Identificacion = usuario.Identificacion,
                    Correo = usuario.Correo
                };
                return usuarioDto;
            }
            return null;                                  
        }
    }
}

