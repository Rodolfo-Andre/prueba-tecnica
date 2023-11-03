using PruebaTecnica.Entities;
using System.Linq.Expressions;

namespace PruebaTecnica.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> Listar();
        Task<Departamento?> BuscarPorId(int id);
        Task Guardar(Departamento departamento);
        void Actualizar(Departamento departamento);
        void Eliminar(Departamento departamento);
        Task<bool> Existe(Expression<Func<Departamento, bool>> funcion);
    }
}
