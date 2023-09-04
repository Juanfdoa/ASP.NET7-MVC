using BlogCore.DataAccess.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SlidersController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string principalRouet = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (slider.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ups = Path.Combine(principalRouet, @"images\sliders");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(ups, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    slider.ImageUrl = @"\images\sliders\" + fileName + extension;
                 

                    _unitOfWork.Slider.Add(slider);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Slider slider = new Slider();
            slider = _unitOfWork.Slider.Get(id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string principalRouet = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var sliderFromDb = _unitOfWork.Slider.Get(slider.Id);


                if (files.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ups = Path.Combine(principalRouet, @"images\sliders");
                    var extension = Path.GetExtension(files[0].FileName);
                    var newExtension = Path.GetExtension(files[0].FileName);

                    var imageRoute = Path.Combine(principalRouet, sliderFromDb.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imageRoute))
                    {
                        System.IO.File.Delete(imageRoute);
                    }

                    using (var fileStream = new FileStream(Path.Combine(ups, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    slider.ImageUrl = @"\images\sliders\" + fileName + extension;

                    _unitOfWork.Slider.Update(slider);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.ImageUrl = sliderFromDb.ImageUrl;
                }

                _unitOfWork.Slider.Update(slider);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }


        #region Llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            //opcion 1
            return Json(new { data = _unitOfWork.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objDb = _unitOfWork.Slider.Get(id);
            if (objDb == null)
            {
                return Json(new { success = false, message = "Error to delete the slider" });
            }

            _unitOfWork.Slider.Remove(objDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Slider was delete successfully" });

        }

        #endregion
    }
}
