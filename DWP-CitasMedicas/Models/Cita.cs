using System;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public int? IdAgenda { get; set; }

    public int? IdPaciente { get; set; }

    public int? IdDoctor { get; set; }

    public string? MotivoDeConsulta { get; set; }

    public virtual Agenda? IdAgendaNavigation { get; set; }

    public virtual Doctor? IdDoctorNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }
}
