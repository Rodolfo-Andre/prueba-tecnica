namespace PruebaTecnica.Interfaces
{
    public interface IUnidadDeTrabajo
    {
        ITrabajadorRepository TrabajadorRepository { get; }
        IDepartamentoRepository DepartamentoRepository { get; }
        IProvinciaRepository ProvinciaRepository { get; }
        IDistritoRepository DistritoRepository { get; }
        Task<int> Guardar();
    }
}
