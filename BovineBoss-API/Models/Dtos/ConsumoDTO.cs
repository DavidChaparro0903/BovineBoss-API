using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Models.Dtos
{
    public class ConsumoDTO
    {
        public int idAlimento { get; set; }
        public int idFinca { get; set; }
        public string NombreAlimento { get; set; }
        public float Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
