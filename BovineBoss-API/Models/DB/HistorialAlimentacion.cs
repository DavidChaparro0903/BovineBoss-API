using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class HistorialAlimentacion
{
    public DateTime FechaAlimentacion { get; set; }

    public float CantidadAlimentacion { get; set; }

    public int IdRes { get; set; }

    public DateTime FechaCompra { get; set; }

    public int IdAlimento { get; set; }

    public int IdFinca { get; set; }

    public virtual FincaAlimento FincaAlimento { get; set; } = null!;

    public virtual Rese IdResNavigation { get; set; } = null!;
}
