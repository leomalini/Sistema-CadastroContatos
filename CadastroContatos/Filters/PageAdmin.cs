using CadastroContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CadastroContatos.Filters
{
    //Filtro para verificar usuário admin, p/ modificar usuarios
    public class PageAdmin : ActionFilterAttribute
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
                else if(usuarioModel.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }


            base.OnActionExecuting(context);
        }
    }
}
