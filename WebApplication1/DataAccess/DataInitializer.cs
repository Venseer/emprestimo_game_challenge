using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess
{
    public static class DbInitializer
    {
        public static void Initializer(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Jogos.Any())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Jogos]");
            }
            Jogo Bayonetta2 = new Jogo { Nome = "Bayonetta II" };
            var jogos = new List<Jogo>
            {
            new Jogo { Nome = "Bayonetta" },
            Bayonetta2,
            new Jogo{Nome="Doom"},
            new Jogo{Nome="Doom II"},
            new Jogo{Nome="Doom III"},
            new Jogo{Nome="Call of Duty"},
            new Jogo{Nome="Super Mario World"},
            };
            jogos.ForEach(j => context.Jogos.Add(j));
            context.SaveChanges();

            if (context.Amigos.Any())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Amigos]");
            }
            Amigo Astolfo = new Amigo { Nome = "Astolfo" };
            var amigos = new List<Amigo>
            {
            Astolfo,
            new Amigo { Nome = "Arnaldo" },
            new Amigo{Nome="Beto"},
            new Amigo{Nome="Cristiano"},
            new Amigo{Nome="Mário"},
            new Amigo{Nome="José"},
            new Amigo{Nome="João"},
            };
            amigos.ForEach(a => context.Amigos.Add(a));
            context.SaveChanges();


            if (context.Emprestimos.Any())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Emprestimos]");
            }
            var emprestimos = new List<Emprestimo>
            {
                new Emprestimo{Amigo_Emprestimo=Astolfo,Jogo_Emprestimo=Bayonetta2,DataHoraEmprestimo=DateTime.Now}
            };
            emprestimos.ForEach(e => context.Emprestimos.Add(e));
            context.SaveChanges();
        }
    }
}
