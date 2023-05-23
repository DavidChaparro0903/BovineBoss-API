using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class HistorialPeso
{
    public DateTime FechaActualizacion { get; set; }

    public int PesoRes { get; set; }

    public int IdRes { get; set; }

    public virtual Rese IdResNavigation { get; set; } = null!;
}
