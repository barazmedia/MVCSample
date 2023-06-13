using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCSample.Models;

namespace MVCSample.DAL
{
    public interface IPengguna
    {
        public void Register(Pengguna pengguna);

        public Pengguna cekLogin(string Username, String Password);
    }
}