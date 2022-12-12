using System.ComponentModel.DataAnnotations;

namespace DesafioATS.WebAPI.Identidade.Models.Login
{
    public class RegistrarViewModel
    {
        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informada.")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informada.")]
        public TipoLogin? TipoLogin { get; set; }
    }
}