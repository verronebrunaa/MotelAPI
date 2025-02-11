namespace MotelAPI.Entities
{
    public class Motel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TipoSuiteId { get; set; }  // foreign key
        public TipoSuite TipoSuite { get; set; }
    }
}