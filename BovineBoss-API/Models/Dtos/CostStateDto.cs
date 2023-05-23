namespace BovineBoss_API.Models.Dtos
{
    public class CostStateDto
    {
        public int IdGasto { get; set; }

        public int IdFinca { get; set; }

        public string DescripcionGasto { get; set; } = null!;

        public int ValorGasto { get; set; }

    }
}
