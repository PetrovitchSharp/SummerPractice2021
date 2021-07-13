using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerPractice2021.Models
{
	public class UserViewModel: User
	{
        [Required]
		[MaxLength(20)]
		[MinLength(6)]
		[Display(Name="")]
		public string CheckPassword { get; set; }
	}
}
