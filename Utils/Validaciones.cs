using Microsoft.AspNetCore.Mvc.ModelBinding;
using PruebaTecnica.DTO;

namespace PruebaTecnica.Utils
{
    public class Validaciones
    {

        public static RespuestaErrorValidacion? EvaluarModelState(ModelStateDictionary modelState, string message = "Error de validación")
        {
            RespuestaErrorValidacion? respuesta = null;

            if (!modelState.IsValid)
            {
                var errores = modelState.Where(e => e.Value.Errors.Any())
                              .ToDictionary(e => e.Key, e => e.Value.Errors.Select(error => error.ErrorMessage).ToArray());

                respuesta = new RespuestaErrorValidacion
                {
                    Message = message,
                    Errors = errores
                };
            }

            return respuesta;
        }
    }
}
