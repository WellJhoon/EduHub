using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Context;
using ProyectoFinal.Services;
using ProyectoFinal.Models;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Tests
{
    public class TeacherServiceTests
    {
        [Fact]
        public async Task CreateSubject_ShouldAddSubjectToContext_WhenValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                var teacherService = new TeacherService(context);
                var subjectDto = new CreateSubjectRequestDto { Nombre = "Test Subject" };

                // Act
                await teacherService.CreateSubject(subjectDto);

                // Assert
                var createdSubject = await context.Materias.FirstOrDefaultAsync(m => m.NombreMateria == "Test Subject");
                Assert.NotNull(createdSubject);
            }
        }

        [Fact]
        public async Task TeachMatterSubject_ShouldAddToContext_WhenValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                var teacherService = new TeacherService(context);
                var teachMatter = new TeachMatterRequestDto { ProfesorId = 1, MateriaId = 2 };

                // Act
                await teacherService.TeachMatterSubject(teachMatter);

                // Assert
                var createdProfesorMateria = await context.ProfesorMateria.FirstOrDefaultAsync();
                Assert.NotNull(createdProfesorMateria);
                Assert.Equal(teachMatter.ProfesorId, createdProfesorMateria.ProfesorId);
                Assert.Equal(teachMatter.MateriaId, createdProfesorMateria.MateriaId);
            }
        }

        [Fact]
        public async Task AllSubjectsTaught_ShouldReturnCorrectData_WhenValidTeacherId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                context.ProfesorMateria.AddRange(
                    new ProfesorMaterium { ProfesorId = 1, MateriaId = 1 },
                    new ProfesorMaterium { ProfesorId = 1, MateriaId = 2 }
                );
                await context.SaveChangesAsync();

                var teacherService = new TeacherService(context);
                var teacher = new AllSubjectsTaughtRequestDto { ProfesorId = 1 };

                // Act
                var subjectsTaught = await teacherService.AllSubjectsTaught(teacher);

                // Assert
                Assert.Equal(2, subjectsTaught.Count);
            }
        }

        [Fact]
        public async Task CreateTask_ShouldAddTaskToContext_WhenValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                var teacherService = new TeacherService(context);
                var newTask = new TaskPublishRequestDto
                {
                    MateriaId = 1,
                    ProfesorId = 1,
                    Titulo = "Test Task",
                    Descripcion = "Test Task Description",
                    FechaVencimiento = DateTime.Now.AddDays(7)
                };

                // Act
                await teacherService.CreateTask(newTask);

                // Assert
                var createdTask = await context.Asignaciones.FirstOrDefaultAsync(t => t.Titulo == "Test Task");
                Assert.NotNull(createdTask);
            }
        }

        [Fact]
        public async Task GetAllSubjects_ShouldReturnAllSubjectsFromContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                context.Materias.AddRange(
                    new Materia { NombreMateria = "Subject 1" },
                    new Materia { NombreMateria = "Subject 2" }
                );
                await context.SaveChangesAsync();

                var teacherService = new TeacherService(context);

                // Act
                var subjects = await teacherService.GetAllSubjects();

                // Assert
                Assert.Equal(2, subjects.Count);
            }
        }

        [Fact]
        public async Task GetSubjectById_ShouldReturnCorrectSubject_WhenValidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                context.Materias.Add(new Materia { MateriaId = 1, NombreMateria = "Test Subject" });
                await context.SaveChangesAsync();

                var teacherService = new TeacherService(context);

                // Act
                var subject = await teacherService.GetSubjectById<Materia>(1);

                // Assert
                Assert.Equal("Test Subject", subject.NombreMateria);
            }
        }

        [Fact]
        public async Task UpdateSubject_ShouldUpdateSubjectInContext_WhenValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                context.Materias.Add(new Materia { MateriaId = 1, NombreMateria = "Original Subject", ProfesorId = 1 });
                await context.SaveChangesAsync();

                var teacherService = new TeacherService(context);
                var updatedSubject = new UpdateSubjectRequestDto { ProfesorId = 2, Descripcion = "Updated Description" };

                // Act
                await teacherService.UpdateSubject(1, updatedSubject);

                // Assert
                var subject = await context.Materias.FindAsync(1);
                Assert.Equal(2, subject.ProfesorId);
                Assert.Equal("Updated Description", subject.Descripcion);
            }
        }

        [Fact]
        public async Task CreateForum_ShouldAddForumToContext_WhenValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                var teacherService = new TeacherService(context);
                var forumDto = new CreateForumRequestDto
                {
                    MateriaId = 1,
                    ProfesorId = 1,
                    Titulo = "Test Forum",
                    Descripcion = "Test Forum Description"
                };

                // Act
                await teacherService.CreateForum(forumDto);

                // Assert
                var createdForum = await context.Forums.FirstOrDefaultAsync(f => f.Titulo == "Test Forum");
                Assert.NotNull(createdForum);
            }
        }

        [Fact]
        public async Task GetAllForums_ShouldReturnAllForumsFromContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                context.Forums.AddRange(
                    new Forum { MateriaId = 1, ProfesorId = 1, Titulo = "Forum 1", Descripcion = "Forum 1 Description" },
                    new Forum { MateriaId = 1, ProfesorId = 1, Titulo = "Forum 2", Descripcion = "Forum 2 Description" }
                );
                await context.SaveChangesAsync();

                var teacherService = new TeacherService(context);

                // Act
                var forums = await teacherService.GetAllForums();

                // Assert
                Assert.Equal(2, forums.Count);
            }
        }

        [Fact]
        public async Task GetForumsByMateria_ShouldReturnForumsForSpecificMateria()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EduAsyncHubContext(options))
            {
                context.Forums.AddRange(
                    new Forum { MateriaId = 1, ProfesorId = 1, Titulo = "Forum 1", Descripcion = "Forum 1 Description" },
                    new Forum { MateriaId = 2, ProfesorId = 1, Titulo = "Forum 2", Descripcion = "Forum 2 Description" }
                );
                await context.SaveChangesAsync();

                var teacherService = new TeacherService(context);

                // Act
                var forums = await teacherService.GetForumsByMateria(1);

                // Assert
                Assert.Equal(1, forums.Count);
            }
        }

    }
}
