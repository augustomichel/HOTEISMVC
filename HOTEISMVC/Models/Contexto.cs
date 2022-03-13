using Microsoft.EntityFrameworkCore;
using HOTEISMVC.Models;

namespace HOTEISMVC.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        public DbSet<Fotos> Fotos { get; set; }
    }
}
