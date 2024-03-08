using System.ComponentModel.DataAnnotations;

namespace CadastroContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o Login")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Digite o Email")]
        [EmailAddress(ErrorMessage = "O Email informado não é válido!")]
        public required string Email { get; set; }
    }
}
