using CadastroContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CadastroContatos.Filters
{
    //Filtro para verificar usuário logado, nao deixando navegar pela rota
    public class PageUserLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string session = context.HttpContext.Session.GetString("sessaoUserLogado");

            if (string.IsNullOrEmpty(session) ) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller","Login"},{"action","Index"}});
            }
            else
            {
                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(session);
                if (usuarioModel == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }


            base.OnActionExecuting(context);
        }
    }
}
