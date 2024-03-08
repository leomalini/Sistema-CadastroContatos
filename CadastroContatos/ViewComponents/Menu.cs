using CadastroContatos.Controllers;
using CadastroContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CadastroContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("sessaoUserLogado");

            if (string.IsNullOrEmpty(userSession) )
            {
                return null;
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(userSession);
                return View(usuario);
            }

           
        }
    }
}
