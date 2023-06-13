using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSample.DAL;
using MVCSample.Models;

namespace MVCSample.Controllers
{
    public class MahasiswaController : Controller
    {
        private IMahasiswa _mhs;

        public MahasiswaController(IMahasiswa mhs)
        {
            _mhs = mhs;
        }


        public bool isLogin()
        {
            if(HttpContext.Session.GetString("Username") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool cekAturan(string aturan)
        {
            if(HttpContext.Session.GetString("Aturan") != null && HttpContext.Session.GetString("Aturan") == aturan)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IActionResult Index()
        {
            if(!isLogin())
            {
                TempData["pesan"] = $"<span class='alert alert-danger'>Silahkan login terlebih dahulu untuk mengakses data mahasiswa</span>";
                return RedirectToAction("Login","Pengguna");
            }
            return View(_mhs.getAll());
        }        

        [HttpPost]
        public IActionResult Search(string  keyword, string pilih)
        {
            //return Content($"pilih = {pilih} keyword = {keyword}");
            if(pilih == "nim")
            {
                return View("Index",_mhs.getByNIM(keyword));
            }
            else if(pilih == "nama")
            {
                return View("Index", _mhs.getByNama(keyword));
            }
            else
            {
                return View("Index",_mhs.getAll());
            }
        }

        public IActionResult Create()
        {
            if(!isLogin()){
                TempData["pesan"] = $"<span class='alert alert-danger'>Silahkan login terlebih dahulu untuk mengakses data mahasiswa</span>";
                return RedirectToAction("Login","Pengguna");
            }
            else
            {
                if(!cekAturan("Admin"))
                {
                    TempData["pesan"] = $"<span class='alert alert-danger'>Silahkan login sebagai admin terlebih dahulu untuk mengakses data mahasiswa</span>";
                    return RedirectToAction("Login","Pengguna");
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(Mahasiswa mhs) 
        {
            try
            {
                _mhs.Insert(mhs);
                ViewData["pesan"] = $"<span class='alert alert-success'>Data berhasil disimpan !</span>";
                return View("Create");
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>{ex.Message}</span>";
                return View("Create");
            }
        }

        public IActionResult Update(string id)
        {
            if(!cekAturan("Admin"))
                {
                    TempData["pesan"] = $"<span class='alert alert-danger'>Silahkan login sebagai admin terlebih dahulu untuk mengakses data mahasiswa</span>";
                    return RedirectToAction("Login","Pengguna");
                }
            var data = _mhs.getById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult UpdatePost(Mahasiswa mhs)
        {
            try
            {
                _mhs.Update(mhs);
                ViewData["pesan"] = $"<span class='alert alert-success'>Data berhasil disimpan !</span>";
                return View("Update");
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>Error 1 :{ex.Message}</span>";
                return View("Update");
            }
        }

        public IActionResult Delete(string id)
        {
            if(!cekAturan("Admin"))
                {
                    TempData["pesan"] = $"<span class='alert alert-danger'>Silahkan login sebagai admin terlebih dahulu untuk mengakses data mahasiswa</span>";
                    return RedirectToAction("Login","Pengguna");
                }
            else{
            try
            {
                _mhs.Delete(id);
                ViewData["pesan"] = $"<span class='alert alert-success'>Data berhasil dihapus !</span>";
                return View("Index",_mhs.getAll());
            }
            catch(Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>Error 1 :{ex.Message}</span>";
                return View("Index",_mhs.getAll());
            }
            }
        }
    }
}