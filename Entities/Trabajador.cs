using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Entities
{
    [Table(name: "Trabajadores")]
    public class Trabajador
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;

        [Column("IdDepartamento")]
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        [Column("IdProvincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; } = null!;

        [Column("IdDistrito")]
        public int DistritoId { get; set; }
        public Distrito Distrito { get; set; } = null!;
    }
}
