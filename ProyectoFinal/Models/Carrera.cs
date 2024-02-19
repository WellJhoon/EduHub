using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            CarrerasMateria = new HashSet<CarrerasMateria>();
            Estudiantes = new HashSet<Estudiante>();
        }

        public int CarreraId { get; set; }
        public string NombreCarrera { get; set; } = null!;

        public virtual ICollection<CarrerasMateria> CarrerasMateria { get; set; }
        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}
