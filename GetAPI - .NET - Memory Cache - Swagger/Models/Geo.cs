using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YourNamespace.Models
{
    /// <summary>
    /// Coğrafi verileri temsil eden sınıf.
    /// </summary>
    public class Geo
    {
        /// <summary>
        /// Coğrafi verinin benzersiz kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Enlem (latitude) bilgisi.
        /// </summary>
        [Required]
        public string Lat { get; set; } = string.Empty;

        /// <summary>
        /// Boylam (longitude) bilgisi.
        /// </summary>
        [Required]
        public string Lng { get; set; } = string.Empty;

        /// <summary>
        /// İlgili adresin kimliği.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Coğrafi verinin ilişkili olduğu adres.
        /// </summary>
        [JsonIgnore]
        public Address Address { get; set; } = new Address();
    }
}
