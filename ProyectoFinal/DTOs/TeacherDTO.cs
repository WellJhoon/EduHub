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

        public class CreateSubjectRequestDto
        {
            public int ProfesorId { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            // Otros campos necesarios para la creación de la materia
        }

        public class UpdateSubjectRequestDto
        {
            public int MateriaId { get; set; }
            public int ProfesorId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }

            public DateTime? FechaVencimiento { get; set; }
        }

        public class CreateForumRequestDto
        {
            public int MateriaId { get; set; }
            public int ProfesorId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
        }

        public class UpdateForumRequestDto
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
        }

        public class updatedForumDto
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
        }
    }
}
