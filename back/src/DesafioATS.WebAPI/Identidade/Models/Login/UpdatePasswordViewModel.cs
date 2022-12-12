using System.ComponentModel.DataAnnotations;

namespace DesafioATS.WebAPI.Identidade.Models.Login
{
    public class UpdatePasswordViewModel
    {
        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informada.")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }
}