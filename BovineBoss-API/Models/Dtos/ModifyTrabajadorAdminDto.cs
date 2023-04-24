namespace BovineBoss_API.Models.Dtos
{
    public class ModifyTrabajadorAdminDto
    {
        public int Id { get; set; }

        public string? NombrePersona { get; set; } = null!;

        public string? ApellidoPersona { get; set; } = null!;

        public string? Cedula { get; set; } = null!;

        public string? TelefonoPersona { get; set; }

        public string? Usuario { get; set; }

        public string? Contrasenia { get; set; } = null!;

       // public int idFincaNuevo { get; set; }

        public int? Salario { get; set; }

    }
}
