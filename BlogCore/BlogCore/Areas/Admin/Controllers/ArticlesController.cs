using BlogCore.DataAccess.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticlesController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = new Article(),
                CategoryList = _unitOfWork.Category.GetListCategories()
            };

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleMV)
        {
            if (ModelState.IsValid)
            {
                string principalRouet = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (articleMV.Article.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ups = Path.Combine(principalRouet, @"images\articles");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(ups,fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    articleMV.Article.ImageUrl = @"\images\articles\" + fileName + extension;
                    articleMV.Article.CreatedDate = DateTime.Now;

                    _unitOfWork.Article.Add(articleMV.Article);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            articleMV.CategoryList = _unitOfWork.Category.GetListCategories();
            return View(articleMV);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = new Article(),
                CategoryList = _unitOfWork.Category.GetListCategories()
            };

            if (id != null)
            {
                articleVM.Article = _unitOfWork.Article.Get(id.GetValueOrDefault());
            }

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleMV)
        {
            if (ModelState.IsValid)
            {
                string principalRouet = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var articleFromDb = _unitOfWork.Article.Get(articleMV.Article.Id);


                if (files.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ups = Path.Combine(principalRouet, @"images\articles");
                    var extension = Path.GetExtension(files[0].FileName);
                    var newExtension = Path.GetExtension(files[0].FileName);

                    var imageRoute = Path.Combine(principalRouet, articleFromDb.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imageRoute))
                    {
                        System.IO.File.Delete(imageRoute);
                    }

                    using (var fileStream = new FileStream(Path.Combine(ups, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    articleMV.Article.ImageUrl = @"\images\articles\" + fileName + extension;
                    articleMV.Article.CreatedDate = DateTime.Now;

                    _unitOfWork.Article.Update(articleMV.Article);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    articleMV.Article.ImageUrl = articleFromDb.ImageUrl;
                }

                _unitOfWork.Article.Update(articleMV.Article);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(articleMV);
        }





        #region Llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            //opcion 1
            return Json(new { data = _unitOfWork.Article.GetAll(includeProperties:"Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articleFromDb = _unitOfWork.Article.Get(id);
            var directoryRoute = _hostEnvironment.WebRootPath;
            var imageRoute = Path.Combine(directoryRoute, articleFromDb.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imageRoute))
            {
                System.IO.File.Delete(imageRoute);
            }

            if (articleFromDb == null)
            {
                return Json(new { success = false, message = "Error to delete the article" });
            }

    

            _unitOfWork.Article.Remove(articleFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Article was delete successfully" });

        }

        #endregion
    }
}
