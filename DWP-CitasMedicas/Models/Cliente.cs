using System;
using System.Collections.Generic;

namespace DWP_CitasMedicas.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual Paciente? Paciente { get; set; }
}
