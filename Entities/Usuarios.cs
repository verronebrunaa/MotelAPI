namespace MotelAPI.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public string Senha { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}
