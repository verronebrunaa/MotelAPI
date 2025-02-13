namespace MotelAPI.Entities
{
    /// <summary>
    /// Represents a motel entity.
    /// </summary>
    public class Motel
    {
        /// <summary>
        /// Gets or sets the motel ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the motel.
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Gets or sets the suite type ID.
        /// </summary>
        public int TipoSuiteId { get; set; } // foreign key

        /// <summary>
        /// Gets or sets the suite type.
        /// </summary>
        public TipoSuite? TipoSuite { get; set; }
    }
}
