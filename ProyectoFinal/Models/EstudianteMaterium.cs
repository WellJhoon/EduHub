using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class EstudianteMaterium
    {
        public int InscripcionMateriaId { get; set; }
        public int? EstudianteId { get; set; }
        public int? MateriaId { get; set; }

        public virtual Estudiante? Estudiante { get; set; }
        public virtual Materia? Materia { get; set; }
    }
}
