using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSample.Models
{
    public class Mahasiswa
    {
        [Required(ErrorMessage ="** Field NIM harus diisi !")]
        public string Nim { get; set;}

        [Required(ErrorMessage ="** Field nama harus diisi !")]
        public string Nama { get; set; }

        [Required(ErrorMessage ="** Field alamat harus diisi !")]
        public string Alamat { get; set; }

        [Required(ErrorMessage ="** Field email harus diisi !")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="** Field telpon harus diisi !")]
        public string Telpon { get; set; }
    }
}