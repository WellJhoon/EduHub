using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using static ProyectoFinal.DTOs.StudentDTO;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly EduAsyncHubContext _context;

        public TeacherService(EduAsyncHubContext dbContext)
        {
            _context = dbContext;
        }

        public async Task TeachMatterSubject(TeachMatterRequestDto teachMatter)
        {

            var teacherSubject = new ProfesorMaterium
            {
                ProfesorId = teachMatter.ProfesorId,
                MateriaId = teachMatter.MateriaId
            };


            _context.ProfesorMateria.Add(teacherSubject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<object>> AllSubjectsTaught(AllSubjectsTaughtRequestDto teacher)
        {
            var subjectsTaught = await _context.ProfesorMateria
                .Where(pm => pm.ProfesorId == teacher.ProfesorId)
                .Select(pm => new
                {
                    materiaId = pm.Materia.MateriaId,
                    nombreMateria = pm.Materia.NombreMateria
                })
                .ToListAsync();

            return subjectsTaught.Cast<object>().ToList();
        }

        public async Task CreateTask(TaskPublishRequestDto newTask)
        {
            var task = new Asignacione
            {
                MateriaId = newTask.MateriaId,
                ProfesorId = newTask.ProfesorId,
                Titulo = newTask.Titulo,
                Descripcion = newTask.Descripcion,
                FechaVencimiento = newTask.FechaVencimiento
            };

            _context.Asignaciones.Add(task);
            await _context.SaveChangesAsync();
        }

    }
}
