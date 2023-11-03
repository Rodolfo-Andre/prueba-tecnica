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
    public class TrabajadorController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public TrabajadorController(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        // GET: api/Trabajador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrabajadorDetalleDTO>>> GetTrabajador([FromQuery] string? nombre = null)
        {
            var listaTrabajador = (await _unidadDeTrabajo.TrabajadorRepository.Listar(nombre)).Adapt<IEnumerable<TrabajadorDetalleDTO>>();

            return Ok(listaTrabajador);
        }

        // GET: api/Trabajador/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TrabajadorDetalleDTO), statusCode: (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTrabajador(int id)
        {
            var trabajador = await _unidadDeTrabajo.TrabajadorRepository.BuscarPorId(id);

            if (trabajador == null)
            {
                return NotFound("Trabajador no encontrado");
            }

            return Ok(trabajador.Adapt<TrabajadorDetalleDTO>());
        }

        // PUT: api/Trabajador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TrabajadorDetalleDTO), statusCode: (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PutTrabajador(int id, TrabajadorCrearOActualizarDTO trabajador)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Trabajador");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var trabajadorEncontrado = await _unidadDeTrabajo.TrabajadorRepository.BuscarPorId(id);

            if (trabajadorEncontrado == null)
            {
                return NotFound("Trabajador no encontrado");
            }


            if (trabajador.DistritoId != trabajadorEncontrado.DistritoId)
            {
                var distrito = await _unidadDeTrabajo.DistritoRepository.BuscarPorId(trabajador.DistritoId);

                if (distrito == null)
                {
                    return NotFound("Distrito no encontrado");
                }

                trabajadorEncontrado.DistritoId = distrito.Id;
                trabajadorEncontrado.ProvinciaId = distrito.ProvinciaId;
                trabajadorEncontrado.DepartamentoId = distrito.Provincia.DepartamentoId;
            }

            var existeMismoNroDocumento = await _unidadDeTrabajo.TrabajadorRepository.Existe(p => p.NumeroDocumento == trabajador.NumeroDocumento && p.Id != id);

            if (existeMismoNroDocumento)
            {
                return Conflict("Ya existe existe un trabajador con el mismo número de documento");
            }

            trabajadorEncontrado.Nombres = trabajador.Nombres;
            trabajadorEncontrado.TipoDocumento = trabajador.TipoDocumento;
            trabajadorEncontrado.NumeroDocumento = trabajador.NumeroDocumento;
            trabajadorEncontrado.Sexo = trabajador.Sexo;

            _unidadDeTrabajo.TrabajadorRepository.Actualizar(trabajadorEncontrado);

            await _unidadDeTrabajo.Guardar();

            var trabajadorRespuesta = (await _unidadDeTrabajo.TrabajadorRepository.BuscarPorId(trabajadorEncontrado.Id))!.Adapt<TrabajadorDetalleDTO>();

            return Ok(trabajadorRespuesta);
        }

        // POST: api/Trabajador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(TrabajadorDetalleDTO), statusCode: (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(RespuestaErrorValidacion), statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PostTrabajador(TrabajadorCrearOActualizarDTO trabajador)
        {
            var respuesta = Validaciones.EvaluarModelState(ModelState, "Error de validación en Trabajador");

            if (respuesta != null)
            {
                return BadRequest(respuesta);
            }

            var distrito = await _unidadDeTrabajo.DistritoRepository.BuscarPorId(trabajador.DistritoId);

            if (distrito == null)
            {
                return NotFound("Distrito no encontrado");
            }

            var existeMismoNroDocumento = await _unidadDeTrabajo.TrabajadorRepository.Existe(p => p.NumeroDocumento == trabajador.NumeroDocumento);

            if (existeMismoNroDocumento)
            {
                return Conflict("Ya existe existe un trabajador con el mismo número de documento");
            }

            var trabajadorEntidad = trabajador.Adapt<Trabajador>();

            trabajadorEntidad.ProvinciaId = distrito.ProvinciaId;
            trabajadorEntidad.DepartamentoId = distrito.Provincia.DepartamentoId;

            await _unidadDeTrabajo.TrabajadorRepository.Guardar(trabajadorEntidad);
            await _unidadDeTrabajo.Guardar();

            var trabajadorRespuesta = (await _unidadDeTrabajo.TrabajadorRepository.BuscarPorId(trabajadorEntidad.Id))!.Adapt<TrabajadorDetalleDTO>();

            return CreatedAtAction("GetTrabajador", new { id = trabajadorRespuesta.Id }, trabajadorRespuesta);
        }

        // DELETE: api/Trabajador/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTrabajador(int id)
        {
            var trabajadorEncontrado = await _unidadDeTrabajo.TrabajadorRepository.BuscarPorId(id);

            if (trabajadorEncontrado == null)
            {
                return NotFound("Trabajador no encontrado");
            }

            _unidadDeTrabajo.TrabajadorRepository.Eliminar(trabajadorEncontrado);
            await _unidadDeTrabajo.Guardar();

            return NoContent();
        }
    }
}
