﻿namespace BovineBoss_API.Models.Dtos
{
    public class FincaDto
    { 
        public int IdFinca { get; set; }
        public string NombreFinca { get; set; } = null!;

        public string DireccionFinca { get; set; } = null!;

        public int ExtensionFinca { get; set;}

     }
}