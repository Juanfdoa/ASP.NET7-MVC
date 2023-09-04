using CrudNet7MVC.Data;
using CrudNet7MVC.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Diagnostics;

namespace CrudNet7MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context= context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //envio de correo
            /*var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Text envio email", "juanfdoa@hotmail.com"));
            message.To.Add(new MailboxAddress("Text enviado", "juanfdoa@hotmail.com"));
            message.Subject = "Test email asp.net core";
            message.Body = new TextPart("plain")
            {
                Text = "Hola saludo desde asp.net"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465);
                client.Authenticate("juanfdoa@hotmail.com", "1040");
                client.Send(message);
                client.Disconnect(true);
            }*/



            return View(await _context.Contact.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contact.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _context.Contact.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contact.Update(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _context.Contact.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _context.Contact.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteContacto(int? id)
        {
            var contacto = await _context.Contact.FindAsync(id);
            if (contacto == null)
            {
                return View();
            }

            _context.Remove(contacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}