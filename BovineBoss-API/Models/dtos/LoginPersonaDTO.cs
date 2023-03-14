namespace BovineBoss_API.Models.Dtos
{
    public class LoginPersonaDTO
    {
        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public String NombreUsuario { get; set; }
        public String RolPersona { get; set; }

        public LoginPersonaDTO(int idPersona, string nombrePersona, string nombreUsuario, string rolPersona)
        {
            IdPersona = idPersona;
            NombrePersona = nombrePersona;
            NombreUsuario = nombreUsuario;
            RolPersona = rolPersona;
        }
    }
}
