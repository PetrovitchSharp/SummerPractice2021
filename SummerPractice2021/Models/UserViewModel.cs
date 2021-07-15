using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Models
{
	public class UserViewModel: User
	{
		[Required(ErrorMessage = "Подтвердите пароль")]
		[MaxLength(20, ErrorMessage = "Пароль не может быть длиннее 20 символов")]
		[MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
		[RegularExpression("[a-zA-Z\\d_]{6,20}", ErrorMessage = "Пароль должен содержать от 6 до 20 символов: латиница, цифры и подчёркивания")]
		public string CheckPassword { get; set; }
	}
}
