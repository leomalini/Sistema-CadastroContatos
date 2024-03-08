using CadastroContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContatos.Controllers
{
    [PageUserLogado]
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
