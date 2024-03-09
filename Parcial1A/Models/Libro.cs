using System;
using System.Collections.Generic;

namespace Parcial1A.Models;

public partial class Libro
{
    public int Id { get; set; }

    public byte[] Titulo { get; set; } = null!;
}
