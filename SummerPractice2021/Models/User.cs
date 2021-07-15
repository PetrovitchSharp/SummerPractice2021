using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MaxLength(20,ErrorMessage = "Псевдоним не может быть длиннее 20 символов")]
        [MinLength(3, ErrorMessage = "Псевдоним не может быть короче 3 символов")]
        [RegularExpression("[a-zA-Zа-яА-Я\\d_]{3,20}",ErrorMessage = "Псевдоним может содержать от 3 до 20 символов: латиница, кириллица, цифры и подчёркивания")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Введите адрес почты")]
        [MaxLength(31, ErrorMessage = "Адрес почты не может быть длиннее 31 символа")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MaxLength(20, ErrorMessage = "Пароль не может быть длиннее 20 символов")]
        [MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
        [RegularExpression("[a-zA-Z\\d_]{6,20}", ErrorMessage = "Пароль должен содержать от 6 до 20 символов: латиница, цифры и подчёркивания")]
        public string Password { get; set; }

        [MaxLength(31, ErrorMessage = "Фамилия не может быть длиннее 31 символа")]
        public string LastName { get; set; }

        [MaxLength(31, ErrorMessage = "Имя не может быть длиннее 31 символа")]
        public string FirstName { get; set; }

        public Guid? Photo { get; set; }

        [MaxLength(255, ErrorMessage = "Контактная информация не может занимать более 255 символов")]
        public string Contacts { get; set; }

        [MaxLength(255, ErrorMessage = "Информация о себе не может занимать более 255 символов")]
        public string AboutMe { get; set; }

        [MaxLength(255, ErrorMessage = "Информация о достижениях не может занимать более 255 символов")]
        public string Achievements { get; set; }
    }

}
