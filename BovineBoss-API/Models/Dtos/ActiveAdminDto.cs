namespace BovineBoss_API.Models.Dtos
{
    public class ActiveAdminDto
    {
        public string NombrePersona { get; set; } = null!;

        public string ApellidoPersona { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string NombreFinca { get; set; } = null!;

        public bool EstadoAdministrador { get; set; }

    }
}
