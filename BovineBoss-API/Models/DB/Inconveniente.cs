using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Inconveniente
{
    public int IdInconveniente { get; set; }

    public string NombreInconveniente { get; set; } = null!;

    public virtual ICollection<ResInconveniente> ResInconvenientes { get; } = new List<ResInconveniente>();
}
