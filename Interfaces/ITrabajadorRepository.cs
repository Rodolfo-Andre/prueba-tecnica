using PruebaTecnica.Entities;
using System.Linq.Expressions;

namespace PruebaTecnica.Interfaces
{
    public interface ITrabajadorRepository
    {
        Task<IEnumerable<Trabajador>> Listar(string? nombre);
        Task<Trabajador?> BuscarPorId(int id);
        Task Guardar(Trabajador trabajador);
        void Actualizar(Trabajador trabajador);
        void Eliminar(Trabajador trabajador);
        Task<bool> Existe(Expression<Func<Trabajador, bool>> funcion);
    }
}
