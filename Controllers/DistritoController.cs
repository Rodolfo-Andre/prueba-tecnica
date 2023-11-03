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
    public class DistritoController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public DistritoController(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        // GET: api/Distrito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistritoDetalleDTO>>> GetDistrito()
        {
            var listaDistrito = (await _unidadDeTrabajo.DistritoRepository.Listar()).Adapt<IEnumerable<DistritoDetalleDTO>>();

            return Ok(listaDistrito);
        }

        // GET: api/Distrito/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DistritoDetalleDTO), statusCode: (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDistrito(int id)
        {
            var distrito = await _unidadDeTrabajo.DistritoRepository.BuscarPorId(id);

            if (distrito == null)
            {
                return NotFound("Distrito no encontrado");
            }

            return Ok(distrito.Adapt<DistritoDetalleDTO>());
        }

        // PUT: api/Distrito/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DistritoDetalleDTO), statusCode: (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PutDistrito(int id, DistritoCrearOActualizarDTO distrito)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Distrito");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var distritoEncontrado = await _unidadDeTrabajo.DistritoRepository.BuscarPorId(id);

            if (distritoEncontrado == null)
            {
                return NotFound("Distrito no encontrado");
            }

            if (distrito.ProvinciaId != distritoEncontrado.ProvinciaId)
            {
                var existePronvincia = await _unidadDeTrabajo.ProvinciaRepository.Existe(p => p.Id == distrito.ProvinciaId);

                if (!existePronvincia)
                {
                    return NotFound("Provincia no encontrada");
                }

                distritoEncontrado.ProvinciaId = distrito.ProvinciaId;
            }

            distrito.NombreDistrito = distrito.NombreDistrito.Trim();

            var existeMismoNombre = await _unidadDeTrabajo.DistritoRepository.Existe(d => d.NombreDistrito.ToLower() == distrito.NombreDistrito.ToLower() && d.Id != id);

            if (existeMismoNombre)
            {
                return Conflict("Ya existe un distrito con el mismo nombre");
            }

            distritoEncontrado.NombreDistrito = distrito.NombreDistrito;

            _unidadDeTrabajo.DistritoRepository.Actualizar(distritoEncontrado);

            await _unidadDeTrabajo.Guardar();

            var distritoRespuesta = (await _unidadDeTrabajo.DistritoRepository.BuscarPorId(distritoEncontrado.Id))!.Adapt<DistritoDetalleDTO>();

            return Ok(distritoRespuesta);
        }

        // POST: api/Distrito
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(DistritoDetalleDTO), statusCode: (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PostDistrito(DistritoCrearOActualizarDTO distrito)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Distrito");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var existePronvincia = await _unidadDeTrabajo.ProvinciaRepository.Existe(p => p.Id == distrito.ProvinciaId);

            if (!existePronvincia)
            {
                return NotFound("Provincia no encontrada");
            }

            distrito.NombreDistrito = distrito.NombreDistrito.Trim();

            var existeMismoNombre = await _unidadDeTrabajo.DistritoRepository.Existe(d => d.NombreDistrito.ToLower() == distrito.NombreDistrito.ToLower());

            if (existeMismoNombre)
            {
                return Conflict("Ya existe un distrito con el mismo nombre");
            }

            var distritoEntidad = distrito.Adapt<Distrito>();

            await _unidadDeTrabajo.DistritoRepository.Guardar(distritoEntidad);
            await _unidadDeTrabajo.Guardar();

            var distritoRespuesta = (await _unidadDeTrabajo.DistritoRepository.BuscarPorId(distritoEntidad.Id))!.Adapt<DistritoDetalleDTO>();

            return CreatedAtAction("GetDistrito", new { id = distritoRespuesta.Id }, distritoRespuesta);
        }

        // DELETE: api/Distrito/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDistrito(int id)
        {
            var distritoEncontrado = await _unidadDeTrabajo.DistritoRepository.BuscarPorId(id);

            if (distritoEncontrado == null)
            {
                return NotFound("Distrito no encontrado");
            }

            _unidadDeTrabajo.DistritoRepository.Eliminar(distritoEncontrado);
            await _unidadDeTrabajo.Guardar();

            return NoContent();
        }
    }
}
