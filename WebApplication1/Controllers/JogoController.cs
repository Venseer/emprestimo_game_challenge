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
    public class JogoController : Controller
    {
        private readonly DatabaseContext _context;

        public JogoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Jogo
        public ActionResult Index()
        {
            List<Jogo> lista =
                _context.Jogos
                .ToList();
            foreach(Jogo jogo in lista)
            {
                jogo.IsEmprestado = _context.Emprestimos.Include(j=>j.Jogo_Emprestimo).Any(j => j.Jogo_Emprestimo.Id == jogo.Id);
            }
            return View(lista);
        }

        // GET: Jogo/Details/5
        public ActionResult Details(int id)
        {
            Jogo jogo = _context.Jogos
                .Single(j => j.Id == id);
            jogo.IsEmprestado = _context.Emprestimos.Include(j => j.Jogo_Emprestimo).Any(j => j.Jogo_Emprestimo.Id == jogo.Id);
            if (jogo.IsEmprestado)
            {
                jogo.Emprestimo_Atual = _context.Emprestimos.Include(j => j.Jogo_Emprestimo).Single(j => j.Jogo_Emprestimo.Id == jogo.Id);
            }
            return View(jogo);
        }

        // GET: Jogo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jogo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Jogo jogo = new Jogo();
                jogo.Nome = collection["Nome"];
                _context.Jogos.Add(jogo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Jogo jogo = _context.Jogos
               .Single(j => j.Id == id);
            jogo.IsEmprestado = _context.Emprestimos.Include(j => j.Jogo_Emprestimo).Any(j => j.Jogo_Emprestimo.Id == jogo.Id);
            if (jogo.IsEmprestado)
            {
                jogo.Emprestimo_Atual = _context.Emprestimos.Include(j => j.Jogo_Emprestimo).Single(j => j.Jogo_Emprestimo.Id == jogo.Id);
            }
            return View(jogo);
        }

        // POST: Jogo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _context.Jogos.Remove(new Jogo { Id = id });
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