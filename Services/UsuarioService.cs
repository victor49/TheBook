using AutoMapper;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        private static string nombreModelo = "Usuario";

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioGetDto>> Get()
        {
            var usuarios = await _repository.Get();

            return usuarios.Select(u => _mapper.Map<UsuarioGetDto>(u));
        }

        public async Task<UsuarioGetDto> GetById(int id)
        {
            var usuario = await _repository.GetById(id);

            if (usuario == null)
                throw new NotFoundException(nombreModelo, id);

            else
            {
                var usuariDto = _mapper.Map<UsuarioGetDto>(usuario);
                return usuariDto;
            }
        }

        public async Task<UsuarioGetDto> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioInsertDto);
            await _repository.Add(usuario);
            await _repository.Save();

            var usuarioDto = _mapper.Map<UsuarioGetDto>(usuario);
            return usuarioDto;

        }       

        public async Task<UsuarioGetDto> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _repository.GetById(id);

            if (usuario == null)
                throw new NotFoundException(nombreModelo, id);

            else
            {
                usuario = _mapper.Map(usuarioUpdateDto, usuario);

                _repository.Update(usuario);
                await _repository.Save();

                var usuarioDto = _mapper.Map<UsuarioGetDto>(usuario);
                return usuarioDto;
            }
        }
    }
}

