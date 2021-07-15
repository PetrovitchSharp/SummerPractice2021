using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SummerPractice2021.DAL;
using SummerPractice2021.Helpers;
using SummerPractice2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

		public IActionResult Register()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserViewModel user, IFormFile imageData)
        {
            var context = new DataContext();

			if (ModelState.IsValid)
			{

				var hasUserWithNickname = context.Users.Any(x => x.Nickname == user.Nickname);
				var hasUserWithEmail = context.Users.Any(x => x.Email == user.Email);

				var flag = false;

				if (user.Password != user.CheckPassword)
				{
					ModelState.AddModelError("CheckPassword", "Пароли не совпадают");
					flag = true;
				}

				if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Nickname) || String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.CheckPassword))
				{
					ModelState.AddModelError(string.Empty, "Заполнены не все обязательные поля!");
					flag = true;
				}

				if (hasUserWithNickname)
				{
					ModelState.AddModelError("Nickname", "Такой псевдоним уже занят");
					flag = true;
				}

				if (hasUserWithEmail)
				{
					ModelState.AddModelError("Email", "Такая почта уже зарегистрирована");
					flag = true;
				}

				if (flag)
				{
					return View("Register", user);
				}

				Guid? img = Guid.Empty;

				if (imageData != null)
				{
					var extention = Path.GetExtension(imageData.FileName);
					if (extention != ".jpg" && extention != ".jpeg" && extention != ".png")
					{
						ViewBag.FileError = true;
						return View("Register", user);
					}
					img = await ImageHelper.UploadImage(imageData);
				}

				var validatedUser = new User()
				{
					Nickname = user.Nickname,
					Password = user.Password,
					Email = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Photo = img,
					Achievements = user.Achievements,
					AboutMe = user.AboutMe,
					Contacts = user.Contacts
				};

				context.Users.Add(validatedUser);
				context.SaveChanges();

				await AuthenticationHelper.Authenticate(user.Nickname, true, HttpContext); // аутентификация

				if (user.Photo.HasValue && Guid.Empty != user.Photo.Value)
				{
					HttpContext.Session.SetString("Photo", user.Photo.Value.ToString());
				}
				HttpContext.Session.SetString("Nickname", user.Nickname);
				var id = context.Users.First(x => x.Nickname == user.Nickname).Id.ToString();
				HttpContext.Session.SetString("Id", id);

				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError("Nickname", "Псевдоним может содержать от 3 до 20 символов: латиница, кириллица, цифры и подчёркивания");
				ModelState.AddModelError("Password", "Пароль должен содержать от 6 до 20 символов: латиница, цифры и подчёркивания");
				return View("Register", user);
			}
			
		}
    }
}
