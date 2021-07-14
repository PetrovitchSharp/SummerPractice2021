using SummerPractice2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.DAL
{
    public class DataContextInitializer
    {
        public static void Initialize(DataContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Nickname = "admin666",
                    Password = "admin666",
                    Email = "admin@mail.ru",
                    FirstName = "Petr",
                    LastName = "Korchagin"
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.News.Any())
            {
                context.News.AddRange(new List<News>()
                {
                    new News()
                    {
                        AuthorId = 1,
                        Text = "Мой первый пост",
                        CreateDate = DateTime.Now.AddMonths(-10)
                    },
                    new News()
                    {
                        AuthorId = 1,
                        Text = "С новым годом!!!",
                        CreateDate = new DateTime(2020, 12, 31, 23, 59, 59)
                    },
                });
                context.SaveChanges();
            }
        }
    }
}
