using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Fincas
{
    public int IdFinca { get; set; }

    public string NombreFinca { get; set; } = null!;

    public string DireccionFinca { get; set; } = null!;

    public int ExtensionFinca { get; set; }

    public virtual ICollection<AdministradorFinca> AdministradorFincas { get; } = new List<AdministradorFinca>();

    public virtual ICollection<FincaAlimento> FincaAlimentos { get; } = new List<FincaAlimento>();

    public virtual ICollection<FincaGasto> FincaGastos { get; } = new List<FincaGasto>();

    public virtual ICollection<Reses> Reses { get; } = new List<Reses>();

    public virtual ICollection<TrabajadorFinca> TrabajadorFincas { get; } = new List<TrabajadorFinca>();
}
