using CadastroContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CadastroContatos.Controllers
{
    [PageUserLogado]
    public class Restrito : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
