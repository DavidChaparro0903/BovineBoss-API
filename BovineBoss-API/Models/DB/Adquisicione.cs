using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Adquisicione
{
    public DateTime FechaAdquisicion { get; set; }

    public int CostoCompraRes { get; set; }

    public int PrecioFlete { get; set; }

    public int ComisionesPagada { get; set; }

    public string DescripcionAdquisicion { get; set; } = null!;

    public int IdRes { get; set; }

    public int IdPropietario { get; set; }

    public virtual Persona IdPropietarioNavigation { get; set; } = null!;

    public virtual Reses IdResNavigation { get; set; } = null!;
}
