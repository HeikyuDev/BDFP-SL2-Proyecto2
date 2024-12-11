using System;
using System.Collections.Generic;

namespace Crud.Server.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public float Precio { get; set; }
}
