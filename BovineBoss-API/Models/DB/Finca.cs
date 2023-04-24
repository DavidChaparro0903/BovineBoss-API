using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Finca
{
    public int IdFinca { get; set; }

    public string NombreFinca { get; set; } = null!;

    public string? DireccionFinca { get; set; }

    public int ExtensionFinca { get; set; }

    public virtual ICollection<AdministradorFinca> AdministradorFincas { get; } = new List<AdministradorFinca>();

    public virtual ICollection<FincaAlimento> FincaAlimentos { get; } = new List<FincaAlimento>();

    public virtual ICollection<FincaGasto> FincaGastos { get; } = new List<FincaGasto>();

    public virtual ICollection<Rese> Reses { get; } = new List<Rese>();

    public virtual ICollection<TrabajadorFinca> TrabajadorFincas { get; } = new List<TrabajadorFinca>();
}
