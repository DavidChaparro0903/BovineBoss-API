namespace BovineBoss_API.Models.Dtos
{
    public class CreateEmployeeDto
    {

        public string NombrePersona { get; set; } = null!;

        public string ApellidoPersona { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string? TelefonoPersona { get; set; }

        public int? Salario { get; set; }
        public string? Usuario { get; set; }

        public string? Contrasenia { get; set; }


        // Un empleado siempre va necesitar una finca
        public int IdFinca { get; set; }


    }
}
