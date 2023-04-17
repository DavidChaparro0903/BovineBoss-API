using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Models.Dtos
{
    public class FullBullDto
    {
        public int id { get; set; }
        public string NombreRes { get; set; } = null!;

        public string Color { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public List<RazaResDTO> listRazas { get; set; }

        public List<Persona> listOwner { get; set; }

        public int CostoCompraRes { get; set; }

        public int PrecioFlete { get; set; }

        public int ComisionesPagada { get; set; }

        public string DescripcionAdquisicion { get; set; } = null!;

    }
}
