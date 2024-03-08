using CadastroContatos.Filters;
using CadastroContatos.Models;
using CadastroContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContatos.Controllers
{
    [PageAdmin]
    public class Usuario : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;
        public Usuario(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ListarContatosPorUsuarioId(int Id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(Id);
            return PartialView("_ContatosUsuario", contatos);
        }

        [HttpPost]
        public IActionResult Create(UsuarioModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user = _usuarioRepositorio.Adicionar(user);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult DeleteConfirm(int Id)
        {
            UsuarioModel user = _usuarioRepositorio.ListarPorId(Id);
            return View(user);
        }
        public IActionResult Delete(int Id)
        {
            try
            {
                bool apagar = _usuarioRepositorio.Apagar(Id);

                if (apagar)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o usuário, tente novamente.";

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");

            }
        }
        public IActionResult Edit(int Id)
        {
            UsuarioModel user = _usuarioRepositorio.ListarPorId(Id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSenhaModel user)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = user.Id,
                        Nome = user.Nome,
                        Email = user.Email,
                        Perfil = user.Perfil,
                        Login = user.Login
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Edit", usuario);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar o usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
