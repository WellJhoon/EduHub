namespace ProyectoFinal.DTOs
{
    public class TeacherDTO
    {
        public class AllSubjectsTaughtRequestDto
        {
            public int ProfesorId { get; set; }

        }

        public class TeachMatterRequestDto
        {
            public int ProfesorId { get; set; }
            public int MateriaId { get; set; }

        }

        public class TaskPublishRequestDto
        {
            public int MateriaId { get; set; }
            public int ProfesorId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }

            public DateTime? FechaVencimiento { get; set; }
        }
    }
}
