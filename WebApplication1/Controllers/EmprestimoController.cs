using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [BasicAuthenticationAttribute("UsuarioTeste", "SenhaTeste", BasicRealm = "Enter Basic Authentication")]
    public class EmprestimoController : Controller
    {
        private readonly DatabaseContext _context;

        public EmprestimoController(DatabaseContext context)
        {
            _context = context;
        }

        
        public ActionResult Index()
        {
            List<Emprestimo> emprestimos = _context.Emprestimos
                .Include(e => e.Amigo_Emprestimo)
                .Include(e => e.Jogo_Emprestimo)
                .ToList();
            return View(emprestimos.OrderByDescending(e => e.DataHoraEmprestimo));
        }

        
        public ActionResult Create()
        {
            List<Jogo> ListaJogosDisponiveis = _context
                .Jogos
                .ToList();
            List<int> ToExclude = new List<int>();
            foreach (Emprestimo e in _context.Emprestimos.Include(e => e.Jogo_Emprestimo).ToList())
            {
                ToExclude.Add(e.Jogo_Emprestimo.Id);
            }
            foreach (int i in ToExclude)
            {
                ListaJogosDisponiveis.Remove(ListaJogosDisponiveis.Where(j => j.Id == i).Single());
            }
            List<Amigo> ListaAmigos = _context.Amigos.ToList();
            ViewBag.ListaAmigos = ListaAmigos.OrderBy(n => n.Nome);
            ViewBag.ListaJogos = ListaJogosDisponiveis.OrderBy(n => n.Nome);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Emprestimo emprestimo = new Emprestimo();
                emprestimo.Amigo_EmprestimoId = int.Parse(collection["Amigo_Emprestimo"]);
                emprestimo.Jogo_EmprestimoId = int.Parse(collection["Jogo_Emprestimo"]);
                emprestimo.DataHoraEmprestimo = DateTime.Parse(collection["DataHoraEmprestimo"]);
                _context.Emprestimos.Add(emprestimo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.Write(e);
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_context.Emprestimos
                .Include(e => e.Amigo_Emprestimo)
                .Include(e => e.Jogo_Emprestimo)
                .Single(e => e.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _context.Emprestimos.Remove(new Emprestimo { Id = id });
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