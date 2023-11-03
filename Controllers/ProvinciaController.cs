using System.Net;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTO;
using PruebaTecnica.Entities;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Utils;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ProvinciaController(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        // GET: api/Provincia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinciaDetalleDTO>>> GetProvincia()
        {
            var listaProvincia = (await _unidadDeTrabajo.ProvinciaRepository.Listar()).Adapt<IEnumerable<ProvinciaDetalleDTO>>();

            return Ok(listaProvincia);
        }

        // GET: api/Provincia/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProvinciaDetalleDTO), statusCode: (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProvincia(int id)
        {
            var provincia = await _unidadDeTrabajo.ProvinciaRepository.BuscarPorId(id);

            if (provincia == null)
            {
                return NotFound("Provincia no encontrada");
            }

            return Ok(provincia.Adapt<ProvinciaDetalleDTO>());
        }

        // PUT: api/Provincia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProvinciaDetalleDTO), statusCode: (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PutProvincia(int id, ProvinciaCrearOActualizarDTO provincia)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Provincia");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var provinciaEncontrada = await _unidadDeTrabajo.ProvinciaRepository.BuscarPorId(id);

            if (provinciaEncontrada == null)
            {
                return NotFound("Provincia no encontrada");
            }

            if (provincia.DepartamentoId != provinciaEncontrada.DepartamentoId)
            {
                var existeDepartamento = await _unidadDeTrabajo.DepartamentoRepository.Existe(d => d.Id == provincia.DepartamentoId);

                if (!existeDepartamento)
                {
                    return NotFound("Departamento no encontrado");
                }

                provinciaEncontrada.DepartamentoId = provincia.DepartamentoId;
            }

            provincia.NombreProvincia = provincia.NombreProvincia.Trim();

            var existeMismoNombre = await _unidadDeTrabajo.ProvinciaRepository.Existe(p => p.NombreProvincia.ToLower() == provincia.NombreProvincia.ToLower() && p.Id != id);

            if (existeMismoNombre)
            {
                return Conflict("Ya existe una provincia con el mismo nombre");
            }

            provinciaEncontrada.NombreProvincia = provincia.NombreProvincia;

            _unidadDeTrabajo.ProvinciaRepository.Actualizar(provinciaEncontrada);

            await _unidadDeTrabajo.Guardar();

            var provinciaRespuesta = (await _unidadDeTrabajo.ProvinciaRepository.BuscarPorId(provinciaEncontrada.Id))!.Adapt<ProvinciaDetalleDTO>();

            return Ok(provinciaRespuesta);
        }

        // POST: api/Provincia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(ProvinciaDetalleDTO), statusCode: (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PostProvincia(ProvinciaCrearOActualizarDTO provincia)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Provincia");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var existeDepartamento = await _unidadDeTrabajo.DepartamentoRepository.Existe(d => d.Id == provincia.DepartamentoId);

            if (!existeDepartamento)
            {
                return NotFound("Departamento no encontrado");
            }

            provincia.NombreProvincia = provincia.NombreProvincia.Trim();

            var existeMismoNombre = await _unidadDeTrabajo.ProvinciaRepository.Existe(p => p.NombreProvincia.ToLower() == provincia.NombreProvincia.ToLower());

            if (existeMismoNombre)
            {
                return Conflict("Ya existe una provincia con el mismo nombre");
            }

            var provinciaEntidad = provincia.Adapt<Provincia>();

            await _unidadDeTrabajo.ProvinciaRepository.Guardar(provinciaEntidad);
            await _unidadDeTrabajo.Guardar();

            var provinciaRespuesta = (await _unidadDeTrabajo.ProvinciaRepository.BuscarPorId(provinciaEntidad.Id))!.Adapt<ProvinciaDetalleDTO>();

            return CreatedAtAction("GetProvincia", new { id = provinciaRespuesta.Id }, provinciaRespuesta);
        }

        // DELETE: api/Provincia/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProvincia(int id)
        {
            var provinciaEncontrada = await _unidadDeTrabajo.ProvinciaRepository.BuscarPorId(id);

            if (provinciaEncontrada == null)
            {
                return NotFound("Provincia no encontrada");
            }

            _unidadDeTrabajo.ProvinciaRepository.Eliminar(provinciaEncontrada);
            await _unidadDeTrabajo.Guardar();

            return NoContent();
        }
    }
}
