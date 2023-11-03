using PruebaTecnica.Data;
using PruebaTecnica.Interfaces;

namespace PruebaTecnica.Repositories
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public ITrabajadorRepository TrabajadorRepository { get; }
        public IDepartamentoRepository DepartamentoRepository { get; }
        public IProvinciaRepository ProvinciaRepository { get; }
        public IDistritoRepository DistritoRepository { get; }
        private readonly DBContext _context;

        public UnidadDeTrabajo(ITrabajadorRepository trabajadorRepository,
            IDepartamentoRepository departamentoRepository, IProvinciaRepository provinciaRepository,
            IDistritoRepository distritoRepository, DBContext context)
        {
            _context = context;
            TrabajadorRepository = trabajadorRepository;
            DepartamentoRepository = departamentoRepository;
            ProvinciaRepository = provinciaRepository;
            DistritoRepository = distritoRepository;
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
