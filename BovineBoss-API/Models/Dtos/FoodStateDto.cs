namespace BovineBoss_API.Models.Dtos
{
    public class FoodStateDto
    {

        public int IdAlimento { get; set; }

        public int IdFinca { get; set; }

        public DateTime FechaCompra { get; set; }

        public string NombreAlimento { get; set; } = null!;

        public int PrecioAlimento { get; set; }

        public double CantidadComprada { get; set; }
     

    }
}
