using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProyectoFinal.Context;
using ProyectoFinal.DTOs;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static ProyectoFinal.DTOs.UsuarioDTO;

namespace ProyectoFinal.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task RegisterUser_ShouldAddUserAndStudent_WhenRoleIsStudent()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var configMock = new Mock<IConfiguration>();
            var userService = new UserService(dbContextMock.Object, configMock.Object);
            var registerUserRequest = new RegisterUserRequestDto
            {
                Nombre = "Test User",
                CorreoElectronico = "test@example.com",
                Contraseña = "password",
                RolID = 1
            };

            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask); // Configuración de SaveChangesAsync()

            // Act
            await userService.RegisterUser(registerUserRequest);

            // Assert
            dbContextMock.Verify(x => x.Usuarios.Add(It.IsAny<Usuario>()), Times.Once);
            dbContextMock.Verify(x => x.Estudiantes.Add(It.IsAny<Estudiante>()), Times.Once);
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task RegisterUser_ShouldAddUserAndTeacher_WhenRoleIsTeacher()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var configMock = new Mock<IConfiguration>();
            var userService = new UserService(dbContextMock.Object, configMock.Object);
            var registerUserRequest = new RegisterUserRequestDto
            {
                Nombre = "Test Teacher",
                CorreoElectronico = "teacher@example.com",
                Contraseña = "password",
                RolID = 2
            };

            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask); // Configuración de SaveChangesAsync()

            // Act
            await userService.RegisterUser(registerUserRequest);

            // Assert
            dbContextMock.Verify(x => x.Usuarios.Add(It.IsAny<Usuario>()), Times.Once);
            dbContextMock.Verify(x => x.Profesores.Add(It.IsAny<Profesore>()), Times.Once);
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task LoginUser_ShouldReturnTrueAndToken_WhenValidCredentials()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var configMock = new Mock<IConfiguration>();
            var userService = new UserService(dbContextMock.Object, configMock.Object);
            var loginUserRequest = new LoginUserRequestDto
            {
                CorreoElectronico = "test@example.com",
                Contraseña = "password"
            };
            var usuario = new Usuario
            {
                UsuarioId = 1,
                Nombre = "Test User",
                CorreoElectronico = "test@example.com",
                Contraseña = "password",
                RolId = 1
            };
            var role = new Rol
            {
                RolId = 1,
                NombreRol = "Estudiante"
            };

            dbContextMock.Setup(x => x.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.CorreoElectronico == loginUserRequest.CorreoElectronico && u.Contraseña == It.IsAny<string>())).ReturnsAsync(usuario);
            dbContextMock.Setup(x => x.Roles.Where(r => r.RolId == usuario.RolId).Select(r => r.NombreRol).FirstOrDefaultAsync()).ReturnsAsync(role.NombreRol);

            // Act
            var result = await userService.LoginUser(loginUserRequest);

            // Assert
            Assert.True(result.Item1);
            Assert.NotNull(result.Item2);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnFalseAndErrorMessage_WhenInvalidCredentials()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var configMock = new Mock<IConfiguration>();
            var userService = new UserService(dbContextMock.Object, configMock.Object);
            var loginUserRequest = new LoginUserRequestDto
            {
                CorreoElectronico = "test@example.com",
                Contraseña = "password"
            };

            dbContextMock.Setup(x => x.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.CorreoElectronico == loginUserRequest.CorreoElectronico && u.Contraseña == It.IsAny<string>())).ReturnsAsync((Usuario)null);

            // Act
            var result = await userService.LoginUser(loginUserRequest);

            // Assert
            Assert.False(result.Item1);
            Assert.Equal("Login inválido", result.Item2);
        }
    }
}
