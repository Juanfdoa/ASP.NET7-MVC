using Autocomplete.Data;
using Autocomplete.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Autocomplete.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetNombres(string term)
        {
            /*List<Usuario> listaUsuarios = new List<Usuario>()
            {
                new Usuario { Id = 1, nombre = "sandra" },
                new Usuario { Id = 2, nombre = "AndRes" },
                new Usuario { Id = 3, nombre = "CAMILO" },
                new Usuario { Id = 4, nombre = "santiago" },
                new Usuario { Id = 5, nombre = "PauLiNa" },
                new Usuario { Id = 6, nombre = "SAMara" },
                new Usuario { Id = 7, nombre = "JUliaNA" },
                new Usuario { Id = 8, nombre = "sofiA" },
                new Usuario { Id = 9, nombre = "ANA" }
            };

            var result = (from u in listaUsuarios where u.nombre.Contains(term) select new { value = u.nombre });*/

            var result = (from u in _context.Usuario where u.nombre.Contains(term) select new { value = u.nombre });

            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}