using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();

                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();
                foreach (var p in produtos)
                {
                    Console.WriteLine(p);
                    
                }

                Console.WriteLine("===========================");

                contexto.ChangeTracker.Entries();

                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e.State);
                }

                Console.WriteLine("===========================");

                var p1 = produtos.Last();

                p1.Nome = "1984";

                contexto.SaveChanges();

                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e.State);
                }

                contexto.SaveChanges();

                Console.ReadLine();
            }
        }

        
    }
}
