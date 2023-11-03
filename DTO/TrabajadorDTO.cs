using PruebaTecnica.Entities;
using PruebaTecnica.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.DTO
{
    public class TrabajadorDTO
    {
        [Required(ErrorMessage = "El tipo de documento es requerido")]
        [EnumDataType(typeof(TipoDocumentoEnum), ErrorMessage = "El valor debe ser 'DNI', 'CIF' o 'CIN'")]
        public string TipoDocumento { get; set; } = null!;

        [Required(ErrorMessage = "El número de documento es requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Debe ser un valor numérico")]
        [MaxLength(50, ErrorMessage = "El número de documento debe tener una longitud máxima de 50 caracteres")]
        public string NumeroDocumento { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(150, ErrorMessage = "El nombre debe tener una longitud máxima de 150 caracteres")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El sexo es requerido")]
        [RegularExpression("^[FM]$", ErrorMessage = "El valor debe ser 'F' (Femenino) o 'M' (Masculino)")]
        public string Sexo { get; set; } = null!;
    }

    public class TrabajadorCrearOActualizarDTO : TrabajadorDTO
    {

        [Required(ErrorMessage = "El id del distrito es requerido")]
        public int DistritoId { get; set; }
    }

    public class TrabajadorDetalleDTO : TrabajadorDTO
    {
        public int Id { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public ProvinciaTrabajadorDTO Provincia { get; set; } = null!;
        public DistritoTrabajadorDTO Distrito { get; set; } = null!;
    }

    public class TrabajadoreConsultaPreparada : TrabajadorDTO
    {
        public int Id { get; set; }

        [Column("IdDepartamento")]
        public int DepartamentoId { get; set; }
        public string NombreDepartamento { get; set; } = null!;

        [Column("IdProvincia")]
        public int ProvinciaId { get; set; }
        public string NombreProvincia { get; set; } = null!;

        [Column("IdDistrito")]
        public int DistritoId { get; set; }
        public string NombreDistrito { get; set; } = null!;
    }
}
