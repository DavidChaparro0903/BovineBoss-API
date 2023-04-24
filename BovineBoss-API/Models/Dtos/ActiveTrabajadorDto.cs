namespace BovineBoss_API.Models.Dtos
{
    public class ActiveTrabajadorDto
    {
        public string NombrePersona { get; set; } = null!;

        public string ApellidoPersona { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string NombreFinca { get; set; } = null!;

        public bool EstadoTrabajador { get; set; }
    }
}
