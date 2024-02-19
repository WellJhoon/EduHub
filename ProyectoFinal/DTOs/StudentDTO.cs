namespace ProyectoFinal.DTOs
{
    public class StudentDTO
    {
        public class EnrollCareerStudentRequestDto
        {
            public int EstudianteId { get; set; }
            public int CarreraId { get; set; }

        }

        public class EnrollSubjectStudentRequestDto
        {
            public int EstudianteId { get; set; }
            public int MateriaId { get; set; }

        }

         public class AllSubjectsStudentRequestDto
         {
           public int EstudianteId { get; set; }

         }
        public class AssignmentDto
        {
            public int AsignacionId { get; set; }
            public int? MateriaId { get; set; }
            public int? ProfesorId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public DateTime? FechaVencimiento { get; set; }
        }


    }
}
