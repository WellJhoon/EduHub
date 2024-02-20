using Microsoft.EntityFrameworkCore;
using Moq;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static ProyectoFinal.DTOs.StudentDTO;

namespace ProyectoFinal.Tests
{
    public class StudentServiceTests
    {
        [Fact]
        public async Task EnrollCareerStudent_ShouldUpdateCareer_WhenValidData()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new StudentService(dbContextMock.Object);
            var studentId = 1;
            var careerId = 100;
            var student = new Estudiante { EstudianteId = studentId };
            var enrollCareerRequest = new EnrollCareerStudentRequestDto
            {
                EstudianteId = studentId,
                CarreraId = careerId
            };

            dbContextMock.Setup(x => x.Estudiantes.FirstOrDefaultAsync(u => u.EstudianteId == studentId)).ReturnsAsync(student);
            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask); // Configuración de SaveChangesAsync()

            // Act
            await service.EnrollCareerStudent(enrollCareerRequest);

            // Assert
            Assert.Equal(careerId, student.CarreraId);
        }

        [Fact]
        public async Task EnrollSubjectStudent_ShouldEnrollSubject_WhenValidData()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new StudentService(dbContextMock.Object);
            var studentId = 1;
            var subjectId = 200;
            var enrollSubjectRequest = new EnrollSubjectStudentRequestDto
            {
                EstudianteId = studentId,
                MateriaId = subjectId
            };

            dbContextMock.Setup(x => x.EstudianteMateria.Add(It.IsAny<EstudianteMaterium>()));
            dbContextMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask); // Configuración de SaveChangesAsync()

            // Act
            await service.EnrollSubjectStudent(enrollSubjectRequest);

            // Assert
            dbContextMock.Verify(x => x.EstudianteMateria.Add(It.IsAny<EstudianteMaterium>()), Times.Once);
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SubjectsEnrolledByStudent_ShouldReturnEnrolledSubjects_WhenValidStudentId()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new StudentService(dbContextMock.Object);
            var studentId = 1;
            var studentRequest = new AllSubjectsStudentRequestDto { EstudianteId = studentId };
            var enrolledSubjects = new List<EstudianteMaterium>
            {
                new EstudianteMaterium { EstudianteId = studentId, Materia = new Materia { MateriaId = 1, NombreMateria = "Subject 1" } },
                new EstudianteMaterium { EstudianteId = studentId, Materia = new Materia { MateriaId = 2, NombreMateria = "Subject 2" } }
            };
            dbContextMock.Setup(x => x.EstudianteMateria.Where(em => em.EstudianteId == studentId)).Returns(enrolledSubjects.AsQueryable());

            // Act
            var result = await service.SubjectsEnrolledByStudent(studentRequest);

            // Assert
            Assert.Equal(enrolledSubjects.Select(em => new { materiaId = em.Materia.MateriaId, nombreMateria = em.Materia.NombreMateria }), result);
        }

        [Fact]
        public async Task GetAllAssignmentsForStudent_ShouldReturnAssignments_WhenValidStudentId()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new StudentService(dbContextMock.Object);
            var studentId = 1;
            var studentRequest = new AllSubjectsStudentRequestDto { EstudianteId = studentId };
            var assignments = new List<EstudianteMaterium>
            {
                new EstudianteMaterium
                {
                    EstudianteId = studentId,
                    Materia = new Materia
                    {
                        MateriaId = 1,
                        NombreMateria = "Subject 1",
                        Asignaciones = new List<Asignacione>
                        {
                            new Asignacione { Titulo = "Assignment 1", Descripcion = "Description 1", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(7) }
                        }
                    }
                },
                new EstudianteMaterium
                {
                    EstudianteId = studentId,
                    Materia = new Materia
                    {
                        MateriaId = 2,
                        NombreMateria = "Subject 2",
                        Asignaciones = new List<Asignacione>
                        {
                            new Asignacione { Titulo = "Assignment 2", Descripcion = "Description 2", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(7) },
                            new Asignacione { Titulo = "Assignment 3", Descripcion = "Description 3", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(7) }
                        }
                    }
                }
            };
            dbContextMock.Setup(x => x.EstudianteMateria.Where(em => em.EstudianteId == studentId && em.Materia.Asignaciones.Any())).Returns(assignments.AsQueryable());

            // Act
            var result = await service.GetAllAssignmentsForStudent(studentRequest);

            // Assert
            var expectedResults = assignments.Select(em => new
            {
                MateriaNombre = em.Materia.NombreMateria,
                Asignaciones = em.Materia.Asignaciones.Select(a => new { Titulo = a.Titulo, Descripcion = a.Descripcion, FechaPublicacion = a.FechaPublicacion, FechaVencimiento = a.FechaVencimiento }).ToList()
            });
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public async Task SubmitAssignment_ShouldReturnTrue_WhenAssignmentIsSubmittedSuccessfully()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new StudentService(dbContextMock.Object);
            var assignmentDto = new AssignmentDto { MateriaId = 1, ProfesorId = 1, Titulo = "Assignment", Descripcion = "Description", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(7) };

            // Act
            var result = await service.SubmitAssignment(assignmentDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetPendingAssignmentsForStudent_ShouldReturnPendingAssignments_WhenValidStudentId()
        {
            // Arrange
            var dbContextMock = new Mock<EduAsyncHubContext>();
            var service = new StudentService(dbContextMock.Object);
            var studentId = 1;
            var studentRequest = new AllSubjectsStudentRequestDto { EstudianteId = studentId };
            var currentDate = DateTime.Now;
            var pendingAssignments = new List<Asignacione>
            {
                new Asignacione { AsignacionId = 1, MateriaId = 1, ProfesorId = 1, Titulo = "Assignment 1", Descripcion = "Description 1", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(7) },
                new Asignacione { AsignacionId = 2, MateriaId = 2, ProfesorId = 2, Titulo = "Assignment 2", Descripcion = "Description 2", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(3) },
                new Asignacione { AsignacionId = 3, MateriaId = 1, ProfesorId = 1, Titulo = "Assignment 3", Descripcion = "Description 3", FechaPublicacion = DateTime.Now, FechaVencimiento = DateTime.Now.AddDays(-1) }
            };
            var studentSubjects = new List<EstudianteMaterium>
            {
                new EstudianteMaterium { EstudianteId = studentId, Materia = new Materia { MateriaId = 1, Asignaciones = pendingAssignments } },
                new EstudianteMaterium { EstudianteId = studentId, Materia = new Materia { MateriaId = 2, Asignaciones = pendingAssignments.Take(1).ToList() } }
            };
            dbContextMock.Setup(x => x.EstudianteMateria.Where(em => em.EstudianteId == studentId)).Returns(studentSubjects.AsQueryable());
            dbContextMock.Setup(x => x.EstudianteMateria.SelectMany(em => em.Materia.Asignaciones)).Returns(pendingAssignments.AsQueryable());

            // Act
            var result = await service.GetPendingAssignmentsForStudent(studentRequest);

            // Assert
            var expectedResults = pendingAssignments.Where(a => a.FechaVencimiento >= currentDate).Select(a => new { AsignacionId = a.AsignacionId, MateriaId = a.MateriaId, ProfesorId = a.ProfesorId, Titulo = a.Titulo, Descripcion = a.Descripcion, FechaPublicacion = a.FechaPublicacion, FechaVencimiento = a.FechaVencimiento });
            Assert.Equal(expectedResults, result);
        }
    }
}
