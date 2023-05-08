using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Models.Dtos
{
    public class SellResDto
    {
        public int IdRes { get; set; }
        public string Nombre { get; set; }
        public int? Precio { get; set; }
    }
}
