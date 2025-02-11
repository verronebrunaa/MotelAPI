namespace MotelAPI.DTOs
{
    public class TipoSuiteDTO
    {
        public string Nome { get; set; }
        public decimal PrecoPorHora { get; set; }
        public bool Disponivel { get; set; }
        public int Quantidade { get; set; }
    }
}
