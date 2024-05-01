﻿using Application.Pacientes.Command.Create;
using Domain.Entities;
using Domain.Excepcions;
using Domain.Repositorio;
using Domain.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Command.Create
{
    public class UsuarioCreateHandler : IRequestHandler<UsuarioCreateCommand, UsuarioCreateResponse>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioRolRepository _rolRepository;
        public UsuarioCreateHandler(IUsuarioRepository repository, IUsuarioRolRepository rolRepository)
        {
            _repository = repository;
            _rolRepository = rolRepository;
        }

        public async Task<UsuarioCreateResponse> Handle(UsuarioCreateCommand request, CancellationToken cancellationToken)
        {
            var roles = await _rolRepository.GetListRoles();
            var existRol = roles.FirstOrDefault(x => x.IdRol == request.IdRol);
            if (existRol == null)
                throw new NotFoundException($"Rol:{request.IdRol}");

            var usuario = await CreateUsuario(request);
            await _repository.AddUsuarioAsync(usuario);

            return new UsuarioCreateResponse()
            {
                Id = usuario.IdUsuario,
                FechaCreacion = usuario.FechaCreacion
            };
        }
        public async Task<Usuario> CreateUsuario(UsuarioCreateCommand request)
        {
            var rut = new DocumentoIdentidad(request.Rut);
            var email = new ContactoEmail(request.Email);
            var usuario = Usuario.Create(Guid.NewGuid(), rut, request.Nombre, request.Contrasena, email, request.IdRol);
            return usuario;
        }
    }
}
