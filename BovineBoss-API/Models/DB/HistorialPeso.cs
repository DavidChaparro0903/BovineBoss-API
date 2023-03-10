using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class HistorialPeso
{
    public DateTime FechaAlimentacion { get; set; }

    public int PesoRes { get; set; }

    public int IdRes { get; set; }

    public virtual Reses IdResNavigation { get; set; } = null!;
}
