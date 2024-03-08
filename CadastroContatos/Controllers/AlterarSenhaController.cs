using CadastroContatos.Helper;
using CadastroContatos.Models;
using CadastroContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContatos.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _Sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao Sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _Sessao = Sessao;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel model)
        {
            try
            {
                UsuarioModel userLogado = _Sessao.GetSessaoUser();
                model.Id = userLogado.Id;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(model);

                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View("Index", model);
                }
                else
                {
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar sua senha, tente novamente, detalhe do erro: {ex.Message}";
                return View("Index", model);
            }
        }
    }
}
