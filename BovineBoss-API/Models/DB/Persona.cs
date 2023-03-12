using System;
using System.Collections.Generic;

namespace BovineBoss_API.Models.DB;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string? TelefonoPersona { get; set; }

    public string TipoPersona { get; set; } = null!;

    public int? Salario { get; set; }

    public DateTime? FechaContratacion { get; set; }

    public string? Usuario { get; set; }

    public string? Contrasenia { get; set; }

    public virtual ICollection<AdministradorFinca> AdministradorFincas { get; } = new List<AdministradorFinca>();

    public virtual ICollection<Adquisicione> Adquisiciones { get; } = new List<Adquisicione>();

    public virtual ICollection<TrabajadorFinca> TrabajadorFincas { get; } = new List<TrabajadorFinca>();

    public virtual ICollection<Venta> Venta { get; } = new List<Venta>();
}
