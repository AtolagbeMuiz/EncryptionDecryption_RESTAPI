using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAndDecryption
{
    public class User
    {
        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public int ProfileTypeId { get; set; }
    }
}
