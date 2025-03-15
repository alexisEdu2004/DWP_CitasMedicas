using System;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Models;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string Nombre { get; set; } = null!;

    public string CedulaProfesional { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Numero { get; set; }

    public string? Horario { get; set; }

    public virtual ICollection<Agenda> Agenda { get; set; } = new List<Agenda>();

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
