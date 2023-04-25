namespace BovineBoss_API.Models.Dtos
{
    public class AddResInconvenientesDto
    {



        public int IdRes { get; set; }
        public int IdInconveniente { get; set; }

  
        public DateTime FechaInconveniente { get; set; }

        public int DineroGastado { get; set; }

        public string DescripcionInconveniente { get; set; } = null!;
    }
}
