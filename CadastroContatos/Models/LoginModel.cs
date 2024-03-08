using System.ComponentModel.DataAnnotations;

namespace CadastroContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o Login")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Digite a Senha")]
        public required string Senha { get; set; }
    }
}
