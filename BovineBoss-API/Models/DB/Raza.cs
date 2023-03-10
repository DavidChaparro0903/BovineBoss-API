using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Raza
{
    public int IdRaza { get; set; }

    public string NombreRaza { get; set; } = null!;

    public virtual ICollection<ResRaza> ResRazas { get; } = new List<ResRaza>();
}
