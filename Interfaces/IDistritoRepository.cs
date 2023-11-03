using PruebaTecnica.Entities;
using System.Linq.Expressions;

namespace PruebaTecnica.Interfaces
{
    public interface IDistritoRepository
    {
        Task<IEnumerable<Distrito>> Listar();
        Task<Distrito?> BuscarPorId(int id);
        Task Guardar(Distrito distrito);
        void Actualizar(Distrito distrito);
        void Eliminar(Distrito distrito);
        Task<bool> Existe(Expression<Func<Distrito, bool>> funcion);
    }
}
