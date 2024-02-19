using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly EduAsyncHubContext _context;

        public TeacherService(EduAsyncHubContext context)
        {
            _context = context;
        }

        public async Task CreateSubject(CreateSubjectRequestDto subjectDto)
        {
            try
            {
                if (string.IsNullOrEmpty(subjectDto.Nombre))
                {
                    throw new ArgumentException("El nombre de la materia es requerido.", nameof(subjectDto.Nombre));
                }

                var nuevaMateria = new Materia
                {
                    NombreMateria = subjectDto.Nombre,
                };

                await _context.Materias.AddAsync(nuevaMateria);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la materia. Detalles: " + ex.Message);
            }
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

        public async Task<List<Materia>> GetAllSubjects()
        {
            return await _context.Materias.ToListAsync();
        }

        public async Task<T> GetSubjectById<T>(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            return (T)Convert.ChangeType(materia, typeof(T));
        }

        public async Task<List<T>> GetAllSubjects<T>()
        {
            var materias = await _context.Materias.ToListAsync();
            return materias.Cast<T>().ToList();
        }

        public async Task UpdateSubject(int id, UpdateSubjectRequestDto updatedSubject)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                throw new ArgumentException($"No se encontró la materia con ID {id}");
            }
            materia.ProfesorId = updatedSubject.ProfesorId;
            materia.Descripcion = updatedSubject.Descripcion;
            _context.Materias.Update(materia);
            await _context.SaveChangesAsync();
        }

    }
}
