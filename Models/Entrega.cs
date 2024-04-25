using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIprojeto.Models;

public partial class Entrega
{
    public int CodEntrega { get; set; }

    public int CodCol { get; set; }

    public int CodEpi { get; set; }

    public DateOnly DtVal { get; set; }

    public DateOnly DtEntrega { get; set; }

[JsonIgnore]
    public virtual Colaborador? CodColNavigation { get; set; } 

[JsonIgnore]
    public virtual Cadastro? CodEpiNavigation { get; set; } 
}
