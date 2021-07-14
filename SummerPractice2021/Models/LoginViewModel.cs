using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Models
{
	public class LoginViewModel
	{
		[Required]
		[MaxLength(20)]
		public string Nickname { get; set; }

		[Required]
		[MaxLength(20)]
		[MinLength(6)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
