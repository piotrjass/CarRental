using Microsoft.AspNetCore.Identity;

namespace VaricoCarRental.Models;

public class User : IdentityUser
{
    public string? Initials { get; set; }
}