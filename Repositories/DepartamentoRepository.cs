using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Entities;
using PruebaTecnica.Interfaces;
using System.Linq.Expressions;

namespace PruebaTecnica.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly DBContext _context;

        public DepartamentoRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departamento>> Listar()
        {
            return await _context.Departamento.ToListAsync();
        }

        public async Task<Departamento?> BuscarPorId(int id)
        {
            return await _context.Departamento.FindAsync(id);
        }

        public async Task Guardar(Departamento departamento)
        {
            await _context.Departamento.AddAsync(departamento);
        }

        public void Actualizar(Departamento departamento)
        {
            _context.Entry(departamento).State = EntityState.Modified;
        }

        public void Eliminar(Departamento departamento)
        {
            _context.Departamento.Remove(departamento);
        }

        public async Task<bool> Existe(Expression<Func<Departamento, bool>> funcion)
        {
            return await _context.Departamento.CountAsync(funcion) > 0;
        }
    }
}
