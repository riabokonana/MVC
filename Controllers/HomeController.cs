﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileWorkPril.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FileWorkPril.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Files = getFiles();
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

        // функция загрузки файла на сервер 
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("Index");
        }
        // функция получения списка файлов
        public List<string> getFiles()
        {
            List<string> files = new List<string>();
            string path = "/Files/";

            DirectoryInfo source = new DirectoryInfo(_appEnvironment.WebRootPath + path);

            foreach (FileInfo fi in source.GetFiles())
            {
                files.Add(fi.Name);
            }

            return files;
        }

        // функция скачивания файла
        [HttpPost]
        public VirtualFileResult GetVirtualFile(string filename)
        {
            var filepath = Path.Combine("~/Files", filename);
            return File(filepath, "text/plain", filename);
        }

        // функция удаления файла
        [HttpPost]
        public IActionResult RemoveFile(string filename)
        {
            if (filename != null)
            {
                // путь к папке Files
                string path = "/Files/" + filename;

                if (System.IO.File.Exists(_appEnvironment.WebRootPath + path))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + path);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
