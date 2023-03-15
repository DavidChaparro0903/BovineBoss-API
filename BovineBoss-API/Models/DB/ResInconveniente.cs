using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class ResInconveniente
{
    public DateTime FechaInconveniente { get; set; }

    public int DineroGastado { get; set; }

    public string DescripcionInconveniente { get; set; } = null!;

    public int IdInconveniente { get; set; }

    public int IdRes { get; set; }

    public virtual Inconveniente IdInconvenienteNavigation { get; set; } = null!;

    public virtual Rese IdResNavigation { get; set; } = null!;
}
