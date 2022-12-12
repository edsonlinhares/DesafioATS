using System.ComponentModel.DataAnnotations;

namespace DesafioATS.WebAPI.Identidade.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informada.")]
        public string Password { get; set; }
    }
}