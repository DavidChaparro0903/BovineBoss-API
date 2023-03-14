namespace BovineBoss_API.Models.Dtos
{
    public class LoginPersonaDTO
    {
        public int IdPersona { get; set; }
        public string RolPersona { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
    }
}
