using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Entities;
using PruebaTecnica.Interfaces;
using System.Linq.Expressions;

namespace PruebaTecnica.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly DBContext _context;

        public DistritoRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Distrito>> Listar()
        {
            return await _context.Distrito.Include(d => d.Provincia).Include(d => d.Provincia.Departamento).ToListAsync();
        }

        public async Task<Distrito?> BuscarPorId(int id)
        {
            return await _context.Distrito.Include(d => d.Provincia).Include(d => d.Provincia.Departamento).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task Guardar(Distrito distrito)
        {
            await _context.Distrito.AddAsync(distrito);
        }

        public void Actualizar(Distrito distrito)
        {
            _context.Entry(distrito).State = EntityState.Modified;
        }

        public void Eliminar(Distrito distrito)
        {
            _context.Distrito.Remove(distrito);
        }

        public async Task<bool> Existe(Expression<Func<Distrito, bool>> funcion)
        {
            return await _context.Distrito.CountAsync(funcion) > 0;
        }
    }
}
