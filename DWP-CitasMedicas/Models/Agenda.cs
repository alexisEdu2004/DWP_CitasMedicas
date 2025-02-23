using System;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Models;

public partial class Agenda
{
    public int IdAgenda { get; set; }

    public int? IdDoctor { get; set; }

    public string? TipodeServicio { get; set; }

    public DateTime? FechaCita { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Doctor? IdDoctorNavigation { get; set; }
}
