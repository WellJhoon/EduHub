using Microsoft.EntityFrameworkCore;
using Moq;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static ProyectoFinal.DTOs.UsuarioDTO;

namespace ProyectoFinal.Tests
{
    public class AdminServiceTests
    {
        [Fact]
        public async Task EditAnyUserAdmin_ShouldUpdateUser_WhenValidData()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new AdminService(dbContextMock.Object);
            var userId = 1;
            var updateUserRequest = new UpdateUserRequestDto
            {
                Nombre = "Updated Name",
                CorreoElectronico = "updated@example.com",
                Contraseña = "updatedpassword",
                pfp = "updated_profile_picture.jpg",
                DescripcionBreve = "Updated description",
                Intereses = "Updated interests",
                Habilidades = "Updated skills",
                ConfiguracionPrivacidad = "Updated privacy configuration",
                ConfiguracionNotificaciones = "Updated notification configuration",
                RolID = 2,
                Permisos = true
            };

            var user = new Usuario { UsuarioId = userId };
            dbContextMock.Setup(x => x.Usuarios.FindAsync(userId)).ReturnsAsync(user);
            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await service.EditAnyUserAdmin(userId, updateUserRequest);

            // Assert
            Assert.Equal(updateUserRequest.Nombre, user.Nombre);
            Assert.Equal(updateUserRequest.CorreoElectronico, user.CorreoElectronico);
            Assert.Equal(updateUserRequest.Contraseña, user.Contraseña);
            Assert.Equal(updateUserRequest.pfp, user.FotoPerfil);
            Assert.Equal(updateUserRequest.DescripcionBreve, user.DescripcionBreve);
            Assert.Equal(updateUserRequest.Intereses, user.Intereses);
            Assert.Equal(updateUserRequest.Habilidades, user.Habilidades);
            Assert.Equal(updateUserRequest.ConfiguracionPrivacidad, user.ConfiguracionPrivacidad);
            Assert.Equal(updateUserRequest.ConfiguracionNotificaciones, user.ConfiguracionNotificaciones);
            Assert.Equal(updateUserRequest.RolID, user.RolId);
            Assert.Equal(updateUserRequest.Permisos, user.Permisos);
        }

        [Fact]
        public async Task DeleteAnyUserAdmin_ShouldRemoveUser_WhenValidData()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new AdminService(dbContextMock.Object);
            var userId = 1;
            var deleteUserRequest = new DeleteUserRequestDto { UserId = userId };

            var user = new Usuario { UsuarioId = userId };
            dbContextMock.Setup(x => x.Usuarios.FindAsync(userId)).ReturnsAsync(user);
            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await service.DeleteAnyUserAdmin(deleteUserRequest);

            // Assert
            dbContextMock.Verify(x => x.Usuarios.Remove(user), Times.Once);
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AsignPermissions_ShouldUpdatePermissions_WhenValidData()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new AdminService(dbContextMock.Object);
            var permissionsRequest = new AssignPermissionsUserRequestDto
            {
                UserId = 1,
                Permissions = true
            };

            var user = new Usuario { UsuarioId = 1 };
            dbContextMock.Setup(x => x.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == permissionsRequest.UserId)).ReturnsAsync(user);
            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await service.AsignPermissions(permissionsRequest);

            // Assert
            Assert.True(user.Permisos);
        }

        [Fact]
        public async Task GetAllUsuarios_ShouldReturnAllUsuariosFromContext()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new AdminService(dbContextMock.Object);
            var expectedUsuarios = new List<Usuario>
            {
                new Usuario { UsuarioId = 1, Nombre = "User 1" },
                new Usuario { UsuarioId = 2, Nombre = "User 2" }
            };
            dbContextMock.Setup(x => x.Usuarios.ToListAsync()).ReturnsAsync(expectedUsuarios);

            // Act
            var actualUsuarios = await service.GetAllUsuarios();

            // Assert
            Assert.Equal(expectedUsuarios, actualUsuarios);
        }

        [Fact]
        public async Task GetEstudiantes_ShouldReturnEstudiantesFromContext()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new AdminService(dbContextMock.Object);
            var expectedEstudiantes = new List<Usuario>
            {
                new Usuario { UsuarioId = 1, Nombre = "Estudiante 1", RolId = 1, Rol = new Rol { NombreRol = "Estudiante" } },
                new Usuario { UsuarioId = 2, Nombre = "Estudiante 2", RolId = 1, Rol = new Rol { NombreRol = "Estudiante" } }
            };
            dbContextMock.Setup(x => x.Usuarios.Where(u => u.Rol.NombreRol == "Estudiante").ToListAsync()).ReturnsAsync(expectedEstudiantes);

            // Act
            var actualEstudiantes = await service.GetEstudiantes();

            // Assert
            Assert.Equal(expectedEstudiantes, actualEstudiantes);
        }

        [Fact]
        public async Task GetProfesores_ShouldReturnProfesoresFromContext()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new AdminService(dbContextMock.Object);
            var expectedProfesores = new List<Usuario>
            {
                new Usuario { UsuarioId = 1, Nombre = "Profesor 1", RolId = 2, Rol = new Rol { NombreRol = "Profesor" } },
                new Usuario { UsuarioId = 2, Nombre = "Profesor 2", RolId = 2, Rol = new Rol { NombreRol = "Profesor" } }
            };
            dbContextMock.Setup(x => x.Usuarios.Where(u => u.Rol.NombreRol == "Profesor").ToListAsync()).ReturnsAsync(expectedProfesores);

            // Act
            var actualProfesores = await service.GetProfesores();

            // Assert
            Assert.Equal(expectedProfesores, actualProfesores);
        }
    }
}
