using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Forum
    {
        public int ForoId { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int MateriaId { get; set; }
        public int ProfesorId { get; set;}
        public string Descripcion { get; set; }

        public int Id { get; set; }

      

    }
}
