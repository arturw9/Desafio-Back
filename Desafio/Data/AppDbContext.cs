using Desafio.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TituloModel> Titulos { get; set; }
        public DbSet<ParcelaModel> Parcelas { get; set; }
    }
}
