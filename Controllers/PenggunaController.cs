using Microsoft.AspNetCore.Mvc;
using MVCSample.DAL;
using MVCSample.Models;
using Microsoft.AspNetCore.Http;

namespace MVCSample.Controllers
{
    public class PenggunaController : Controller
    {
        private IPengguna _pengguna;

        public PenggunaController(IPengguna pengguna)
        {
            _pengguna = pengguna;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterPost(Pengguna pengguna)
        {
            try
            {
                _pengguna.Register(pengguna);
                ViewData["pesan"] = $"<span class='alert alert-success'>Proses registrasi berhasil !</span>";
                return View("Register");
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>Error 1 :{ex.Message}</span>";
                return View("Register");
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var data = _pengguna.cekLogin(username,password);
                HttpContext.Session.SetString("Username", data.Username);
                HttpContext.Session.SetString("Aturan", data.Aturan);
                ViewData["pesan"] = $"<p>Selamat Datang {data.Username}, Anda berhasil login.";
                return View("Views/Home/Index.cshtml");
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>** {ex.Message}</span>";
                return View("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}