using System;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public int? IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Edad { get; set; }

    public decimal? Estatura { get; set; }

    public decimal? Peso { get; set; }

    public string? TipodeSangre { get; set; }

    public string? Direccion { get; set; }

    public string? NumeroDeEmergencia { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
