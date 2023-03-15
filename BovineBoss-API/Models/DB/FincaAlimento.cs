using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class FincaAlimento
{
    public DateTime FechaCompra { get; set; }

    public string NombreAlimento { get; set; } = null!;

    public int PrecioAlimento { get; set; }

    public int CantidadComprada { get; set; }

    public int IdAlimento { get; set; }

    public int IdFinca { get; set; }

    public virtual ICollection<HistorialAlimentacion> HistorialAlimentacions { get; } = new List<HistorialAlimentacion>();

    public virtual Alimento IdAlimentoNavigation { get; set; } = null!;

    public virtual Finca IdFincaNavigation { get; set; } = null!;
}
