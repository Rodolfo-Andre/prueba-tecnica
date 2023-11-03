using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Entities
{
    public class Distrito
    {
        public int Id { get; set; }
        public string NombreDistrito { get; set; } = string.Empty;

        [Column("IdProvincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; } = null!;
    }
}
