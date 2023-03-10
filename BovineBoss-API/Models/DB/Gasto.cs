using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Gasto
{
    public int IdGasto { get; set; }

    public string TipoGasto { get; set; } = null!;

    public virtual ICollection<FincaGasto> FincaGastos { get; } = new List<FincaGasto>();
}
