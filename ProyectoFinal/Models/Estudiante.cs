using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            Asistencia = new HashSet<Asistencium>();
            Calificaciones = new HashSet<Calificacione>();
            EstudianteMateria = new HashSet<EstudianteMaterium>();
            RespuestasEstudiantes = new HashSet<RespuestasEstudiante>();
        }

        public int EstudianteId { get; set; }
        public int? UsuarioId { get; set; }
        public int? CarreraId { get; set; }

        public virtual Carrera? Carrera { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public virtual ICollection<Asistencium> Asistencia { get; set; }
        public virtual ICollection<Calificacione> Calificaciones { get; set; }
        public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; }
        public virtual ICollection<RespuestasEstudiante> RespuestasEstudiantes { get; set; }
    }
}
