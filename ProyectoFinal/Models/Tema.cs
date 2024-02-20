namespace ProyectoFinal.Models
{
    public class Tema
    {
        public int TemaId { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int ForoId { get; set; }
        public Forum Foro { get; set; }

        public List<Mensaje> Mensajes { get; set; }
    }
}
