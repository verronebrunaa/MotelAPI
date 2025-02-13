namespace MotelAPI.Entities
{
    /// <summary>
    /// Represents a type of suite in the motel.
    /// </summary>
    public class TipoSuite
    {
        /// <summary>
        /// Gets or sets the unique identifier for the suite type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the suite type.
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Gets or sets the price per hour for the suite type.
        /// </summary>
        public decimal PrecoPorHora { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the suite type is available.
        /// </summary>
        public bool Disponivel { get; set; }

        /// <summary>
        /// Gets or sets the quantity of this suite type available.
        /// </summary>
        public int Quantidade { get; set; }
    }
}
