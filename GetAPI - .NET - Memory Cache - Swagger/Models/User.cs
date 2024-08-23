using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Models
{
    /// <summary>
    /// Kullanıcı bilgilerini temsil eden model sınıfı.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Kullanıcının benzersiz kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Kullanıcının adı.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının kullanıcı adı.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının e-posta adresi.
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Adres kimliği (Foreign Key).
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Kullanıcının adresi.
        /// </summary>
        [ForeignKey("AddressId")]
        public Address? Address { get; set; } // Null olmasına izin verildi.

        /// <summary>
        /// Şirket kimliği (Foreign Key).
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Kullanıcının çalıştığı şirket.
        /// </summary>
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; } // Null olmasına izin verildi.
    }
}





