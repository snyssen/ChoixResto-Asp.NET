using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChoixResto_Asp.NET.Models
{
	[Table("Restos")]
	public class Resto
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
		public string Nom { get; set; }
		[Display(Name = "Téléphone")]
		[RegularExpression(@"^0\d{9}$", ErrorMessage = "Le numéro de téléphone est incorrect")]
		public string Telephone { get; set; }
	}
}