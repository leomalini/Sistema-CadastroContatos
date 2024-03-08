using CadastroContatos.Helper;
using CadastroContatos.Models;
using CadastroContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContatos.Controllers
{
    public class Login : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _Sessao;
        private readonly IEmail _Email;
        public Login(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _Sessao = sessao;
            _Email = email;
        }
        public IActionResult Index()
        {
            // se o usuário estiver logado, redirecionar para a home
            if (_Sessao.GetSessaoUser() != null)
                return RedirectToAction("Index","Home");
            else
                return View();
        }
        public IActionResult RedefinirSenha()
        {

            return View();
        }
        public IActionResult Sair()
        {
            _Sessao.RemoverSessaoUser();

            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult Entrar(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel user = _usuarioRepositorio.BuscarLogin(model.Login);
                    
                    if (user != null)
                    {
                        if (user.SenhaValida(model.Senha))
                        {
                            _Sessao.CriarSessaoUser(user);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Senha inválida. Por favor, tente novamente";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Usuário e/ou Senha inválido(s). Por favor, tente novamente";
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente, detalhe do erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult ResetSenha(RedefinirSenhaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel user = _usuarioRepositorio.BuscarLoginEmail(model.Login, model.Email);

                    if (user != null)
                    {
                        string novaSenha = user.NovaSenha();

                        string msg = $"Sua nova senha é : {novaSenha}";

                        bool enviado = _Email.Enviar(user.Email, "Sistema de Contatos - Redefinição de senha", msg);

                        if (enviado)
                        {
                            //atualizar
                            _usuarioRepositorio.Atualizar(user);
                            TempData["MensagemSucesso"] = $"Enviamos para o email cadastrado uma nova senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o email de redefinição de senha. Por favor, tente novamente";
                        }
                        return RedirectToAction("Index", "Login");

                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados";
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente, detalhe do erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}
