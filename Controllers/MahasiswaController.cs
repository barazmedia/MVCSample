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
        public IActionResult Index()
        {
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