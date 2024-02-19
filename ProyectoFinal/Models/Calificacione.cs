using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Calificacione
    {
        public int CalificacionId { get; set; }
        public int? EstudianteId { get; set; }
        public int? MateriaId { get; set; }
        public int? ProfesorId { get; set; }
        public double Calificacion { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public virtual Estudiante? Estudiante { get; set; }
        public virtual Materia? Materia { get; set; }
        public virtual Profesore? Profesor { get; set; }
    }
}
