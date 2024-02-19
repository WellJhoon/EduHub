using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Asignaciones = new HashSet<Asignacione>();
            Asistencia = new HashSet<Asistencium>();
            Calificaciones = new HashSet<Calificacione>();
            CarrerasMateria = new HashSet<CarrerasMateria>();
            EstudianteMateria = new HashSet<EstudianteMaterium>();
            ProfesorMateria = new HashSet<ProfesorMaterium>();
        }

        public int MateriaId { get; set; }
        public string NombreMateria { get; set; } = null!;

        public virtual ICollection<Asignacione> Asignaciones { get; set; }
        public virtual ICollection<Asistencium> Asistencia { get; set; }
        public virtual ICollection<Calificacione> Calificaciones { get; set; }
        public virtual ICollection<CarrerasMateria> CarrerasMateria { get; set; }
        public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; }
        public virtual ICollection<ProfesorMaterium> ProfesorMateria { get; set; }
    }
}
