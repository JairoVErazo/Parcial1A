using System;
using System.Collections.Generic;

namespace Parcial1A.Models;

public partial class Autore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
