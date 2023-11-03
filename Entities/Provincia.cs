using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Entities
{
    public class Provincia
    {
        public int Id { get; set; }
        public string NombreProvincia { get; set; } = string.Empty;

        [Column("IdDepartamento")]
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;
    }
}
