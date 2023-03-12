using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class ResRaza
{
    public int PorcentajeRaza { get; set; }

    public int IdRaza { get; set; }

    public int IdRes { get; set; }

    public virtual Raza IdRazaNavigation { get; set; } = null!;

    public virtual Rese IdResNavigation { get; set; } = null!;
}
