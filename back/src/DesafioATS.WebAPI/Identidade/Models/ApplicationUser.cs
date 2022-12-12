using Microsoft.AspNetCore.Identity;

namespace DesafioATS.WebAPI.Identidade.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}
