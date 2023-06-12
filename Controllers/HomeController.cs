using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCSample.Models;

namespace MVCSample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegiterAnggota(Anggota anggota)
    {
        return Content($"Username = {anggota.Username} Nama = {anggota.Nama} Alamat = {anggota.Alamat} Email = {anggota.Email}");
    }

    public IActionResult TampilAnggota()
    {
        List<Anggota> anggota = new List<Anggota>();
        anggota.Add(
            new Anggota{
                Username = "kangbaraz",
                Nama = "Miftakhudin",
                Alamat = "Karave",
                Email = "kangbaraz@gmail.com"});

        anggota.Add(
            new Anggota{
                Username = "pandu",
                Nama = "Pandu Satria Mahardika",
                Alamat = "Bulili",
                Email = "pandu@gmail.com"});

        anggota.Add(
            new Anggota{
                Username = "amar",
                Nama = "Amar Satria Mahardika",
                Alamat = "Bulili",
                Email = "amar@gmail.com"});

        anggota.Add(
            new Anggota{
                Username = "putra",
                Nama = "Putra Satria",
                Alamat = "Motu",
                Email = "putra@gmail.com"});

        return View(anggota);
    }
}
