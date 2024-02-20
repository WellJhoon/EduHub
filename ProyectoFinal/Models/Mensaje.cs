namespace ProyectoFinal.Models
{
    public partial class Mensaje
    {
        public int MensajeId { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leido { get; set; }
        public int RemitenteId { get; set; }
        public int DestinatarioId { get; set; }

        public Estudiante Remitente { get; set; }
        public Estudiante Destinatario { get; set; }
    }
}
