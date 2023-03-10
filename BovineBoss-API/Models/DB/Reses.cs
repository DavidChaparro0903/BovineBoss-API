using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Reses
{
    public int IdRes { get; set; }

    public string NombreRes { get; set; } = null!;

    public string Color { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public int? ValorVenta { get; set; }

    public int? IdVenta { get; set; }

    public int IdFinca { get; set; }

    public virtual ICollection<Adquisicione> Adquisiciones { get; } = new List<Adquisicione>();

    public virtual ICollection<HistorialAlimentacion> HistorialAlimentacions { get; } = new List<HistorialAlimentacion>();

    public virtual ICollection<HistorialPeso> HistorialPesos { get; } = new List<HistorialPeso>();

    public virtual Fincas IdFincaNavigation { get; set; } = null!;

    public virtual Venta? IdVentaNavigation { get; set; }

    public virtual ICollection<ResInconveniente> ResInconvenientes { get; } = new List<ResInconveniente>();

    public virtual ICollection<ResRaza> ResRazas { get; } = new List<ResRaza>();
}
