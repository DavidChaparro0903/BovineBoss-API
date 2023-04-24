using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Alimento
{
    public int IdAlimento { get; set; }

    public string TipoAlimento { get; set; } = null!;

    public virtual ICollection<FincaAlimento> FincaAlimentos { get; } = new List<FincaAlimento>();
}
