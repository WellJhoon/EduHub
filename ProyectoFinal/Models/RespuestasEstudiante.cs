using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class RespuestasEstudiante
    {
        public int RespuestaId { get; set; }
        public int? EstudianteId { get; set; }
        public int? AsignacionId { get; set; }
        public string? Respuesta { get; set; }
        public double? Calificacion { get; set; }
        public string? ComentariosProfesor { get; set; }

        public virtual Asignacione? Asignacion { get; set; }
        public virtual Estudiante? Estudiante { get; set; }
    }
}
