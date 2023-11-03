using PruebaTecnica.Entities;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTO
{
    public class ProvinciaDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener una longitud máxima de 50 caracteres")]
        public string NombreProvincia { get; set; } = string.Empty;
    }

    public class ProvinciaCrearOActualizarDTO : ProvinciaDTO
    {
        [Required(ErrorMessage = "El id del departamento es requerido")]
        public int DepartamentoId { get; set; }
    }

    public class ProvinciaDetalleDTO : ProvinciaDTO
    {
        public int Id { get; set; }
        public Departamento Departamento { get; set; } = null!;
    }

    public class ProvinciaTrabajadorDTO : ProvinciaDTO
    {
        public int Id { get; set; }
    }
}
