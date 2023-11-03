using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTO
{
    public class DepartamentoDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener una longitud máxima de 50 caracteres")]
        public string NombreDepartamento { get; set; } = string.Empty;
    }
}
