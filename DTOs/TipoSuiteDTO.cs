using System.Globalization;
using System.Text.Json.Serialization;

namespace MotelAPI.DTOs
{
    public class TipoSuiteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoPorHora { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PrecoFormatado => PrecoPorHora.ToString("F2", CultureInfo.InvariantCulture);
        public bool Disponivel { get; set; }
        public int Quantidade { get; set; }
    }
}
