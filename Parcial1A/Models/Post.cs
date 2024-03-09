using System;
using System.Collections.Generic;

namespace Parcial1A.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime FechaPublicacion { get; set; }

    public int? AutorId { get; set; }

    public virtual Autore? Autor { get; set; }
}
