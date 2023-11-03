using Microsoft.EntityFrameworkCore;
using PruebaTecnica.DTO;
using PruebaTecnica.Entities;

namespace PruebaTecnica.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Trabajador> Trabajador { get; set; } = default!;
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Provincia> Provincia { get; set; }

        public DbSet<TrabajadoreConsultaPreparada> TrabajadorConsultaPreparada { get; set; } = default!;
    }
}
