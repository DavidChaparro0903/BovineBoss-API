namespace BovineBoss_API.Models.Dtos
{
    public class CompleteDataBull
    {
        public int IdRes { get; set; }

        public string NombreRes { get; set; } = null!;

        public string Color { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public int IdFinca { get; set; }

        public string NombreFinca { get; set; } = null!;

        public int IdPropietario { get; set; }

        public string NombreCompletoPropietario { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public int IdRaza { get; set; }

        public string NombreRaza { get; set; } = null!;

        public int IdInconveniente { get; set; }

        public string NombreInconveniente { get; set; } = null!;



    }
}
