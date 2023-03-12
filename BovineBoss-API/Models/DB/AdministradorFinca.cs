using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class AdministradorFinca
{
    public bool EstadoAdministrador { get; set; }

    public int IdFinca { get; set; }

    public int IdAdministrador { get; set; }

    public virtual Persona IdAdministradorNavigation { get; set; } = null!;

    public virtual Finca IdFincaNavigation { get; set; } = null!;
}
