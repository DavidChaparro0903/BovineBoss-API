namespace BovineBoss_API.Models.Dtos
{
    public class AdquisicionDTO
    {
        public List<CreateOwner> owners { get; set; }
        public String descripcionAdquisicion { get; set; }
        public int CostoCompraRes { get; set; }
        public int idRes { get; set; }
        public int PrecioFlete { get; set; }
        public int ComisionesPagada { get; set; }
    }
}
