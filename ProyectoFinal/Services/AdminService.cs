using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using static ProyectoFinal.DTOs.UsuarioDTO;

namespace ProyectoFinal.Services
{
    public class AdminService : IAdminService
    {
        private readonly EduAsyncHubContext _context;

        public AdminService(EduAsyncHubContext dbContext)
        {
            _context = dbContext;
        }

        public async Task EditAnyUserAdmin(int id, UpdateUserRequestDto usuario)
        {
            var user = await _context.Usuarios.FindAsync(id);

            user.Nombre = usuario.Nombre;
            user.CorreoElectronico = usuario.CorreoElectronico;
            user.Contraseña = usuario.Contraseña;
            user.FotoPerfil = usuario.pfp;
            user.DescripcionBreve = usuario.DescripcionBreve;
            user.Intereses = usuario.Intereses;
            user.Habilidades = usuario.Habilidades;
            user.ConfiguracionPrivacidad = usuario.ConfiguracionPrivacidad;
            user.ConfiguracionNotificaciones = usuario.ConfiguracionNotificaciones;
            user.RolId = usuario.RolID;
            user.Permisos = usuario.Permisos;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnyUserAdmin(DeleteUserRequestDto usuario)
        {
            var user = await _context.Usuarios.FindAsync(usuario.UserId);

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task AsignPermissions(AssignPermissionsUserRequestDto permissionsRequest)
        {
            permissionsRequest.Permissions = true;

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == permissionsRequest.UserId);

            user.Permisos = permissionsRequest.Permissions;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }

        public async Task<List<Usuario>> GetEstudiantes()
        {
            var estudiantes = await _context.Usuarios
                .Where(u => u.Rol.NombreRol == "Estudiante")
                .ToListAsync();

            return estudiantes;
        }

        public async Task<List<Usuario>> GetProfesores()
        {
            var estudiantes = await _context.Usuarios
                .Where(u => u.Rol.NombreRol == "Profesor")
                .ToListAsync();

            return estudiantes;
        }

    }
}
