using CadastroContatos.Models;
using Newtonsoft.Json;

namespace CadastroContatos.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _HttpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _HttpContext = httpContext;
        }
        public void CriarSessaoUser(UsuarioModel model)
        {
            string valor = JsonConvert.SerializeObject(model);
            _HttpContext.HttpContext.Session.SetString("sessaoUserLogado", valor);
        }

        public UsuarioModel GetSessaoUser()
        {
            string SessaoUser = _HttpContext.HttpContext.Session.GetString("sessaoUserLogado");

            if (string.IsNullOrEmpty(SessaoUser))
                return null;
            else
                return JsonConvert.DeserializeObject<UsuarioModel>(SessaoUser);
        }

        public void RemoverSessaoUser()
        {
            _HttpContext.HttpContext.Session.Remove("sessaoUserLogado");
        }
    }
}
