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



    }
}
