using BlogCore.DataAccess.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVm homeVM = new HomeVm()
            {
                Slider = _unitOfWork.Slider.GetAll(),
                ArticleList = _unitOfWork.Article.GetAll()
            };

            //para saber si estamos en el home
            ViewBag.IsHome = true;
           
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            var articleFromDb = _unitOfWork.Article.Get(id);
            return View(articleFromDb);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}