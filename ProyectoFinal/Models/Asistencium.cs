using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Asistencium
    {
        public int AsistenciaId { get; set; }
        public int? EstudianteId { get; set; }
        public int? MateriaId { get; set; }
        public int? ProfesorId { get; set; }
        public DateTime FechaAsistencia { get; set; }
        public bool? Asistio { get; set; }

        public virtual Estudiante? Estudiante { get; set; }
        public virtual Materia? Materia { get; set; }
        public virtual Profesore? Profesor { get; set; }
    }
}
