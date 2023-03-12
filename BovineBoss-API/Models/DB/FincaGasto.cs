using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class FincaGasto
{
    public DateTime FechaGasto { get; set; }

    public string DescripcionGasto { get; set; } = null!;

    public int ValorGasto { get; set; }

    public int IdFinca { get; set; }

    public int IdGasto { get; set; }

    public virtual Finca IdFincaNavigation { get; set; } = null!;

    public virtual Gasto IdGastoNavigation { get; set; } = null!;
}
