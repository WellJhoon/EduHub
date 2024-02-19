using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static ProyectoFinal.DTOs.UsuarioDTO;

namespace ProyectoFinal.Services
{
    public class UserService : IUserService
    {
        private readonly EduAsyncHubContext _context;
        private readonly string Key;


        public UserService(EduAsyncHubContext dbContext, IConfiguration config)
        {
            _context = dbContext;
            Key = config.GetSection("settings:Key").Value;

        }

        public async Task RegisterUser(RegisterUserRequestDto usuario)
        {
            usuario.Contraseña = ConvertSha256(usuario.Contraseña);

            var user = new Usuario
            {
                Nombre = usuario.Nombre,
                CorreoElectronico = usuario.CorreoElectronico,
                Contraseña = usuario.Contraseña,
                RolId = usuario.RolID,
                Permisos = false,
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            int id = await _context.Usuarios.Where(x => x.CorreoElectronico == usuario.CorreoElectronico).Select(x => x.UsuarioId).FirstOrDefaultAsync();

            if(usuario.RolID == 1)
            {
                var stundent = new Estudiante
                {
                    UsuarioId = id,
                    CarreraId = 6
                };

                _context.Estudiantes.Add(stundent);
            }

            else if (usuario.RolID == 2)
            {
                var teacher = new Profesore
                {
                    UsuarioId = id,
                };

                _context.Profesores.Add(teacher);

            }

            await _context.SaveChangesAsync();

        }

        public async Task<(bool, string)> LoginUser(LoginUserRequestDto request)
        {
            var claveEncriptada = ConvertSha256(request.Contraseña);

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .Where(u => u.CorreoElectronico == request.CorreoElectronico && u.Contraseña == claveEncriptada)
                .FirstOrDefaultAsync(); 

            if (usuario != null)
            {
                var keyBytes = Encoding.UTF8.GetBytes(Key);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()));

                claims.AddClaim(new Claim(ClaimTypes.Name, usuario.CorreoElectronico));

                var rolNombre = await _context.Roles.Where(r => r.RolId == usuario.RolId).Select(r => r.NombreRol).FirstOrDefaultAsync();
                claims.AddClaim(new Claim(ClaimTypes.Role, rolNombre));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return (true, tokenCreado);
            }
            else 
            {
                return (false, "Login inválido");
            }
        }

        private string ConvertSha256(string inputString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
