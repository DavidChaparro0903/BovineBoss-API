using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class TrabajadorFinca
{
    public bool EstadoTrabajador { get; set; }

    public int IdFinca { get; set; }

    public int IdTrabajador { get; set; }

    public DateTime FechaCambioTrabajador { get; set; }

    public virtual Finca IdFincaNavigation { get; set; } = null!;

    public virtual Persona IdTrabajadorNavigation { get; set; } = null!;
}
