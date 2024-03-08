using System.ComponentModel.DataAnnotations;

namespace CadastroContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o Nome do contato!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Email do contato!")]
        [EmailAddress(ErrorMessage = "O Email informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o Celular do contato!")]
        [Phone(ErrorMessage = "O Celular informado não é válido!")]
        public string Celular { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }
    }
}
