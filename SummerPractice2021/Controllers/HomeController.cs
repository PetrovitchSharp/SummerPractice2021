using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SummerPractice2021.DAL;
using SummerPractice2021.Helpers;
using SummerPractice2021.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPost(News news, IFormFile imageData)
        {
            var context = new DataContext();

            if (string.IsNullOrEmpty(news.Text)
                && imageData == null)
            {
                ViewBag.Posts = context.News
                    .Include(n => n.Author)
                    .OrderByDescending(n => n.CreateDate)
                    .ToList();
                return View("Index", news);
            }

            if (imageData != null)
			{
                var extention = Path.GetExtension(imageData.FileName);
                if (extention != ".jpg" && extention != ".jpeg" && extention != ".png")
				{
                    ViewBag.FileError = true;
                    ViewBag.Posts = context.News
                    .Include(n => n.Author)
                    .OrderByDescending(n => n.CreateDate)
                    .ToList();
                    return View("Index", news);
                }
			}

            news.AuthorId = 1;
            news.CreateDate = DateTime.Now;

            news.Photo = await ImageHelper.UploadImage(imageData);

            context.News.Add(news);
            context.SaveChanges();
            ViewBag.Posts = context.News
                    .Include(n => n.Author)
                    .OrderByDescending(n => n.CreateDate)
                    .ToList();
            return View("Index", news);
        }


        public IActionResult Index()
        {
            var context = new DataContext();
            var news = context.News
                .Include(n => n.Author)
                .OrderByDescending(n => n.CreateDate)
                .ToList();

            if (!news.Any())
            {
                ViewBag.Posts = new List<News>();
            }
            else
            {
                ViewBag.Posts = news;
            }
            
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
