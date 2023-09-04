using BlogCore.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //opcion 1: Obtener todos los usuarios
            //return View(_unitOfWork.User.GetAll());

            //opcion 2: obtener todos los usuarios, menos es que esta autenticado
            var claimsIdentity = (ClaimsIdentity)this.User.Identity!;
            var currentUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_unitOfWork.User.GetAll(u => u.Id != currentUser!.Value));
        }

        [HttpGet]
        public IActionResult Lock(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            _unitOfWork.User.LockUser(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Unlock(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            _unitOfWork.User.UnlockUser(userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
