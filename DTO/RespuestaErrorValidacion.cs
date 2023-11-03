namespace PruebaTecnica.DTO
{
    public class RespuestaErrorValidacion
    {
        public string Message { get; set; } = string.Empty;
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
