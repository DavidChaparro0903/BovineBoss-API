namespace BovineBoss_API.Models.Dtos
{
    public class InconvenienteResDto
    {
        public int IdInconveniente { get; set; }

        public string NombreInconveniente { get; set; } = null!;

        public DateTime FechaInconveniente { get; set; }

        public int DineroGastado { get; set; }

        public string DescripcionInconveniente { get; set; } = null!;

    }
}
