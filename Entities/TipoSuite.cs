namespace MotelAPI.Entities
{
    public class TipoSuite
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoPorHora { get; set; }
        public bool Disponivel { get; set; }
        public int Quantidade { get; set; }
    }
}