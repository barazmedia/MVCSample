using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSample.Models
{
    public class Pengguna
    {
        [Required(ErrorMessage ="Username wajib diisi")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password wajib diisi")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        public string Repassword { get; set; }

        [Required]
        public string Aturan { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Telpon { get; set; }
    }
}