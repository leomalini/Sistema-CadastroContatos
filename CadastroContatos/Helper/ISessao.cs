using CadastroContatos.Models;

namespace CadastroContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoUser(UsuarioModel model);
        void RemoverSessaoUser();
        UsuarioModel GetSessaoUser();
    }
}
