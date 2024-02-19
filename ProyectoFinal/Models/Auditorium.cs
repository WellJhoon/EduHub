using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Auditorium
    {
        public int AuditoriaId { get; set; }
        public int? UsuarioId { get; set; }
        public string Accion { get; set; } = null!;
        public DateTime? Fecha { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
