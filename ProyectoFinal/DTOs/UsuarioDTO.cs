namespace ProyectoFinal.DTOs
{
    public class UsuarioDTO
    {
        public class RegisterUserRequestDto
        {
            public string Nombre { get; set; }
            public string CorreoElectronico { get; set; }
            public string Contraseña { get; set; }
            public int RolID { get; set; }

        }

        public class LoginUserRequestDto
        {
            public string CorreoElectronico { get; set; }
            public string Contraseña { get; set; }
        }

        public class UpdateUserRequestDto
        {
            public string Nombre { get; set; }
            public string CorreoElectronico { get; set; }
            public string Contraseña { get; set; }
            public string pfp { get; set; }
            public string DescripcionBreve { get; set; }
            public string Intereses { get; set; }
            public string Habilidades { get; set; }
            public bool ConfiguracionPrivacidad { get; set; }
            public bool ConfiguracionNotificaciones { get; set; }
            public int RolID { get; set; }
            public bool Permisos { get; set; }
        }

        public class DeleteUserRequestDto
        {
           public int UserId { get; set; }
        }

        public class AssignPermissionsUserRequestDto
        {
            public int UserId { get; set; }
            public bool Permissions { get; set; }
        }

    }
}
