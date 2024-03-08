using CadastroContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace CadastroContatos.Models
{
    public class UsuarioSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o Nome do usuário!")]

        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o Nome do usuário!")]

        public string Login { get; set;}
        [Required(ErrorMessage = "Informe o Email do usuário!")]
        [EmailAddress(ErrorMessage = "O Email informado não é válido!")]

        public string Email { get; set;}
        [Required(ErrorMessage = "Informe o Perfil do usuário!")]

        public PerfilEnum? Perfil { get; set; }
    }
}
