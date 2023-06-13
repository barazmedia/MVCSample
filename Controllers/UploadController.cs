using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCSample.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                
                if(file == null || file.Length == 0)
                {
                    return Content("File not selected...");
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(),
                           Path.Combine("wwwroot","images"),file.FileName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Content($"File {file.FileName} berhasil ditambah");
            }
            catch(Exception ex)
            {
                return Content($"Error : {ex.Message}");
            }
        }
    }
}