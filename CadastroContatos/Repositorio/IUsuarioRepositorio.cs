using CadastroContatos.Models;

namespace CadastroContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarLogin(string username);
        UsuarioModel BuscarLoginEmail(string login, string email);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel user);
        UsuarioModel Atualizar (UsuarioModel user);
        UsuarioModel AlterarSenha (AlterarSenhaModel alterarSenha);
        bool Apagar(int id);
    }
}
