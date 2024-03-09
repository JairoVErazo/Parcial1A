using System;
using System.Collections.Generic;

namespace Parcial1A.Models;

public partial class AutorLibro
{
    public int AutorId { get; set; }

    public int LibroId { get; set; }

    public int Orden { get; set; }

    public virtual Autore Autor { get; set; } = null!;

    public virtual Libro Libro { get; set; } = null!;
}
