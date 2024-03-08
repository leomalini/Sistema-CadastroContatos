using CadastroContatos.Controllers;
using CadastroContatos.Data;
using CadastroContatos.Models;

namespace CadastroContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            //insert no bd
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();

            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorId(contato.Id);

            if (contatoDb == null)
            {
                throw new Exception("Houve um erro na atualização do contato!");
            }
            else
            {
                contatoDb.Nome = contato.Nome;
                contatoDb.Email = contato.Email;
                contatoDb.Celular = contato.Celular;

                _bancoContext.Contatos.Update(contatoDb);
                _bancoContext.SaveChanges();

                return contatoDb;
            }
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDb = ListarPorId(id);

            if (contatoDb == null)
            {
                throw new Exception("Houve um erro ao apagar o contato!");
            }
            else
            {

                _bancoContext.Contatos.Remove(contatoDb);
                _bancoContext.SaveChanges();

                return true;
            }

        }
    }
}
