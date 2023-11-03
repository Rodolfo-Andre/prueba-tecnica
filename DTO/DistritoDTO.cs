using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTO
{
    public class DistritoDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener una longitud máxima de 50 caracteres")]
        public string NombreDistrito { get; set; } = string.Empty;
    }

    public class DistritoCrearOActualizarDTO : DistritoDTO
    {
        [Required(ErrorMessage = "El id de la provincia es requerido")]
        public int ProvinciaId { get; set; }
    }

    public class DistritoDetalleDTO : DistritoDTO
    {
        public int Id { get; set; }
        public ProvinciaDetalleDTO Provincia { get; set; } = null!;
    }

    public class DistritoTrabajadorDTO : DistritoDTO
    {
        public int Id { get; set; }
    }
}
