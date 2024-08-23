using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YourNamespace.Models
{
    /// <summary>
    /// Adres bilgilerini temsil eden sınıf.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Adresin benzersiz kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Sokağın adı.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Ek bilgi veya daire numarası.
        /// </summary>
        [MaxLength(100)]
        public string Suite { get; set; } = string.Empty;

        /// <summary>
        /// Şehir adı.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Posta kodu.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Adrese ait coğrafi bilgiler.
        /// </summary>
        public Geo? Geo { get; set; }

        /// <summary>
        /// Adrese ait kullanıcılar.
        /// </summary>
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

