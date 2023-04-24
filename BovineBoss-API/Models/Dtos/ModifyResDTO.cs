namespace BovineBoss_API.Models.Dtos
{
    public class ModifyResDTO
    {
        public int IdRes { get; set; }
        public int idFinca { get; set; }
        public string NombreRes { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<CreateResRaza> listRazas { get; set; }
        public List<CreateOwner> listOwner { get; set; }
        public int costoCompraRes { get; set; }
        public string DescripcionAdquisicion { get; set; }
        public int ComisionesPagada { get; set; }
        public int PrecioFlete { get; set; }
    }
}
