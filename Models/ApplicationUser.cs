using Microsoft.AspNetCore.Identity;

namespace APIprojeto.Models;

public class ApplicationUser : IdentityUser
{
    public decimal Cpf { get; set; }
}

