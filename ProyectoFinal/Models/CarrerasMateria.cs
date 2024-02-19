using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class CarrerasMateria
    {
        public int MateriaCarreraId { get; set; }
        public int? CarreraId { get; set; }
        public int? MateriaId { get; set; }

        public virtual Carrera? Carrera { get; set; }
        public virtual Materia? Materia { get; set; }
    }
}
