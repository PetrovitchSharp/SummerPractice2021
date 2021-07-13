using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SummerPractice2021.DAL;
using SummerPractice2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Controllers
{
	public class RegisterController : Controller
	{
		private readonly ILogger<RegisterController> _logger;

		public RegisterController(ILogger<RegisterController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserViewModel user, IFormFile imageData)
        {
            var context = new DataContext();

            
            context.SaveChanges();

            return View("Index");
        }
    }
}
