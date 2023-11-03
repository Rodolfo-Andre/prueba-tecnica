using Mapster;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.DTO;
using PruebaTecnica.Entities;
using PruebaTecnica.Interfaces;
using System.Linq.Expressions;

namespace PruebaTecnica.Repositories
{
    public class TrabajadorRepository : ITrabajadorRepository
    {
        private readonly DBContext _context;

        public TrabajadorRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trabajador>> Listar(string? nombre)
        {
            var resultado = await _context.TrabajadorConsultaPreparada.FromSqlRaw("EXEC SP_OBTENER_TRABAJADORES {0}", nombre ?? "").ToListAsync();

            return resultado.Select(t => Mapear(t));
        }

        public async Task<Trabajador?> BuscarPorId(int id)
        {
            return await _context.Trabajador.Include(t => t.Departamento).Include(t => t.Provincia).Include(t => t.Distrito).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Guardar(Trabajador trabajador)
        {
            await _context.Trabajador.AddAsync(trabajador);
        }

        public void Actualizar(Trabajador trabajador)
        {
            _context.Entry(trabajador).State = EntityState.Modified;
        }

        public void Eliminar(Trabajador trabajador)
        {
            _context.Trabajador.Remove(trabajador);
        }

        public async Task<bool> Existe(Expression<Func<Trabajador, bool>> funcion)
        {
            return await _context.Trabajador.CountAsync(funcion) > 0;
        }

        private Trabajador Mapear(TrabajadoreConsultaPreparada t)
        {
            var trabajadorMapeado = t.Adapt<Trabajador>();

            trabajadorMapeado.Departamento = new Departamento()
            {
                Id = trabajadorMapeado.DepartamentoId,
                NombreDepartamento = t.NombreDepartamento
            };

            trabajadorMapeado.Distrito = new Distrito()
            {
                Id = trabajadorMapeado.DistritoId,
                NombreDistrito = t.NombreDistrito
            };

            trabajadorMapeado.Provincia = new Provincia
            {
                Id = trabajadorMapeado.ProvinciaId,
                NombreProvincia = t.NombreProvincia
            };

            return trabajadorMapeado;
        }
    }
}
