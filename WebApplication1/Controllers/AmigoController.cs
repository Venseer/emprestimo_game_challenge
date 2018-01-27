using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AmigoController : Controller
    {
        private readonly DatabaseContext _context;

        public AmigoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Amigo
        public ActionResult Index()
        {
            
            return View(_context.Amigos.ToList().OrderBy(a=>a.Nome));
        }

        // GET: Amigo/Details/5
        public ActionResult Details(int id)
        {
            Amigo amigo = _context.Amigos
                .Include(a => a.Emprestimos)
                    .ThenInclude(e => e.Jogo_Emprestimo)
                .Single(a => a.Id == id);
            return View(amigo);
        }

        // GET: Amigo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
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

        // GET: Amigo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Amigo/Edit/5
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

        // GET: Amigo/Delete/5
        public ActionResult Delete(int id)
        {
            Amigo amigo = _context.Amigos
               .Include(a => a.Emprestimos)
                   .ThenInclude(e => e.Jogo_Emprestimo)
               .Single(a => a.Id == id);
            return View(amigo);
        }

        // POST: Amigo/Delete/5
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