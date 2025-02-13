namespace MotelAPI.Entities
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the user's document.
        /// </summary>
        public string? Documento { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string? Senha { get; set; }

        /// <summary>
        /// Gets or sets the user's reservations.
        /// </summary>
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
