using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Asignacione
    {
        public Asignacione()
        {
            RespuestasEstudiantes = new HashSet<RespuestasEstudiante>();
        }

        public int AsignacionId { get; set; }
        public int? MateriaId { get; set; }
        public int? ProfesorId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public virtual Materia? Materia { get; set; }
        public virtual Profesore? Profesor { get; set; }
        public virtual ICollection<RespuestasEstudiante> RespuestasEstudiantes { get; set; }
    }
}
