using Mapster;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTO;
using PruebaTecnica.Entities;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Utils;
using System.Net;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public DepartamentoController(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        // GET: api/Departamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
            var listaDepartamento = await _unidadDeTrabajo.DepartamentoRepository.Listar();

            return Ok(listaDepartamento);
        }

        // GET: api/Departamento/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Departamento), statusCode: (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDepartamento(int id)
        {
            var departamento = await _unidadDeTrabajo.DepartamentoRepository.BuscarPorId(id);

            if (departamento == null)
            {
                return NotFound("Departamento no encontrado");
            }

            return Ok(departamento);
        }

        // PUT: api/Departamento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Departamento), statusCode: (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PutDepartamento(int id, [FromBody] DepartamentoDTO departamento)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Departamento");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var departamentoEncontrado = await _unidadDeTrabajo.DepartamentoRepository.BuscarPorId(id);

            if (departamentoEncontrado == null)
            {
                return NotFound("Departamento no encontrado");
            }

            departamento.NombreDepartamento = departamento.NombreDepartamento.Trim();

            var existeMismoNombre = await _unidadDeTrabajo.DepartamentoRepository.Existe(d => d.NombreDepartamento.ToLower() == departamento.NombreDepartamento.ToLower() && d.Id != id);

            if (existeMismoNombre)
            {
                return Conflict("Ya existe un departamento con el mismo nombre");
            }

            departamentoEncontrado.NombreDepartamento = departamento.NombreDepartamento;

            _unidadDeTrabajo.DepartamentoRepository.Actualizar(departamentoEncontrado);

            await _unidadDeTrabajo.Guardar();

            return Ok(departamentoEncontrado);
        }

        // POST: api/Departamento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Departamento), statusCode: (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PostDepartamento([FromBody] DepartamentoDTO departamento)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Departamento");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            departamento.NombreDepartamento = departamento.NombreDepartamento.Trim();

            var existeMismoNombre = await _unidadDeTrabajo.DepartamentoRepository.Existe(d => d.NombreDepartamento.ToLower() == departamento.NombreDepartamento.ToLower());

            if (existeMismoNombre)
            {
                return Conflict("Ya existe un departamento con el mismo nombre");
            }

            var departamentoEntidad = departamento.Adapt<Departamento>();

            await _unidadDeTrabajo.DepartamentoRepository.Guardar(departamentoEntidad);
            await _unidadDeTrabajo.Guardar();

            return CreatedAtAction("GetDepartamento", new { id = departamentoEntidad.Id }, departamentoEntidad);
        }

        // DELETE: api/Departamento/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var departamentoEncontrado = await _unidadDeTrabajo.DepartamentoRepository.BuscarPorId(id);

            if (departamentoEncontrado == null)
            {
                return NotFound("Departamento no encontrado");
            }

            _unidadDeTrabajo.DepartamentoRepository.Eliminar(departamentoEncontrado);
            await _unidadDeTrabajo.Guardar();

            return NoContent();
        }
    }
}
