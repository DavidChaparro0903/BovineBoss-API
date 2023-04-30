namespace BovineBoss_API.Models.Dtos
{
    public class SellDTO
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<object> Reses { get; set; }
    }
}
