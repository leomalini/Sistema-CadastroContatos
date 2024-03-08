using CadastroContatos.Controllers;
using CadastroContatos.Data;
using CadastroContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel BuscarLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login == login);

        }
        public UsuarioModel BuscarLoginEmail(string login, string email)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.First(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.Include(x => x.Contatos)
                .ToList(); 
        }

        public UsuarioModel Adicionar(UsuarioModel user)
        {
            //insert no bd
            user.DataCadastro = DateTime.Now;
            user.SetSenhaHash();
            _bancoContext.Usuarios.Add(user);
            _bancoContext.SaveChanges();

            return user;
        }

        public UsuarioModel Atualizar(UsuarioModel user)
        {
            UsuarioModel userDb = ListarPorId(user.Id);

            if (userDb == null)
            {
                throw new Exception("Houve um erro na atualização do usuário!");
            }
            else
            {
                userDb.Nome = user.Nome;
                userDb.Email = user.Email;
                userDb.Login = user.Login;
                userDb.Perfil = user.Perfil;
                userDb.DataAtualizacao = DateTime.Now;

                _bancoContext.Usuarios.Update(userDb);
                _bancoContext.SaveChanges();

                return userDb;
            }
        }
        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha)
        {
            UsuarioModel userDb = ListarPorId(alterarSenha.Id) ?? throw new Exception("Houve um erro na atualização da senha, usuário não encontrado");

            if (!userDb.SenhaValida(alterarSenha.SenhaAtual))
                throw new Exception("Senha atual não confere!");

            if (userDb.SenhaValida(alterarSenha.NovaSenha))
                throw new Exception("A nova senha deve ser diferente da senha atual!");

            userDb.SetNOvaSenha(alterarSenha.NovaSenha);
            userDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(userDb);
            _bancoContext.SaveChanges();

            return userDb;
        }


        public bool Apagar(int id)
        {
            UsuarioModel userDB = ListarPorId(id);

            if (userDB == null)
            {
                throw new Exception("Houve um erro ao apagar o usuário!");
            }
            else
            {
                _bancoContext.Usuarios.Remove(userDB);
                _bancoContext.SaveChanges();

                return true;
            }

        }

    }
}
