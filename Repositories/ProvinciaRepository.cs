using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Entities;
using PruebaTecnica.Interfaces;
using System.Linq.Expressions;

namespace PruebaTecnica.Repositories
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly DBContext _context;

        public ProvinciaRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provincia>> Listar()
        {
            return await _context.Provincia.Include(p => p.Departamento).ToListAsync();
        }

        public async Task<Provincia?> BuscarPorId(int id)
        {
            return await _context.Provincia.Include(p => p.Departamento).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Guardar(Provincia provincia)
        {
            await _context.Provincia.AddAsync(provincia);
        }

        public void Actualizar(Provincia provincia)
        {
            _context.Entry(provincia).State = EntityState.Modified;
        }

        public void Eliminar(Provincia provincia)
        {
            _context.Provincia.Remove(provincia);
        }

        public async Task<bool> Existe(Expression<Func<Provincia, bool>> funcion)
        {
            return await _context.Provincia.CountAsync(funcion) > 0;
        }
    }
}
