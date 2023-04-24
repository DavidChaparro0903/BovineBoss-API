namespace BovineBoss_API.Models.Dtos
{
    public class CreateResDto
    {
        public string NombreRes { get; set; } = null!;

        public string Color { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public int idFinca { get; set; }

        public List<CreateResRaza> listRazas { get; set; }

        public List<CreateOwner> listOwner { get; set; }

        public int CostoCompraRes { get; set; }

        public int PrecioFlete { get; set; }

        public int ComisionesPagada { get; set; }

        public string DescripcionAdquisicion { get; set; } = null!;

    }
}
