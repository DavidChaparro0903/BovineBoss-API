namespace BovineBoss_API.Models.Dtos
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string NombrePersona { get; set; } = null!;

        public string ApellidoPersona { get; set; } = null!;

        public string Cedula { get; set; } = null!;
    }
}
