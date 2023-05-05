namespace BovineBoss_API.Models.Dtos
{
    public class VentasDto
    {
        public int? IdVenta { get; set; }

        public DateTime FechaVenta { get; set;}


        public List<BullVentasDto> ListBull { get; set; }

        public int IdPersona { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public int? ValorVenta { get; set; }




    }
}
