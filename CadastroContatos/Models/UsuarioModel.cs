using CadastroContatos.Enums;
using CadastroContatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace CadastroContatos.Models
{
    public class UsuarioModel
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
        [Required(ErrorMessage = "Informe a Senha do usuário!")]

        public string Senha { get; set;}
        public DateTime DataCadastro {  get; set; }
        public DateTime? DataAtualizacao {  get; set; }
        public virtual List<ContatoModel>? Contatos { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.Hash();
        }
        public void SetSenhaHash()
        {
            Senha = Senha.Hash();
        }
        public string NovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.Hash();
            return novaSenha;
        }
        public void SetNOvaSenha(string novaSenha)
        {
            Senha = novaSenha.Hash();
        }
    }
}
