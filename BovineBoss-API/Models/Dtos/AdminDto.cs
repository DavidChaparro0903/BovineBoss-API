namespace BovineBoss_API.Models.Dtos
{
    public class AdminDto
    { 
        public string NombrePersona { get; set; } = null!;

        public string ApellidoPersona { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public int? Salario { get; set; }

        public DateTime? FechaContratacion { get; set; }

        public string? Usuario { get; set; }

    }
}
