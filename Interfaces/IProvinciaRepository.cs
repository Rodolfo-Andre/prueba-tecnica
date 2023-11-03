using PruebaTecnica.Entities;
using System.Linq.Expressions;

namespace PruebaTecnica.Interfaces
{
    public interface IProvinciaRepository
    {
        Task<IEnumerable<Provincia>> Listar();
        Task<Provincia?> BuscarPorId(int id);
        Task Guardar(Provincia provincia);
        void Actualizar(Provincia provincia);
        void Eliminar(Provincia provincia);
        Task<bool> Existe(Expression<Func<Provincia, bool>> funcion);
    }
}
