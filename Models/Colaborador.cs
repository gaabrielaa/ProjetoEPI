using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIprojeto.Models;

public partial class Colaborador
{
    public int CodCol { get; set; }

    public string? Nome { get; set; }

    public string? Ctps { get; set; }

    public DateOnly? DtAdm { get; set; }

    public int? Tel { get; set; }

    public decimal Cpf { get; set; }

[JsonIgnore]
    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
