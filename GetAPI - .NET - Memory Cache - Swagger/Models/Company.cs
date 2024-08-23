using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YourNamespace.Models
{
    /// <summary>
    /// Şirket bilgilerini temsil eden sınıf.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Şirketin benzersiz kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Şirketin adı.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Şirketin sloganı.
        /// </summary>
        [MaxLength(250)]
        public string CatchPhrase { get; set; } = string.Empty;

        /// <summary>
        /// Şirketin iş tanımı.
        /// </summary>
        [MaxLength(100)]
        public string Bs { get; set; } = string.Empty;

        /// <summary>
        /// Şirkete ait kullanıcılar.
        /// </summary>
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}



