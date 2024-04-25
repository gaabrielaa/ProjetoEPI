using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIprojeto.Models;

public partial class Cadastro
{
    public int CodEpi { get; set; }

    public string? Nome { get; set; }

    public string? UsuEpi { get; set; }

[JsonIgnore]
    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
