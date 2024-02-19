using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using static ProyectoFinal.DTOs.StudentDTO;

namespace ProyectoFinal.Services
{
    public class StudentService : IStudentService
    {
        private readonly EduAsyncHubContext _context;

        public StudentService(EduAsyncHubContext dbContext)
        {
            _context = dbContext;
        }

        public async Task EnrollCareerStudent(EnrollCareerStudentRequestDto student)
        {
            var studentSelect = await _context.Estudiantes.FirstOrDefaultAsync(u => u.EstudianteId == student.EstudianteId);
            studentSelect.CarreraId = student.CarreraId;
            await _context.SaveChangesAsync();
        }

        public async Task EnrollSubjectStudent(EnrollSubjectStudentRequestDto student)
        {
            var studentSubject = new EstudianteMaterium
            {
                EstudianteId = student.EstudianteId,
                MateriaId = student.MateriaId
            };

            _context.EstudianteMateria.Add(studentSubject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<object>> SubjectsEnrolledByStudent(AllSubjectsStudentRequestDto student)
        {
            var subjectsEnrolled = await _context.EstudianteMateria
                .Where(em => em.EstudianteId == student.EstudianteId)
                .Select(em => new
                {
                    materiaId = em.Materia.MateriaId,
                    nombreMateria = em.Materia.NombreMateria
                })
                .ToListAsync();

            return subjectsEnrolled.Cast<object>().ToList();
        }

        public async Task<List<object>> GetAllAssignmentsForStudent(AllSubjectsStudentRequestDto student)
        {
            var assignments = await _context.EstudianteMateria
                .Where(em => em.EstudianteId == student.EstudianteId &&
                             em.Materia.Asignaciones.Any())
                .Select(em => new
                {
                    MateriaNombre = em.Materia.NombreMateria,
                    Asignaciones = em.Materia.Asignaciones
                        .Select(a => new
                        {
                            Titulo = a.Titulo,
                            Descripcion = a.Descripcion,
                            FechaPublicacion = a.FechaPublicacion,
                            FechaVencimiento = a.FechaVencimiento
                        })
                        .ToList()
                })
                .ToListAsync();

            return assignments.Cast<object>().ToList();
        }

        public async Task<bool> SubmitAssignment(AssignmentDto assignmentDto)
        {
            try
            {
                var newAssignment = new Asignacione
                {
                    MateriaId = assignmentDto.MateriaId,
                    ProfesorId = assignmentDto.ProfesorId,
                    Titulo = assignmentDto.Titulo,
                    Descripcion = assignmentDto.Descripcion,
                    FechaPublicacion = assignmentDto.FechaPublicacion,
                    FechaVencimiento = assignmentDto.FechaVencimiento
                };

                _context.Asignaciones.Add(newAssignment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar la asignación: {ex.Message}");
                return false;
            }
        }
    }
}
