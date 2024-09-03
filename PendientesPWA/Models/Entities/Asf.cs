using System;
using System.Collections.Generic;

namespace PendientesPWA.Models.Entities;

public partial class Asf
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }
}
