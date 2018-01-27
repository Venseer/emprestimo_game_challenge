using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [BasicAuthenticationAttribute("UsuarioTeste", "SenhaTeste", BasicRealm = "Enter Basic Authentication")]
    public class AmigoController : Controller
    {
        private readonly DatabaseContext _context;

        public AmigoController(DatabaseContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            
            return View(_context.Amigos.ToList().OrderBy(a=>a.Nome));
        }

        public ActionResult Details(int id)
        {
            Amigo amigo = _context.Amigos
                .Include(a => a.Emprestimos)
                    .ThenInclude(e => e.Jogo_Emprestimo)
                .Single(a => a.Id == id);
            return View(amigo);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Amigo amigo = new Amigo();
                amigo.Nome = collection["Nome"];
                _context.Amigos.Add(amigo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Amigo amigo = _context.Amigos
               .Include(a => a.Emprestimos)
                   .ThenInclude(e => e.Jogo_Emprestimo)
               .Single(a => a.Id == id);
            return View(amigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _context.Amigos.Remove(new Amigo { Id = id });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}