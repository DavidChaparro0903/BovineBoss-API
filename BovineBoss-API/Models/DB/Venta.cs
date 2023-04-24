using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public int IdComprador { get; set; }

    public virtual Persona IdCompradorNavigation { get; set; } = null!;

    public virtual ICollection<Rese> Reses { get; } = new List<Rese>();
}
